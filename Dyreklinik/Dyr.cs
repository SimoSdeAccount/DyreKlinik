using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
namespace Dyreklinik
{
    class Dyr : KolonneSelektor
    {
        public Dyr(SqlConnection c)
        {
            con = c;
        }
        private SqlConnection con;
        private int id;
        private string navn;
        private int alder;
        private int ejerid;
        private int kønid;
        private int artid;
        private static List<string> Kolonner= new List<string> { "Navn", "Alder", "EjerId", "KønId", "ArtId" };
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
        public int GetSetEjerid
        {
            get { return ejerid; }
            set { ejerid = value; }
        }
        public int GetSetKønId
        {
            get { return kønid; }
            set { kønid = value; }
        }
        public int GetSetArtId
        {
            get { return artid; }
            set { artid = value; }
        }
        public string insert()
        {
            List<object> getSetters = new List<object> { GetSetNavn, GetSetAlder, GetSetEjerid, GetSetKønId, GetSetArtId };
            DataBind insertBind = new DataBind(con);
            return insertBind.Insert("Dyr", Kolonner, getSetters, "Id");
        }
        public void update(List<string> updateKolonner)
        {
            List<string> validUpdateKolonner = GetUpdateKolonner(updateKolonner, Kolonner);
            List<object> validUpdateVærdier = GetUpdateVærdier(validUpdateKolonner, Kolonner, new List<object> { GetSetNavn, GetSetAlder, GetSetEjerid, GetSetKønId, GetSetArtId });
            DataBind updateBind = new DataBind(con);
            updateBind.Update("Dyr", validUpdateKolonner, validUpdateVærdier, "Id", GetSetId.ToString());
        }
        public void delete()
        {
            DataBind deleteBind = new DataBind(con);
            deleteBind.Delete("Dyr", "Id", GetSetId.ToString());
        }
    }
}
