

using System.Collections.Generic;
using Utills.domain;

namespace mod1.repository;

public interface Repository<TId, E> where E : Entity<TId>
{
    E FindOne(TId id);
    IEnumerable<E> FindAll();
    E Save(E entity);
    E Delete(TId id);
    E Update(E entity);
    int Size();
}