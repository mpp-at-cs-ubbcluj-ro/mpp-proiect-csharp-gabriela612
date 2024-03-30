using System.Data;
using mod1.domain;
using Utils;

namespace mod1.repository;

public class BiletDBRepository : IBiletRepository
{
    private DBUtils dbUtils;
    public static readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

    public BiletDBRepository(Dictionary<string, string> props)
    {
        dbUtils = new DBUtils(props);
        logger.InfoFormat("Initializing BiletDBRepository with DBUtils: {0} ", dbUtils);
    }

    public Bilet FindOne(int id)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Bilet> FindAll()
    {
        throw new NotImplementedException();
    }

    public Bilet Save(Bilet entity)
    {
        logger.InfoFormat("Entering create with value {0}", entity);
        logger.InfoFormat("Getting a connection with db");
        IDbConnection con = dbUtils.GetConnection();

        using (var comm = con.CreateCommand())
        {
            logger.InfoFormat("Prepare Statement: insert into bilete " +
                              "(id_meci, nume_client, nr_locuri) values " +
                              "({0},{1},{2})", entity.Meci.id,
                entity.NumeClient, entity.NrLocuri);
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
            if (rowsAff == 0)
            {
                throw new Exception("Adaugarea nu s-a putut efectua in baza de date");
            }
        }
        logger.InfoFormat("Exiting create with value {0}", entity);
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
        
        logger.InfoFormat("Entering Size");
        logger.InfoFormat("Getting a connection with db");
        IDbConnection con = dbUtils.GetConnection();

        using (var comm = con.CreateCommand())
        {
            logger.InfoFormat("Prepare Statement: SELECT COUNT(*) AS numar_bilete FROM bilete");
            comm.CommandText = "SELECT COUNT(*) AS numar_bilete FROM bilete";

            using (var dataR = comm.ExecuteReader())
            {
                while (dataR.Read())
                {
                    int nr_bilete = dataR.GetInt32(0);
                    logger.InfoFormat("Exiting Size with value {0}", nr_bilete);
                    return nr_bilete;
                }
            }
        }
        logger.InfoFormat("Exiting Size with wrong value {0}", 0);
        return 0;
    }

    public int NrLocuriOcupateMeci(int idMeci)
    {
        logger.InfoFormat("Entering NrLocuriOcupateMeci");
        logger.InfoFormat("Getting a connection with db");
        IDbConnection con = dbUtils.GetConnection();
        int numarBilete = -1;
        
        logger.InfoFormat("Prepare Statement: SELECT SUM(nr_locuri) AS numar_bilete FROM bilete WHERE id_meci={0}", idMeci);
        
        using (var comm = con.CreateCommand())
        {
            logger.InfoFormat("Prepare Statement: SELECT COUNT(*) AS numar_bilete FROM bilete");
            comm.CommandText = "SELECT SUM(nr_locuri) AS numar_bilete FROM bilete WHERE id_meci=@id_meci";
            
            IDbDataParameter paramId1 = comm.CreateParameter();
            paramId1.ParameterName = "@id_meci";
            paramId1.Value = idMeci;
            comm.Parameters.Add(paramId1);

            using (var dataR = comm.ExecuteReader())
            {
                while (dataR.Read())
                {
                    numarBilete = dataR.GetInt32(0);
                    logger.InfoFormat("Exiting NrLocuriOcupateMeci with value {0}", numarBilete);
                    return numarBilete;
                }
                logger.InfoFormat("Exiting NrLocuriOcupateMeci with value {0}", 0);
                return 0;
            }
        }

        return numarBilete;
    }
}