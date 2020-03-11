using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dyreklinik
{
    class KolonneSelektor
    {
        /*Øvrig note: Grunden til disse to meget ens funktioner er delt i 2 og ikke lagt sammen i én funktion
          er fordi der skal bruges to seperate lister til at sende gennem databinding fra en tabelklasse. (kolonner og værdier).
          en anden løsning vil være at sætte dem sammen og returnere en liste over listerne. Men indtil videre fungere det med 2 seperate funktioner.
         */
        protected List<string> GetUpdateKolonner(List<string> valgtKolonner, List<string> muligeKolonner)
        {
            //Denne metode fungere som en validator. Såfremt en valgt kolonne matcher med en mulig kolonne, sættes den i liste over kolonner der skal odpateres
            List<string> UpdateKolonner = new List<string>();
            //Der loopes igennem valgte kolonner
            for (int i = 0; i < valgtKolonner.Count; i++)
            {
                //For hver valgt kolonne loopes der igennem mulige kolonner
                for (int j = 0; j < muligeKolonner.Count; j++)
                {
                    //Hvis der opstår et match lagres den valgte kolonne som en af de kolonner der skal opdateres
                    if (valgtKolonner[i] == muligeKolonner[j])
                    {
                        UpdateKolonner.Add(valgtKolonner[i]);
                    }
                }
            }
            return UpdateKolonner;
        }
        protected List<object> GetUpdateVærdier(List<string> valgteKolonner, List<string> muligeKolonner, List<object> muligeVærdier)
        {
            //Denne metode finder værdier der er sat på basis af hvilke kolonner der er valgt (Hvis værdier der tilhøre kolonner ikke er sat vil programmet crashe, dette kan der korigeres for senere)
            List<object> værdier = new List<object>();
            //Der loopes igennem valgte kolonner
            for (int i = 0; i < valgteKolonner.Count; i++)
            {
                //For hver valgt kolonne loopes igennem mulige kolonner
                for (int j = 0; j < muligeKolonner.Count; j++)
                {
                    //Når der er et match vil den getset værdi der ligger parallelt med den mulige kolonne blive lagt i værdier.
                    if (valgteKolonner[i] == muligeKolonner[j])
                    {
                        værdier.Add(muligeVærdier[j]);
                    }
                }
            }
            return værdier;
        }
    }
}