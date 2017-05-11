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



        public void changeIntVars(string category, int var, int kartenID)
        {
            VerbindungAufbauen();
            string sql = "UPDATE Karten set "+"'"+category+"'="+var+" WHERE KartenID=="+kartenID;
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();
            VerbindungBeenden();
        }

        public int count(string field="KartenID", string table = "Karten")
        {
            VerbindungAufbauen();
            int max = 0;
            string sql = "select "+"'"+field+"'"+" from "+"'"+table+"'";
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            SQLiteDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                    max += 1;
            }
            VerbindungBeenden();
            return max;
        }

        public int count(string field = "KartenID", string table = "Karten", string where="StapelID==")
        {
            VerbindungAufbauen();
            int max = 0;
            string sql = "select " + "'" + field + "'" + " from " + "'" + table + "'"+where;
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            SQLiteDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                max += 1;
            }
            VerbindungBeenden();
            return max;
        }

        public int getInt(string sql,string erg)
        {
            VerbindungAufbauen();
            List<int> stapelIds = new List<int>();


            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            SQLiteDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                var temp = Convert.ChangeType(reader[erg], typeof(int)) as int?;
                return (int)temp;
            }
            return 325;
            VerbindungBeenden();
           
        }


        public List<int> getIntVarList(string sql,string erg)
        {
            VerbindungAufbauen();
            List<int> stapelIds = new List<int>();


            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            SQLiteDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                var temp = Convert.ChangeType(reader[erg], typeof(int)) as int?;
                stapelIds.Add((int)temp);
            }

            VerbindungBeenden();
            return stapelIds;
        }

        







      /*  public List<int> HoleStapelIDs(int StapelID)
        {

            VerbindungAufbauen();
            List<int> stapelIds = new List<int>();
            int max = 0;
            string sql = "select StapelID from Kategorien where UeberID==" + StapelID;
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            SQLiteDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                var temp = Convert.ChangeType(reader["StapelID"], typeof(int)) as int?;
                stapelIds.Add((int)temp);
            }

            VerbindungBeenden();
            return stapelIds;
        }*/


        public int ZaehleStapel()
        {
            VerbindungAufbauen();
            int max = 0;
            string sql = "select StapelID from Kategorien";
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            SQLiteDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                max += 1;
            }
            VerbindungBeenden();
            return max;
        }

        public int ZaehleStapel(int StapelID)
        {

            VerbindungAufbauen();
            int max = 0;
            string sql = "select StapelID from Kategorien where UeberID==" + StapelID;
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            SQLiteDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                max += 1;

            }

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

        public int ZaehleKarten(int StapelID)
        {
            VerbindungAufbauen();
            int max = 0;
            //var temp;
            string sql = "select KartenID from Karten where StapelID==" + StapelID;
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            SQLiteDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {

                var temp = Convert.ChangeType(reader["KartenID"], typeof(int)) as int?;
                max += 1;

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
                string frage = ("Frage: " + reader["Frage"]);
                string antwort = ("Antwort: " + reader["Antwort"]);

                var temp = Convert.ChangeType(reader["Korrekt"], typeof(int)) as int?;
                int correct = (int)temp;

                temp = Convert.ChangeType(reader["Falsch"], typeof(int)) as int?;
                int wrong = (int)temp;

                temp = Convert.ChangeType(reader["Prioritaet"], typeof(int)) as int?;
                int prority = (int)temp;

                temp = Convert.ChangeType(reader["KartenID"], typeof(int)) as int?;
                int KartenID = (int)temp;
                karten.Add(new Karte(KartenID, frage, antwort, correct, wrong, prority));
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

        public int getRight(int KartenID)
        {
            VerbindungAufbauen();
            int erg = 0;
            string sql = "select Korrekt from Karten where KartenID==" + KartenID;
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            SQLiteDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                var temp = Convert.ChangeType(reader["Korrekt"], typeof(int)) as int?;
                erg = (int)temp;
            }
            VerbindungBeenden();
            return erg;
        }

        public int getWrong(int KartenID)
        {
            VerbindungAufbauen();
            int erg = 0;
            string sql = "select Falsch from Karten where KartenID==" + KartenID;
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            SQLiteDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                var temp = Convert.ChangeType(reader["Falsch"], typeof(int)) as int?;
                erg = (int)temp;
            }
            VerbindungBeenden();
            return erg;
        }

        public Boolean hatKarten(int StapelID)
        {
            bool temp = false;
            VerbindungAufbauen();
            string erg = "";
            string sql = "select hatKarten from Kategorien where StapelID==" + StapelID;
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            SQLiteDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                temp = (bool)reader["hatKarten"];

            }
            VerbindungBeenden();
            return temp;
        }

        public Boolean hatKarten(string Kategorie)
        {
            bool temp = false;
            VerbindungAufbauen();
            string erg = "";
            string sql = "select hatKarten from Kategorien where Kategorie==" + "'" + Kategorie + "'";
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);

            try
            {
                SQLiteDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    temp = (bool)reader["hatKarten"];

                }
                VerbindungBeenden();
                return temp;
            }
            catch
            {
                temp = false;
            }
            return temp;
        }

        public Boolean nochFrei(string kategorie)
        {
            bool temp = true;
            VerbindungAufbauen();
            string erg = "";

            try
            {
                string sql = "select Kategorie from Kategorien where Kategorie==" + "'" + kategorie + "'";
                SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
                SQLiteDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    string str = (string)reader["Kategorie"];
                    if (str.Equals(kategorie))
                    {
                        temp = false;
                    }
                    temp = false;
                }
                VerbindungBeenden();

            }
            catch
            {

            }


            return temp;
        }

        public Boolean hatUeber(int StapelID)
        {
            bool temp = false;
            VerbindungAufbauen();
            string erg = "";
            string sql = "select HatUeber from Kategorien where StapelID==" + StapelID;
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
            string sql = "select Kategorie from Kategorien where StapelID==" + (StapelID);
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
            string sql = "select Kategorie from Kategorien where UeberID==" + (StapelID);
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            SQLiteDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                erg = (string)reader["Kategorie"];
            }

            VerbindungBeenden();
            return erg;
        }

        public void ErstelleKategorie(string Kategorie, int UeberID, bool HatUeber)
        {
            VerbindungAufbauen();
            string erg = "";
            string sql = "INSERT INTO Kategorien(Kategorie,UeberID, HatUeber) VALUES('" + Kategorie + "','" + UeberID + "', '" + HatUeber + "')";


            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            //SQLiteDataReader reader = command.ExecuteReader();
            command.ExecuteNonQuery();
            //command.ExecuteReader();
            VerbindungBeenden();
        }

        public List<int> HoleAlleStapelIDs()
        {
            List<int> stapelIds = new List<int>();

            VerbindungAufbauen();
            int max = 0;
            //var temp;
            string sql = "select StapelID from Kategorien";
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            SQLiteDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {

                var temp = Convert.ChangeType(reader["StapelID"], typeof(int)) as int?;
                stapelIds.Add((int)temp);

            }
            VerbindungBeenden();
            return stapelIds;
        }



        public List<int> HoleStapelIDs(string kategorie)
        {

            VerbindungAufbauen();
            List<int> stapelIds = new List<int>();
            int max = 0;
            string sql = "select StapelID from Kategorien where Kategorie==" + "'" + kategorie + "'";
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            SQLiteDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                var temp = Convert.ChangeType(reader["StapelID"], typeof(int)) as int?;
                stapelIds.Add((int)temp);
            }

            VerbindungBeenden();
            return stapelIds;
        }

        public int HoleStapelID(string kategorie)
        {

            VerbindungAufbauen();
            int stapelId = 0;
            string sql = "select StapelID from Kategorien where Kategorie==" + "'" + kategorie + "'";
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            SQLiteDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                var temp = Convert.ChangeType(reader["StapelID"], typeof(int)) as int?;
                stapelId = (int)temp;
            }

            VerbindungBeenden();
            return stapelId;
        }



        public static void VerbindungBeenden()
        {
            m_dbConnection.Close();
        }

        public void neueKarte(string frage, string antwort, int StapelID, string Kategorie)
        {
            VerbindungAufbauen();
            string erg = "";
            string sql = "INSERT INTO Karten(Frage,Antwort,StapelID,Kategorie) VALUES('" + frage + "','" + antwort + "','" + StapelID + "','" + Kategorie + "')";
            string sql2 = "UPDATE Kategorien set hatKarten = 'true' WHERE StapelID==" + StapelID;

            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            SQLiteCommand command2 = new SQLiteCommand(sql2, m_dbConnection);
            //SQLiteDataReader reader = command.ExecuteReader();
            command.ExecuteNonQuery();
            command2.ExecuteNonQuery();
            //command.ExecuteReader();
            VerbindungBeenden();
        }

    }
}
