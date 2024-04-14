using System;
using Utills.domain;

namespace mod1.repository;

public interface IAngajatRepository : Repository<int, Angajat>
{

    public Angajat findByUsername(String username);
}