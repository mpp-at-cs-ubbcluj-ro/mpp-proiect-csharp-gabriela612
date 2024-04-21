using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Utills.domain;
using Utills.networking;
using Utills.services;

namespace WindowsFormsApp1
{
    
    static class Program
    {
        
        private static int DEFAULT_SERVER_PORT = 55556;
        private static String DEFAULT_SERVER_IP = "127.0.0.1";
        
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            int port = DEFAULT_SERVER_PORT;
            String ip = DEFAULT_SERVER_IP;
            
            String portS= ConfigurationManager.AppSettings["port_server"];
            if (portS == null)
            {
                Console.WriteLine("Port property not set. Using default value "+DEFAULT_SERVER_PORT);
            }
            else
            {
                bool result = Int32.TryParse(portS, out port);
                if (!result)
                {
                    Console.WriteLine("Port property not a number. Using default value "+DEFAULT_SERVER_PORT);
                    port = DEFAULT_SERVER_PORT;
                    Console.WriteLine("Portul "+port);
                }
            }
            String ipS=ConfigurationManager.AppSettings["ip_server"];
           
            if (ipS == null)
            {
                Console.WriteLine("Port property not set. Using default value "+DEFAULT_SERVER_IP);
            }
            
            
            
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            
            IServices server = new ServerProxy(ip, port);
            ClientCtrl ctrl=new ClientCtrl(server);
            Login win = new Login(ctrl);
            Application.Run(win);
            
        }
    }
}