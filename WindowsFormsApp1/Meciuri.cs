using System;
using System.Collections.Generic;
using System.Windows.Forms;
using WindowsFormsApp1.domain;
using WindowsFormsApp1.service;
using WindowsFormsApp1.utils;

namespace WindowsFormsApp1;

public partial class Meciuri : Form
{
    
    Service service;
    private int idAngajat;
    private Login login;
    private HashSet<Meci> meciuriList;
    private bool filtru = false;

    public void setService(Service service, int idAngajat, Login login) {
        this.service = service;
        this.idAngajat = idAngajat;
        this.login = login;
    
        // Inițializați datele și sursa de date
        initData();
    
        // Asociați evenimentul de formatare a celulelor pentru a afișa numărul de locuri disponibile
        meciuriTable.CellFormatting += meciuriTable_CellFormatting;
    
        // Actualizați DataGridView-ul
        Refresh();
    }



    private void meciuriTable_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
    {
        // Verificați dacă este vorba despre coloana "nrLocuriDisponibileColumn"
        if (meciuriTable.Columns[e.ColumnIndex].Name == "nrLocuriDisponibileColumn")
        {
            // Obțineți valoarea entității pentru rândul curent
            Meci meci = meciuriTable.Rows[e.RowIndex].DataBoundItem as Meci;

            // Verificați dacă entitatea este validă
            if (meci != null)
            {
                // Calculați numărul de locuri disponibile pentru meci
                int nr = service.NrLocuriDisponibileMeci(meci);

                // Setare valoare afișată în funcție de numărul de locuri disponibile
                if (nr != 0)
                    e.Value = nr.ToString();
                else
                    e.Value = "SOLD OUT";

                // Setare tipul de afișare al celulei (String)
                e.FormattingApplied = true;
            }
        }
        numeColumn.DataPropertyName = "Nume";
        pretBiletColumn.DataPropertyName = "Pret";
        dataColumn.DataPropertyName = "Data";
        meciuriTable.Columns["Nume"].Visible = false;
        meciuriTable.Columns["Pret"].Visible = false;
        meciuriTable.Columns["Capacitate"].Visible = false;
        meciuriTable.Columns["Data"].Visible = false;
        meciuriTable.Columns["Id"].Visible = false;
    }



    public void initData()
    {
        var source = new BindingSource();
        if (!filtru)
            meciuriList = (HashSet<Meci>)this.service.GetMeciuri();
        else
        {
            meciuriList = (HashSet<Meci>)this.service.GetMeciuriLibere();
        }

        if (filtru)
            buttonFiltru.Text = "Toate meciurile";
        else
        {
            buttonFiltru.Text = "Doar meciurile la care mai sunt bilete";
        }
        
        source.DataSource = meciuriList;
        meciuriTable.DataSource = source;
        Refresh();
    }
    
    public Meciuri()
    {
        InitializeComponent();
    }

    private void buttonVanzare_Click(object sender, EventArgs e)
    {
        decimal nrLocuri = nrLocuriSelector.Value;
        if (nrLocuri == 0)
        {
            MyMessageBox.Show("Nu ati selectat numarul de locuri");
            return;
        }

        if (meciuriTable.SelectedRows.Count != 1)
        {
            MyMessageBox.Show("Trebuie sa selectati un meci");
            return;
        }

        if (numeClientField.Text == String.Empty)
        {
            MyMessageBox.Show("Trebuie sa introduceti numele clientului");
            return;
        }
        
        DataGridViewRow selectedRow = meciuriTable.SelectedRows[0];
        Meci meciSelectat = selectedRow.DataBoundItem as Meci;
        string numeClient = numeClientField.Text;
        int nrL = int.Parse(nrLocuri.ToString());

        Bilet bilet = service.CumparaBilet(meciSelectat, numeClient, nrL);
        
        MyMessageBox.Show("Biletul a fost inregistrat in baza de date");
        this.initData();
    }

    private void buttonFiltru_Click(object sender, EventArgs e)
    {
        filtru = !filtru;
        initData();
    }

    private void LogoutButton_Click(object sender, EventArgs e)
    {
        this.login.reopen();
        this.login.Show();
        this.Close();
    }
}