
using System;
using Utills.domain;

namespace Server.repository;

public interface IBiletRepository : Repository<int, Bilet>
{
    int NrLocuriOcupateMeci(int idMeci);
}