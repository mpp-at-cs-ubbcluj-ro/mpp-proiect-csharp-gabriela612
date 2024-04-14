using System;
using System.Collections.Generic;
using Utills.domain;

namespace Utills.services
{
    public interface IServices
    {
        int Login(string username, string parola);
        Bilet CumparaBilet(Meci meci, string numeClient, int nrLocuri);
        int NrLocuriDisponibileMeci(Meci meci);
        IEnumerable<MeciL> GetMeciuri();
        IEnumerable<MeciL> GetMeciuriLibere();
    }
}