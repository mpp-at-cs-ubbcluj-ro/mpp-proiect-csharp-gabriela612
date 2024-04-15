using System;
using System.Collections.Generic;
using Utills.domain;
using Utills.observer;

namespace Utills.services
{
    public interface IServices
    {
        int Login(string username, string parola, IObserver client);
        Bilet CumparaBilet(Meci meci, string numeClient, int nrLocuri);
        int NrLocuriDisponibileMeci(Meci meci);
        IEnumerable<MeciL> GetMeciuri();
        IEnumerable<MeciL> GetMeciuriLibere();
        public void Logout(int idAngajat);
    }
}