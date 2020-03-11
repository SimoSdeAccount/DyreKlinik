using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace Dyreklinik
{
    class Program
    {
        static void Main(string[] args)
        {
            // OpretPostnummer();
            // OpdaterPostnummer();
            // DeletePostnummer();
            // OpretKunde();
            // UpdateKunde();
            // DeleteKunde();
            // OpretArt();
            // UpdateArt();
            // DeleteArt();
            // OpretKøn();
            // UpdateKøn();
            // DeleteKøn();
            // OpretDyr();
            // OpdaterDyr();
            // DeleteDyr();
            // OpretBehandling();
            // OpdaterBehandling();
            // DeleteBehandling();
            // OpretBehandlingType();
            // UpdateBehandlingType();
            // DeleteBehandlingType();
            // OpretBehandlingBehandlingstype();
            // UpdateBehandlingBehandlingstype();
            // DeleteBehandlingBehandlingstype();
            // faktura();
            // PrintBehandlingsHistorik();
            Console.ReadLine();
        }
        static void PrintBehandlingsHistorik()
        {
            BehandlingsHistorik printHistorik = new BehandlingsHistorik(Con());
            printHistorik.printBehandlingsHistorik();
        }
        static void faktura()
        {
            Faktura nyFaktura = new Faktura(Con());
            nyFaktura.PrintFaktura("benny@email.dk", "2019-07-01");
        }
        static void DeleteBehandlingBehandlingstype()
        {
            BehandlingBehandlingstype deleteBehandlingBehandlingstype = new BehandlingBehandlingstype(Con());
            //Man angiver hvilken sammensætning af behandlingid samt behandlingtype man vil overskrive, og indsætter den som argument
            List<string> overskrivBetingelser = new List<string> { "5", "'Vaccination'" };
            deleteBehandlingBehandlingstype.Delete(overskrivBetingelser);
        }
        static void UpdateBehandlingBehandlingstype()
        {
            BehandlingBehandlingstype updateBehandlingBehandlingstype = new BehandlingBehandlingstype(Con());
            //Getsetteren giver værdien man vil ændre til
            updateBehandlingBehandlingstype.GetSetBehandlingId = 4;
            //Første argument er værdien man vil overskrive
            List<string> derplist = new List<string> { "BehandlingId" };
            //Andet argument i updatebehandlingbehandlingstype er den række man vil gå ind og overskrive.
            List<string> kagederp = new List<string> { "3", "'Kastrering'" };
            updateBehandlingBehandlingstype.Update(derplist, kagederp);
        }
        static void OpretBehandlingBehandlingstype()
        {
            BehandlingBehandlingstype opretBehandlingBehandlingstype = new BehandlingBehandlingstype(Con());
            opretBehandlingBehandlingstype.GetSetBehandlingId = 5;
            opretBehandlingBehandlingstype.GetSetBehandlingsType = "Vaccination";
            opretBehandlingBehandlingstype.Insert();
        }
        static void DeleteBehandlingType()
        {
            BehandlingsType behandlingsType = new BehandlingsType(Con());
            behandlingsType.GetSetBehandlingsType = "'Syning'";
            behandlingsType.Delete();
        }
        static void UpdateBehandlingType()
        {
            BehandlingsType updateBehandlingsType = new BehandlingsType(Con());
            //updateBehandlingsType.GetSetPris = 5300;
            updateBehandlingsType.GetSetBehandlingsType = "'Amputering'";
            List<string> kolonner = new List<string> { "Behandlingtype" };
            updateBehandlingsType.Update(kolonner, "Syning");
        }
        static void OpretBehandlingType()
        {
            BehandlingsType opretBehandlingsType = new BehandlingsType(Con());
            opretBehandlingsType.GetSetBehandlingsType = "ÅrligtTjek";
            opretBehandlingsType.GetSetPris = 2000;
            opretBehandlingsType.Insert();
        }
        static void DeleteBehandling()
        {
            Behandling deleteBehandling = new Behandling(Con());
            deleteBehandling.GetSetId = 1;
            deleteBehandling.Delete();
        }
        static void OpdaterBehandling()
        {
            Behandling opdaterBehandling = new Behandling(Con());
            opdaterBehandling.GetSetDyrId = 2;
            opdaterBehandling.GetSetTid = "08:45:00";
            opdaterBehandling.GetSetId = 2;
            List<string> kolonner = new List<string> {"Tid", "DyrId" };
            opdaterBehandling.Update(kolonner);

        }
        static void OpretBehandling()
        {
            Behandling opretBehandling = new Behandling(Con());
            opretBehandling.GetSetDato = "01-02-2019";
            opretBehandling.GetSetTid = "10:30:00";
            opretBehandling.GetSetDyrId = 1;
            opretBehandling.Insert();
        }
        static void DeleteDyr()
        {
            Dyr deleteDyr = new Dyr(Con());
            deleteDyr.GetSetId = 5;
            deleteDyr.delete();
        }
        static void OpdaterDyr()
        {
            Dyr opdaterDyr = new Dyr(Con());
            opdaterDyr.GetSetAlder = 11;
            opdaterDyr.GetSetNavn = "pussy";
            opdaterDyr.GetSetId = 4;
            List<string> kolonner = new List<string> {"Navn", "Alder"};
            opdaterDyr.update(kolonner);
        }
        static void OpretDyr()
        {
            Dyr opretDyr = new Dyr(Con());
            opretDyr.GetSetNavn = "Rollo";
            opretDyr.GetSetAlder = 6;
            opretDyr.GetSetKønId = 1;
            opretDyr.GetSetEjerid = 1;
            opretDyr.GetSetArtId = 1;
            opretDyr.insert();
        }
        static void DeleteKøn()
        {
            Køn deleteKøn = new Køn(Con());
            deleteKøn.GetSetId = 4;
            deleteKøn.Delete();
        }
        static void UpdateKøn()
        {
            Køn updateKøn = new Køn(Con());
            updateKøn.GetSetNavn = "Andet";
            updateKøn.GetSetId = 3;
            updateKøn.Update();
        }
        static void OpretKøn()
        {
            Køn opretKøn = new Køn(Con());
            opretKøn.GetSetNavn = "Hun";
            opretKøn.Insert();
        }
        static void DeleteArt()
        {
            Art deleteArt = new Art(Con());
            deleteArt.GetSetId = 3;
            deleteArt.Delete();
        }
        static void UpdateArt()
        {
            Art updateArt = new Art(Con());
            updateArt.GetSetId = 3;
            updateArt.GetSetNavn = "Zebra";
            updateArt.Update();
        }
        static void OpretArt()
        {
            Art opretArt = new Art(Con());
            opretArt.GetSetNavn = "Kat";
            opretArt.Insert();
        }
        static void DeleteKunde()
        {
            Kunder deleteKunde = new Kunder(Con());
            deleteKunde.GetSetId = 1003;
            deleteKunde.Delete();
        }
        static void UpdateKunde()
        {
            Kunder updateKunde = new Kunder(Con());
            updateKunde.GetSetNavn = "Sofie";
            updateKunde.GetSetVej = "Sofievej 2";
            updateKunde.GetSetEmail = "Sofie@email.dk";
            updateKunde.GetSetId = 1004;
            //Kolonner der skal opdateres sættes ind i kundens update funktion som argument
            List<string> kolonner = new List<string> { "Navn", "Vej", "email" };
            updateKunde.Update(kolonner);
        }
        static void OpretKunde()
        {
            Kunder nyKunde = new Kunder(Con());
            nyKunde.GetSetNavn = "Sofie";
            nyKunde.GetSetAlder = 22;
            nyKunde.GetSetVej = "Sofievej 2";
            nyKunde.GetSetTelefon = "11223344";
            nyKunde.GetSetEmail = "sofie@mail.dk";
            nyKunde.GetSetPostNummer = "8000";
            Console.WriteLine(nyKunde.Insert());
        }
        static void OpretPostnummer()
        {
            PostNummer PostNummer = new PostNummer(Con());
            PostNummer.GetSetPostNummer = "8000";
            PostNummer.GetSetBy = "Århus";
            Console.WriteLine(PostNummer.Insert());
        }
        static void OpdaterPostnummer()
        {
            PostNummer PostNummer = new PostNummer(Con());
            PostNummer.GetSetPostNummer = "1234";
            PostNummer.GetSetBy = "SvendbentBy";
            PostNummer.Update();
        }
        static void DeletePostnummer()
        {
            PostNummer PostNummer = new PostNummer(Con());
            PostNummer.GetSetPostNummer = "1234";
            PostNummer.Delete();
        }
        static SqlConnection Con()
        {
            SqlConnectionStringBuilder conString = new SqlConnectionStringBuilder()
            {
                InitialCatalog = "DyreklinikDB",
                UserID = "SimonHC",
                Password = "1234",
                DataSource = "Localhost"
            };
            return new SqlConnection(conString.ConnectionString);
        }
    }
}
