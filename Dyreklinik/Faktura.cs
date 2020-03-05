using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace Dyreklinik
{
    class Faktura
    {
        public Faktura(SqlConnection c)
        {
            con = c;
        }
        private SqlConnection con;
        public void PrintFaktura(string mail, string dato)
        {
            string selectKundeQuery = "SELECT Kunder.Id, Kunder.Navn AS Kundenavn, Vej, " +
                "Kunder.Postnummer, PostNummer.Bynavn FROM Kunder " +
                "INNER JOIN PostNummer ON PostNummer.Postnummer = Kunder.Postnummer " +
                "WHERE Kunder.Email = '" + mail + "';";
            SqlCommand SelectKundeCmd = new SqlCommand(selectKundeQuery);
            SelectKundeCmd.Connection = con;
            con.Open();
            SqlDataReader readKundeData = SelectKundeCmd.ExecuteReader();
            readKundeData.Read();
            string KundeId = readKundeData["Id"].ToString();
            string KundeNavn = readKundeData["Kundenavn"].ToString();
            string Vej = readKundeData["Vej"].ToString();
            string Postnummer = readKundeData["Postnummer"].ToString();
            string Bynavn = readKundeData["Bynavn"].ToString();
            readKundeData.Close();
            con.Close();
            Console.WriteLine(KundeNavn);
            Console.WriteLine(Vej);
            Console.WriteLine(Postnummer);
            Console.WriteLine(Bynavn);
            readKundeData.Close();
            List<string> dyrData = new List<string>();
            string fakturaLine = string.Empty;
            string selectKundeDyrQuery = "SELECT Dyr.Navn AS Dyrnavn, Behandling.Dato, Behandling.Tid, BehandlingBehandlingsType.BehandlingId, BehandlingBehandlingsType.Behandlingstype, BehandlingsType.Pris FROM Dyr " +
               "INNER JOIN Kunder ON Kunder.Id = Dyr.EjerId " +
               "INNER JOIN Behandling ON Behandling.DyrId = Dyr.Id " +
               "INNER JOIN BehandlingBehandlingsType ON BehandlingBehandlingsType.BehandlingId = Behandling.Id " +
               "INNER JOIN BehandlingsType ON Behandlingstype.Behandlingtype = BehandlingBehandlingsType.Behandlingstype " +
               "WHERE Kunder.Id = 1 AND Behandling.Dato = '" + dato + "';";
            SqlCommand SelectKundeDyrCmd = new SqlCommand(selectKundeDyrQuery);
            SelectKundeDyrCmd.Connection = con;
            con.Open();
            SqlDataReader readDyrData = SelectKundeDyrCmd.ExecuteReader();
            while (readDyrData.Read())
            {
                fakturaLine = "Navn: " + readDyrData["Dyrnavn"].ToString() +
                      " Tid: " + readDyrData["Tid"].ToString() +
                      " Behandlingtype: " + readDyrData["Behandlingstype"].ToString() +
                      " Pris: " + readDyrData["Pris"].ToString();
                dyrData.Add(fakturaLine);
            }
            readDyrData.Close();
            con.Close();
            Console.WriteLine("Dato: " + dato);
            for (int i = 0; i < dyrData.Count; i++)
            {
                Console.WriteLine(dyrData[i]);
            }
        }
    }
}
