using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Karteikarten_APP
{
   public class Karte
   {
       public int kartenID;
       public string frage;
       public string antwort;

       /* private string bla;

        public string Bla
        {
            get { return "AA" + bla; }
            set { bla = value; }
        }*/


        public Karte()
        {

        }

        public Karte(int kartenID, string frage, string antwort)
        {
            this.kartenID = kartenID;
            this.frage = frage;
            this.antwort = antwort;
        }
    }
}
