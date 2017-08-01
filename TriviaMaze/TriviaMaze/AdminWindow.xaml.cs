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
    /// Interaction logic for AdminWindow.xaml
    /// </summary>
    public partial class AdminWindow : Window
    {
        public AdminWindow()
        {
            InitializeComponent();
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
        }

        private void window_admin_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // Event handler for when the user closes the window

            MainWindow window_main = new MainWindow();
            window_main.Show();
        }

        private void EnterQuestion_Click(object sender, RoutedEventArgs e)
        {
            // Event handler for when the user clicks the EnterQuestion button

            QuestionEntryWindow enter_question = new QuestionEntryWindow();
            enter_question.Show();
        }

        private void QueryQuestions_Click(object sender, RoutedEventArgs e)
        {
            // Event handler for when the user clicks the QueryQuestion button
            QuestionsQueryandRemoval query_question = new QuestionsQueryandRemoval();
            query_question.Show();
        }
    }
}
