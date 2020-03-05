using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace Dyreklinik
{
    class BehandlingsHistorik
    {
        public BehandlingsHistorik(SqlConnection c)
        {
            con = c;
        }
        private SqlConnection con;
    
        public void printBehandlingsHistorik()
        {
            string selectHistorikQuery = "SELECT Dyr.Id, Dyr.Navn AS DyreNavn, Art.Navn AS ArtNavn, Dyr.Alder, Kunder.Navn AS KundeNavn, Behandling.Dato, BehandlingBehandlingsType.Behandlingstype FROM Dyr " +
                "INNER JOIN Art ON Art.Id = Dyr.ArtId " +
                "INNER JOIN Kunder ON Kunder.Id = Dyr.EjerId " +
                "INNER JOIN Behandling ON Behandling.DyrId = Dyr.Id " +
                "INNER JOIN BehandlingBehandlingsType ON BehandlingBehandlingsType.BehandlingId = Behandling.Id;";
            SqlCommand SelectHistorikCmd = new SqlCommand(selectHistorikQuery);
            SelectHistorikCmd.Connection = con;
            con.Open();
            SqlDataReader readHistorikData = SelectHistorikCmd.ExecuteReader();
            while(readHistorikData.Read())
            {
                string Id = readHistorikData["Id"].ToString();
                string DyreNavn = readHistorikData["DyreNavn"].ToString();
                string ArtNavn = readHistorikData["ArtNavn"].ToString();
                string Alder = readHistorikData["Alder"].ToString();
                string KundeNavn = readHistorikData["KundeNavn"].ToString();
                string Dato = readHistorikData["Dato"].ToString();
                string BehandlingsType = readHistorikData["Behandlingstype"].ToString();
                Console.WriteLine(Id + " " + DyreNavn + " " + ArtNavn + " " + Alder + " " + KundeNavn + " " + Dato + " " + BehandlingsType);
            }
            readHistorikData.Close();
            con.Close();
        }
    }
}
