// Team //noComment
//
// Matt Kerr
// Mary Floyd
// Tim Unger
//
// CSCD350
// Spring 2015

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Data;
using System.Collections;
using System.Windows;


namespace TriviaMaze
{
    class DatabaseConnectionQuery
    {
        private static SQLiteConnection sqlite_conn;
        private static SQLiteCommand sqlite_cmd;
        private static SQLiteDataReader sqlite_datareader;
        private static SQLiteDataAdapter sqlite_dataAdapter;
        private static DataTable query;

        public static void InitializeDatabase()
        {
            // Initializes the database

            // create a new database connection:
            sqlite_conn = new SQLiteConnection("Data Source=database.db;Version=3;New=True;Compress=True;");

            // open the connection:
            sqlite_conn.Open();

            // create a new SQL command:
            sqlite_cmd = sqlite_conn.CreateCommand();

            sqlite_cmd.CommandText = "CREATE TABLE IF NOT EXISTS trueFalse (ID integer primary key autoincrement,"
            + "Question varchar(500),"
            + "Answer varchar(5),"
            + "Category varchar(15));";
            sqlite_cmd.ExecuteNonQuery();
            sqlite_cmd.CommandText = "CREATE TABLE IF NOT EXISTS multipleChoice (ID integer primary key autoincrement,"
            + "Question varchar(500),"
            + "OptionA varchar(100),"
            + "OptionB varchar(100),"
            + "OptionC varchar(100),"
            + "OptionD varchar(100),"
            + "Answer varchar(2),"
            + "Category varchar(15));";
            sqlite_cmd.ExecuteNonQuery();
            sqlite_cmd.CommandText = "CREATE TABLE IF NOT EXISTS shortAnswer (ID integer primary key autoincrement,"
            + "Question varchar(500),"
            + "AnswerA varchar(100),"
            + "AnswerB varchar(100),"
            + "AnswerC varchar(100),"
            + "AnswerD varchar(100),"
            + "Category varchar(15));";
            sqlite_cmd.ExecuteNonQuery();
        }

        public static DataTable getTableForDataGrid(String type, String catagory)
        {
            // Retrieves questions from the database based on the specified type and category

            if (type.Equals("True/False"))
            {
                sqlite_cmd.CommandText = "SELECT * FROM trueFalse WHERE Category = @cat;";
            }
            else if (type.Equals("Multiple Choice"))
            {
                sqlite_cmd.CommandText = "SELECT * FROM multipleChoice WHERE Category = @cat;";
            }
            else
            {
                sqlite_cmd.CommandText = "SELECT * FROM shortAnswer WHERE Category = @cat;";
            }
            sqlite_cmd.Parameters.AddWithValue("@cat", catagory);
            sqlite_dataAdapter = new SQLiteDataAdapter(sqlite_cmd);
            DataTable data = new DataTable();
            sqlite_dataAdapter.Fill(data);
            return data;
        }
        public static void addTFQuestion(String question, String answer, String category)
        {
            // Adds a true/false question to the database

            sqlite_cmd.CommandText = "INSERT INTO trueFalse (Question, Answer, Category) values (@question, @answer, @cat);";
            sqlite_cmd.Parameters.AddWithValue("@question", question);
            sqlite_cmd.Parameters.AddWithValue("@answer", answer);
            sqlite_cmd.Parameters.AddWithValue("@cat", category);
            sqlite_cmd.ExecuteNonQuery();
        }
        public static void addMCQuestion(String question, String optionA, String optionB, String optionC, String optionD, String answer, String category)
        {
            // Adds a multiple choice question to the database

            sqlite_cmd.CommandText = "INSERT INTO multipleChoice (Question, OptionA, OptionB, OptionC, OptionD, Answer, Category) values (@question, @opA, @opB, @opC, @opD, @answer, @cat);";
            sqlite_cmd.Parameters.AddWithValue("@question", question);
            sqlite_cmd.Parameters.AddWithValue("@opA", optionA);
            sqlite_cmd.Parameters.AddWithValue("@opB", optionB);
            sqlite_cmd.Parameters.AddWithValue("@opC", optionC);
            sqlite_cmd.Parameters.AddWithValue("@opD", optionD);
            sqlite_cmd.Parameters.AddWithValue("@answer", answer);
            sqlite_cmd.Parameters.AddWithValue("@cat", category);
            sqlite_cmd.ExecuteNonQuery();
        }
        public static void addSAQuestion(String question, String optionA, String optionB, String optionC, String optionD, String category)
        {
            // Adds a short answer question to the database

            sqlite_cmd.CommandText = "INSERT INTO shortAnswer (Question, AnswerA, AnswerB, AnswerC, AnswerD, Category) values (@question, @opA, @opB, @opC, @opD, @cat);";
            sqlite_cmd.Parameters.AddWithValue("@question", question);
            sqlite_cmd.Parameters.AddWithValue("@opA", optionA);
            sqlite_cmd.Parameters.AddWithValue("@opB", optionB);
            sqlite_cmd.Parameters.AddWithValue("@opC", optionC);
            sqlite_cmd.Parameters.AddWithValue("@opD", optionD);
            sqlite_cmd.Parameters.AddWithValue("@cat", category);
            sqlite_cmd.ExecuteNonQuery();
        }
        public static void removeItem(int id, String type)
        {
            // Removes a question from the database

            if (type.Equals("trueFalse"))
            {
                sqlite_cmd.CommandText = "DELETE FROM trueFalse WHERE ID = @id;";
            }
            else if(type.Equals("multipleChoice"))
            {
                sqlite_cmd.CommandText = "DELETE FROM multipleChoice WHERE ID = @id;";
            }
            else if(type.Equals("shortAnswer"))
            {
                sqlite_cmd.CommandText = "DELETE FROM shortAnswer WHERE ID = @id;";
            }
            sqlite_cmd.Parameters.AddWithValue("@id", id);
            sqlite_cmd.ExecuteNonQuery();
        }
        public static DataRow getItemToEdit(int id, String type)
        {
            // Retrieves a single question from the database to be edited
            if (type.Equals("trueFalse"))
            {
                sqlite_cmd.CommandText = "SELECT * FROM trueFalse WHERE ID = @id;";
            }
            else if (type.Equals("multipleChoice"))
            {
                sqlite_cmd.CommandText = "SELECT * FROM multipleChoice WHERE ID = @id;";
            }
            else if (type.Equals("shortAnswer"))
            {
                sqlite_cmd.CommandText = "SELECT * FROM shortAnswer WHERE ID = @id;";
            }
            sqlite_cmd.Parameters.AddWithValue("@id", id);
            sqlite_dataAdapter = new SQLiteDataAdapter(sqlite_cmd);
            DataTable toEdit = new DataTable();
            sqlite_dataAdapter.Fill(toEdit);
            DataRow toReturn = toEdit.Rows[0];
            return toReturn;
        }
       
        public static List<Question> fillQuestionList()
        { 
            // Fills a List with questions for the GameWindow

            List<Question> questionToAsk = new List<Question>(36);
            //Get Entertainment Questions
            query = getTableForDataGrid("True/False", "Entertainment");
            questionToAsk.Add(getTF());
            questionToAsk.Add(getTF());
            questionToAsk.Add(getTF());
            query = getTableForDataGrid("Multiple Choice", "Entertainment");
            questionToAsk.Add(getMultChoice());
            questionToAsk.Add(getMultChoice());
            questionToAsk.Add(getMultChoice());
            query = getTableForDataGrid("Short", "Entertainment");
            questionToAsk.Add(getShortAnswer());
            questionToAsk.Add(getShortAnswer());
            questionToAsk.Add(getShortAnswer());
            //Get History
            query = getTableForDataGrid("True/False", "History");
            questionToAsk.Add(getTF());
            questionToAsk.Add(getTF());
            questionToAsk.Add(getTF());
            query = getTableForDataGrid("Multiple Choice", "History");
            questionToAsk.Add(getMultChoice());
            questionToAsk.Add(getMultChoice());
            questionToAsk.Add(getMultChoice());
            query = getTableForDataGrid("Short", "History");
            questionToAsk.Add(getShortAnswer());
            questionToAsk.Add(getShortAnswer());
            questionToAsk.Add(getShortAnswer());
            //Get Science
            query = getTableForDataGrid("True/False", "Science");
            questionToAsk.Add(getTF());
            questionToAsk.Add(getTF());
            questionToAsk.Add(getTF());
            query = getTableForDataGrid("Multiple Choice", "Science");
            questionToAsk.Add(getMultChoice());
            questionToAsk.Add(getMultChoice());
            questionToAsk.Add(getMultChoice());
            query = getTableForDataGrid("Short", "Science");
            questionToAsk.Add(getShortAnswer());
            questionToAsk.Add(getShortAnswer());
            questionToAsk.Add(getShortAnswer());
            //Get Sports
            query = getTableForDataGrid("True/False", "Sports");
            questionToAsk.Add(getTF());
            questionToAsk.Add(getTF());
            questionToAsk.Add(getTF());
            query = getTableForDataGrid("Multiple Choice", "Sports");
            questionToAsk.Add(getMultChoice());
            questionToAsk.Add(getMultChoice());
            questionToAsk.Add(getMultChoice());
            query = getTableForDataGrid("Short", "Sports");
            questionToAsk.Add(getShortAnswer());
            questionToAsk.Add(getShortAnswer());
            questionToAsk.Add(getShortAnswer());

            return questionToAsk;
        }
        private static Question getTF()
        {
            // Retrieves a random true/false question from the DataTable

            DataRow toAdd;
            Question newQ;

            Random rand = new Random();
            int cur = rand.Next(0, query.Rows.Count);
            toAdd = query.Rows[cur];
            newQ = new Question("TF", (String)toAdd.ItemArray[3], (String)toAdd.ItemArray[1], "True", "False", null, null, (String)toAdd.ItemArray[2]);
            query.Rows.RemoveAt(cur);

            return newQ;
        }
        private static Question getMultChoice()
        {
            // Retrieves a random multiple choice question from the DataTable

            DataRow toAdd;
            Question newQ;

            Random rand = new Random();
            int cur = rand.Next(0, query.Rows.Count);
            toAdd = query.Rows[cur];
            newQ = new Question("MC", (String)toAdd.ItemArray[7], (String)toAdd.ItemArray[1], (String)toAdd.ItemArray[2], (String)toAdd.ItemArray[3], (String)toAdd.ItemArray[4], (String)toAdd.ItemArray[5], (String)toAdd.ItemArray[6]);
            query.Rows.RemoveAt(cur);

            return newQ;
        }
        private static Question getShortAnswer()
        {
            // Retrieves a random short answer question from the DataTable

            DataRow toAdd;
            Question newQ;

            Random rand = new Random();
            int cur = rand.Next(0, query.Rows.Count);
            toAdd = query.Rows[cur];
            newQ = new Question("SHORT", (String)toAdd.ItemArray[6], (String)toAdd.ItemArray[1], (String)toAdd.ItemArray[2], (String)toAdd.ItemArray[3], (String)toAdd.ItemArray[4], (String)toAdd.ItemArray[5], (String)toAdd.ItemArray[2]);
            query.Rows.RemoveAt(cur);

            return newQ;
        }

        public static void updateTFQuestion(String question, String answer, String category, int id)
        {
            sqlite_cmd.CommandText = "UPDATE trueFalse SET question= @question, answer= @answer, category= @cat WHERE id = @id;";
            sqlite_cmd.Parameters.AddWithValue("@question", question);
            sqlite_cmd.Parameters.AddWithValue("@answer", answer);
            sqlite_cmd.Parameters.AddWithValue("@cat", category);
            sqlite_cmd.Parameters.AddWithValue("@id", id);
            sqlite_cmd.ExecuteNonQuery();
        }
        public static void updateMCQuestion(String question, String optionA, String optionB, String optionC, String optionD, String answer, string category, int id)
        {
            sqlite_cmd.CommandText = "UPDATE multipleChoice SET question= @question, answer= @answer, category= @cat WHERE id = @id;";
            sqlite_cmd.Parameters.AddWithValue("@question", question);
            sqlite_cmd.Parameters.AddWithValue("@opA", optionA);
            sqlite_cmd.Parameters.AddWithValue("@opB", optionB);
            sqlite_cmd.Parameters.AddWithValue("@opC", optionC);
            sqlite_cmd.Parameters.AddWithValue("@opD", optionD);
            sqlite_cmd.Parameters.AddWithValue("@answer", answer);
            sqlite_cmd.Parameters.AddWithValue("@cat", category);
            sqlite_cmd.Parameters.AddWithValue("@id", id);
            sqlite_cmd.ExecuteNonQuery();
        }
        public static void updateShortQuestion(String question, String a, String b, String c, String d, String category, int id)
        {
            sqlite_cmd.CommandText = "UPDATE shortAnswer SET question= @question, answer= @answer, category= @cat WHERE id = @id;";
            sqlite_cmd.Parameters.AddWithValue("@question", question);
            sqlite_cmd.Parameters.AddWithValue("@opA", a);
            sqlite_cmd.Parameters.AddWithValue("@opB", b);
            sqlite_cmd.Parameters.AddWithValue("@opC", c);
            sqlite_cmd.Parameters.AddWithValue("@opD", d);
            sqlite_cmd.Parameters.AddWithValue("@cat", category);
            sqlite_cmd.Parameters.AddWithValue("@id", id);
            sqlite_cmd.ExecuteNonQuery();
        }
    }
}
