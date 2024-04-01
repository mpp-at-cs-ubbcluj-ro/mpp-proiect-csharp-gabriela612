using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp1.service;
using WindowsFormsApp1.utils;

namespace WindowsFormsApp1
{
    public partial class Login : Form
    {
        
        Service service;

        public void setService(Service service) {
            this.service = service;
        }
        
        public Login()
        {
            InitializeComponent();
        }

        private void loginButton_Click(object sender, EventArgs e)
        {
            
            String username = usernameField.Text;
            String parola = parolaField.Text;

            int id_angajat = -1;
            try
            {
                id_angajat = service.Login(username, parola);
            }
            catch (Exception ex)
            {
                MyMessageBox.Show(ex.Message);
            }

            if (id_angajat == -1)
            {
                return;
            }
            
            
            Meciuri meciuri = new Meciuri();
            meciuri.setService(service, id_angajat, this);
            meciuri.Show();
            this.Hide();
        }
        
        public void reopen()
        {
            this.usernameField.Text = "";
            this.parolaField.Text = "";
        }
    }
}