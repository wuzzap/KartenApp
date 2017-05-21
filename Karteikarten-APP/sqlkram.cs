using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Data.SQLite.EF6;
using System.Data.SQLite.Linq;
// hallo mein freund hier solltest du aufhören ;)
namespace Karteikarten_APP
{
    public class sqlkram
    {

        //basics
        public static SQLiteConnection m_dbConnection;
        public static void VerbindungAufbauen()
        {
            m_dbConnection = new SQLiteConnection("Data Source=KarteiDB.sqlite;Version=3;");
            m_dbConnection.Open();
        }


        // getter



        public string getCategoryName(int StapelID)
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
            reader.Close();

            VerbindungBeenden();
            return erg;
        }

        public List<string> getChildCategoryName(int StapelID)
        {
            List<string> erg = new List<string>();

            VerbindungAufbauen();
            string sql = "select Kategorie from Kategorien where UeberID==" + (StapelID);
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            SQLiteDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                erg.Add((string)reader["Kategorie"]);
            }
            reader.Close();

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
            reader.Close();

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
            reader.Close();

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
            reader.Close();

            VerbindungBeenden();
            return temp;
        }

        public Boolean hatKarten(string Kategorie)
        {
            VerbindungAufbauen();
            bool temp = false;

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
                reader.Close();

                VerbindungBeenden();
                return temp;
            }
            catch
            {
                temp = false;
            }
            VerbindungBeenden();
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
                reader.Close();

                VerbindungBeenden();

            }
            catch
            {
                VerbindungBeenden();
                return temp;
            }

            VerbindungBeenden();
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
            reader.Close();

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
            reader.Close();

            VerbindungBeenden();
            return temp;
        }

        public int getInt(string sql, string erg)
        {
            VerbindungAufbauen();
            List<int> stapelIds = new List<int>();


            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            SQLiteDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                var temp = Convert.ChangeType(reader[erg], typeof(int)) as int?;
                //       VerbindungBeenden();
                reader.Close();
                return (int)temp;
            }
            VerbindungBeenden();
            return 325;


        }
        public long getLong(string sql, string erg)
        {
            VerbindungAufbauen();
            List<int> stapelIds = new List<int>();


            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            SQLiteDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                var temp = Convert.ChangeType(reader[erg], typeof(long)) as long?;
                reader.Close();
                VerbindungBeenden();

                return (long)temp;
            }
            return 325;
            VerbindungBeenden();

        }
        public DateTime get_correctTime(int cardID)
        {
            string sql = "select correctTime from Karten where KartenID=" + cardID;
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            SQLiteDataReader reader = command.ExecuteReader();
            long dt = getLong(sql, "correctTime");
            DateTime dtx = new DateTime(dt);
            return dtx;
        }

        public DateTime get_wrongTime(int cardID)
        {
            string sql = "select wrongTime from Karten where KartenID=" + cardID;
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            SQLiteDataReader reader = command.ExecuteReader();
            long dt = getLong(sql, "wrongTime");
            DateTime dtx = new DateTime(dt);
            return dtx;
        }





        public List<int> getIntVarList(string sql, string erg)
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
            reader.Close();

            VerbindungBeenden();
            return stapelIds;
        }

        public TimeSpan getDuration(int KartenID)
        {
            TimeSpan dur = new TimeSpan();
            long dauer = 0;
            string sql = "select dauer from Karten where KartenID==" + KartenID;
            dauer = getLong(sql, "dauer");
            dur = new TimeSpan(dauer);
            return dur;

        }

        //setter

        public void setHatUnter(int StapelID)
        {
            VerbindungAufbauen();

            string sql = "UPDATE Kategorien set hatUnter = 'true' WHERE StapelID==" + StapelID + "";

            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);

            command.ExecuteNonQuery();

            VerbindungBeenden();
        }

        public void changeIntVars(string category, int var, int kartenID)
        {
            VerbindungAufbauen();
            string sql = "UPDATE Karten set "+"'"+category+"'="+var+" WHERE KartenID="+kartenID;
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();
           // System.Threading.Thread.Sleep(5000);
            VerbindungBeenden();
        }

        public void changeCategoryTime(int ID)
        {
            VerbindungAufbauen();
            DateTime dt = DateTime.Now;
            long dtx = dt.Ticks;
            string sql = "UPDATE Kategorien set time= " + dtx + " WHERE StapelID== " + ID;
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();
            VerbindungBeenden();
        }



        public void changeDuration(int KartenID,long stamp)
        {
            VerbindungAufbauen();
            string sql = "UPDATE Karten set dauer= '" + stamp + "' WHERE KartenID == " + KartenID;
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();
            VerbindungBeenden();
        }

        public void changeCorrectTime(int ID)
        {
            VerbindungAufbauen();
            DateTime dtc = DateTime.Now;
            long dtxc = dtc.Ticks;
            
            string sql = "UPDATE Karten set correctTime= " + dtxc + " WHERE KartenID == " + ID;
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();
            VerbindungBeenden();
        }

        public void changeWrongTime(int ID)
        {
            VerbindungAufbauen();
            DateTime dtw = DateTime.Now;
            long dtxw = dtw.Ticks;

            string sql = "UPDATE Karten set wrongTime= " + dtxw + " WHERE KartenID == " + ID;
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();
            VerbindungBeenden();
        }


        //counter

        public int countAllCards(int UeberID,string field = "KartenID", string table = "Karten")
        {
            VerbindungAufbauen();
            int max = 0;
            string sql = "select StapelID from Kategorien where UeberID==" + UeberID +" AND hatKarten=='true'";
            List<int> stapelIdxs = new List<int>();
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            SQLiteDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                var temp = Convert.ChangeType(reader["StapelID"], typeof(int)) as int?;
                stapelIdxs.Add((int)temp);
            }
            reader.Close();
            VerbindungBeenden();
            VerbindungAufbauen();

            for (int i = 0; i < stapelIdxs.Count(); i++)
            {
                sql = "select " + "'" + field + "'" + " from " + table+" where StapelID="+stapelIdxs[i];
                command = new SQLiteCommand(sql, m_dbConnection);
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    max += 1;
                }
            }
            reader.Close();
            VerbindungBeenden();
            return max;
        }

        public int count(string field = "KartenID", string table = "Karten")
        {
            VerbindungAufbauen();
            int max = 0;
            string sql = "select " + "'" + field + "'" + " from " + "'" + table + "'";
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
            string sql = "select " + "'" + field + "'" + " from " + "'" + table + "'"+"where "+where;
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            SQLiteDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                max += 1;
            }
            reader.Close();
            VerbindungBeenden();
            return max;
        }

       
        
        public int countStacks()
        {

            int erg = count("StapelID", "Kategorien");

            return erg;
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
            reader.Close();

            VerbindungBeenden();
            return max;
        }


        // todo: wiederholungen eliminieren
        public List<Karte> createPriorityCards(int StapelID,int priority)
        {
            VerbindungAufbauen();
            List<Karte> karten = new List<Karte>();
            string erg = "";
            string sql = "select * from Karten where StapelID==" + StapelID+" AND Prioritaet > "+priority;
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            SQLiteDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                erg = ("Name: " + reader["Frage"]);
                string frage = ("Frage: " + reader["Frage"]);
                string antwort = ("Antwort: " + reader["Antwort"]);
                string kategorie = ("Kategorie: " + reader["Kategorie"]);
                var temp = Convert.ChangeType(reader["Korrekt"], typeof(int)) as int?;
                int correct = (int)temp;

                temp = Convert.ChangeType(reader["Falsch"], typeof(int)) as int?;
                int wrong = (int)temp;

                temp = Convert.ChangeType(reader["Prioritaet"], typeof(int)) as int?;
                int prority = (int)temp;

                temp = Convert.ChangeType(reader["KartenID"], typeof(int)) as int?;
                int KartenID = (int)temp;

                var var = Convert.ChangeType(reader["correctTime"], typeof(long)) as long?;
                long dt = (long)var;
                DateTime correctTime = new DateTime(dt);

                var = Convert.ChangeType(reader["wrongTime"], typeof(long)) as long?;
                dt = (long)var;
                DateTime wrongTime = new DateTime(dt);

                var = Convert.ChangeType(reader["time"], typeof(long)) as long?;
                dt = (long)var;
                DateTime time = new DateTime(dt);

                var = Convert.ChangeType(reader["dauer"], typeof(long)) as long?;
                dt = (long)var;
                TimeSpan dauer = new TimeSpan(dt);
                karten.Add(new Karte(KartenID, frage, antwort, correct, wrong, prority, kategorie, correctTime, wrongTime, time, dauer));
            }

            VerbindungBeenden();
            return karten;

        }





        // todo: wiederholungen eliminieren
        public List<int> ErstelleKarten(int StapelID)
        {
            List<int> ids = new List<int>();
            VerbindungAufbauen();
            string sql = "select * from Karten where StapelID==" + StapelID;
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            SQLiteDataReader reader = command.ExecuteReader();
            int max = 0;
            while (reader.Read())
            {
                var temp = Convert.ChangeType(reader["KartenID"], typeof(int)) as int?;
                int KartenID = (int)temp;
                ids.Add(KartenID);
            }
            return ids;
        }

        public List<Karte> buildCards(List<int> list)
        {
                       VerbindungAufbauen();
            List<Karte> karten = new List<Karte>();

            for(int i = 0; i < list.Count(); i++)
            {
                string sql = "select * from Karten where StapelID==" + list[i];
                SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
                SQLiteDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    string erg = ("Name: " + reader["Frage"]);
                    string frage = ("Frage: " + reader["Frage"]);
                    string antwort = ("Antwort: " + reader["Antwort"]);
                    string kategorie = ("Kategorie: " + reader["Kategorie"]);
                    var temp = Convert.ChangeType(reader["Korrekt"], typeof(int)) as int?;
                    int correct = (int)temp;

                    temp = Convert.ChangeType(reader["Falsch"], typeof(int)) as int?;
                    int wrong = (int)temp;

                    temp = Convert.ChangeType(reader["Prioritaet"], typeof(int)) as int?;
                    int prority = (int)temp;

                    temp = Convert.ChangeType(reader["KartenID"], typeof(int)) as int?;
                    int KartenID = (int)temp;


                    var var = Convert.ChangeType(reader["correctTime"], typeof(long)) as long?;
                    long dt = (long)var;
                    DateTime correctTime = new DateTime(dt);

                    var = Convert.ChangeType(reader["wrongTime"], typeof(long)) as long?;
                    dt = (long)var;
                    DateTime wrongTime = new DateTime(dt);

                    var = Convert.ChangeType(reader["time"], typeof(long)) as long?;
                    dt = (long)var;
                    DateTime time = new DateTime(dt);

                    var = Convert.ChangeType(reader["dauer"], typeof(long)) as long?;
                    dt = (long)var;
                    TimeSpan dauer = new TimeSpan(dt);
                    karten.Add(new Karte(KartenID, frage, antwort, correct, wrong, prority, kategorie, correctTime, wrongTime, time, dauer));

                }
                reader.Close();
            }

            VerbindungBeenden();
            return karten;
        }
        // todo: wiederholungen eliminieren
        public List<Karte> createRandomCards(int anzAuto)
        {
            VerbindungAufbauen();
            List<int> allIds = new List<int>();
            string sql = "select KartenID from Karten";
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            SQLiteDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                var temp = Convert.ChangeType(reader["KartenID"], typeof(int)) as int?;
                int KartenID = (int)temp;
                allIds.Add(KartenID);
            }
            reader.Close();
            VerbindungBeenden();

            VerbindungAufbauen();
            List<Karte> karten = new List<Karte>();
            Random rnd = new Random();
            for (int i = 0; i < anzAuto; i++)
            {
                sql = "select * from Karten where KartenID==" + allIds[rnd.Next(0, (allIds.Count-1))];
                command = new SQLiteCommand(sql, m_dbConnection);
                reader = command.ExecuteReader();
                while (reader.Read())
                {

                    string frage = ("Frage: " + reader["Frage"]);
                    string antwort = ("Antwort: " + reader["Antwort"]);
                    string kategorie = ("Kategorie: " + reader["Kategorie"]);

                    var temp = Convert.ChangeType(reader["Korrekt"], typeof(int)) as int?;
                    int correct = (int)temp;

                    temp = Convert.ChangeType(reader["Falsch"], typeof(int)) as int?;
                    int wrong = (int)temp;

                    temp = Convert.ChangeType(reader["Prioritaet"], typeof(int)) as int?;
                    int prority = (int)temp;

                    temp = Convert.ChangeType(reader["KartenID"], typeof(int)) as int?;
                    int KartenID = (int)temp;
                    var var = Convert.ChangeType(reader["correctTime"], typeof(long)) as long?;
                    long dt = (long)var;
                    DateTime correctTime = new DateTime(dt);

                    var = Convert.ChangeType(reader["wrongTime"], typeof(long)) as long?;
                    dt = (long)var;
                    DateTime wrongTime = new DateTime(dt);

                    var = Convert.ChangeType(reader["time"], typeof(long)) as long?;
                    dt = (long)var;
                    DateTime time = new DateTime(dt);

                    var = Convert.ChangeType(reader["dauer"], typeof(long)) as long?;
                    dt = (long)var;
                    TimeSpan dauer = new TimeSpan(dt);
                    karten.Add(new Karte(KartenID, frage, antwort, correct, wrong, prority, kategorie, correctTime, wrongTime, time, dauer));
                }
            }
            reader.Close();
            VerbindungBeenden();
            return karten;
        }

        // todo: wiederholungen eliminieren
        public List<Karte> createRandomPrioCards(int anzAuto)
        {
            VerbindungAufbauen();
            List<int> allIds = new List<int>();
            string sql = "select KartenID from Karten where Prioritaet > 5";
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            SQLiteDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                var temp = Convert.ChangeType(reader["KartenID"], typeof(int)) as int?;
                int KartenID = (int)temp;
                allIds.Add(KartenID);
            }
            reader.Close();
            VerbindungBeenden();

            VerbindungAufbauen();
            List<Karte> karten = new List<Karte>();
            Random rnd = new Random();
            for (int i = 0; i < anzAuto; i++)
            {
                sql = "select * from Karten where KartenID==" + allIds[rnd.Next(0, (allIds.Count - 1))];
                command = new SQLiteCommand(sql, m_dbConnection);
                reader = command.ExecuteReader();
                while (reader.Read())
                {

                    string frage = ("Frage: " + reader["Frage"]);
                    string antwort = ("Antwort: " + reader["Antwort"]);
                    string kategorie = ("Kategorie: " + reader["Kategorie"]);

                    var temp = Convert.ChangeType(reader["Korrekt"], typeof(int)) as int?;
                    int correct = (int)temp;

                    temp = Convert.ChangeType(reader["Falsch"], typeof(int)) as int?;
                    int wrong = (int)temp;

                    temp = Convert.ChangeType(reader["Prioritaet"], typeof(int)) as int?;
                    int prority = (int)temp;

                    temp = Convert.ChangeType(reader["KartenID"], typeof(int)) as int?;
                    int KartenID = (int)temp;

                    var var = Convert.ChangeType(reader["correctTime"], typeof(long)) as long?;
                    long dt = (long)var;
                    DateTime correctTime = new DateTime(dt);

                    var = Convert.ChangeType(reader["wrongTime"], typeof(long)) as long?;
                    dt = (long)var;
                    DateTime wrongTime = new DateTime(dt);

                    var = Convert.ChangeType(reader["time"], typeof(long)) as long?;
                    dt = (long)var;
                    DateTime time = new DateTime(dt);

                    var = Convert.ChangeType(reader["dauer"], typeof(long)) as long?;
                    dt = (long)var;
                    TimeSpan dauer = new TimeSpan(dt);
                    karten.Add(new Karte(KartenID, frage, antwort, correct, wrong, prority, kategorie, correctTime, wrongTime, time, dauer));
                }
            }
            reader.Close();
            VerbindungBeenden();
            return karten;
        }







        public void ErstelleKategorie(string Kategorie, int UeberID, bool HatUeber)
        {
            VerbindungAufbauen();
            string erg = "";
            string sql = "INSERT INTO Kategorien(Kategorie,UeberID, HatUeber,time) VALUES('" + Kategorie + "','" + UeberID + "', '" + HatUeber + "','" + DateTime.Now.Ticks + "')";


            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            //SQLiteDataReader reader = command.ExecuteReader();
            command.ExecuteNonQuery();
            //command.ExecuteReader();
            VerbindungBeenden();
        }

        public List<int> getAllStackIds()
        {
            int max = 0;
            string sql = "select StapelID from Kategorien";
            List<int> stackIds = new List<int>();
            VerbindungAufbauen();
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);

            SQLiteDataReader reader = command.ExecuteReader();
           while (reader.Read())
            {
                var temp = Convert.ChangeType(reader["StapelID"], typeof(int)) as int?;
                stackIds.Add((int)temp);
            }
            reader.Close();
            VerbindungBeenden();
            return stackIds;
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
            reader.Close();

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
            DateTime dt = DateTime.Now;
            TimeSpan dauer = new TimeSpan(1);
           // dauer = TimeSpan.FromSeconds(1);
            long dtx = dt.Ticks;
            long dtxd = dauer.Ticks;
            string erg = "";
            string sql = "INSERT INTO Karten(Frage,Antwort,StapelID,Kategorie,time,correctTime,wrongTime,dauer) VALUES('" + frage + "','" + antwort + "','" + StapelID + "','" + Kategorie +"',"+ dtx +","+ dtx + "," + dtx +",'"+ dtxd + "')";
            string sql2 = "UPDATE Kategorien set hatKarten = 'true' WHERE StapelID==" + StapelID;
            string sql3 = "UPDATE Kategorien set hatUnter = 'false' WHERE Kategorie==" +"'"+Kategorie+"'";

            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            SQLiteCommand command2 = new SQLiteCommand(sql2, m_dbConnection);
            SQLiteCommand command3 = new SQLiteCommand(sql3, m_dbConnection);
            //SQLiteDataReader reader = command.ExecuteReader();
            command.ExecuteNonQuery();
            command2.ExecuteNonQuery();
            command3.ExecuteNonQuery();
            //command.ExecuteReader();
            VerbindungBeenden();
        }

    }
}
