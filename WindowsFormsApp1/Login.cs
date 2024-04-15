using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Utills.services;
using WindowsFormsApp1.utils;

namespace WindowsFormsApp1
{
    public partial class Login : Form
    {
        private ClientCtrl clientCtrl;
        
        public Login(ClientCtrl clientCtrl)
        {
            InitializeComponent();
            this.clientCtrl = clientCtrl;
        }

        private void loginButton_Click(object sender, EventArgs e)
        {
            
            String username = usernameField.Text;
            String parola = parolaField.Text;

            int id_angajat = -1;
            try
            {
                clientCtrl.Login(username, parola);
                //MessageBox.Show("Login succeded");
                Meciuri meciuriWindow = new Meciuri(clientCtrl);
                meciuriWindow.Text = "Chat window for " + username;
                meciuriWindow.Show();
                this.Hide();
                return;

            }
            catch (Exception ex)
            {
                MyMessageBox.Show(ex.Message);
                return;
            }

            // if (id_angajat == -1)
            // {
            //     MyMessageBox.Show("Angajatul nu a fost gasit");
            // }
        }
        
        public void reopen()
        {
            this.usernameField.Text = "";
            this.parolaField.Text = "";
        }
    }
}