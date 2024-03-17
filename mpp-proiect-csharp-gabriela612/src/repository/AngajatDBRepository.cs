using System.Data;
using mod1.domain;
using Utils;

namespace mod1.repository;

public class AngajatDBRepository : IAngajatRepository
{
    private DBUtils dbUtils;

    public AngajatDBRepository(Dictionary<string, string> props)
    {
        dbUtils = new DBUtils(props);
    }

    public Angajat FindOne(int id)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Angajat> FindAll()
    {
        throw new NotImplementedException();
    }

    public Angajat Create(Angajat entity)
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
        //log.InfoFormat("Entering findOne with value {0}", id);
        IDbConnection con = dbUtils.GetConnection();

        using (var comm = con.CreateCommand())
        {
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
                    //log.InfoFormat("Exiting findOne with value {0}", task);
                    return angajat;
                }
            }
        }
        //log.InfoFormat("Exiting findOne with value {0}", null);
        return null;
    }
}