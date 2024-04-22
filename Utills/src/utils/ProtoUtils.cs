
using System;
using System.Collections.Generic;
using Utills.domain;
using Utills.proto;
using MeciL = Utills.domain.MeciL;
using proto=Utills.proto;


namespace Utills.utils
{
    public static class ProtoUtils
    {
        public static proto.Request CreateLoginRequest(Utills.domain.Angajat angajat)
        {
            var ang = new proto.Angajat
            {
                Parola = angajat.Parola,
                Username = angajat.Username
            };
            var request = new proto.Request
            {
                Angajat = ang,
                Type = proto.Request.Types.Type.Login
            };
            return request;
        }

        public static proto.Request CreateLogoutRequest(int idAngajat)
        {
            var request = new proto.Request
            {
                Type = proto.Request.Types.Type.Logout,
                IdAngajat = idAngajat
            };
            return request;
        }

        public static proto.Request CreateGetMeciuriRequest()
        {
            var request = new proto.Request
            {
                Type = proto.Request.Types.Type.GetMeciuri
            };
            return request;
        }

        public static proto.Request CreateGetMeciuriLibereRequest()
        {
            var request = new proto.Request
            {
                Type = proto.Request.Types.Type.GetMeciuriLibere
            };
            return request;
        }

        public static proto.Request CreateCumparaBiletRequest(domain.Bilet bilet)
        {
            var m = bilet.Meci;
            var ld = m.Data;
            var data = new proto.MyDate
            {
                Day = ld.Day,
                Month = ld.Month,
                Year = ld.Year
            };
            var meci = new proto.Meci
            {
                Id = m.id,
                Data = data,
                Capacitate = m.Capacitate,
                Nume = m.Nume,
                PretBilet = m.Pret
            };
            var bil = new proto.Bilet
            {
                Meci = meci,
                NrLocuri = bilet.NrLocuri,
                NumeClient = bilet.NumeClient
            };
            var request = new proto.Request
            {
                Type = proto.Request.Types.Type.CumparaBilet,
                Bilet = bil
            };
            return request;
        }

        public static proto.Request CreateLocuriDisponibileRequest(domain.Meci m)
        {
            var ld = m.Data;
            var data = new proto.MyDate
            {
                Day = ld.Day,
                Month = ld.Month,
                Year = ld.Year
            };
            var meci = new proto.Meci
            {
                Id = m.id,
                Data = data,
                Capacitate = m.Capacitate,
                Nume = m.Nume,
                PretBilet = m.Pret
            };
            var request = new proto.Request
            {
                Type = proto.Request.Types.Type.LocuriDisponibile,
                Meci = meci
            };
            return request;
        }

        public static proto.Response CreateOkResponse()
        {
            var response = new proto.Response
            {
                Type = proto.Response.Types.Type.Ok
            };
            return response;
        }

        public static proto.Response CreateErrorResponse(string mesaj)
        {
            var response = new proto.Response
            {
                Type = proto.Response.Types.Type.Error,
                Message = mesaj
            };
            return response;
        }

        public static proto.Response CreateLoginResponse(int idAngajat)
        {
            var response = new proto.Response
            {
                Type = proto.Response.Types.Type.Ok,
                IdAngajat = idAngajat
            };
            return response;
        }

        public static proto.Response CreateNrLocuriDisponibileMeciResponse(int nrLocuriDosponibile)
        {
            var response = new proto.Response
            {
                Type = proto.Response.Types.Type.LocuriDisponibile,
                NrLocuriDisponibile = nrLocuriDosponibile
            };
            return response;
        }

        public static proto.Response CreateCumparaBiletResponse(domain.Bilet bilet)
        {
            var m = bilet.Meci;
            var ld = m.Data;
            var data = new proto.MyDate
            {
                Day = ld.Day,
                Month = ld.Month,
                Year = ld.Year
            };
            var meci = new proto.Meci
            {
                Id = m.id,
                Data = data,
                Capacitate = m.Capacitate,
                Nume = m.Nume,
                PretBilet = m.Pret
            };
            var bil = new proto.Bilet
            {
                Meci = meci,
                Id = bilet.id,
                NrLocuri = bilet.NrLocuri,
                NumeClient = bilet.NumeClient
            };
            var response = new proto.Response
            {
                Type = proto.Response.Types.Type.Ok,
                Bilet = bil
            };
            return response;
        }

        public static proto.Response CreateGetMeciuriResponse(HashSet<domain.MeciL> meciuri)
        {
            var response = new proto.Response
            {
                Type = proto.Response.Types.Type.Ok
            };

            foreach (var m in meciuri)
            {
                var ld = m.Data;
                var data = new proto.MyDate
                {
                    Day = ld.Day,
                    Month = ld.Month,
                    Year = ld.Year
                };
                var meci = new proto.MeciL
                {
                    Id = m.id,
                    Data = data,
                    NrLocuriDisponibile = m.NrLocuriDisponibile,
                    Capacitate = m.Capacitate,
                    Nume = m.Nume,
                    PretBilet = m.Pret
                };
                response.Meciuri.Add(meci);
            }

            return response;
        }

        public static proto.Response CreateSchimbareMeciuriResponse(HashSet<domain.MeciL> meciuri)
        {
            var response = new proto.Response
            {
                Type = proto.Response.Types.Type.NewMeciuriList
            };

            foreach (var m in meciuri)
            {
                var ld = m.Data;
                var data = new proto.MyDate
                {
                    Day = ld.Day,
                    Month = ld.Month,
                    Year = ld.Year
                };
                var meci = new proto.MeciL
                {
                    Id = m.id,
                    Data = data,
                    NrLocuriDisponibile = m.NrLocuriDisponibile,
                    Capacitate = m.Capacitate,
                    Nume = m.Nume,
                    PretBilet = m.Pret
                };
                response.Meciuri.Add(meci);
            }

            return response;
        }

        public static string GetError(proto.Response response)
        {
            return response.Message;
        }

        public static DateTime GetDate(proto.MyDate myDate)
        {
            return new DateTime(myDate.Year, myDate.Month, myDate.Day);
        }

        public static domain.Meci GetMeci(proto.Meci meci)
        {
            var ld = new DateTime(meci.Data.Year, meci.Data.Month, meci.Data.Day);
            var m = new domain.Meci(meci.Nume, meci.PretBilet, meci.Capacitate, ld);
            m.id = meci.Id;
            return m;
        }

        public static domain.MeciL GetMeciL(proto.MeciL meci)
        {
            var ld = new DateTime(meci.Data.Year, meci.Data.Month, meci.Data.Day);
            var m = new domain.MeciL(meci.Nume, meci.PretBilet, meci.Capacitate, ld, meci.NrLocuriDisponibile);
            m.id = meci.Id;
            return m;
        }

        public static domain.Angajat GetAngajat(proto.Angajat angajat)
        {
            Console.WriteLine(angajat.Parola + "  " + angajat.Username);
            domain.Angajat a = new domain.Angajat(angajat.Parola, angajat.Username);
            return a;
        }

        public static HashSet<domain.MeciL> GetMeciuri(proto.Response response)
        {
            var meciuri = new HashSet<domain.MeciL>();
            foreach (var m in response.Meciuri)
            {
                var ld = new DateTime(m.Data.Year, m.Data.Month, m.Data.Day);
                var meci = new domain.MeciL(m.Nume, m.PretBilet, m.Capacitate, ld, m.NrLocuriDisponibile);
                meci.id = m.Id;
                meciuri.Add(meci);
            }
            return meciuri;
        }

        public static domain.Bilet GetBilet(proto.Bilet bilet)
        {
            var meci = GetMeci(bilet.Meci);
            var bilet_nou = new domain.Bilet(meci, bilet.NumeClient, bilet.NrLocuri);
            bilet_nou.id = bilet.Id;
            return bilet_nou;
        }
    }
}
