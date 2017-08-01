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
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Data.SQLite;
using System.Data;

namespace TriviaMaze
{
    /// <summary>
    /// Interaction logic for QuestionsQueryandRemoval.xaml
    /// </summary>
    public partial class QuestionsQueryandRemoval : Window
    {
        public QuestionsQueryandRemoval()
        {
            InitializeComponent();
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            this.RemoveButton.IsEnabled = false;
            this.NumberToRemove.Text = "";
            this.EditButton.IsEnabled = false;
        }

        private void QueryButton_Click(object sender, RoutedEventArgs e)
        {
            // Event handler for when the user clicks the Query button
            // Retrieves questions from the database

            this.RemoveButton.IsEnabled = true;
            DataTable data = DatabaseConnectionQuery.getTableForDataGrid(this.QuestionTypeToQuery.Text, this.CatagoryToQuery.Text);
            this.QueryDataGrid.ItemsSource = data.DefaultView;
            this.QueryDataGrid.IsReadOnly = true;
            this.EditButton.IsEnabled = true;
        }

        private void RemoveButton_Click(object sender, RoutedEventArgs e)
        {
            // Event handler for when the user clicks the Remove button
            // Removes a question from the database

            Boolean isInt = false;
            int remove = 0;
            String type = "";
            try
            {
                remove = Convert.ToInt32(NumberToRemove.Text);
                isInt = true;
            }
            catch (Exception convertError)
            {
                MessageBox.Show("Can't remove at value!");
            }
            if(isInt)
            {
                if(this.QuestionTypeToQuery.SelectedIndex == 0)
                {
                    type = "trueFalse";
                }
                else if(this.QuestionTypeToQuery.SelectedIndex == 1)
                {
                    type = "multipleChoice";
                }
                else if(this.QuestionTypeToQuery.SelectedIndex == 2)
                {
                    type = "shortAnswer";
                }
                DatabaseConnectionQuery.removeItem(remove, type);
                
                QueryButton_Click(null, null);
                MessageBox.Show("Question removed!");
            }

        }

        private void mnu_exit_Click(object sender, RoutedEventArgs e)
        {
            // Event handler for when the user clicks Exit
            // from the File menu
            this.Close();
        }

        private void mnu_howToPlay_Click(object sender, RoutedEventArgs e)
        {
            // Event handler for when the user clicks How to Play
            // from the Help menu
            // Opens a new HowToPlayWindow
            InstructionWindow in_window = new InstructionWindow();
            in_window.Show();
            this.Close();

        }

        private void mnu_about_Click(object sender, RoutedEventArgs e)
        {
            // Event handler for when the user clicks About
            // from the Help menu
            // Displays info about the program in a MessageBox

            string msg = "Trivia Maze\n\n"
                       + "Team //noComment\n"
                       + "Matthew Kerr\n"
                       + "Mary Floyd\n"
                       + "Tim Unger\n"
                       + "Eastern Washington University\n"
                       + "Spring 2015\n\n"
                       + "Version 1.0.0.0\n"
                       + ".NET Framework v4.5.1\n"
                       + "Compiled for 64-bit systems";
            MessageBox.Show(msg, "About");
        }
        //doesn't do anything yet!
        private void EditButton_Click(object sender, RoutedEventArgs e)
        {   
            // Event handler for when the user clicks the Edit button
            // Modifies a question in the database

            QuestionEntryWindow editor = new QuestionEntryWindow();
            Boolean isInt = false;
            int edit = 0;
            String type = "";
            try
            {
                edit = Convert.ToInt32(NumberToRemove.Text);
                isInt = true;
            }
            catch (Exception convertError)
            {
                MessageBox.Show("Can't Edit at value!");
            }
            if (isInt)
            {
                if (this.QuestionTypeToQuery.SelectedIndex == 0)
                {
                    type = "trueFalse";
                    DataRow toEdit = DatabaseConnectionQuery.getItemToEdit(edit, type);
                    editor = new QuestionEntryWindow(edit, (String)toEdit.ItemArray[1], "True", "False", null, null, type, this.CatagoryToQuery.SelectedIndex);
                }
                else if (this.QuestionTypeToQuery.SelectedIndex == 1)
                {
                    type = "multipleChoice";
                    DataRow toEdit = DatabaseConnectionQuery.getItemToEdit(edit, type);
                    editor = new QuestionEntryWindow(edit, (String)toEdit.ItemArray[1], (String)toEdit.ItemArray[2], (String)toEdit.ItemArray[3], (String)toEdit.ItemArray[4], (String)toEdit.ItemArray[5], type, this.CatagoryToQuery.SelectedIndex);
                }
                else if (this.QuestionTypeToQuery.SelectedIndex == 2)
                {
                    type = "shortAnswer";
                    DataRow toEdit = DatabaseConnectionQuery.getItemToEdit(edit, type);
                    editor = new QuestionEntryWindow(edit, (String)toEdit.ItemArray[1], (String)toEdit.ItemArray[2], (String)toEdit.ItemArray[3], (String)toEdit.ItemArray[4], (String)toEdit.ItemArray[5], type, this.CatagoryToQuery.SelectedIndex);
                }
                
                 
                
                editor.Show();
            }
        }

    }
}

