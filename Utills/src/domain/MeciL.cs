using System;

namespace Utills.domain;

[Serializable]
public class MeciL : Meci
{
    private int nrLocuriDisponibile;
    public MeciL(string nume, double pret, int capacitate, DateTime data, int nrLocuriDisponibile) : base(nume, pret, capacitate, data)
    {
        this.nrLocuriDisponibile = nrLocuriDisponibile;
    }

    public int NrLocuriDisponibile => nrLocuriDisponibile;
}
