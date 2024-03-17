// See https://aka.ms/new-console-template for more information

using System.Configuration;
using mod1.domain;
using mod1.repository;


static string GetConnectionStringByName(string name)
{
    // Assume failure.
    string returnValue = null;

    // Look for the name in the connectionStrings section.
    ConnectionStringSettings settings =ConfigurationManager.ConnectionStrings[name];

    // If found, return the connection string.
    if (settings != null)
        returnValue = settings.ConnectionString;

    return returnValue;
}


Dictionary<String, string> props = new Dictionary<String, String>();
props.Add("ConnectionString", GetConnectionStringByName("baschetDB"));

IAngajatRepository angajatRepository = new AngajatDBRepository(props);
Angajat angajat = angajatRepository.findByUsername("ionescu_ion");
IMeciRepository meciRepository = new MeciDBRepository(props);
Meci meci = meciRepository.FindOne(1);
meciRepository.FindAll();
IBiletRepository biletRepository = new BiletDBRepository(props);
biletRepository.Create(new Bilet(meci, "Mihai Eminescu", 2));
biletRepository.Size();





