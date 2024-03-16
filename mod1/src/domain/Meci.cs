namespace mod1.domain;

public class Meci : Entity<long>
{
    private string nume;
    private double pret;
    private int capacitate;
    private LocalDate data;

    public Meci(string nume, double pret, int capacitate, LocalDate data)
    {
        this.nume = nume;
        this.pret = pret;
        this.capacitate = capacitate;
        this.data = data;
    }

    public string Nume => nume;

    public double Pret => pret;

    public int capacitate
    {
        get => capacitate;
    }
    
    public int data
    {
        get => data;
    }
}