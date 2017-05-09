using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Data.SQLite.EF6;
using System.Data.SQLite.Linq;

namespace Karteikarten_APP
{
    public class sqlkram
    {
        public static SQLiteConnection m_dbConnection;
        public static void VerbindungAufbauen()
        {
            m_dbConnection = new SQLiteConnection("Data Source=KarteiDB.sqlite;Version=3;");
            m_dbConnection.Open();
        }

        public static void VerbindungBeenden()
        {
            m_dbConnection.Close();
        }

        public int ZaehleStapel()
        {
            VerbindungAufbauen();
            int max = 0;
            string sql = "select StapelID from Karten";
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            SQLiteDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {

                var temp = Convert.ChangeType(reader["StapelID"], typeof(int)) as int?;
                if (temp > max)
                    max = (int)temp;
                //Console.WriteLine(temp);
            }
            VerbindungBeenden();
            return max;
        }

        public int ZaehleStapel(int StapelID)
        {

            VerbindungAufbauen();
            int max = 0;
            string sql = "select StapelID from Kategorien where UeberID=="+StapelID;
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            SQLiteDataReader reader = command.ExecuteReader();
            Console.WriteLine("Zaehle Stapel begonnen");
            while (reader.Read())
            {
                max += 1;
                Console.WriteLine("Zaehle Stapel "+max+"mal durchgelaufen");
            }
            Console.WriteLine("Zaehle Stapel hat nichts gefunden");
            VerbindungBeenden();
            return max;
        }



        public static void erstelleKarten()
        {
            int max = 0;
            //var temp;
            string sql = "select KartenID from Karten ";
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            SQLiteDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                max += 1;
            }
           
        }

        public int ZaehleKarten()
        {
            VerbindungAufbauen();
            int max = 0;
            //var temp;
            string sql = "select KartenID from Karten";
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            SQLiteDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {

                var temp = Convert.ChangeType(reader["KartenID"], typeof(int)) as int?;
                max = (int)temp;
                //Console.WriteLine(temp);
            }
            VerbindungBeenden();
            
            return max;
        }

        public int ZaehleKarten(int StapelID)
        {
            VerbindungAufbauen();
            int max = 0;
            //var temp;
            string sql = "select KartenID from Karten where StapelID=="+StapelID;
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            SQLiteDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {

                var temp = Convert.ChangeType(reader["KartenID"], typeof(int)) as int?;
                max = (int)temp;

            }
            VerbindungBeenden();

            return max;
        }



        public List<Karte> ErstelleKarten(int StapelID)
        {
            VerbindungAufbauen();
            List<Karte> karten = new List<Karte>();
            string erg = "";
            string sql = "select * from Karten where StapelID==" + StapelID;
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            SQLiteDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                erg = ("Name: " + reader["Frage"]);
                string frage=("Frage: " + reader["Frage"]);
                string antwort=("Antwort: " + reader["Antwort"]);
                var temp = Convert.ChangeType(reader["KartenID"], typeof(int)) as int?;
                int KartenID = (int)temp;
                karten.Add(new Karte(KartenID, frage, antwort));
            }
            VerbindungBeenden();
            return karten;
        }

        
        public static string ZeigeFragen()
        {
            VerbindungAufbauen();
            string erg = "";
            string sql = "select * from Karten where StapelID==1";
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            SQLiteDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                erg = ("Name: " + reader["Frage"]);
                Console.WriteLine("Name: " + reader["Frage"]);
            }
            VerbindungBeenden();
            return erg;
        }

        public Boolean hatUeber(int StapelID)
        {
            bool temp=false;
            VerbindungAufbauen();
            string erg = "";
            string sql = "select HatUeber from Kategorien where StapelID=="+StapelID;
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            SQLiteDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                temp = (bool)reader["HatUeber"];

            }
            VerbindungBeenden();
            return temp;
        }

        public Boolean HatUnter(int StapelID)
        {
            bool temp = false;
            VerbindungAufbauen();
            string erg = "";
            string sql = "select HatUnter from Kategorien where StapelID==" + StapelID;
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            SQLiteDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                temp = (bool)reader["HatUnter"];

            }
            VerbindungBeenden();
            return temp;
        }

        public string zeigeKategorie(int StapelID)
        {
            VerbindungAufbauen();
            string erg = "";
            string sql = "select Kategorie from Karten where StapelID=="+(StapelID+1);
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            SQLiteDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                erg = (string)reader["Kategorie"];
            }

            VerbindungBeenden();
            return erg;
        }

        public string zeigeUnterKategorien(int StapelID)
        {
            VerbindungAufbauen();
            string erg = "";
            string sql = "select Kategorie from Kategorien where UeberID=="+(StapelID);
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            SQLiteDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                erg = (string)reader["Kategorie"];
            }

            VerbindungBeenden();
            return erg;
        }

        public void ErstelleKategorie()
        {
            VerbindungAufbauen();
            string erg = "";
            string sql = "INSERT INTO Kategorien(Kategorie,UeberID, HatUnter,HatUeber) VALUES('Kategorie','UeberID', 'false', 'false')";
                      

            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            SQLiteDataReader reader = command.ExecuteReader();
            command.ExecuteNonQuery();
            //command.ExecuteReader();
            VerbindungBeenden();
        }
    }
}
