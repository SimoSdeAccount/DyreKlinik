using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace Dyreklinik
{
    class Behandling : KolonneSelektor
    {
        public Behandling(SqlConnection c)
        {
            con = c;
        }
        private SqlConnection con;
        private int id;
        private string dato;
        private string tid;
        private int dyrid;
        private static List<string> Kolonner = new List<string> { "Dato", "Tid", "DyrId" };
        public int GetSetId
        {
            get { return id; }
            set { id = value; }
        }
        public string GetSetDato
        {
            get { return dato; }
            set { dato = value; }
        }
        public string GetSetTid
        {
            get { return tid; }
            set { tid = value; }
        }
        public int GetSetDyrId
        {
            get { return dyrid; }
            set { dyrid = value; }
        }
        public string Insert()
        {
            List<object> getSetters = new List<object> { GetSetDato, GetSetTid, GetSetDyrId};
            DataBind insertBind = new DataBind(con);
            return insertBind.Insert("Behandling", Kolonner, getSetters, "Id");
        }
        public void Update(List<string> kolonner)
        {
            List<string> UpdateKolonner = GetUpdateKolonner(kolonner, Kolonner);
            List<object> UpdateGetSetters = GetUpdateVærdier(kolonner, Kolonner, new List<object> { GetSetDato, GetSetTid, GetSetDyrId });
            DataBind updateBind = new DataBind(con);
            updateBind.Update("Behandling", UpdateKolonner, UpdateGetSetters, "Id", GetSetId.ToString());
        }
        public void Delete()
        {
            DataBind deleteBind = new DataBind(con);
            deleteBind.Delete("Behandling", "Id", GetSetId.ToString());
        }
    }
}
