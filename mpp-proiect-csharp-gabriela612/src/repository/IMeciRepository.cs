using mod1.domain;

namespace mod1.repository;

public interface IMeciRepository : Repository<int, Meci>
{
    Meci Repository<int, Meci>.FindOne(int id)
    {
        throw new NotImplementedException();
    }

    IEnumerable<Meci> Repository<int, Meci>.FindAll()
    {
        throw new NotImplementedException();
    }
    
    IEnumerable<Meci> FindMeciuriDisponibile();
}