namespace mod1.domain;

public class Bilet : Entity<int>
{
    private Meci meci;
    private string numeClient;
    private int nrLocuri;

    public Bilet(Meci meci, string numeClient, int nrLocuri)
    {
        this.meci = meci;
        this.numeClient = numeClient;
        this.nrLocuri = nrLocuri;
    }

    public Meci Meci => meci;

    public string NumeClient => numeClient;

    public int NrLocuri => nrLocuri;
}