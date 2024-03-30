using System.Collections.Generic;
using mod1.repository;
using WindowsFormsApp1.domain;

namespace WindowsFormsApp1.repository;

public interface IMeciRepository : Repository<int, Meci>
{
    IEnumerable<Meci> FindMeciuriDisponibile();
}