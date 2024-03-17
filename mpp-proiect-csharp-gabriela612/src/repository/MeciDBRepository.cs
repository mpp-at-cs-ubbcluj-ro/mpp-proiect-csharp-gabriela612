using System.Data;
using System.Data.SqlClient;
using mod1.domain;
using Utils;

namespace mod1.repository;

public class MeciDBRepository : IMeciRepository
{
    private DBUtils dbUtils;

    public MeciDBRepository(Dictionary<string, string> props)
    {
        dbUtils = new DBUtils(props);
    }

    public Meci FindOne(int id)
    {
        Meci meci = null;
        
        //log.InfoFormat("Entering findOne with value {0}", id);
        IDbConnection con = dbUtils.GetConnection();

        using (var comm = con.CreateCommand())
        {
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
                    DateOnly data = DateUtils.FromString(dataR.GetString(4));
                    meci = new Meci(nume, pretBilet, capacitate, data);
                    meci.id = id;
                    //log.InfoFormat("Exiting findOne with value {0}", task);
                    return meci;
                }
            }
        }
        //log.InfoFormat("Exiting findOne with value {0}", null);
        return meci;
    }

    public Meci Create(Meci entity)
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
        
        //log.InfoFormat("Entering findOne with value {0}", id);
        IDbConnection con = dbUtils.GetConnection();

        using (var comm = con.CreateCommand())
        {
            comm.CommandText = "SELECT * FROM meciuri";

            using (var dataR = comm.ExecuteReader())
            {
                while (dataR.Read())
                {
                    int id = dataR.GetInt32(0);
                    string nume = dataR.GetString(1);
                    double pretBilet = dataR.GetDouble(2);
                    int capacitate = dataR.GetInt32(3);
                    DateOnly data = DateUtils.FromString(dataR.GetString(4));
                    Meci meci = new Meci(nume, pretBilet, capacitate, data);
                    meci.id = id;
                    //log.InfoFormat("Exiting findOne with value {0}", task);
                    meciuri.Add(meci);
                }
            }
        }
        //log.InfoFormat("Exiting findOne with value {0}", null);
        return meciuri;
    }
}