using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace Dyreklinik
{
    class ArtDyr
    {
        public ArtDyr(SqlConnection c)
        {
            con = c;
        }
        private SqlConnection con;
        public void PrintArtDyr() {
            //Query til at vise art samt dyr der tilhøre arter laves.
            string selectArtDyrQuery = "SELECT Art.Id, Art.Navn As Artnavn, Dyr.Navn AS Dyrnavn FROM Art " +
                "LEFT JOIN Dyr ON Dyr.ArtId = Art.Id ";
            //Der laves et sqlcommand objekt der benytter sig af query til at være artdyr, og forbindelsen sættes til at være con
            SqlCommand SelectArtDyrCmd = new SqlCommand(selectArtDyrQuery);
            SelectArtDyrCmd.Connection = con;
            //forbindelsen åbnes og der påbegyndes læsning af data
            con.Open();
            SqlDataReader readArtDyrData = SelectArtDyrCmd.ExecuteReader();
            //Mens der er data der skal læses skal der læses data og dataen skal printes ud
            while (readArtDyrData.Read())
            {
                string id = readArtDyrData["Id"].ToString();
                string artNavn = readArtDyrData["Artnavn"].ToString();
                string dyrNavn = readArtDyrData["Dyrnavn"].ToString();
                Console.WriteLine("Art id: " + id + " Artnavn: " + artNavn + " Dyrnavn: " + dyrNavn);
            }
            //læsning stopper og forbindelsen lukkes
            readArtDyrData.Close();
            con.Close();
        }
    }
}
