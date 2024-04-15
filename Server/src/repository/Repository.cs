

using System.Collections.Generic;
using Utills.domain;

namespace Server.repository;

public interface Repository<TId, E> where E : Entity<TId>
{
    E FindOne(TId id);
    IEnumerable<E> FindAll();
    E Save(E entity);
    E Delete(TId id);
    E Update(E entity);
    int Size();
}