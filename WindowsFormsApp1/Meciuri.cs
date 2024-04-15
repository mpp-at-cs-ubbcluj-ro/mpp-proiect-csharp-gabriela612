using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Utills.domain;
using Utills.services;
using WindowsFormsApp1.utils;

namespace WindowsFormsApp1;

public partial class Meciuri : Form
{
    private ClientCtrl clientCtrl;
    private HashSet<MeciL> meciuriList;
    private bool filtru = false;

    public Meciuri(ClientCtrl clientCtrl)
    {
        InitializeComponent();
        this.clientCtrl = clientCtrl;
        meciuriList = (HashSet<MeciL>)this.clientCtrl.GetMeciuri();
        
        initData();
    
        meciuriTable.CellFormatting += meciuriTable_CellFormatting;
        
        Refresh();

        clientCtrl.updateEvent += userUpdate;
    }

    public void userUpdate(object sender, UserEventArgs e)
    {
        if (e.UserEventType==UserEvent.BiletVandut)
        {
            HashSet<MeciL> meciLs = (HashSet<MeciL>)e.Data;
            Console.WriteLine("[ChatWindow] am primit alte meciuri "+ meciuriList);
            initData(meciLs);
        }
    }
    

    private void meciuriTable_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
    {
        // Verificați dacă este vorba despre coloana "nrLocuriDisponibileColumn"
        if (meciuriTable.Columns[e.ColumnIndex].Name == "nrLocuriDisponibileColumn")
        {
            // Obțineți valoarea entității pentru rândul curent
            MeciL meci = meciuriTable.Rows[e.RowIndex].DataBoundItem as MeciL;

            // Verificați dacă entitatea este validă
            if (meci != null)
            {
                // Calculați numărul de locuri disponibile pentru meci
                int nr = meci.NrLocuriDisponibile;

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
        meciuriTable.Columns["NrLocuriDisponibile"].Visible = false;
    }



    public void initData()
    {
        var source = new BindingSource();
        if (!filtru)
            meciuriList = (HashSet<MeciL>)this.clientCtrl.GetMeciuri();
        else
        {
            meciuriList = (HashSet<MeciL>)this.clientCtrl.GetMeciuriLibere();
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
    
    public void initData(HashSet<MeciL> meciLs)
    {
        var source = new BindingSource();

        filtru = false;
        meciuriList = meciLs;

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
        MeciL meciSelectat = selectedRow.DataBoundItem as MeciL;
        string numeClient = numeClientField.Text;
        int nrL = int.Parse(nrLocuri.ToString());

        try
        {
            Bilet bilet = clientCtrl.Vanzare(meciSelectat, numeClient, nrL);
            return;
        }
        catch (Exception ex)
        {
            MyMessageBox.Show(ex.Message);
        }
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
        clientCtrl.Logout();
        this.Close();
    }
}