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

namespace TriviaMaze
{
    /// <summary>
    /// Interaction logic for QuestionEntryWindow.xaml
    /// </summary>
    public partial class QuestionEntryWindow : Window
    {
        private Boolean isTrueFalse;
        private Boolean isMultipleChoice;
        private Boolean isShortAnswer;
        private int id;

        public QuestionEntryWindow()
        {
            InitializeComponent();
            SubmitQuestionToDatebase.IsEnabled = false;
            UpdateQuestionToDatebase.IsEnabled = false;
            UpdateQuestionToDatebase.Visibility = Visibility.Hidden;
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
        }
        public QuestionEntryWindow(int idToUpdate, String question, String a, String b, String c, String d, String type, int catIndex)
        {
            InitializeComponent();
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            this.QuestionEntry.Text = question;
            this.OptionAEntry.Text = a;
            this.OptionBEntry.Text = b;
            this.OptionCEntry.Text = c;
            this.OptionDEntry.Text = d;
            this.id = idToUpdate;
            this.CatagoryToAddTo.SelectedIndex = catIndex;
            if (type.Equals("trueFalse"))
            {
                this.CreateTrueFalse.IsChecked = true;
                this.CreateTrueFalse_Click(null, null);
            }
            if (type.Equals("multipleChoice"))
            {
                this.CreateMultipleChoice.IsChecked = true;
                this.CreateMultipleChoice_Click(null, null);
            }
            if (type.Equals("shortAnswer"))
            {
                this.CreateShortAnswer.IsChecked = true;
                this.CreateShortAnswer_Click(null, null);
            }
            SubmitQuestionToDatebase.IsEnabled = false;
            SubmitQuestionToDatebase.Visibility = Visibility.Hidden;
            UpdateQuestionToDatebase.IsEnabled = true;
            UpdateQuestionToDatebase.Visibility = Visibility.Visible;
        }
        private void CreateTrueFalse_Click(object sender, RoutedEventArgs e)
        {
            // Event handler for when the user clicks Submit
            // when true/false is selected

            this.OptionAEntry.IsEnabled = true;
            this.OptionBEntry.IsEnabled = true;
            this.OptionCEntry.IsEnabled = false;
            this.OptionDEntry.IsEnabled = false;

            this.OptionAEntry.Text = "True";
            this.OptionAEntry.IsReadOnly = true;
            this.OptionBEntry.Text = "False";
            this.OptionBEntry.IsReadOnly = true;

            this.IsACorrect.IsEnabled = true;
            this.IsBCorrect.IsEnabled = true;
            this.IsCCorrect.IsEnabled = false;
            this.IsDCorrect.IsEnabled = false;

            isTrueFalse = true;
            isMultipleChoice = false;
            isShortAnswer = false;

            SubmitQuestionToDatebase.IsEnabled = true;
        }

        private void CreateMultipleChoice_Click(object sender, RoutedEventArgs e)
        {
            // Event handler for when the user clicks Submit
            // when multiple choice is selected

            this.OptionAEntry.IsEnabled = true;
            this.OptionBEntry.IsEnabled = true;
            this.OptionCEntry.IsEnabled = true;
            this.OptionDEntry.IsEnabled = true;

            this.OptionAEntry.IsReadOnly = false;
            this.OptionBEntry.IsReadOnly = false;
            this.OptionCEntry.IsReadOnly = false;
            this.OptionDEntry.IsReadOnly = false;

            this.OptionAEntry.Text = "";
            this.OptionBEntry.Text = "";
            this.OptionCEntry.Text = "";
            this.OptionDEntry.Text = "";

            this.IsACorrect.IsEnabled = true;
            this.IsBCorrect.IsEnabled = true;
            this.IsCCorrect.IsEnabled = true;
            this.IsDCorrect.IsEnabled = true;

            isTrueFalse = false;
            isMultipleChoice = true;
            isShortAnswer = false;

            SubmitQuestionToDatebase.IsEnabled = true;
        }

        private void CreateShortAnswer_Click(object sender, RoutedEventArgs e)
        {
            // Event handler for when the user clicks Submit
            // when short answer is selected

            this.OptionAEntry.IsEnabled = true;
            this.OptionBEntry.IsEnabled = true;
            this.OptionCEntry.IsEnabled = true;
            this.OptionDEntry.IsEnabled = true;

            this.OptionAEntry.IsReadOnly = false;
            this.OptionBEntry.IsReadOnly = false;
            this.OptionCEntry.IsReadOnly = false;
            this.OptionDEntry.IsReadOnly = false;

            this.OptionAEntry.Text = "";
            this.OptionBEntry.Text = "";
            this.OptionCEntry.Text = "";
            this.OptionDEntry.Text = "";

            this.IsACorrect.IsEnabled = false;
            this.IsBCorrect.IsEnabled = false;
            this.IsCCorrect.IsEnabled = false;
            this.IsDCorrect.IsEnabled = false;

            isTrueFalse = false;
            isMultipleChoice = false;
            isShortAnswer = true;

            SubmitQuestionToDatebase.IsEnabled = true;
        }

        private void SubmitQuestionToDatebase_Click(object sender, RoutedEventArgs e)
        {
            // Event handler for when the user clicks Submit

            if(isTrueFalse)
            {
                if (IsACorrect.IsChecked == true)
                {
                    DatabaseConnectionQuery.addTFQuestion(QuestionEntry.Text, "true", CatagoryToAddTo.Text);
                    MessageBox.Show("Success! Question Added to Database!");
                }
                else if (IsBCorrect.IsChecked == true)
                {
                    DatabaseConnectionQuery.addTFQuestion(QuestionEntry.Text, "false", CatagoryToAddTo.Text);
                    MessageBox.Show("Success! Question Added to Database!");
                }
                else
                {
                    MessageBox.Show("Answer must be true or false, please select a value and try again!");
                }
            }
            else if(isMultipleChoice)
            {
                if (OptionAEntry.Text.Equals("") || OptionBEntry.Text.Equals("") || OptionCEntry.Text.Equals("") || OptionDEntry.Text.Equals(""))
                {
                    MessageBox.Show("One or more of your options are blank! Please input an answer and try again!");
                }
                else if (IsACorrect.IsChecked == false && IsBCorrect.IsChecked == false && IsCCorrect.IsChecked == false && IsDCorrect.IsChecked == false)
                {
                    MessageBox.Show("You must select an answer! Please pick the button that represents the correct answer and submit again!");
                }
                else if(IsACorrect.IsChecked == true)
                {
                    DatabaseConnectionQuery.addMCQuestion(QuestionEntry.Text, OptionAEntry.Text, OptionBEntry.Text, OptionCEntry.Text, OptionDEntry.Text, "A", CatagoryToAddTo.Text);
                    MessageBox.Show("Success! Question Added to Database!");
                }
                else if (IsBCorrect.IsChecked == true)
                {
                    DatabaseConnectionQuery.addMCQuestion(QuestionEntry.Text, OptionAEntry.Text, OptionBEntry.Text, OptionCEntry.Text, OptionDEntry.Text, "B", CatagoryToAddTo.Text);
                    MessageBox.Show("Success! Question Added to Database!");
                }
                else if (IsCCorrect.IsChecked == true)
                {
                    DatabaseConnectionQuery.addMCQuestion(QuestionEntry.Text, OptionAEntry.Text, OptionBEntry.Text, OptionCEntry.Text, OptionDEntry.Text, "C", CatagoryToAddTo.Text);
                    MessageBox.Show("Success! Question Added to Database!");
                }
                else if (IsDCorrect.IsChecked == true)
                {
                    DatabaseConnectionQuery.addMCQuestion(QuestionEntry.Text, OptionAEntry.Text, OptionBEntry.Text, OptionCEntry.Text, OptionDEntry.Text, "D", CatagoryToAddTo.Text);
                    MessageBox.Show("Success! Question Added to Database!");
                }
            }
            else if (isShortAnswer)
            {
                if (OptionAEntry.Text.Equals("") || OptionBEntry.Text.Equals("") || OptionCEntry.Text.Equals("") || OptionDEntry.Text.Equals(""))
                {
                    MessageBox.Show("One or more of your options are blank! Short Answer questions must have 4 possible correct answers! Please input an answer and try again!");
                }
                else
                {
                    DatabaseConnectionQuery.addSAQuestion(QuestionEntry.Text, OptionAEntry.Text, OptionBEntry.Text, OptionCEntry.Text, OptionDEntry.Text, CatagoryToAddTo.Text);
                    MessageBox.Show("Success! Question Added to Database!");
                }
            }
            else
            {
                MessageBox.Show("Something went terribly wrong!");
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

        private void UpdateQuestionToDatebase_Click(object sender, RoutedEventArgs e)
        {
            // Event handler for when the user clicks Update

            if (isTrueFalse)
            {
                if (IsACorrect.IsChecked == true)
                {
                    DatabaseConnectionQuery.updateTFQuestion(QuestionEntry.Text, "true", CatagoryToAddTo.Text, id);
                    MessageBox.Show("Success! Question Updated!");
                    this.Close();
                }
                else if (IsBCorrect.IsChecked == true)
                {
                    DatabaseConnectionQuery.updateTFQuestion(QuestionEntry.Text, "false", CatagoryToAddTo.Text, id);
                    MessageBox.Show("Success! Question Updated!");
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Answer must be true or false, please select a value and try again!");
                }
            }
            else if (isMultipleChoice)
            {
                if (OptionAEntry.Text.Equals("") || OptionBEntry.Text.Equals("") || OptionCEntry.Text.Equals("") || OptionDEntry.Text.Equals(""))
                {
                    MessageBox.Show("One or more of your options are blank! Please input an answer and try again!");
                }
                else if (IsACorrect.IsChecked == false && IsBCorrect.IsChecked == false && IsCCorrect.IsChecked == false && IsDCorrect.IsChecked == false)
                {
                    MessageBox.Show("You must select an answer! Please pick the button that represents the correct answer and submit again!");
                }
                else if (IsACorrect.IsChecked == true)
                {
                    DatabaseConnectionQuery.updateMCQuestion(QuestionEntry.Text, OptionAEntry.Text, OptionBEntry.Text, OptionCEntry.Text, OptionDEntry.Text, "A", CatagoryToAddTo.Text, id);
                    MessageBox.Show("Success! Question Updated!");
                    this.Close();
                }
                else if (IsBCorrect.IsChecked == true)
                {
                    DatabaseConnectionQuery.updateMCQuestion(QuestionEntry.Text, OptionAEntry.Text, OptionBEntry.Text, OptionCEntry.Text, OptionDEntry.Text, "B", CatagoryToAddTo.Text, id);
                    MessageBox.Show("Success! Question Updated!");
                    this.Close();
                }
                else if (IsCCorrect.IsChecked == true)
                {
                    DatabaseConnectionQuery.updateMCQuestion(QuestionEntry.Text, OptionAEntry.Text, OptionBEntry.Text, OptionCEntry.Text, OptionDEntry.Text, "C", CatagoryToAddTo.Text, id);
                    MessageBox.Show("Success! Question Updated!");
                    this.Close();
                }
                else if (IsDCorrect.IsChecked == true)
                {
                    DatabaseConnectionQuery.updateMCQuestion(QuestionEntry.Text, OptionAEntry.Text, OptionBEntry.Text, OptionCEntry.Text, OptionDEntry.Text, "D", CatagoryToAddTo.Text, id);
                    MessageBox.Show("Success! Question Updated!");
                    this.Close();
                }
            }
            else if (isShortAnswer)
            {
                if (OptionAEntry.Text.Equals("") || OptionBEntry.Text.Equals("") || OptionCEntry.Text.Equals("") || OptionDEntry.Text.Equals(""))
                {
                    MessageBox.Show("One or more of your options are blank! Short Answer questions must have 4 possible correct answers! Please input an answer and try again!");
                }
                else
                {
                    DatabaseConnectionQuery.updateShortQuestion(QuestionEntry.Text, OptionAEntry.Text, OptionBEntry.Text, OptionCEntry.Text, OptionDEntry.Text, CatagoryToAddTo.Text, id);
                    MessageBox.Show("Success! Question Updated!");
                    this.Close();
                }
            }
            else
            {
                MessageBox.Show("Something went terribly wrong!");
            }
        }

    }
}
