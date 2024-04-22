using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;
using Google.Protobuf;
using Utills.observer;
using Utills.services;
using Utills.utils;

namespace Utills.networking;

public class ProtoClientWorker : IObserver
{
    private IServices server;
    private TcpClient connection;

    private NetworkStream stream;
    private volatile bool connected;

    public ProtoClientWorker(IServices server, TcpClient connection)
    {
        Console.WriteLine("Protot Cl Wo : da");
        this.server = server;
        this.connection = connection;
        try
        {
            Console.WriteLine("Proto Cl Wo : nu");
            stream = connection.GetStream();
            Console.WriteLine("Proto Cl Wo : merge");
            //      formatter = new BinaryFormatter();
            connected=true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            Console.WriteLine(e.StackTrace);
        }
    }

    public virtual void run()
    {
        while(connected)
        {
            try
            {
                proto.Request request = proto.Request.Parser.ParseDelimitedFrom(stream);
                proto.Response response = handleRequest(request);
                if (response!=null)
                {
                    sendResponse(response);
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
            Console.WriteLine("Error "+e);
        }

    }

    private void sendResponse(proto.Response response)
    {
        Console.WriteLine("sending response "+response);
        lock (stream)
        {
            response.WriteDelimitedTo(stream);
            stream.Flush();
        }


    }

    private static proto.Response okResponse = ProtoUtils.CreateOkResponse();
 
    private proto.Response handleRequest(proto.Request request)
    {
        proto.Response response = null;
        if (request.Type == proto.Request.Types.Type.Login)
        {
            domain.Angajat angajat = ProtoUtils.GetAngajat(request.Angajat);
            try
            {
                int idAngajat;
                lock (server)
                {
                    idAngajat = server.Login(angajat.Username, angajat.Parola, this);
                }

                return ProtoUtils.CreateLoginResponse(idAngajat);
            }
            catch (Exception e)
            {
                connected = false;
                return ProtoUtils.CreateErrorResponse(e.Message);
            }
        }

        if (request.Type == proto.Request.Types.Type.Logout)
        {
            Console.WriteLine("Logout request");
            // LogoutRequest logReq=(LogoutRequest)request;
            int idAngajat = request.IdAngajat;
            try
            {
                lock (server)
                {
                    server.Logout(idAngajat);
                }
                connected = false;
                return okResponse;

            }
            catch (Exception e)
            {
                return ProtoUtils.CreateErrorResponse(e.Message);
            }
        }

        if (request.Type == proto.Request.Types.Type.GetMeciuri)
        {
            Console.WriteLine("GetMeciuriRequest ...");
            try
            {
                lock (server)
                {
                    return ProtoUtils.CreateGetMeciuriResponse((HashSet<domain.MeciL>) server.GetMeciuri());
                } 
            }
            catch (Exception e)
            {
                return ProtoUtils.CreateErrorResponse(e.Message);
            }
        }

        if (request.Type == proto.Request.Types.Type.GetMeciuriLibere)
        {
            Console.WriteLine("GetMeciuriLibereRequest ...");
            try
            {
                lock (server)
                {
                    return ProtoUtils.CreateGetMeciuriResponse((HashSet<domain.MeciL>) server.GetMeciuriLibere());
                }
            }
            catch (Exception e)
            {
                return ProtoUtils.CreateErrorResponse(e.Message);
            }
        }

        if (request.Type == proto.Request.Types.Type.CumparaBilet)
        {
            // System.out.println("Vinde bilet request ..."+request.type());
            Console.WriteLine("Vinde bilet request ..." + request.Type);
            domain.Bilet bilet = ProtoUtils.GetBilet(request.Bilet);
            try
            {
                domain.Bilet bilet_nou;
                lock (server)
                {
                    bilet_nou = server.CumparaBilet(bilet.Meci, bilet.NumeClient, bilet.NrLocuri);
                }
                return ProtoUtils.CreateCumparaBiletResponse(bilet_nou);
            }
            catch (Exception e)
            {
                // connected = false;
                return ProtoUtils.CreateErrorResponse(e.Message);
            }
        }

        if (request.Type == proto.Request.Types.Type.LocuriDisponibile)
        {
            Console.WriteLine("Locuri disponibile ..." + request.Type);
            domain.Meci meci = ProtoUtils.GetMeci(request.Meci);
            try
            {
                int nrLocuri;
                lock (server)
                {
                    nrLocuri = server.NrLocuriDisponibileMeci(meci);
                }
                return ProtoUtils.CreateNrLocuriDisponibileMeciResponse(nrLocuri);
            }
            catch (Exception e)
            {
                // connected = false;
                return ProtoUtils.CreateErrorResponse(e.Message);
            }
        }

        return response;
    }

    public void schimbareMeciuri(IEnumerable<domain.MeciL> meciuri)
    {
        proto.Response resp = ProtoUtils.CreateSchimbareMeciuriResponse((HashSet<domain.MeciL>) meciuri);
        Console.WriteLine("Schimbare meciuri " + meciuri);
        try {
            sendResponse(resp);
        } catch (IOException e) {
//            throw new Exception("Sending error: "+e);
        }
    }
}
