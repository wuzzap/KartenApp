using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Data.SQLite.EF6;
using System.Data.SQLite.Linq;

namespace KonsoleSQL
{
    class Program

    {
        
        static int anzStapel;
        static int anzKarten;

        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            VerbindungAufbauen();
            // Console.WriteLine(ZeigeFragen());

           // anzStapel = ZaehleStapel();
            //Console.WriteLine("Es sind " + anzStapel + " Stapel vorhanden");

            //Console.WriteLine("Es gibt insgesamt " + ZaehleKarten() + " Karten");

            //for (int i = 1; i <= anzStapel; i++)
            //{
            //    Console.WriteLine("Stapel " + i + " enthällt " + ZaehleKarten(i) + " Karten");
            //}
           // ZeigeFragen(1);
            VerbindungBeenden();
            Console.Read();
        }
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

        public static void KategorieHinzufuegen()
        {
            int anzStapel = ZaehleStapel();
            string sql = "select StapelID from Karten";
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();
        }

        public static int ZaehleStapel()
        {
            int max = 0;
            //int temp = 0;
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
            return max;
        }



        public static int ZaehleKarten(int stapel)
        {
            int max = 0;
            //var temp;
            string sql = ("select KartenID from Karten where StapelID=="+stapel);
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            SQLiteDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                max += 1;
            }
            return max;
        }


        public static int ZaehleKarten()
        {
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
            return max;
        }

        public static string ZeigeFragen(int StapelID)
        {
            string erg = "";
            string sql = "select * from Karten where StapelID=="+StapelID;
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            SQLiteDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                erg = ("Name: " + reader["Frage"]);
                Console.Write("Frage: " + reader["Frage"]);
                Console.WriteLine("Antwort: " + reader["Antwort"]);
                Console.WriteLine("KartenID: " + reader["KartenID"]);
            }
            return erg;
        }


        public static string ZeigeFragen()
        {
            string erg = "";
            string sql = "select * from Karten where StapelID==1";
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            SQLiteDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                erg = ("Name: " + reader["Frage"]);
                Console.Write("Frage: " + reader["Frage"]);
                Console.WriteLine("Antwort: " + reader["Antwort"]);
            }
            return erg;
        }
    }
}
