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
        private static List<string> muligeKolonner = new List<string> {"Navn", "Alder", "Vej", "Telefon", "email", "Postnummer" };
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
            List<object> getSetters = new List<object> { GetSetNavn, GetSetAlder, GetSetVej, GetSetTelefon, GetSetEmail, GetSetPostNummer };
            DataBind insertBind = new DataBind(con);
            return insertBind.Insert("Kunder", muligeKolonner, getSetters, "Id");
        }
        public void Update(List<string> updateKolonner)
        {
            List<object> muligeVærdier = new List<object> { GetSetNavn, GetSetAlder, GetSetVej, GetSetTelefon, GetSetEmail, GetSetPostNummer };
            List<string> validUpdateKolonner = GetUpdateKolonner(updateKolonner, muligeKolonner);
            List<object> validUpdateVærdier = GetUpdateVærdier(validUpdateKolonner, muligeKolonner, muligeVærdier);
            DataBind updateBind = new DataBind(con);
            updateBind.Update("Kunder", validUpdateKolonner, validUpdateVærdier, "Id", GetSetId.ToString());
        }
        public void Delete()
        {
            DataBind deleteBind = new DataBind(con);
            deleteBind.Delete("Kunder", "Id", GetSetId.ToString());
        }
        public void View()
        {
        }
    }
}
