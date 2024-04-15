using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;
using Utills.domain;
using Utills.observer;
using Utills.services;

namespace Utills.networking;

public class ServerProxy : IServices
{
	private string host;
	private int port;

	private IObserver client;

	private NetworkStream stream;

	private IFormatter formatter;
	private TcpClient connection;

	private Queue<Response> responses;
	private volatile bool finished;
	private EventWaitHandle _waitHandle;

	public ServerProxy(string host, int port)
	{
		this.host = host;
		this.port = port;
		responses = new Queue<Response>();
	}

	public virtual int Login(String username, String parola, IObserver client)
	{
		initializeConnection();
		Angajat angajat = new Angajat(parola, username);
		Request req = new Request.Builder().Type(RequestType.LOGIN).Data(angajat).Build();
		sendRequest(req);
		Response response = readResponse();
		if (response.Type == ResponseType.OK)
		{
			this.client = client;
			return (int)response.Data;
		}

		if (response.Type == ResponseType.ERROR)
		{
			String err = response.Data.ToString();
			closeConnection();
//            throw new Exception(err);
		}

		return -1;
	}

	public void Logout(int idAngajat)
	{
		Request req=new Request.Builder().Type(RequestType.LOGOUT).Data(idAngajat).Build();
		sendRequest(req);
		Response response=readResponse();
		closeConnection();
		if (response.Type == ResponseType.ERROR){
			String err=response.Data.ToString();
//            throw new ChatException(err);
		}
	}

	public int NrLocuriDisponibileMeci(Meci meci)
	{
		Request req=new Request.Builder().Type(RequestType.LOCURI_DISPONIBILE).Data(meci).Build();
		sendRequest(req);
		Response response=readResponse();
		if (response.Type == ResponseType.ERROR){
			String err = response.Data.ToString();
//            throw new ChatException(err);
			return 0;
		}
		int nrLocuri = (int) response.Data;
		return nrLocuri;
	}

	public IEnumerable<MeciL> GetMeciuri()
	{
		Request req=new Request.Builder().Type(RequestType.GET_MECIURI).Build();
		sendRequest(req);
		Response response=readResponse();
		if (response.Type == ResponseType.ERROR){
			String err = response.Data.ToString();
//            throw new ChatException(err);
			return null;
		}
		IEnumerable<MeciL> meciuri = (IEnumerable<MeciL>) response.Data;

		return meciuri;
	}

	public IEnumerable<MeciL> GetMeciuriLibere()
	{
		Request req=new Request.Builder().Type(RequestType.GET_MECIURI_LIBERE).Build();
		sendRequest(req);
		Response response=readResponse();
		if (response.Type == ResponseType.ERROR){
			String err = response.Data.ToString();
//            throw new ChatException(err);
			return null;
		}
		IEnumerable<MeciL> meciuri = (IEnumerable<MeciL>) response.Data;

		return meciuri;
	}

	public Bilet CumparaBilet(Meci meci, string numeClient, int nrLocuri)
	{
		Bilet bilet = new Bilet(meci, numeClient, nrLocuri);
		Request req=new Request.Builder().Type(RequestType.CUMPARA_BILET).Data(bilet).Build();
		sendRequest(req);
		Response response=readResponse();
		if (response.Type == ResponseType.ERROR){
			String err=response.Data.ToString();
//            throw new ChatException(err);
			return null;
		}
		Bilet bilet_salvat = (Bilet) response.Data;
		return bilet_salvat;
	}

	private void closeConnection()
	{
		finished = true;
		try
		{
			stream.Close();
			//output.close();
			connection.Close();
			_waitHandle.Close();
			client = null;
		}
		catch (Exception e)
		{
			Console.WriteLine(e.StackTrace);
		}

	}

	private void sendRequest(Request request)
	{
		try
		{
			formatter.Serialize(stream, request);
			stream.Flush();
		}
		catch (Exception e)
		{
			throw new Exception("Error sending object " + e);
		}

	}

	private Response readResponse()
	{
		Response response = null;
		try
		{
			_waitHandle.WaitOne();
			lock (responses)
			{
				//Monitor.Wait(responses); 
				response = responses.Dequeue();

			}


		}
		catch (Exception e)
		{
			Console.WriteLine(e.StackTrace);
		}

		return response;
	}

	private void initializeConnection()
	{
		try
		{
			connection = new TcpClient(host, port);
			stream = connection.GetStream();
			formatter = new BinaryFormatter();
			finished = false;
			_waitHandle = new AutoResetEvent(false);
			startReader();
		}
		catch (Exception e)
		{
			Console.WriteLine(e.StackTrace);
		}
	}

	private void startReader()
	{
		Thread tw = new Thread(run);
		tw.Start();
	}

	private bool isUpdate(Response response)
	{
		return response.Type == ResponseType.NEW_MECIURI_LIST;
	}


	private void handleUpdate(Response response)
	{
		if (response.Type == ResponseType.NEW_MECIURI_LIST)
		{
			IEnumerable<MeciL> meciuri = (IEnumerable<MeciL>)response.Data;
			Console.WriteLine("Lista noua de meciuri " + meciuri);
			try
			{
				client.schimbareMeciuri(meciuri);
			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
			}
		}
	}

	public virtual void run()
	{
		while (!finished)
		{
			try
			{
				object response = formatter.Deserialize(stream);
				Console.WriteLine("response received " + response);
				if (isUpdate((Response)response))
				{
					handleUpdate((Response)response);
				}
				else
				{

					lock (responses)
					{


						responses.Enqueue((Response)response);

					}

					_waitHandle.Set();
				}
			}
			catch (Exception e)
			{
				Console.WriteLine("Reading error " + e);
			}

		}
	}
    
}