using System;

namespace Utills.domain;

[Serializable]
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
        return base.Equals(other) || (parola.Equals(other.parola) && username.Equals(other.username));
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((Angajat)obj);
    }

    public string Parola => parola;

    public string Username => username;
}