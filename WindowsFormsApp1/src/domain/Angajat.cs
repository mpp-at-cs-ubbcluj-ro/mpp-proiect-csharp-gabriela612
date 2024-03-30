namespace WindowsFormsApp1.domain;

public class Angajat : Entity<int>
{
    private string parola;
    private string username;

    public Angajat(string parola, string username)
    {
        this.parola = parola;
        this.username = username;
    }

    protected bool Equals(Angajat other)
    {
        return base.Equals(other) || (parola == other.parola && username == other.username);
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((Angajat)obj);
    }
    
}