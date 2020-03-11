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
            string selectArtDyrQuery = "SELECT Art.Id, Art.Navn As Artnavn, Dyr.Navn AS Dyrnavn FROM Art " +
                "LEFT JOIN Dyr ON Dyr.ArtId = Art.Id ";
            SqlCommand SelectArtDyrCmd = new SqlCommand(selectArtDyrQuery);
            SelectArtDyrCmd.Connection = con;
            con.Open();
            SqlDataReader readArtDyrData = SelectArtDyrCmd.ExecuteReader();
            while (readArtDyrData.Read())
            {
                string id = readArtDyrData["Id"].ToString();
                string artNavn = readArtDyrData["Artnavn"].ToString();
                string dyrNavn = readArtDyrData["Dyrnavn"].ToString();
                Console.WriteLine("Art id: " + id + " Artnavn: " + artNavn + " Dyrnavn: " + dyrNavn);
            }
            readArtDyrData.Close();
            con.Close();
        }
    }
}
