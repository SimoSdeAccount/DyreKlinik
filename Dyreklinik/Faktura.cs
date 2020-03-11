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
            //Forbindelse der skal anvendes modtages ved objekt instanciering
            con = c;
        }
        private SqlConnection con;
        public void PrintFaktura(string mail, string dato)
        {
            //Der laves et SQL query som tager fat i udvalgt kundeinformation, blandt andet id med henblik på at vælge dyr tilhørende kunde
            string selectKundeQuery = "SELECT Kunder.Id, Kunder.Navn AS Kundenavn, Vej, " +
                "Kunder.Postnummer, PostNummer.Bynavn FROM Kunder " +
                "INNER JOIN PostNummer ON PostNummer.Postnummer = Kunder.Postnummer " +
                "WHERE Kunder.Email = '" + mail + "';";
            //Der laves en sqlcommand der modtager sql query i sin constructor med henblik på at blive læst
            SqlCommand SelectKundeCmd = new SqlCommand(selectKundeQuery);
            //Forbindelsen sqlcommand objektet skal benytte sig af, sættes til at være den forbindelse der kom ind ved instanciering af objektet
            SelectKundeCmd.Connection = con;
            //Der åbnes for forbindelsen og der laves en SqlDataReader variabel som modtager sin read værdier fra sqlcommand objektets ExecuteReader funktion
            con.Open();
            SqlDataReader readKundeData = SelectKundeCmd.ExecuteReader();
            //Der påbegyndes læsning af kundeværdier til variable med  henblik på udskrift af kundedata
            readKundeData.Read();
            string KundeId = readKundeData["Id"].ToString();
            string KundeNavn = readKundeData["Kundenavn"].ToString();
            string Vej = readKundeData["Vej"].ToString();
            string Postnummer = readKundeData["Postnummer"].ToString();
            string Bynavn = readKundeData["Bynavn"].ToString();
            //Læsning stoppes og forbindelsen lukkes
            readKundeData.Close();
            con.Close();
            //Kundedata udprintes
            Console.WriteLine(KundeNavn);
            Console.WriteLine(Vej);
            Console.WriteLine(Postnummer);
            Console.WriteLine(Bynavn);
            //Der instancieres en streng med henblik på concatinering af en række data relateret til dyr associeret med kunde og behandling
            string fakturaLine = string.Empty;
            //Der instancieres en liste til fakturaLines
            List<string> dyrData = new List<string>();
            //Der laves sql query til udtrækning af relevant faktura data på basis af valgt kunde samt dato.
            string selectKundeDyrQuery = "SELECT Dyr.Navn AS Dyrnavn, Behandling.Dato, Behandling.Tid, BehandlingBehandlingsType.BehandlingId, BehandlingBehandlingsType.Behandlingstype, BehandlingsType.Pris FROM Dyr " +
               "INNER JOIN Kunder ON Kunder.Id = Dyr.EjerId " +
               "INNER JOIN Behandling ON Behandling.DyrId = Dyr.Id " +
               "INNER JOIN BehandlingBehandlingsType ON BehandlingBehandlingsType.BehandlingId = Behandling.Id " +
               "INNER JOIN BehandlingsType ON Behandlingstype.Behandlingtype = BehandlingBehandlingsType.Behandlingstype " +
               "WHERE Kunder.Id = " + KundeId +" AND Behandling.Dato = '" + dato + "';";
            //Der laves en sqlcommand der modtager sql query i sin constructor med henblik på at blive læst
            SqlCommand SelectKundeDyrCmd = new SqlCommand(selectKundeDyrQuery);
            //Forbindelsen sqlcommand objektet skal benytte sig af, sættes til at være den forbindelse der kom ind ved instanciering af objektet
            SelectKundeDyrCmd.Connection = con;
            //Der åbnes for forbindelsen og der laves en SqlDataReader variabel som modtager sin read værdier fra sqlcommand objektets ExecuteReader funktion
            con.Open();
            SqlDataReader readDyrData = SelectKundeDyrCmd.ExecuteReader();
            //Så længe der er data der kan indlæses, skal denne data indlæses
            while (readDyrData.Read())
            {
                //Relevant data for dyr der skal behandles lægges sammen i en streng som indsætte i liste over data til udprintning
                fakturaLine = "Navn: " + readDyrData["Dyrnavn"].ToString() +
                      " Tid: " + readDyrData["Tid"].ToString() +
                      " Behandlingtype: " + readDyrData["Behandlingstype"].ToString() +
                      " Pris: " + readDyrData["Pris"].ToString();
                dyrData.Add(fakturaLine);
            }
            //læsning stoppes og forbindelsen lukkes
            readDyrData.Close();
            con.Close();
            //Der udskrives dato for behandlingen og alle rækker med data vedr dyr samt behandling udskrives.
            Console.WriteLine("Dato: " + dato);
            for (int i = 0; i < dyrData.Count; i++)
            {
                Console.WriteLine(dyrData[i]);
            }
        }
    }
}
