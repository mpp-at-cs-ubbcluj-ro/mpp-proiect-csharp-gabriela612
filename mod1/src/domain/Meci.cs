namespace mod1.domain;

public class Meci : Entity<long>
{
    private string nume;
    private double pret;
    private int nrLocuriDisponibile;

    public Meci(string nume, double pret, int nrLocuriDisponibile)
    {
        this.nume = nume;
        this.pret = pret;
        this.nrLocuriDisponibile = nrLocuriDisponibile;
    }

    public string Nume => nume;

    public double Pret => pret;

    public int NrLocuriDisponibile
    {
        get => nrLocuriDisponibile;
        set => nrLocuriDisponibile = value;
    }
}