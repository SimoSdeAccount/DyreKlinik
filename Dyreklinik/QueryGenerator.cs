using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dyreklinik
{
    class QueryGenerator
    {
        protected string GenerateInsertStatement(string insertTabel, List<string> insertKolonner, string returnKolonne)
        {
            //Denne funktion laver et insert statement med strengkonkatinering hvor der indsættes i tabel der modtages som parameter og der indsættes i kolonnerne angivet i insertKolonner
            //returnKolonne angiver hvilken kolonne fra ens insert man vil have returneret ved eksekvering.

            //Første sektion af insert statement laves.
            string insertQuery = "INSERT INTO " + insertTabel + " (";
            //Strengen konkatineres med kolonnenavne gennem nedenstående loop.
            for (int i = 0; i < insertKolonner.Count; i++)
            {
                if (i != insertKolonner.Count - 1)
                {
                    //såfremt vi ikke er i sidste iteration, sepereres kolonner med komma
                    insertQuery += insertKolonner[i] + ", ";
                }
                else
                {
                    //Såfremt vi er i sidste iteration er der ikke noget komma til sidst.
                    insertQuery += insertKolonner[i];
                }
            }
            //Anden sektion af insert statement laves. Den sættes til at returnere den kolonne der er sat som returnkolonne argument
            insertQuery = insertQuery + ") OUTPUT INSERTED."+returnKolonne+" VALUES (";
            //Da der anvendes parameterized query sættes værdier der skal indsætte til at være kolonnenavne med snabel-a forand.
            for (int i = 0; i < insertKolonner.Count; i++)
            {
                if (i != insertKolonner.Count - 1)
                {
                    insertQuery += "@" + insertKolonner[i] + ", ";
                }
                else
                {
                    insertQuery += "@" + insertKolonner[i];
                }
            }
            //Til sidst laves slutning på insert statement og den genererede query-streng returneres.
            insertQuery = insertQuery + ");";
            return insertQuery;
        }
        protected string GenerateUpdateStatement(string updateTabel, List<string> updateKolonner, string betingelsesKolonne, string betingelse)
        {
            //Denne funktion laver et update statement med strengkonkatinering hvor værdier opdateres i tabel der modtages som parameter og der opdateres i kolonnerne angivet i updateKolonner
            //betingelsesKolonne argumentet er den kolonne hvorved der sættes en betingelse der svare til betingelsen sat i betingelse argumentet, som resten af kolonnerne opdateres ud fra

            //Første sektion af update statement laves
            string updateQuery = "UPDATE " + updateTabel + " SET " ;
            //Der loopes igennem kolonner der skal opdateres, så et updatestatement genereres med dem.
            for (int i = 0; i < updateKolonner.Count; i++)
            {
                if (i != updateKolonner.Count - 1)
                {
                    //Såfremt vi ikke er i sidste iteration sættes der komma til sidst for hver kolonne med tilhørende updateværdi (da der anvendes parameterized query er værdien bare kolonnenavnet med snabel-a forand)
                    updateQuery += updateKolonner[i] + "=" + "@" + updateKolonner[i] + ", ";
                }
                else
                {
                    //Såfremt vi er er i sidste iteration sættes der ikke komma efter kolonne med tilhørende updateværdi
                    updateQuery += updateKolonner[i] + "=" + "@" + updateKolonner[i];
                }
            }
            //slutningen af strengen laves med WHERE clause der sætter betingelseskolonne til at være betingelse, og derefter returneres query.
            updateQuery = updateQuery + " WHERE " + betingelsesKolonne + " = " + betingelse;
            return updateQuery;
        }
        protected string GenerateUpdateStatement(string updateTabel, List<string> updateKolonner, List<string> betingelsesKolonner, List<string> betingelser)
        {
            //Denne funktion laver et update statement med strengkonkatinering hvor værdier opdateres i tabel der modtages som parameter og der opdateres i kolonnerne angivet i updateKolonner
            //betingelsesKolonner argumentet er de kolonner hvorved der sættes en betingelse der svare til betingelsen sat i betingelser. Betingelseskolonner og tilhørende betingelser skal ligge på samme indexer i hver sin liste.

            //Første sektion af update statement laves
            string updateQuery = "UPDATE " + updateTabel + " SET ";
            //Der loopes igennem kolonner der skal opdateres, så et updatestatement genereres med dem.
            for (int i = 0; i < updateKolonner.Count; i++)
            {
                if (i != updateKolonner.Count - 1)
                {
                    //Såfremt vi ikke er i sidste iteration sættes der komma til sidst for hver kolonne med tilhørende updateværdi (da der anvendes parameterized query er værdien bare kolonnenavnet med snabel-a forand)
                    updateQuery += updateKolonner[i] + "=" + "@" + updateKolonner[i] + ", ";
                }
                else
                {
                    //Såfremt vi er er i sidste iteration sættes der ikke komma efter kolonne med tilhørende updateværdi
                    updateQuery += updateKolonner[i] + "=" + "@" + updateKolonner[i];
                }
            }
            //Anden sektion med where clause i update statement laves.
            updateQuery = updateQuery + " WHERE ";
            //Der loopes igennem betingelseskolonner og hver betingelseskolonne får sat sin betingelse.
            for (int i = 0; i < betingelsesKolonner.Count; i++)
            {
                if(i != betingelsesKolonner.Count - 1)
                {
                    //Såfremt vi ikke er i sidste iteration, sepereres betingelser med et AND. (Kan evt udvides med en der laver OR)
                    updateQuery += betingelsesKolonner[i] + " = " + betingelser[i] + " AND ";
                }
                else
                {
                    updateQuery += betingelsesKolonner[i] + " = " + betingelser[i];
                }
            }
            //Når den er færdig med at generere updateQuery skal ens query returneres.
            return updateQuery;
        }
        protected string GenerateDeleteStatement(string deleteTabel, string betingelsesKolonne, string betingelse)
        {
            //Denne metode sletter fra tabellen med samme navn som deleteTabel hvor en Kolonne sat i betingelseskolonne er lig en betingelse sat i betingelse.
            return "DELETE FROM " + deleteTabel + " WHERE " + betingelsesKolonne + " = " + betingelse;
        }
        protected string GenerateDeleteStatement(string deleteTabel, List<string> betingelsesKolonner, List<string> betingelser)
        {
            //Denne metode sletter fra tabellen med samme navn som deleteTabel og kolonner sat i betingelseskolonner er lig betingelser sat i betingelser
            //Første del af delete query laves
            string deleteQuery = "DELETE FROM " + deleteTabel + " WHERE ";
            //Der loopes igennem betingelseskolonner og for hver kolonne sættes den tilhørende betingelse
            for (int i = 0; i < betingelsesKolonner.Count; i++)
            {
                if (i != betingelsesKolonner.Count - 1)
                {
                    //Såfremt vi ikke er i sidste iteration sepereres betingelser med et AND (kan evt udvides med en der laver OR)
                    deleteQuery += betingelsesKolonner[i] + " = " + betingelser[i] + " AND ";
                }
                else
                {
                    deleteQuery += betingelsesKolonner[i] + " = " + betingelser[i];
                }
            }
            //Når query er lavet returneres den
            return deleteQuery;
        }
    }
}
