using mod1.domain;

namespace mod1.repository;

public interface IBiletRepository : Repository<int, Bilet>
{
    Bilet Repository<int, Bilet>.Save(Bilet entity)
    {
        throw new NotImplementedException();
    }

    int Repository<int, Bilet>.Size()
    {
        throw new NotImplementedException();
    }
    
    int NrLocuriOcupateMeci(int idMeci);
}