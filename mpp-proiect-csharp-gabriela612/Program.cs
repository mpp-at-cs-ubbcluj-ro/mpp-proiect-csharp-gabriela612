// See https://aka.ms/new-console-template for more information

using System.Configuration;
using mod1.domain;
using mod1.repository;
using mpp_proiect_csharp_gabriela612.service;


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
IMeciRepository meciRepository = new MeciDBRepository(props);
IBiletRepository biletRepository = new BiletDBRepository(props);

Service service = new Service(angajatRepository, meciRepository, biletRepository);
int idA = service.Login("test", "test");
Console.WriteLine(((HashSet<Meci>)service.GetMeciuri()).Count);
Console.WriteLine(((HashSet<Meci>)service.GetMeciuriLibere()).Count);
Meci meci = new Meci("", 0, 3000, new DateOnly());
meci.id = 1;
Console.WriteLine(service.CumparaBilet(meci, "ana", 2));




