# Birthday Reminder

This is a desktop application where users are able to add and remove names and birthdates of thir friends from a SQLite database using queries mapped with Dapper. 
They are then able to and see who's birthday is coming up and how many days are remaining until then.

For more information about the code, refer to the comments in Program.cs and BirthdayForm.cs

 Note: Only tested in Windows 10

## How to Use

To add a person, fill out their information then select "Add person".
Leave the year as 0 or blank if you do not know the birth year.
Their Age will be automatically calculated.

To remove a person, click their name on the list then select "Remove".

Select "Find Nearest Birthday" and a pop-up will appear containg the friend's Name, Brithday, and how many days are left until their birthday.

## Built Using

* [Visual Studios](https://visualstudio.microsoft.com/) - IDE
* [.NET](https://dotnet.microsoft.com/) - Application framework
* [SQLite](https://www.sqlite.org/docs.html) - Self contaitned SQL datebase
* [Dapper](https://dapper-tutorial.net/dapper) - Object mapper for SQL queries


