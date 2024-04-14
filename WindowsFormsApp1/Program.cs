using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using mod1.repository;
using Utills.domain;
using WindowsFormsApp1.repository;
using WindowsFormsApp1.service;

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
            
            ConnectionStringSettings settings = ConfigurationManager.ConnectionStrings["baschetDB"];

            string returnValue = "";
            if (settings != null)
                returnValue = settings.ConnectionString;
            
            Dictionary<String, string> props = new Dictionary<String, String>();
            props.Add("ConnectionString", returnValue);
            
            IAngajatRepository angajatRepository = new AngajatDBRepository(props);
            IMeciRepository meciRepository = new MeciDBRepository(props);
            IBiletRepository biletRepository = new BiletDBRepository(props);

            Service service = new Service(angajatRepository, meciRepository, biletRepository);
            
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Login login = new Login();
            login.setService(service);
            Application.Run(login);
        }
    }
}