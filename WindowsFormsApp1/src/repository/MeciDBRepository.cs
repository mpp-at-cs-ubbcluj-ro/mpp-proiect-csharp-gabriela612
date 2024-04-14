using System;
using System.Collections.Generic;
using System.Data;
using Utills.utils;
using Utills.domain;
using WindowsFormsApp1.utils;

namespace WindowsFormsApp1.repository;

public class MeciDBRepository : IMeciRepository
{
    private DBUtils dbUtils;
    public static readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

    public MeciDBRepository(Dictionary<string, string> props)
    {
        dbUtils = new DBUtils(props);
        logger.InfoFormat("Initializing MeciDBRepository with DBUtils: {0} ", dbUtils);
    }

    public Meci FindOne(int id)
    {
        Meci meci = null;
        
        logger.InfoFormat("Entering findOne with value {0}", id);
        logger.InfoFormat("Getting a connection with db");
        IDbConnection con = dbUtils.GetConnection();

        using (var comm = con.CreateCommand())
        {
            logger.InfoFormat("Prepare Statement: SELECT * FROM meciuri " +
                              "WHERE id={0}", id);
            comm.CommandText = "SELECT * FROM meciuri WHERE id=@id";
            IDbDataParameter paramId = comm.CreateParameter();
            paramId.ParameterName = "@id";
            paramId.Value = id;
            comm.Parameters.Add(paramId);

            using (var dataR = comm.ExecuteReader())
            {
                if (dataR.Read())
                {
                    string nume = dataR.GetString(1);
                    double pretBilet = dataR.GetDouble(2);
                    int capacitate = dataR.GetInt32(3);
                    DateTime data = DateUtils.FromString(dataR.GetString(4));
                    meci = new Meci(nume, pretBilet, capacitate, data);
                    meci.id = id;
                    logger.InfoFormat("Exiting findOne with value {0}", meci);
                    return meci;
                }
            }
        }
        logger.InfoFormat("Exiting findOne with wrong value {0}", meci);
        throw new Exception("Meciul nu a fost gasit");
    }

    public Meci Save(Meci entity)
    {
        throw new NotImplementedException();
    }

    public Meci Delete(int id)
    {
        throw new NotImplementedException();
    }

    public Meci Update(Meci entity)
    {
        throw new NotImplementedException();
    }

    public int Size()
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Meci> FindAll()
    {
        HashSet<Meci> meciuri = new HashSet<Meci>();
        
        logger.InfoFormat("Entering findAll");
        logger.InfoFormat("Getting a connection with db");
        IDbConnection con = dbUtils.GetConnection();

        using (var comm = con.CreateCommand())
        {
            logger.InfoFormat("Prepare Statement: SELECT * FROM meciuri");
            comm.CommandText = "SELECT * FROM meciuri";

            using (var dataR = comm.ExecuteReader())
            {
                while (dataR.Read())
                {
                    int id = dataR.GetInt32(0);
                    string nume = dataR.GetString(1);
                    double pretBilet = dataR.GetDouble(2);
                    int capacitate = dataR.GetInt32(3);
                    DateTime data = DateUtils.FromString(dataR.GetString(4));
                    Meci meci = new Meci(nume, pretBilet, capacitate, data);
                    meci.id = id;
                    logger.InfoFormat("Meci gasit : {0}", meci);
                    meciuri.Add(meci);
                }
            }
        }
        logger.InfoFormat("Exiting findAll with value {0}", meciuri);
        return meciuri;
    }

    public IEnumerable<Meci> FindMeciuriDisponibile()
    {
        logger.InfoFormat("Entering FindMeciuriDisponibile");
        logger.InfoFormat("Getting a connection with db");
        IDbConnection con = dbUtils.GetConnection();
        
        logger.InfoFormat("Prepare Statement: SELECT m.id, m.nume, m.pret_bilet, m.capacitate, m.data\n" +
                          "FROM meciuri m\n" +
                          "         LEFT JOIN (\n" +
                          "    SELECT id_meci, SUM(nr_locuri) AS total_locuri\n" +
                          "    FROM bilete\n" +
                          "    GROUP BY id_meci\n" +
                          ") b ON m.id = b.id_meci\n" +
                          "WHERE total_locuri < m.capacitate OR total_locuri IS NULL\n" +
                          "ORDER BY CASE WHEN total_locuri IS NULL THEN 0 ELSE 1 END;");
        
        HashSet<Meci> meciuri = new HashSet<Meci>();
        
        using (var comm = con.CreateCommand())
        {
            comm.CommandText = "SELECT m.id, m.nume, m.pret_bilet, m.capacitate, m.data " +
                               "FROM meciuri m " +
                               "LEFT JOIN ( " +
                               "SELECT id_meci, SUM(nr_locuri) AS total_locuri " +
                               "FROM bilete " +
                               "GROUP BY id_meci ) b ON m.id = b.id_meci " +
                               "WHERE total_locuri < m.capacitate OR total_locuri IS NULL " +
                               "ORDER BY CASE WHEN total_locuri IS NULL THEN 0 ELSE 1 END;";

            using (var dataR = comm.ExecuteReader())
            {
                while (dataR.Read())
                {
                    int id = dataR.GetInt32(0);
                    string nume = dataR.GetString(1);
                    double pretBilet = dataR.GetDouble(2);
                    int capacitate = dataR.GetInt32(3);
                    DateTime data = DateUtils.FromString(dataR.GetString(4));
                    Meci meci = new Meci(nume, pretBilet, capacitate, data);
                    meci.id = id;
                    logger.InfoFormat("Meci gasit : {0}", meci);
                    meciuri.Add(meci);
                }
            }
        }
        logger.InfoFormat("Exiting findAll with value {0}", meciuri);
        return meciuri;
        
    }
}