using System;
using System.Collections.Generic;
using Utills.domain;
using Utills.observer;
using Utills.services;
using WindowsFormsApp1.utils;

namespace WindowsFormsApp1;

public class ClientCtrl : IObserver
{
    public event EventHandler<UserEventArgs> updateEvent; //ctrl calls it when it has received an update
    private readonly IServices server;
    private int idAngajat;
    public ClientCtrl(IServices server)
    {
        this.server = server;
        idAngajat = -1;
    }

    public int Login(String username, String parola)
    {
        idAngajat = server.Login(username, parola, this);
        Console.WriteLine("Login succeeded ....");
        Console.WriteLine("Current user {0}", idAngajat);
        return idAngajat;
    }
    
    public void Logout()
    {
        Console.WriteLine("Ctrl logout");
        server.Logout(idAngajat);
        idAngajat = -1;
    }

    public void schimbareMeciuri(IEnumerable<MeciL> meciuri)
    {
        UserEventArgs userArgs = new UserEventArgs(UserEvent.BiletVandut, meciuri);
        Console.WriteLine("Schimbare lista de meciuri");
        OnUserEvent(userArgs);
    }
    
    protected virtual void OnUserEvent(UserEventArgs e)
    {
        if (updateEvent == null) return;
        updateEvent(this, e);
        Console.WriteLine("Update Event called");
    }
    
    public Bilet Vanzare(Meci meciSelectat, string numeClient, int nrL)
    {
        try
        {
            Bilet bilet = server.CumparaBilet(meciSelectat, numeClient, nrL);
            return bilet;
        }
        catch (Exception ex)
        {
            MyMessageBox.Show(ex.Message);
        }

        return null;
    }

    public IEnumerable<MeciL> GetMeciuri()
    {
        // Console.WriteLine("Am ajuns aici");
        HashSet<MeciL> meciuriList = (HashSet<MeciL>)this.server.GetMeciuri();
        return meciuriList;
    }
    
    public IEnumerable<MeciL> GetMeciuriLibere()
    {
        HashSet<MeciL> meciuriList = (HashSet<MeciL>)this.server.GetMeciuriLibere();
        return meciuriList;
    }

}