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
            //Forbindelse der skal anvendes modtages ved objekt instanciering
            con = c;
        }
        private SqlConnection con;
    
        public void printBehandlingsHistorik(string startDato, string slutDato)
        {
            //Der laves sql query der viser relevant data relateret til historik over behandling af dyr.
            string selectHistorikQuery = "SELECT Dyr.Id, Dyr.Navn AS DyreNavn, Art.Navn AS ArtNavn, Dyr.Alder, Kunder.Navn AS KundeNavn, Behandling.Dato, BehandlingBehandlingsType.Behandlingstype FROM Dyr " +
                "INNER JOIN Art ON Art.Id = Dyr.ArtId " +
                "INNER JOIN Kunder ON Kunder.Id = Dyr.EjerId " +
                "INNER JOIN Behandling ON Behandling.DyrId = Dyr.Id " +
                "INNER JOIN BehandlingBehandlingsType ON BehandlingBehandlingsType.BehandlingId = Behandling.Id " +
                "WHERE Behandling.Dato BETWEEN '" + startDato + "' AND '" + slutDato + "';";
            //Der laves et sqlcommand objekt som modtager ovenstående sql query i sin construktor, og forbindelsen sættes til at være den modtagede forbindelse med objekt instanciering
            SqlCommand SelectHistorikCmd = new SqlCommand(selectHistorikQuery);
            SelectHistorikCmd.Connection = con;
            //Der åbnes for forbindelsen og der laves en sqldatareader variabal som modtager sine readværdier fra executereader funktionen i sqlcommand klassen
            con.Open();
            SqlDataReader readHistorikData = SelectHistorikCmd.ExecuteReader();
            //Så længe der er data der kan læses, lagres disse data i variable som concatineres til en streng over relevant data der udskrives 
            while(readHistorikData.Read())
            {
                string Id = readHistorikData["Id"].ToString();
                string DyreNavn = readHistorikData["DyreNavn"].ToString();
                string ArtNavn = readHistorikData["ArtNavn"].ToString();
                string Alder = readHistorikData["Alder"].ToString();
                string KundeNavn = readHistorikData["KundeNavn"].ToString();
                string Dato = readHistorikData["Dato"].ToString();
                string BehandlingsType = readHistorikData["Behandlingstype"].ToString();
                Console.WriteLine("Dyr id:" + Id + " Navn:" + DyreNavn + " Art:" + ArtNavn + " Alder:" + Alder + " Kundenavn:" + KundeNavn + " Dato:" + Dato + " Behandling:" + BehandlingsType);
            }
            //Læsning stoppes og forbindelses lukkes
            readHistorikData.Close();
            con.Close();
        }
    }
}
