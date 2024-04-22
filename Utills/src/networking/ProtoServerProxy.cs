using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;
using Google.Protobuf;
using Utills.observer;
using Utills.services;
using Utills.utils;

namespace Utills.networking;

public class ProtoServerProxy : IServices
	{
		private string host;
		private int port;

		private IObserver client;

		private NetworkStream stream;
		private TcpClient connection;

		private Queue<proto.Response> responses;
		private volatile bool finished;
		private EventWaitHandle _waitHandle;

		public ProtoServerProxy(string host, int port)
		{
			this.host = host;
			this.port = port;
			responses = new Queue<proto.Response>();
		}

		public virtual int Login(String username, String parola, IObserver client)
		{
			initializeConnection();
			domain.Angajat angajat = new domain.Angajat(parola, username);
			proto.Request req = ProtoUtils.CreateLoginRequest(angajat);
			sendRequest(req);
			proto.Response response = readResponse();
			if (response.Type == proto.Response.Types.Type.Ok)
			{
				this.client = client;
				return response.IdAngajat;
			}

			if (response.Type == proto.Response.Types.Type.Error)
			{
				String err = response.Message.ToString();
				closeConnection();
//            throw new Exception(err);
			}

			return -1;
		}

		public void Logout(int idAngajat)
		{
			proto.Request req = ProtoUtils.CreateLogoutRequest(idAngajat);;
			sendRequest(req);
			proto.Response response = readResponse();
			closeConnection();
			if (response.Type == proto.Response.Types.Type.Error )
			{
				String err = response.Message.ToString();
//            throw new ChatException(err);
			}
		}

		public int NrLocuriDisponibileMeci(domain.Meci meci)
		{
			proto.Request req = ProtoUtils.CreateLocuriDisponibileRequest(meci);
			sendRequest(req);
			proto.Response response = readResponse();
			if (response.Type == proto.Response.Types.Type.Error)
			{
				String err = response.Message.ToString();
//            throw new ChatException(err);
				return 0;
			}

			int nrLocuri = (int)response.NrLocuriDisponibile;
			return nrLocuri;
		}

		public IEnumerable<domain.MeciL> GetMeciuri()
		{
			proto.Request req = ProtoUtils.CreateGetMeciuriRequest();
			sendRequest(req);
			proto.Response response = readResponse();
			if (response.Type == proto.Response.Types.Type.Error)
			{
				String err = response.Message.ToString();
//            throw new ChatException(err);
				return null;
			}

			IEnumerable<domain.MeciL> meciuri = ProtoUtils.GetMeciuri(response);

			return meciuri;
		}

		public IEnumerable<domain.MeciL> GetMeciuriLibere()
		{
			proto.Request req = ProtoUtils.CreateGetMeciuriLibereRequest();
			sendRequest(req);
			proto.Response response = readResponse();
			if (response.Type == proto.Response.Types.Type.Error)
			{
				String err = response.Message.ToString();
//            throw new ChatException(err);
				return null;
			}

			IEnumerable<domain.MeciL> meciuri = ProtoUtils.GetMeciuri(response);

			return meciuri;
		}

		public domain.Bilet CumparaBilet(domain.Meci meci, string numeClient, int nrLocuri)
		{
			domain.Bilet bilet = new domain.Bilet(meci, numeClient, nrLocuri);
			proto.Request req = ProtoUtils.CreateCumparaBiletRequest(bilet);
			sendRequest(req);
			proto.Response response = readResponse();
			if (response.Type == proto.Response.Types.Type.Error)
			{
				String err = response.Message.ToString();
//            throw new ChatException(err);
				return null;
			}

			domain.Bilet bilet_salvat = ProtoUtils.GetBilet(response.Bilet);
			return bilet_salvat;
		}

		private void closeConnection()
		{
			finished=true;
			try
			{
				stream.Close();
				//output.close();
				connection.Close();
				_waitHandle.Close();
				client=null;
			}
			catch (Exception e)
			{
				Console.WriteLine(e.StackTrace);
			}

		}

		private void sendRequest(proto.Request request)
		{
			try
			{
				request.WriteDelimitedTo(stream);
				stream.Flush();
			}
			catch (Exception e)
			{
				throw new Exception("Error sending object " + e);
			}

		}

		private proto.Response readResponse()
		{
			proto.Response response = null;
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

		private bool isUpdate(proto.Response response)
		{
			return response.Type == proto.Response.Types.Type.NewMeciuriList;
		}


		private void handleUpdate(proto.Response response)
		{
			if (response.Type == proto.Response.Types.Type.NewMeciuriList)
			{
				IEnumerable<domain.MeciL> meciuri = ProtoUtils.GetMeciuri(response);
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
					// object response = formatter.Deserialize(stream);
					proto.Response response = proto.Response.Parser.ParseDelimitedFrom(stream);
					Console.WriteLine("response received " + response);
					if (isUpdate((proto.Response)response))
					{
						handleUpdate((proto.Response)response);
					}
					else
					{

						lock (responses)
						{


							responses.Enqueue((proto.Response)response);

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