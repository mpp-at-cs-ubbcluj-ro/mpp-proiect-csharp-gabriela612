using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;
using Utills.domain;
using Utills.observer;
using Utills.services;

namespace Utills.networking;

public class ClientWorker : IObserver
{
    private IServices server;
    private TcpClient connection;

    private NetworkStream stream;
    private IFormatter formatter;
    private volatile bool connected;

    public ClientWorker(IServices server, TcpClient connection)
    {
        this.server = server;
        this.connection = connection;
        try
        {

            stream = connection.GetStream();
            formatter = new BinaryFormatter();
            connected = true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.StackTrace);
        }
    }

    public virtual void run()
    {
        while (connected)
        {
            try
            {
                Console.WriteLine("Waiting request ...");
                object request = formatter.Deserialize(stream);
                Console.WriteLine("Request received ...{0}", request);
                object response = handleRequest((Request)request);
                if (response != null)
                {
                    sendResponse((Response)response);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }

            try
            {
                Thread.Sleep(1000);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }
        }

        try
        {
            stream.Close();
            connection.Close();
        }
        catch (Exception e)
        {
            Console.WriteLine("Error " + e);
        }
    }

    private void sendResponse(Response response)
    {
        Console.WriteLine("sending response " + response);
        formatter.Serialize(stream, response);
        stream.Flush();

    }

    private static Response okResponse = new Response.Builder().Type(ResponseType.OK).Build();

    private Response handleRequest(Request request)
    {
        Response response = null;
        if (request.Type == RequestType.LOGIN)
        {

            Angajat angajat = (Angajat)request.Data;
            try
            {
                int idAngajat = server.Login(angajat.Username, angajat.Parola, this);

                return new Response.Builder().Type(ResponseType.OK).Data(idAngajat).Build();
            }
            catch (Exception e)
            {
                connected = false;
                return new Response.Builder().Type(ResponseType.ERROR).Data(e.Message).Build();
            }
        }

        if (request.Type == RequestType.LOGOUT)
        {
            Console.WriteLine("Logout request");
            // LogoutRequest logReq=(LogoutRequest)request;
            int idAngajat = (int)request.Data;
            try
            {
                server.Logout(idAngajat);
                connected = false;
                return okResponse;

            }
            catch (Exception e)
            {
                return new Response.Builder().Type(ResponseType.ERROR).Data(e.Message).Build();
            }
        }

        if (request.Type == RequestType.GET_MECIURI)
        {
            Console.WriteLine("GetMeciuriRequest ...");
            try
            {
                return new Response.Builder().Type(ResponseType.OK).Data(server.GetMeciuri()).Build();
            }
            catch (Exception e)
            {
                return new Response.Builder().Type(ResponseType.ERROR).Data(e.Message).Build();
            }
        }

        if (request.Type == RequestType.GET_MECIURI_LIBERE)
        {
            Console.WriteLine("GetMeciuriLibereRequest ...");
            try
            {
                return new Response.Builder().Type(ResponseType.OK).Data(server.GetMeciuriLibere()).Build();
            }
            catch (Exception e)
            {
                return new Response.Builder().Type(ResponseType.ERROR).Data(e.Message).Build();
            }
        }

        if (request.Type == RequestType.CUMPARA_BILET)
        {
            // System.out.println("Vinde bilet request ..."+request.type());
            Console.WriteLine("Vinde bilet request ..." + request.Type);
            Bilet bilet = (Bilet)request.Data;
            try
            {
                Bilet bilet_nou = server.CumparaBilet(bilet.Meci, bilet.NumeClient, bilet.NrLocuri);
                return new Response.Builder().Type(ResponseType.OK).Data(bilet_nou).Build();
            }
            catch (Exception e)
            {
                connected = false;
                return new Response.Builder().Type(ResponseType.ERROR).Data(e.Message).Build();
            }
        }

        if (request.Type == RequestType.LOCURI_DISPONIBILE)
        {
            Console.WriteLine("Locuri disponibile ..." + request.Type);
            Meci meci = (Meci)request.Data;
            try
            {
                int nrLocuri = server.NrLocuriDisponibileMeci(meci);
                return new Response.Builder().Type(ResponseType.LOCURI_DISPONIBILE).Data(nrLocuri).Build();
            }
            catch (Exception e)
            {
                connected = false;
                return new Response.Builder().Type(ResponseType.ERROR).Data(e.Message).Build();
            }
        }

        return response;
    }

    public void schimbareMeciuri(IEnumerable<MeciL> meciuri)
    {
        Response resp=new Response.Builder().Type(ResponseType.NEW_MECIURI_LIST).Data(meciuri).Build();
        Console.WriteLine("Schimbare meciuri " + meciuri);
        try {
            sendResponse(resp);
        } catch (IOException e) {
//            throw new Exception("Sending error: "+e);
        }
    }
}
