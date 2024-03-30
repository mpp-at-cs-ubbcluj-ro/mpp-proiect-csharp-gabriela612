

using System;
using System.Collections.Generic;
using mod1.repository;
using WindowsFormsApp1.domain;
using WindowsFormsApp1.repository;

namespace WindowsFormsApp1.service;

public class Service
{
    IAngajatRepository _angajatRepository;
    IMeciRepository _meciRepository;
    IBiletRepository _biletRepository;

    public Service(IAngajatRepository angajatRepository, IMeciRepository meciRepository, IBiletRepository biletRepository)
    {
        this._angajatRepository = angajatRepository;
        this._meciRepository = meciRepository;
        this._biletRepository = biletRepository;
    }
    
    public int Login(String username, String parola) {
        Angajat angajat = _angajatRepository.findByUsername(username);
        if (angajat.id < 0)
            return -1;
        if (angajat.Equals(new Angajat(parola, username)))
            return angajat.id;
        return -1;
    }

    public Bilet CumparaBilet(Meci meci, String numeClient, int nrLocuri)
    {
        if (nrLocuri > this.NrLocuriDisponibileMeci(meci))
            throw new Exception("Nu mai sunt atatea bilete disponibile.");
        Bilet bilet = new Bilet(meci, numeClient, nrLocuri);
        bilet = _biletRepository.Save(bilet);
        // if (bilet.id == null)
        //     return null;
        return bilet;
    }

    public int NrLocuriDisponibileMeci(Meci meci)
    {
        int nrLocuri = _biletRepository.NrLocuriOcupateMeci(meci.id);
        if (nrLocuri == -1)
            throw new Exception("Nu am putut gasi numarul de locuri ocupate");
        return meci.Capacitate - _biletRepository.NrLocuriOcupateMeci(meci.id);
    }

    public IEnumerable<Meci> GetMeciuri() {
        return _meciRepository.FindAll();
    }

    public IEnumerable<Meci> GetMeciuriLibere() {
        return _meciRepository.FindMeciuriDisponibile();
    }
    
}
