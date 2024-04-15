using System;
using Utills.domain;

namespace Server.repository;

public interface IAngajatRepository : Repository<int, Angajat>
{

    public Angajat findByUsername(String username);
}