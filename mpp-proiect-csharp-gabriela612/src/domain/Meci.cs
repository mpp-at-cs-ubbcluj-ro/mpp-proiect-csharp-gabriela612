namespace mod1.domain;

public class Meci : Entity<int>
{
    private string nume;
    private double pret;
    private int capacitate;
    private DateOnly data;

    public Meci(string nume, double pret, int capacitate, DateOnly data)
    {
        this.nume = nume;
        this.pret = pret;
        this.capacitate = capacitate;
        this.data = data;
    }

    public string Nume => nume;

    public double Pret => pret;

    public int Capacitate => capacitate;

    public DateOnly Data => data;
}