namespace mod1.domain;

public class Angajat : Entity<long>
{
    private string parola;

    public Angajat(string parola)
    {
        this.parola = parola;
    }

    protected bool Equals(Angajat other)
    {
        return base.Equals(other) && parola == other.parola;
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        if ((Entity<long>)obj != (Entity<long>)this) return false;
        return Equals((Angajat)obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(base.GetHashCode(), parola);
    }
}