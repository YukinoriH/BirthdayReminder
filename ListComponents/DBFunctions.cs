using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;


namespace Birthday_Reminder.ListComponents
{
    class DBFunctions
    {
        public static Dictionary<String, int> monToNum = new Dictionary<string, int>();
        public static void Reminder(List<Friend> ListOfFriends)
        {
            int closestIndex = 0;
            long daysAway = 0;
            String birthdayName = "";
            String birthdayDate = "";
            DateTime Today = DateTime.Now;
            int currMMDD = (Today.Month * 100) + Today.Day;

            foreach (Friend x in ListOfFriends)
            {
                //Since the BirthdayDB is ordered by date, the first date in the DB 
                //that's after the current date will always be the upcoming birthday.
                //In the case where the upcoming birthday is next year (i = -1), the
                //first value in the ordered BirthdayDb will be the upcoming birthday.
                if (x.MMDD >= currMMDD && closestIndex == 0)
                {
                    String stringDate = x.Date;
                    DateTime toDate = DateTime.ParseExact(stringDate, "MMMM-dd-yyyy", null);
                    daysAway = x.MMDD - currMMDD;
                    birthdayName = x.Name;
                    birthdayDate = x.Date;
                }
            }
            MessageBox.Show(birthdayName + ":" + birthdayDate + "\n" + daysAway + " Days remaining");
        }
        public static void AddDictionary()
        {
            monToNum.Add("January", 1);
            monToNum.Add("February", 2);
            monToNum.Add("March", 3);
            monToNum.Add("April", 4);
            monToNum.Add("May", 5);
            monToNum.Add("June", 6);
            monToNum.Add("July", 7);
            monToNum.Add("August", 8);
            monToNum.Add("September", 9);
            monToNum.Add("October", 10);
            monToNum.Add("November", 11);
            monToNum.Add("December", 12);
        }

        public static String AddPeople(Friend person)
        {
            //TODO: add "st" or "th" suffix to the end of Day string
            //Extracts the String value within each bracket
            String Name = person.Name;
            String Birthday = person.Date;

            String Day = Birthday.Substring(Birthday.IndexOf("(") + 1, Birthday.IndexOf(")") - Birthday.IndexOf("(") - 1);
            String Month = Birthday.Substring(Birthday.IndexOf("[") + 1, Birthday.IndexOf("]") - Birthday.IndexOf("[") - 1);
            String Y = Birthday.Substring(Birthday.IndexOf("{") + 1, Birthday.IndexOf("}") - Birthday.IndexOf("{") - 1);
            int Year = Int32.Parse(Y);
            int Age = 0;

            //TODO: be able to determine the age or birth year given only one of the values.           
            if (Year > 0)
            {
                var YearAge = FindAgeYear(Year, Age);
                //Year = YearAge.Item1;
                Age = YearAge.Item2;
            }

            String day = Month + " " + Day + ", " + Y;
            int MMDD = (monToNum[Month] * 100) + Int32.Parse(Day);
            Name = Name.ToUpper();

            String Values = "NAME: " + Name + " BIRTHDAY: " + day + " AGE: " + Age;
            String message = ("Add the following values to the database?\n " + Values);
            String caption = ("Add Friend");
            MessageBoxButton buttons = MessageBoxButton.YesNo;
            MessageBoxResult result;
            result = MessageBox.Show(message, caption, buttons);

            String cmd = "";
            if (result == System.Windows.MessageBoxResult.Yes)
            {
                cmd = String.Format("INSERT INTO BirthdayList (Name,Date,MMDD,Age) VALUES ('{0}','{1}',{2},{3});", Name, day, Age, MMDD);   
            }
            return cmd;

        }

        
        public static bool RemovePersonCheck(String friendName)
        {

            String message = ("Remove " + friendName + " from friends list?");
            String caption = ("Remove Friend");
            MessageBoxButton buttons = MessageBoxButton.YesNo;
            MessageBoxResult result;
            result = MessageBox.Show(message, caption, buttons);
            return (result == System.Windows.MessageBoxResult.Yes);

        }
        
        private static Tuple<int, int> FindAgeYear(int Year, int Age)
        {
            DateTime Today = DateTime.Now;
            int currYear = Today.Year;
            if (Year <= 0 && Age > 0)
            {
                Year = currYear - Age;

            }
            else if (Age <= 0 && Year > 0)
            {
                Age = currYear - Year;
            }
            return Tuple.Create(Year, Age);
        }

    }
}
