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
        public static void VerbindungAufbauen()
        {
            m_dbConnection = new SQLiteConnection("Data Source=KarteiDB.sqlite;Version=3;");
            m_dbConnection.Open();
        }

        public static void VerbindungBeenden()
        {
            m_dbConnection.Close();
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

        public List<int> getAllStackIds()
        {
            string sql = "select ID from Categories";
            List<int> stackIds = new List<int>();
            VerbindungAufbauen();
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            SQLiteDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                var temp = Convert.ChangeType(reader["ID"], typeof(int)) as int?;
                stackIds.Add((int)temp);
            }
            reader.Close();
            VerbindungBeenden();
            return stackIds;
        }

        public List<int> getChildStackIds(int StapelID)
        {
            List<int> stackIds = new List<int>();

            VerbindungAufbauen();
            string sql = "select StackID from Categories where UeberID==" + (StapelID);
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            SQLiteDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                var temp = Convert.ChangeType(reader["ID"], typeof(int)) as int?;
                stackIds.Add((int)temp);
            }
            reader.Close();

            VerbindungBeenden();
            return stackIds;
        }

    }
}
