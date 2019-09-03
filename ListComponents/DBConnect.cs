using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.SQLite;
using System.Data;
using System.Windows;
using Dapper;

namespace Birthday_Reminder.ListComponents
{
    class DBConnect
    {
        public static void CreateTable()
        {
            String tableSQL = "CREATE TABLE BirthdayList (Name varchar(20), Date varchar(20), Age int, MMDD int)";
            string connectionString = ConfigurationManager.AppSettings["connectionString"];
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                SQLiteCommand command = new SQLiteCommand(tableSQL, connection);
                try {
                    command.ExecuteNonQuery();
                }
                catch (Exception TableExists) { }
                connection.Close();

            }
        }

        public static void ConnectToDB(int methodNum, Friend person)
        {
            string connectionString = ConfigurationManager.AppSettings["connectionString"];
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {

                if (connection == null)
                {
                    MessageBox.Show("Conneciton Error");
                    return;
                }

                if (methodNum == 1)
                {
                    String cmd = "SELECT Name,Date,Age,MMDD From BirthdayList ORDER BY BirthdayList.MMDD ASC";
                    List <Friend> output = connection.Query<Friend>(cmd, new DynamicParameters()).ToList();
                    DBFunctions.Reminder(output);
                }
                else if (methodNum == 2)
                {
                    String cmd = DBFunctions.AddPeople(person);
                    if (cmd != "")
                    {
                        connection.Execute(cmd);
                    }                   

                }               
                else if (methodNum == 3)
                {
                    String removeName = person.Name;
                    if (DBFunctions.RemovePersonCheck(removeName))
                    {
                        String cmd = ("DELETE FROM BirthdayList WHERE Name = " + "'" + removeName.ToUpper() + "'" + ";");
                        connection.Execute(cmd);
                    }

                }

                
            }
        }

    }
}
