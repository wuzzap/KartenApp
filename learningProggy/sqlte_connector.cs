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
        // getter

        //getter Boolean

                 public Boolean gotChild(int ID)
        {
            bool temp = false;
            connect();
            string sql = "select gotChild from Categories where ID==" + ID;
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            SQLiteDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                temp = (bool)reader["gotChild"];

            }
            reader.Close();

            dc();
            return temp;
        }

        public Boolean gotCards(int ID)
        {
            bool temp = false;
            connect();
            string sql = "select gotCards from Categories where ID==" + ID;
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            SQLiteDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                temp = (bool)reader["gotCards"];

            }
            reader.Close();

            dc();
            return temp;
        }

        //getter string

        public string getCategoryName(int ID)
        {
            connect();
            string erg = "";
            string sql = "select stackName from Categories where ID=="+ID;
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            SQLiteDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                erg = (string)reader["stackName"];
            }
            reader.Close();

            dc();
            return erg;
        }
        public string getQuestion(int ID)
        {
            string sql = "select Question from Cards";
            string erg = "";
            connect();
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            SQLiteDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                var temp = reader["Question"];
                erg = (string)temp;
            }
            reader.Close();
            dc();
            return erg;
        }

        public string getAnswer(int ID)
        {
            string sql = "select Answer from Cards";
            string erg = "";
            connect();
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            SQLiteDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                var temp = reader["Answer"];
                erg = (string)temp;
            }
            reader.Close();
            dc();
            return erg;
        }

        public string getStackName(int ID)
        {
            string sql = "select stackName from Cards where ID="+ID;
            string erg = "";
            connect();
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            SQLiteDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                var temp = reader["stackName"];
                erg = (string)temp;
            }
            reader.Close();
            dc();
            return erg;
        }

        //getter numeric

        public int getCorrect(int ID)
        {
            int erg = 0;
            string sql = "select Correkt from Cards where ID==" + ID;
            connect();
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            SQLiteDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                var temp = Convert.ChangeType(reader["Correkt"], typeof(int)) as int?;
                erg = (int)temp;
            }
            reader.Close();
            dc();
            return erg;
        }

        public int getWrong(int ID)
        {
            int erg = 0;
            string sql = "select Wrong from Cards where ID==" + ID;
            connect();
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            SQLiteDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                var temp = Convert.ChangeType(reader["Wrong"], typeof(int)) as int?;
                erg = (int)temp;
            }
            reader.Close();
            dc();
            return erg;
        }

        public int getPriority(int ID)
        {
            int erg = 0;
            string sql = "select Priority from Cards where ID==" + ID;
            connect();
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            SQLiteDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                var temp = Convert.ChangeType(reader["Priority"], typeof(int)) as int?;
                erg = (int)temp;
            }
            reader.Close();
            dc();
            return erg;
        }


        public int getParentId(int ID)
        {
            int erg = 0;
            string sql = "select parentID from Categories where ID=="+ID;
            connect();
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            SQLiteDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                var temp = Convert.ChangeType(reader["parentID"], typeof(int)) as int?;
                erg = (int)temp;
            }
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

        public TimeSpan getDuration(int ID)
        {
            TimeSpan dur = new TimeSpan();
            long dauer = 0;
            connect();

            string sql = "select Duration from Cards where ID="+ID;
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            SQLiteDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                var temp = Convert.ChangeType(reader["Duration"], typeof(int)) as int?;
                dauer = (long)temp;
            }
            dur = new TimeSpan(dauer);
            reader.Close();
            dc();
            return dur;

        }


        public DateTime get_StackLastView(int ID)
        {
            connect();
            string sql = "select lastView from Categories where ID="+ID;
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            SQLiteDataReader reader = command.ExecuteReader();
            long dt = getLong(sql, "lastView");
            DateTime dtx = new DateTime(dt);
            reader.Close();
            dc();
            return dtx;
        }

        public DateTime get_CorrectTime(int ID)
        {
            connect();
            string sql = "select correctTime from Cards where ID=" + ID;
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            SQLiteDataReader reader = command.ExecuteReader();
            long dt = getLong(sql, "correctTime");
            DateTime dtx = new DateTime(dt);
            reader.Close();
            dc();
            return dtx;
        }

        public DateTime get_wrongTime(int ID)
        {
            connect();
            string sql = "select wrongTime from Cards where ID=" + ID;
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            SQLiteDataReader reader = command.ExecuteReader();
            long dt = getLong(sql, "wrongTime");
            DateTime dtx = new DateTime(dt);
            reader.Close();
            dc();
            return dtx;
        }

        public DateTime get_CardCreationTime(int ID)
        {
            connect();
            string sql = "select creationTime from Cards where ID=" + ID;
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            SQLiteDataReader reader = command.ExecuteReader();
            long dt = getLong(sql, "creationTime");
            DateTime dtx = new DateTime(dt);
            reader.Close();
            dc();
            return dtx;
        }

        // getter lists


        public List<int> getAllStackIds()
        {
            string sql = "select ID from Categories";
            List<int> stackIds = new List<int>();
            connect();
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            SQLiteDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                var temp = Convert.ChangeType(reader["ID"], typeof(int)) as int?;
                stackIds.Add((int)temp);
            }
            reader.Close();
            dc();
            return stackIds;
        }

        public List<int> getChildStackIds(int ID)
        {
            List<int> stackIds = new List<int>();
            connect();
            string sql = "select ID from Categories where parentID==" + ID;
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            SQLiteDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                var temp = Convert.ChangeType(reader["ID"], typeof(int)) as int?;
                stackIds.Add((int)temp);
            }
            reader.Close();

            dc();
            return stackIds;
        }

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
