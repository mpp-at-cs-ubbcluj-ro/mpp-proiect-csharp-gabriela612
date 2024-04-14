
using System;
using mod1.repository;
using Utills.domain;

namespace WindowsFormsApp1.repository;

public interface IBiletRepository : Repository<int, Bilet>
{
    int NrLocuriOcupateMeci(int idMeci);
}