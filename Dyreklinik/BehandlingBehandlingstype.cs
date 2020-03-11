using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace Dyreklinik
{
    class BehandlingBehandlingstype : KolonneSelektor
    {
        public BehandlingBehandlingstype(SqlConnection c)
        {
            con = c;
        }
        private SqlConnection con;
        private int behandlingId;
        private string behandlingsType;
        private static List<string> muligeKolonner = new List<string> { "BehandlingId", "Behandlingstype"};
        public int GetSetBehandlingId
        {
            get { return behandlingId; }
            set { behandlingId = value; }
        }
        public string GetSetBehandlingsType
        {
            get { return behandlingsType; }
            set { behandlingsType = value; }
        }
        public string Insert()
        {
            List<object> getSetters = new List<object> { GetSetBehandlingId, GetSetBehandlingsType};
            DataBind insertBind = new DataBind(con);
            return insertBind.Insert("BehandlingBehandlingsType", muligeKolonner, getSetters, "BehandlingId");
        }
        public void Update(List<string> updateKolonner, List<string> betingelser)
        {
            List<string> validUpdateKolonner = GetUpdateKolonner(updateKolonner, muligeKolonner);
            List<object> validUpdateVærdier = GetUpdateVærdier(validUpdateKolonner, muligeKolonner, new List<object> { GetSetBehandlingId, GetSetBehandlingsType });
            DataBind updateBind = new DataBind(con);
            updateBind.Update("BehandlingBehandlingsType", validUpdateKolonner, validUpdateVærdier, muligeKolonner, betingelser);
        }
        public void Delete(List<string> betingelser)
        {
            DataBind deleteBind = new DataBind(con);
            deleteBind.Delete("BehandlingBehandlingsType", muligeKolonner, betingelser);
        }
    }
}
