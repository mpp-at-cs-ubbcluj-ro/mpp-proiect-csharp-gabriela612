using System.Collections.Generic;
using Utills.domain;

namespace Server.repository;

public interface IMeciRepository : Repository<int, Meci>
{
    IEnumerable<Meci> FindMeciuriDisponibile();
}