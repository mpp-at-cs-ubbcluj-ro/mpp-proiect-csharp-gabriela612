

using System;
using System.Collections.Generic;
using System.Linq;
using Server.repository;
using Utills.domain;
using Utills.observer;
using Utills.services;

namespace Server.service;

public class Service : IServices
{
    IAngajatRepository _angajatRepository;
    IMeciRepository _meciRepository;
    IBiletRepository _biletRepository;
    
    private readonly IDictionary <int, IObserver> loggedClients;

    public Service(IAngajatRepository angajatRepository, IMeciRepository meciRepository, IBiletRepository biletRepository)
    {
        this._angajatRepository = angajatRepository;
        this._meciRepository = meciRepository;
        this._biletRepository = biletRepository;
        loggedClients = new Dictionary<int, IObserver>();
    }
    
    public int Login(String username, String parola, IObserver client) {
        Angajat angajat = _angajatRepository.findByUsername(username);
        if (angajat == null || angajat.id < 1)
            return -1;
        if (loggedClients.Keys.Contains(angajat.id))
        {
            Console.WriteLine("Nu e bine");
            return -1;
        }
        if (angajat.Equals(new Angajat(parola, username)))
        {
            this.loggedClients.Add(angajat.id, client);
            return angajat.id;
        }
        return -1;
    }
    
    private void NotifyCumparareBilet(IEnumerable<MeciL> meciuri){
        Console.WriteLine("notify new meciuri list "+meciuri.Count());
        foreach(IObserver client in loggedClients.Values){
            if (client!=null)
                try {
                    Console.WriteLine("Notifying " + client);
                    client.schimbareMeciuri(meciuri);
                } catch (Exception e) {
                    Console.WriteLine("Error notifying client " + client + " with " + e.Message);
                }
        }
    }

    public Bilet CumparaBilet(Meci meci, String numeClient, int nrLocuri)
    {
        if (nrLocuri > this.NrLocuriDisponibileMeci(meci))
            throw new Exception("Nu mai sunt atatea bilete disponibile.");
        Bilet bilet = new Bilet(meci, numeClient, nrLocuri);
        bilet = _biletRepository.Save(bilet);
        // if (bilet.id == null)
        //     return null;
        NotifyCumparareBilet(this.GetMeciuri());
        return bilet;
    }

    public int NrLocuriDisponibileMeci(Meci meci)
    {
        int nrLocuri = _biletRepository.NrLocuriOcupateMeci(meci.id);
        if (nrLocuri == -1)
            throw new Exception("Nu am putut gasi numarul de locuri ocupate");
        return meci.Capacitate - _biletRepository.NrLocuriOcupateMeci(meci.id);
    }

    public IEnumerable<MeciL> GetMeciuri()
    {
        HashSet<Meci> meciuri = (HashSet<Meci>)_meciRepository.FindAll();
        HashSet<MeciL> meciLs = new HashSet<MeciL>();
        foreach (Meci m in meciuri)
        {
            MeciL meciL = new MeciL(m.Nume, m.Pret, m.Capacitate, m.Data, this.NrLocuriDisponibileMeci(m));
            meciL.id = m.id;
            meciLs.Add(meciL);
        }

        return meciLs;
    }

    public IEnumerable<MeciL> GetMeciuriLibere() {
        HashSet<Meci> meciuri = (HashSet<Meci>)_meciRepository.FindMeciuriDisponibile();
        HashSet<MeciL> meciLs = new HashSet<MeciL>();
        foreach (Meci m in meciuri)
        {
            MeciL meciL = new MeciL(m.Nume, m.Pret, m.Capacitate, m.Data, this.NrLocuriDisponibileMeci(m));
            meciL.id = m.id;
            meciLs.Add(meciL);
        }

        return meciLs;
    }

    public void Logout(int idAngajat) {
        loggedClients.Remove(idAngajat);
        // IObserver localClient = loggedClients.Remove(idAngajat);
//        if (localClient==null)
//            throw new Exception("User "+idAngajat+" is not logged in.");
    }
}
