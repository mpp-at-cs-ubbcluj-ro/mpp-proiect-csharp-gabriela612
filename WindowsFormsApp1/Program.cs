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
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            
            IServices server = new ServerProxy("127.0.0.1", 55556);
            ClientCtrl ctrl=new ClientCtrl(server);
            Login win = new Login(ctrl);
            Application.Run(win);
            
        }
    }
}