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
    public class sqlte_connector
    { 
        
        public static SQLiteConnection m_dbConnection;
        public static void connect()
        {
            m_dbConnection = new SQLiteConnection("Data Source=db.sqlite;Version=3;");
            m_dbConnection.Open();
        }
        public static void dc() { m_dbConnection.Close(); }



        //getter Boolean
        public bool getBool_Stack(int ID, string field)
        {
            connect();
            bool erg = false;
            string sql = "select " + field + " from Stacks where ID==" + ID;
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            SQLiteDataReader reader = command.ExecuteReader();
            while (reader.Read()) { erg = (bool)reader[field]; }
            reader.Close();
            dc();
            return erg;
        }
        public bool getBool_Stack(string sql, string field)
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
        public bool get_bool_gotCards(string stackName) { return getBool_Stack("select gotCards from Stacks where stackName= '"+stackName+"'", "gotCards"); }
        public bool get_bool_gotChild(string stackName) { return getBool_Stack("select gotChild from Stacks where stackName= '"+stackName+"'", "gotChild"); }
        public bool get_bool_gotChild(int ID) { return getBool_Stack(ID, "gotChild"); }
        public bool get_bool_gotCards(int ID) { return getBool_Stack(ID, "gotCards"); }



        //getter string

        public string getString(int ID,string field,string table)
        {
            connect();
            string erg = "";
            string sql = "select " + field + " from "+table+" where ID==" + ID;
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            SQLiteDataReader reader = command.ExecuteReader();
            while (reader.Read()) { erg = (string)reader[field]; }
            reader.Close();
            dc();
            return erg;
        }
        public String getString_Stacks(int ID, string field){return getString(ID, field, "Stacks");}
        public string get_string_stackName_stacks(int ID) { return getString_Stacks(ID, "stackName"); }

        public String getString_Cards(int ID, string field){return getString(ID, field, "Cards");}
        public string get_string_question(int ID) { return getString_Cards(ID, "question"); }
        public string get_string_answer(int ID) { return getString_Cards(ID, "answer"); }
        public string get_string_stackName_cards(int ID) { return getString_Cards(ID, "stackName"); }


        public int getInt(string sql, string field)
        {
            int erg = 0;
            connect();
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            SQLiteDataReader reader = command.ExecuteReader();
            while (reader.Read()) { var temp = Convert.ChangeType(reader[field], typeof(int)) as int?; erg = (int)temp; }
            reader.Close();
            dc();
            return erg;
        }
        public int getInt_byStackID(int ID, string field)
        { return getInt("select "+field+" from Stacks where ID="+ID,field); }

        public int get_int_parentID(int ID){return getInt_byStackID(ID,"parentID"); }



        public int getInt_byCardID(int ID, string field)
        { return getInt("select "+field+" from Cards where ID="+ID,field);}
        public int get_int_correct(int ID) {return getInt_byCardID(ID, "correct");}
        public int get_int_wrong(int ID) { return getInt_byCardID(ID, "wrong"); }
        public int get_int_priority(int ID) { return getInt_byCardID(ID, "priority"); }


        public int getInt_byStackName(string stackName,string field)
        { return getInt("select " + field + " from Stacks where stackName='"+stackName+"'",field); }
        public int get_int_parentID(string stackName) { return getInt_byStackName(stackName, "parentID"); }
        public int get_int_ID_Stack(string stackName) { return getInt_byStackName(stackName, "ID"); }


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


        public TimeSpan get_Duration(int ID) { return getTimeSpan(ID, "Duration"); }

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



        public DateTime getDateTime(int ID, string field,string table)
        {
            connect();
            string sql = "select " + field + " from "+table+" where ID=" + ID;
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            SQLiteDataReader reader = command.ExecuteReader();
            long dt = getLong(sql, field);
            DateTime dtx = new DateTime(dt);
            reader.Close();
            dc();
            return dtx;
        }
        public DateTime get_DateTime_stackLastView(int ID) { return getDateTime(ID, "lastView","Stacks"); }
        public DateTime get_DateTime_stackCreationTime(int ID) { return getDateTime(ID, "time", "Stacks"); }
        public DateTime get_DateTime_correctTime(int ID) { return getDateTime(ID, "correctTime","Cards"); }
        public DateTime get_DateTime_wrongTime(int ID) { return getDateTime(ID, "wrongTime","Cards"); }
        public DateTime get_DateTime_cardCreationTime(int ID) { return getDateTime(ID, "creationTime","Cards"); }



        // getter lists

        public List<int> getList_Int(string sql, string field)
        {
            List<int> stackIds = new List<int>();
            connect();
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            SQLiteDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                var temp = Convert.ChangeType(reader[field], typeof(int)) as int?;
                stackIds.Add((int)temp);
            }
            reader.Close();
            dc();
            return stackIds;
        }

        public List<int> get_list_allStackIds() { return getList_Int("select ID from Stacks", "ID"); }
        public List<int> get_list_childStackIds(int ID) { return getList_Int("select ID from Stacks where parentID=="+ID, "ID"); }
        public List<int> get_list_cardsOnStackIDs(int ID) { return getList_Int("select ID from Cards where stackID = " + ID, "ID"); }

        //counter

        /*
         * 
         * ( nun sinnlos? )
         * 
         * 
         */
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
            string sql = "select ID from Cards where StackID=" + ID + " AND priority >5";
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            SQLiteDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                max += 1;
            }
            reader.Close();
            reader.Dispose(); // nicht vorgeschlagen... veraltet?
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

        public void setInt(int ID,int var, string field, string table)
        {
            connect();
            string sql = "UPDATE "+table+" set " + "'" + field + "'=" + var + " WHERE ID=" + ID;
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();
            dc();
        }
        public void setIntCard(int ID, int var, string field) { setInt(ID,var,field, "Cards");}
        public void set_int_priority(int ID,int var) { setIntCard(ID, var, "priority"); }
        public void set_int_correct(int ID, int var) { setIntCard(ID, var, "correct"); }
        public void set_int_wrong(int ID, int var) { setIntCard(ID, var, "wrong"); }


        public void setIntStack(int ID, int var,string field){setInt(ID,var,field, "Stacks");}

        public void set_duration(int ID, long stamp)
        {
            connect();
            string sql = "UPDATE Cards set duration= '" + stamp + "' WHERE ID == " + ID;
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();
            dc();
        }

        public void setDateTime(int ID, string field,string table)
        {
            connect();
            DateTime dtc = DateTime.Now;
            long dtx = dtc.Ticks;
            string sql = "UPDATE "+table+" set " + field + "= " + dtx + " WHERE ID == " + ID;
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();
            dc();
        }
        public void set_dateTime_stackTime(int ID, string field) { setDateTime(ID, field, "Stacks"); }
        public void set_dateTime_lastView(int ID) { set_dateTime_stackTime(ID, "lastView"); }


        public void set_dateTime_cardTime(int ID, string field) {setDateTime(ID, field, "Cards");}
        public void set_dateTime_correctTime(int ID) { set_dateTime_cardTime(ID, "correctTime"); }
        public void set_dateTime_wrongTime(int ID) { set_dateTime_cardTime(ID, "wrongTime"); }



        public void setBool(int ID,string field,bool bl, string table)
        {
            connect();
            DateTime dtc = DateTime.Now;
            long dtx = dtc.Ticks;
            string sql = "UPDATE "+table+" set " + field + "= '"+ bl+"' WHERE ID == " + ID;
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();
            dc();
        }
        public void setBoolCard(int ID,string field, bool bl) { setBool(ID,field,bl,"Card"); }
       
        public void setBoolStack(int ID, string field, bool bl){ setBool(ID, field, bl, "Stacks"); }
        public void set_bool_gotChild(int ID, bool bl) { setBoolStack(ID, "gotChild", bl); }
        public void set_bool_gotCards(int ID, bool bl) { setBoolStack(ID, "gotCards", bl); }

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


        public void add_NewStack(string stack, int parentID)
        {
            {
                connect();
                string erg = "";
                string sql = "INSERT INTO Stacks(stackName,parentID, time,lastView,gotChild,gotCards) VALUES('" + stack + "', " + parentID + ", " + DateTime.Now.Ticks + ", " + DateTime.Now.Ticks + ", 'false','false' )";
                SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
                command.ExecuteNonQuery();
                dc();
                command.Dispose();  // freigabe der ressourcen
            }
           
        }

        public void add_NewCard(string stackName,int stackID, int priority, string question, string answer, int correct, int wrong, long dtx,long tsx)
        {
            connect();
            string sql = "INSERT INTO Cards(stackName,stackID, priority, question, answer, correct, wrong,duration,lastTry, creationTime, correctTime,wrongTime) VALUES('"+stackName+"', " + stackID + ", " + priority + ", '" + question + "', '" + answer + "', " + correct + ", " + wrong + ", '" + tsx + "', '" + dtx + "' ,'"+dtx + "', '" + dtx + "', '" + dtx + "')";
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();
            set_bool_gotCards(stackID, true);
            // set_gotChild(stackID, false);  überflüssig?

            dc();
            command.Dispose();
        }

    }
}
