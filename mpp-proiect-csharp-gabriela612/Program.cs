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


// Dictionary<String, string> props = new Dictionary<String, String>();
// props.Add("ConnectionString", GetConnectionStringByName("baschetDB"));
// Console.WriteLine("Hello, World!");
// IBiletRepository angajatRepository = new BiletDBRepository((Dictionary<string, string>)props);
// Console.WriteLine(angajatRepository.Size());
// Meci meci = new Meci(null, 0, 0, DateOnly.MinValue);
// meci.id = 1;
// angajatRepository.Create(new Bilet(meci, "Petre Ispirescu", 7));
// Console.WriteLine(angajatRepository.Size());

Console.WriteLine("HELLO");
