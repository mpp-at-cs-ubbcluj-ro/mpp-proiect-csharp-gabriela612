using System;
using System.Collections.Generic;
using System.Data;
using mod1.repository;
using Utills.domain;
using Utills.utils;

namespace WindowsFormsApp1.repository;

public class AngajatDBRepository : IAngajatRepository
{
    private DBUtils dbUtils;
    public static readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

    public AngajatDBRepository(Dictionary<string, string> props)
    {
        dbUtils = new DBUtils(props);
        logger.InfoFormat("Initializing AngajatDBRepository with DBUtils: {0} ", dbUtils);
    }

    public Angajat FindOne(int id)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Angajat> FindAll()
    {
        throw new NotImplementedException();
    }

    public Angajat Save(Angajat entity)
    {
        throw new NotImplementedException();
    }

    public Angajat Delete(int id)
    {
        throw new NotImplementedException();
    }

    public Angajat Update(Angajat entity)
    {
        throw new NotImplementedException();
    }

    public int Size()
    {
        throw new NotImplementedException();
    }

    public Angajat findByUsername(string username)
    {
        logger.InfoFormat("Entering findByUsername with value {0}", username);
        logger.InfoFormat("Getting a connection with db");
        IDbConnection con = dbUtils.GetConnection();

        using (var comm = con.CreateCommand())
        {
            logger.InfoFormat("Prepare Statement: select * from angajati where username={0}", username);
            comm.CommandText = "select * from angajati where username=@username";
            IDbDataParameter paramId = comm.CreateParameter();
            paramId.ParameterName = "@username";
            paramId.Value = username;
            comm.Parameters.Add(paramId);

            using (var dataR = comm.ExecuteReader())
            {
                if (dataR.Read())
                {
                    int id = dataR.GetInt32(0);
                    string parola = dataR.GetString(2);
                    Angajat angajat = new Angajat(parola, username);
                    angajat.id = id;
                    logger.InfoFormat("Angajat gasit : {0} ", angajat);
                    return angajat;
                }
            }
        }
        logger.InfoFormat("Exiting findOne with value {0}", null);
        throw new Exception("Angajatul nu a fost gasit");
    }
}