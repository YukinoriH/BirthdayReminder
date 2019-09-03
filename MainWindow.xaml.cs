using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Configuration;
using System.Data.SQLite;
using Dapper;
using Birthday_Reminder.ListComponents;

namespace Birthday_Reminder
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            try { 
                CreateTable();
            }
            catch (Exception TableExists) { }
            for (int x = 1; x <= 31; x++)
            {
                FriendDay.Items.Add(x);
            }
            DBFunctions.AddDictionary();
            UpdateList();

        }

        private void CreateTable()
        {
            String tableSQL = "CREATE TABLE BirthdayList (Name varchar(20), Date varchar(20), Age int, MMDD int)";
            string connectionString = ConfigurationManager.AppSettings["connectionString"];
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                SQLiteCommand command = new SQLiteCommand(tableSQL, connection);
                command.ExecuteNonQuery();
                connection.Close();

            }
        }

        private void UpdateList()
        {
            string connectionString = ConfigurationManager.AppSettings["connectionString"];
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {

                String cmd = "SELECT Name,Date,Age FROM BirthdayList ORDER BY BirthdayList.Name ASC";
                var output = connection.Query<Friend>(cmd, new DynamicParameters());
                FriendList.ItemsSource = output;
                
            }
            
        }

        private void AddFriendBtn_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("(" + FriendDay.Text + ")");
            sb.Append("[" + FriendMonth.Text + "]");
            sb.Append("{" + FriendYear.Text + "}");

            Friend person = new Friend
            {
                Name = FriendName.Text, Date = sb.ToString()
            };

            DBConnect.ConnectToDB(2,person);
            UpdateList();

        }
        private void RemoveFriendBtn_Click(object sender, RoutedEventArgs e)
        {
            Friend person = (Friend)FriendList.SelectedItem;
            DBConnect.ConnectToDB(3, person);
            UpdateList();

        }

        private void FindBirthdayBtn_Click(object sender, RoutedEventArgs e)
        {
            DBConnect.ConnectToDB(1, null);
        }

    }
}
