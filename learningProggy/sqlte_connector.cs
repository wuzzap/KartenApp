using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Data.SQLite.EF6;
using System.Data.SQLite.Linq;
namespace learningProggy
{
    class sqlte_connector
    {

        //basics
        public static SQLiteConnection m_dbConnection;
        public static void connect()
        {
            m_dbConnection = new SQLiteConnection("Data Source=KarteiDB.sqlite;Version=3;");
            m_dbConnection.Open();
        }

        public static void dc()
        {
            m_dbConnection.Close();
        }

        //getter Boolean
        public Boolean getStackBool(int ID,string field)
        {
            bool temp = false;
            connect();
            string sql = "select "+field+" from Categories where ID==" + ID;
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            SQLiteDataReader reader = command.ExecuteReader();
            while (reader.Read()){temp = (bool)reader[field];}
            reader.Close();
            dc();
            return temp;
        }
        public Boolean gotChild(int ID) {return getStackBool(ID, "gotChild");}
        public Boolean gotCards(int ID) { return getStackBool(ID, "gotCards"); }
   

        //getter string
        public string getStackStringFromCategories(int ID,string field)
        {
            connect();
            string erg = "";
            string sql = "select "+field+" from Categories where ID==" + ID;
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            SQLiteDataReader reader = command.ExecuteReader();
            while (reader.Read()){erg = (string)reader[field];}
            reader.Close();
            dc();
            return erg;
        }
        public string getCategoryName(int ID) { return getStackStringFromCategories(ID, "stackName"); }



        public string getCardString(int ID, string field)
        {
            connect();
            string erg = "";
            string sql = "select " + field + " from Cards where ID==" + ID;
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            SQLiteDataReader reader = command.ExecuteReader();
            while (reader.Read()) { erg = (string)reader[field]; }
            reader.Close();
            dc();
            return erg;
        }
        public string getQuestion(int ID) { return getCardString(ID, "Question"); }
        public string getAnswer(int ID) { return getCardString(ID, "Answer"); }
        public string getStackNameFromCards(int ID) { return getCardString(ID, "stackName"); }
        



        //getter numeric
        public int getIntFromCards(int ID,string field)
        {
                int erg = 0;
                string sql = "select "+field+" from Cards where ID==" + ID;
                connect();
                SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
                SQLiteDataReader reader = command.ExecuteReader();
                while (reader.Read()){var temp = Convert.ChangeType(reader[field], typeof(int)) as int?; erg = (int)temp;}
                reader.Close();
                dc();
                return erg;
        }
        public int getCorrect(int ID) { return getIntFromCards(ID, "Correkt"); }
        public int getWrong(int ID) { return getIntFromCards(ID, "Wrong"); }
        public int getPriority(int ID) { return getIntFromCards(ID, "Priority"); }
        public int getParentID(int ID) { return getIntFromCards(ID, "parentID"); }



        public long getLong(string sql, string erg)
        {
            connect();
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            SQLiteDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                var temp = Convert.ChangeType(reader[erg], typeof(long)) as long?;
                reader.Close(); dc(); return (long)temp;
            }
            reader.Close(); dc(); return 325;
        }


        //getter DateTime
        public TimeSpan getTimeSpanFromCards(int ID,string field)
        {
            TimeSpan dur = new TimeSpan();
            long ts = 0;
            connect();
            string sql = "select "+field+" from Cards where ID=" + ID;
            ts = getLong(sql, field);
            dur = new TimeSpan(ts);
            return dur;
        }
        public TimeSpan getDuration(int ID) { return getTimeSpanFromCards(ID, "Duration"); }


        public DateTime getDateTime(string sql, string field)
        {
            connect();
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            SQLiteDataReader reader = command.ExecuteReader();
            long dt = getLong(sql, "lastView");
            DateTime dtx = new DateTime(dt);
            reader.Close();
            dc();
            return dtx;
        }
        public DateTime get_StackLastView(int ID){ return getDateTime("select lastView from Categories where ID="+ID,"lastView"); }
        public DateTime get_CorrectTime(int ID) { return getDateTime("select correctTime from Cards where ID=" + ID, "correctTime"); }
        public DateTime get_WrongTime(int ID) { return getDateTime("select WrongTime from Cards where ID=" + ID, "WrongTime"); }
        public DateTime get_CardCreationTime(int ID) { return getDateTime("select creationTime from Cards where ID=" + ID, "creationTime"); }




        // getter lists
        public List<int> getIntList(string sql,string field)
        {
            List<int> list = new List<int>();
            connect();
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            SQLiteDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                var temp = Convert.ChangeType(reader[field], typeof(int)) as int?;
                list.Add((int)temp);
            }
            reader.Close();
            dc();
            return list;
        }
        public List<int> getAllStackIds(){return getIntList("select ID from Categories", "ID");}
        public List<int> getChildStackIds(int ID) { return getIntList("select ID from Categories where parentID==" + ID, "ID"); }
        public List<int> get_CardStack(int ID) { return getIntList("select ID from Cards where stackID=" + ID, "ID"); }


        //counter 
        public int countStackCards(int ID)
        {
            int max = 0;
            connect();
                string sql = "select ID from Cards where StackID=" +ID;
                SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
                SQLiteDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    max += 1;
                }
                reader.Close();
                dc();
            
            return max;
        }

        public int countChildStackCards(List<int> childIdList)
        {
           int max = 0;
           connect();
           for (int i = 0; i < childIdList.Count(); i++)
           {
                max += countStackCards(childIdList[i]);
           }
            return max;
        }

        public int countStackPrioCards(int ID)
        {
            int max = 0;
            connect();
            string sql = "select ID from Cards where StackID="+ID+" AND Priority >5";
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            SQLiteDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                max += 1;
            }
            reader.Close();
            dc();

            return max;
        }

        public int countChildStackPrioCards(List<int> childIDList)
        {
            int max = 0;
            connect();
            for (int i = 0; i < childIDList.Count(); i++)
            {
                max += countStackPrioCards(childIDList[i]);
            }
            return max;
        }

        // setter

        public void changeIntVars(string category, int var, int kartenID)
        {
            connect();
            string sql = "UPDATE Card set " + "'" + category + "'=" + var + " WHERE ID=" + kartenID;
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();
            // System.Threading.Thread.Sleep(5000);
            dc();
        }

        public void changeDuration(int ID, long stamp)
        {
            connect();
            string sql = "UPDATE Card set dauer= '" + stamp + "' WHERE ID == " + ID;
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();
            dc();
        }

        public void changeCorrectTime(int ID)
        {
            connect();
            DateTime dtc = DateTime.Now;
            long dtxc = dtc.Ticks;

            string sql = "UPDATE Card set correctTime= " + dtxc + " WHERE ID == " + ID;
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();
            dc();
        }

        public void changeWrongTime(int ID)
        {
            connect();
            DateTime dtw = DateTime.Now;
            long dtxw = dtw.Ticks;

            string sql = "UPDATE Card set wrongTime= " + dtxw + " WHERE ID == " + ID;
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();
            dc();
        }


    }
}
