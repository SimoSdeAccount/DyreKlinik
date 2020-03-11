using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace Dyreklinik
{
    class BehandlingsType : KolonneSelektor
    {
        public BehandlingsType(SqlConnection c)
        {
            con = c;
        }
        private SqlConnection con;
        private string behandlingsType;
        private int pris;
        public string GetSetBehandlingsType
        {
            get { return behandlingsType; }
            set { behandlingsType = value; }
        }
        public int GetSetPris
        {
            get { return pris; }
            set { pris = value; }
        }
        public string Insert()
        {
            List<string> Kolonner = new List<string> { "Behandlingtype", "Pris" };
            List<object> getSetters = new List<object> { GetSetBehandlingsType, GetSetPris };
            DataBind insertBind = new DataBind(con);
            return insertBind.Insert("BehandlingsType", Kolonner, getSetters, "Behandlingtype");
        }
        public void Update(List<string> updateKolonner, string behandlingsType = null)
        {
            List<string> muligeKolonner = new List<string> {"Behandlingtype", "Pris" };
            List<string> validUpdateKolonner = GetUpdateKolonner(updateKolonner, muligeKolonner);
            List<object> validUpdateVærdier = GetUpdateVærdier(validUpdateKolonner, muligeKolonner, new List<object> {behandlingsType, GetSetPris });
            DataBind updateBind = new DataBind(con);
            updateBind.Update("BehandlingsType", validUpdateKolonner, validUpdateVærdier, "Behandlingtype", GetSetBehandlingsType);
        }
        public void Delete()
        {
            DataBind deleteBind = new DataBind(con);
            deleteBind.Delete("BehandlingsType", "Behandlingtype", GetSetBehandlingsType);
        }
    }
}
