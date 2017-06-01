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

        public static string SQL_COMMAND_XY = "select gotCards from Categories where StackName=";

        //basics
        public static SQLiteConnection m_dbConnection;
        public static void connect()
        {
            m_dbConnection = new SQLiteConnection("Data Source=KarteiDB.sqlite;Version=3;");
            m_dbConnection.Open();
        }
        public static void dc() { m_dbConnection.Close(); }



        //getter Boolean
        public bool getStackBool(int ID, string field)
        {
            connect();
            bool erg = false;
            string sql = "select " + field + " from Categories where ID==" + ID;
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            SQLiteDataReader reader = command.ExecuteReader();
            while (reader.Read()) { erg = (bool)reader[field]; }
            reader.Close();
            dc();
            return erg;
        }
        public bool getStackBool(string sql, string field)
        {
            connect();
            bool erg = false;
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            SQLiteDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                erg = (bool)reader[field];
            }
            reader.Close();
            dc();
            return erg;
        }
        public bool gotCards(string stackName) { return getStackBool("select gotCards from Categories where StackName= ", stackName); }
        public bool getChild(string stackName) { return getStackBool("select gotChild from Categories where StackName= ", stackName); }
        public bool gotChild(int ID) { return getStackBool(ID, "gotChild"); }
        public bool gotCards(int ID) { return getStackBool(ID, "gotCards"); }



        //getter string
        public String getStackString(int ID, string field)
        {
            connect();
            string erg = "";
            string sql = "select " + field + " from Categories where ID==" + ID;
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            SQLiteDataReader reader = command.ExecuteReader();
            while (reader.Read()) { erg = (string)reader[field]; }
            reader.Close();
            dc();
            return erg;
        }
        public string getQuestion(int ID) { return getStackString(ID, "Question"); }
        public string getAnswer(int ID) { return getStackString(ID, "Answer"); }
        public string getStackName(int ID) { return getStackString(ID, "StackName"); }

        //getter numeric
        public int getStackIntbyID(int ID, string field)
        {
            int erg = 0;
            string sql = "select " + field + " from Categories where ID==" + ID;
            connect();
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            SQLiteDataReader reader = command.ExecuteReader();
            while (reader.Read()) { var temp = Convert.ChangeType(reader[field], typeof(int)) as int?; erg = (int)temp; }
            reader.Close();
            dc();
            return erg;
        }
        public int getCorrect(int ID) { return getStackIntbyID(ID, "Correkt"); }
        public int getWrong(int ID) { return getStackIntbyID(ID, "Correkt"); }
        public int getPriority(int ID) { return getStackIntbyID(ID, "Priority"); }
        public int getParentId(int ID) { return getStackIntbyID(ID, "parentID"); }


        public int getStackIntbyName(string StackName, string field)
        {
            int erg = 0;
            string sql = "select " + field + " from Categories where StackName==" + StackName;
            connect();
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            SQLiteDataReader reader = command.ExecuteReader();
            while (reader.Read()) { var temp = Convert.ChangeType(reader[field], typeof(int)) as int?; erg = (int)temp; }
            reader.Close();
            dc();
            return erg;
        }

        public long getLong(string sql, string erg)
        {
            connect();
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            SQLiteDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                var temp = Convert.ChangeType(reader[erg], typeof(long)) as long?;
                reader.Close();
                dc();
                return (long)temp;
            }
            reader.Close();
            dc();
            return 325;
        }



        //getter DateTime

        public TimeSpan getTimeSpan(int ID, string field)
        {
            TimeSpan dur;
            string sql = "select " + field + " from Cards where ID=" + ID;
            long dauer = 0;
            connect();
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            SQLiteDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                var temp = Convert.ChangeType(reader[field], typeof(int)) as int?;
                dauer = (long)temp;
            }
            dur = new TimeSpan(dauer);
            reader.Close();
            dc();
            return dur;
        }
            public TimeSpan getDuration(int ID) { return getTimeSpan(ID, "Duration"); }


        public DateTime getStackDateTime(int id, string field)
        {
            connect();
            string sql = "select " + field + " from Categories where ID=" + ID;
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            SQLiteDataReader reader = command.ExecuteReader();
            long dt = getLong(sql, field);
            DateTime dtx = new DateTime(dt);
            reader.Close();
            dc();
            return dtx;
        }
            public DateTime getStackLastView(int ID) { return getStackDateTime(ID, "lastView"); }
            public DateTime get_CorrectTime(int ID) { return getStackDateTime(ID, "correctTime"); }
            public DateTime get_wrongTime(int ID) { return getStackDateTime(ID, "wrongTime"); }
            public DateTime get_CardCreationTime(int ID) { return getStackDateTime(ID, "creationTime"); }



        // getter lists
        public List<int> getStackIntList(string sql, string field)
        {
            List<int> stackIds = new List<int>();
            connect();
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            SQLiteDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                var temp = Convert.ChangeType(reader["field"], typeof(int)) as int?;
                stackIds.Add((int)temp);
            }
            reader.Close();
            dc();
            return stackIds;
        }
        public List<int> getAllStackIds() { return getStackIntList("select ID from Categories", "ID"); }
        public List<int> getChildStackIds(int ID) { return getStackIntList("select ID from Categories where parentID==", "ID"); }
        public List<int> get_CardStack(int ID)
        {
            List<int> cardIDs = new List<int>();
            connect();
            string sql = "select ID from Cards where stackID=" + ID;
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            SQLiteDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                var temp = Convert.ChangeType(reader["ID"], typeof(int)) as int?;
                cardIDs.Add((int)temp);
            }
            reader.Close();

            dc();
            return cardIDs;
        }

        //counter 
        public int countStackCards(int ID)
        {
            int max = 0;
            connect();
            string sql = "select ID from Cards where StackID=" + ID;
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
            string sql = "select ID from Cards where StackID=" + ID + " AND Priority >5";
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

        public int countChildStackPrioCards(List<int> childIdList)
        {
            int max = 0;
            connect();
            for (int i = 0; i < childIdList.Count(); i++)
            {
                max += countStackPrioCards(childIdList[i]);
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

        public void changeCardTime(int ID, string field)
        {
            connect();
            DateTime dtc = DateTime.Now;
            long dtx = dtc.Ticks;

            string sql = "UPDATE Card set " + field + "= " + dtx + " WHERE ID == " + ID;
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();
            dc();
        }
             public void changeCorrectTime(int ID) { changeCardTime(ID, "correctTime"); }
             public void changeWrongTime(int ID) { changeCardTime(ID, "wrongTime"); }

        public void set_Bool(int ID,string field,bool bl, string table)
        {
            connect();
            DateTime dtc = DateTime.Now;
            long dtx = dtc.Ticks;

            string sql = "UPDATE "+table+" set " + field + "= "+ bl+" WHERE ID == " + ID;
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();
            dc();
        }
            public void setCardBool(int ID,string field, bool bl) { changeBool(ID,field,bl,"Card"); }
                public void set_gotChild(int ID)
                {
                    setCardBool(int ID,)
                }
       


            public void changStackBool(int ID, string field, bool bl){ changeBool(ID, field, bl, "Category"); }

        /*  public void changeCorrectTime(int ID)
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

      */


    }
}
