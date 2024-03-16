namespace mod1.domain;

public class Bilet
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
    
}