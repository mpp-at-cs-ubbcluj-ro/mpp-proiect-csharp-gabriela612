using System;

namespace WindowsFormsApp1.domain;

public class Meci : Entity<int>
{
    private string nume;
    private double pret;
    private int capacitate;
    private DateTime data;

    public Meci(string nume, double pret, int capacitate, DateTime data)
    {
        this.nume = nume;
        this.pret = pret;
        this.capacitate = capacitate;
        this.data = data;
    }

    public string Nume => nume;

    public double Pret => pret;

    public int Capacitate => capacitate;

    public DateTime Data => data;
}