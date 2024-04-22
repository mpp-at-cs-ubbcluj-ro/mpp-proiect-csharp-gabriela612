using System;
using System.Collections.Generic;
using System.Configuration;
using Server.repository;
using Server.service;
using Utills.networking;
using Utills.services;

namespace Server
{
    internal class Program
    {
        private static int DEFAULT_PORT=55556;
        private static String DEFAULT_IP="127.0.0.1";
        public static void Main(string[] args)
        {
            Console.WriteLine("Reading properties from app.config ...");
            int port = DEFAULT_PORT;
            String ip = DEFAULT_IP;
            String portS= ConfigurationManager.AppSettings["port"];
            if (portS == null)
            {
                Console.WriteLine("Port property not set. Using default value "+DEFAULT_PORT);
            }
            else
            {
                bool result = Int32.TryParse(portS, out port);
                if (!result)
                {
                    Console.WriteLine("Port property not a number. Using default value "+DEFAULT_PORT);
                    port = DEFAULT_PORT;
                    Console.WriteLine("Portul "+port);
                }
            }
            String ipS=ConfigurationManager.AppSettings["ip"];
           
            if (ipS == null)
            {
                Console.WriteLine("Port property not set. Using default value "+DEFAULT_IP);
            }
            
            
            
            ConnectionStringSettings settings = ConfigurationManager.ConnectionStrings["baschetDB"];

            string returnValue = "";
            if (settings != null)
                returnValue = settings.ConnectionString;
            
            Dictionary<String, string> props = new Dictionary<String, String>();
            props.Add("ConnectionString", returnValue);
            
            IAngajatRepository angajatRepository = new AngajatDBRepository(props);
            IMeciRepository meciRepository = new MeciDBRepository(props);
            IBiletRepository biletRepository = new BiletDBRepository(props);

            IServices service = new Service(angajatRepository, meciRepository, biletRepository);

            AbstractServer server = new ProtoConcurrentServer(ip, port, service);
            try {
                server.Start();
            } catch (Exception e) {
                Console.WriteLine("Error starting the server" + e.Message);
            }finally {
                try {
                    
                }catch(Exception e){
                    Console.WriteLine("Error stopping the server" + e.Message);
                }
            }

        }
    }
}