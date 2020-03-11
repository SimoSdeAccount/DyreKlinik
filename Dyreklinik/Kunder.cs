using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
namespace Dyreklinik
{
    class Kunder : KolonneSelektor
    {
        public Kunder(SqlConnection c)
        {
            con = c;
        }
        private SqlConnection con;
        private int id;
        private string navn;
        private int alder;
        private string vej;
        private string telefon;
        private string email;
        private string postnummer;
        private static List<string> muligeKolonner = new List<string> {"Navn", "Alder", "Vej", "Telefon", "email", "Postnummer" };  //Bruges bla til at holde udvalgte update kolonner op imod
        public int GetSetId
        {
            get { return id; }
            set { id = value; }
        }
        public string GetSetNavn
        {
            get { return navn; }
            set { navn = value; }
        }
        public int GetSetAlder
        {
            get { return alder; }
            set { alder = value; }
        }
        public string GetSetVej
        {
            get { return vej; }
            set { vej = value; }
        }
        public string GetSetTelefon
        {
            get { return telefon; }
            set { telefon = value; }
        }
        public string GetSetEmail
        {
            get { return email; }
            set { email = value; }
        }
        public string GetSetPostNummer
        {
            get { return postnummer; }
            set { postnummer = value; }
        }
        public string Insert()
        {
            //Alle de satte værdier lægges i en liste og der laves et nyt databind objekt med henblik på indsætning af data
            List<object> getSetters = new List<object> { GetSetNavn, GetSetAlder, GetSetVej, GetSetTelefon, GetSetEmail, GetSetPostNummer };
            DataBind insertBind = new DataBind(con);
            //Tabel samt Kolonner der skal indsættes i, samt tilhørende værdier til kolonner og retur kolonne sættes i insert funktionen på databind og resultatet
            //returneres tilbage til hovedprogrammet
            return insertBind.Insert("Kunder", muligeKolonner, getSetters, "Id");
        }
        public void Update(List<string> updateKolonner)
        {
            //Der laves en liste over muligt satte værdier og kolonner der skal opdateres og deres tilhørende værdier lagres i hver deres liste
            //Dette sker med validering og indsætning af værdier gennem kolonneselektor klassen
            List<object> muligeVærdier = new List<object> { GetSetNavn, GetSetAlder, GetSetVej, GetSetTelefon, GetSetEmail, GetSetPostNummer };
            List<string> validUpdateKolonner = GetUpdateKolonner(updateKolonner, muligeKolonner);
            List<object> validUpdateVærdier = GetUpdateVærdier(validUpdateKolonner, muligeKolonner, muligeVærdier);
            //Der laves et databind objekt der får forbindelsen modtaget fra main programmet og dens update metode eksekveres med relevante argumenter
            DataBind updateBind = new DataBind(con);
            updateBind.Update("Kunder", validUpdateKolonner, validUpdateVærdier, "Id", GetSetId.ToString());
        }
        public void Delete()
        {
            //Der laves et databind objekt med forbindelsen modtaget fra main programmet og der tilgås delete metoden med relevante argumenter indsat
            //(der skal laves delete på kunde tabellen og det skal være hvor kolonnen Id er sat til et specifikt Id.
            DataBind deleteBind = new DataBind(con);
            deleteBind.Delete("Kunder", "Id", GetSetId.ToString());
        }
    }
}
