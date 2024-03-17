using System.Data;
using mod1.domain;
using Utils;

namespace mod1.repository;

public class BiletDBRepository : IBiletRepository
{
    private DBUtils dbUtils;

    public BiletDBRepository(Dictionary<string, string> props)
    {
        dbUtils = new DBUtils(props);
    }

    public Bilet FindOne(int id)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Bilet> FindAll()
    {
        throw new NotImplementedException();
    }

    public Bilet Create(Bilet entity)
    {
        //log.InfoFormat("Entering findOne with value {0}", id);
        IDbConnection con = dbUtils.GetConnection();

        using (var comm = con.CreateCommand())
        {
            comm.CommandText = "insert into bilete (id_meci, nume_client, nr_locuri)" +
                               " values (@id_meci,@nume_client,@nr_locuri)";

            IDbDataParameter paramId1 = comm.CreateParameter();
            paramId1.ParameterName = "@id_meci";
            paramId1.Value = entity.Meci.id;
            comm.Parameters.Add(paramId1);
            
            IDbDataParameter paramId2 = comm.CreateParameter();
            paramId2.ParameterName = "@nume_client";
            paramId2.Value = entity.NumeClient;
            comm.Parameters.Add(paramId2);
            
            IDbDataParameter paramId3 = comm.CreateParameter();
            paramId3.ParameterName = "@nr_locuri";
            paramId3.Value = entity.NrLocuri;
            comm.Parameters.Add(paramId3);

            int rowsAff = comm.ExecuteNonQuery();
            if (rowsAff != 0)
            {
                entity = null!;
            }

            return entity;
        }
        //log.InfoFormat("Exiting findOne with value {0}", null);
        return entity;
    }

    public Bilet Delete(int id)
    {
        throw new NotImplementedException();
    }

    public Bilet Update(Bilet entity)
    {
        throw new NotImplementedException();
    }

    public int Size()
    {
        
        //log.InfoFormat("Entering findOne with value {0}", id);
        IDbConnection con = dbUtils.GetConnection();

        using (var comm = con.CreateCommand())
        {
            comm.CommandText = "SELECT COUNT(*) AS numar_bilete FROM bilete";

            using (var dataR = comm.ExecuteReader())
            {
                while (dataR.Read())
                {
                    int nr_nilete = dataR.GetInt32(0);
                    //log.InfoFormat("Exiting findOne with value {0}", task);
                    return nr_nilete;
                }
            }

            return 0;
        }
        //log.InfoFormat("Exiting findOne with value {0}", null);
        return 0;
    }
}