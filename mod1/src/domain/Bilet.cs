namespace mod1.domain;

public class Bilet
{
    private long idMeci;
    private string numeClient;
    private int nrLocuri;

    public Bilet(long idMeci, string numeClient, int nrLocuri)
    {
        this.idMeci = idMeci;
        this.numeClient = numeClient;
        this.nrLocuri = nrLocuri;
    }
    
}