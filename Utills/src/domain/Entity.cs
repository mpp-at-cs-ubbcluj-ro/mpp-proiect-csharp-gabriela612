using System;
using System.Collections.Generic;

namespace Utills.domain;

[Serializable]
public class Entity<TId>
{
    public TId id { get; set; }

    public override string ToString()
    { 
        return $"Entity{{id={id}}}";
    }

    protected bool Equals(Entity<TId> other)
    {
        return EqualityComparer<TId>.Default.Equals(id, other.id);
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((Entity<TId>)obj);
    }

    public override int GetHashCode()
    {
        return EqualityComparer<TId>.Default.GetHashCode(id);
    }
}