using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace Dyreklinik
{
    class DataBind : QueryGenerator
    {
        private SqlConnection con;
        public DataBind(SqlConnection c) {
            //Forbindelse til databasen sættes ved objekt instanciering
            con = c;
        }
        public string Insert(string insertTabel, List<string> insertKolonner, List<object> insertVærdier, string returnKolonne)
        {
            //Denne metode indsætter værdier i en tabel med navn angivet i insertTabel. Der indsættes i kolonner angivet med insertKolonner og de får værdierne insertVærdier
            
            //Der genereres et insertquery på basis af tabelnavn, kolonner, samt hvilken kolonne man vil have returneret.
            string insertQuery = GenerateInsertStatement(insertTabel, insertKolonner, returnKolonne);
            //Den genererede query der skal eksekveres samt forbindelse til databasen loades ind i konstruktoren på et SqlCommand objekt med henblik på eksekvering.
            SqlCommand InsertCmd = new SqlCommand(insertQuery, con);
            //Værdier associeret med kolonner der refereres til i insertQuery sættes på sqlCommand objektet. 
            for (int i = 0; i < insertKolonner.Count; i++)
            {
                InsertCmd.Parameters.AddWithValue("@" + insertKolonner[i], insertVærdier[i]);
            }
            //Der returneres returnKolonne ved eksekvering af insertstatement gennem Executer funktionen.
            return Executer(InsertCmd, false);
        }
        public void Update(string updateTabel, List<string> updateKolonner, List<object> updateVærdier, string BetingelsesKolonne, string Betingelse)
        {
            //Denne metode opdatere værdier i tabel med navn angivet i updateTabel. Der opdateres på de kolonner sat i updateKolonner og disse kolonner får værdier sat med updateVærdier.
            //Den række eller de rækker der skal opdateres identificeres med en betingelseskolonne der skal være lig en betingelse

            //Der genereres et updatequery  på basis af de indsatte argumenter.
            string UpdateQuery = GenerateUpdateStatement(updateTabel, updateKolonner,  BetingelsesKolonne, Betingelse);
            //Den genererede query der skal eksekveres samt forbindelse til databasen loades ind i konstruktoren på et SqlCommand objekt med henblik på eksekvering.
            SqlCommand UpdateCmd = new SqlCommand(UpdateQuery, con);
            //Værdier til kolonner skal skal opdateres, associeres med kolonner der skal opdateres
            for (int i = 0; i < updateKolonner.Count; i++)
            {
                UpdateCmd.Parameters.AddWithValue("@" + updateKolonner[i], updateVærdier[i]);
            }
            //Opdateringen eksekveres gennem executer funktionen.
            Executer(UpdateCmd, true);
        }
        public void Update(string updateTabel, List<string> updateKolonner, List<object> updateVærdier, List<string> BetingelsesKolonner, List<string> Betingelser)
        {
            //Denne metode opdatere værdier i tabel med navn angivet i updateTabel. Der opdateres på de kolonner sat i updateKolonner og disse kolonner får værdier sat med updateVærdier.
            //Den række eller de rækker der skal opdateres identificeres på basis af betingelseskolonner der skal være lig nogle specifikke betingelser

            //Der genereres et updatequery ud fra de indsatte argumenter.
            string UpdateQuery = GenerateUpdateStatement(updateTabel, updateKolonner, BetingelsesKolonner, Betingelser);
            //Den genererede query der skal eksekveres samt forbindelse til databasen loades ind i konstruktoren på et SqlCommand objekt med henblik på eksekvering.
            SqlCommand UpdateCmd = new SqlCommand(UpdateQuery, con);
            //Værdier til kolonner skal skal opdateres, associeres med kolonner der skal opdateres
            for (int i = 0; i < updateKolonner.Count; i++)
            {
                UpdateCmd.Parameters.AddWithValue("@" + updateKolonner[i], updateVærdier[i]);
            }
            //Opdateringen eksekveres gennem executer funktionen.
            Executer(UpdateCmd, true);
        }
        public void Delete(string deleteTabel, string betingelsesKolonne, string betingelse)
        {
            //Denne metode sletter række/rækker i tabel med navn angivet i deleteTabel på basis af betingelseskolonne der skal opfylde betingelse sat gennem betingelse

            //Der genereres en deletequery ud fra de indsatte argumenter
            string deleteQuery = GenerateDeleteStatement(deleteTabel, betingelsesKolonne, betingelse);
            //Den genererede query der skal eksekveres samt forbindelse til databasen loades ind i konstruktoren på et SqlCommand objekt med henblik på eksekvering.
            SqlCommand DeleteCmd = new SqlCommand(deleteQuery, con);
            //Delete operationen eksekveres gennem executer funktionen
            Executer(DeleteCmd, true);
        }
        public void Delete(string Tabel, List<string> BetingelsesKolonner, List<string> Betingelser)
        {
            //Denne metode sletter række/rækker i tabel med navn angivet i deleteTabel på basis af betingelseskolonner der skal opfylde betingelser sat gennem betingelser

            //Der genereres en deletequery ud fra de indsatte argumenter
            string deleteQuery = GenerateDeleteStatement(Tabel, BetingelsesKolonner, Betingelser);
            //Den genererede query der skal eksekveres samt forbindelse til databasen loades ind i konstruktoren på et SqlCommand objekt med henblik på eksekvering.
            SqlCommand DeleteCmd = new SqlCommand(deleteQuery, con);
            //Delete operationen eksekveres gennem executer funktionen
            Executer(DeleteCmd, true);
        }
        private string Executer(SqlCommand cmd, bool Void)
        {
            //Denne funktion eksekvere de forskellige former for sql queries
            string returVærdi = string.Empty;
            //Såfremt void (at der ikke skal returneres nogen værdi), er sat til true, eksekveres sql med executenonquery
            //Ellers anvendes executescalar til at returnere værdi.
            if(Void)
            {
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
            else
            {
                con.Open();
                returVærdi = cmd.ExecuteScalar().ToString();
                con.Close();
            }
            return returVærdi;
        }
    }
}
