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
       public int correct;
       public int wrong;
        public int priority;

       /* private string bla;

        public string Bla
        {
            get { return "AA" + bla; }
            set { bla = value; }
        }*/


        public Karte()
        {

        }

        public Karte(int kartenID, string frage, string antwort, int correct, int wrong, int priority)
        {
            this.kartenID = kartenID;
            this.frage = frage;
            this.antwort = antwort;
            this.correct = correct;
            this.wrong = wrong;
            this.priority = priority;
        }
    }
}
