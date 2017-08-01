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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Security.Permissions;
using System.Data.SQLite;

namespace TriviaMaze
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
            DatabaseConnectionQuery.InitializeDatabase();
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
        }

        private void StartGame()
        {
            // Opens a new GameWindow instance and closes down this window

            GameWindow game_window = new GameWindow();
            game_window.Show();
            window_main.Close();
        }

        private void btn_startGame_Click(object sender, RoutedEventArgs e)
        {
            // Event handler for when the user clicks the Start Game button

            StartGame();
            
        }

        private void btn_adminTools_Click(object sender, RoutedEventArgs e)
        {
            // Event handler for when the user clicks the Admin Tools button
            // Opens a new AdminWindow instance and closes down this window

            AdminWindow admin_window = new AdminWindow();
            admin_window.Show();
            window_main.Close();
        }

        private void mnu_exit_Click(object sender, RoutedEventArgs e)
        {
            // Event handler for when the user clicks Exit
            // from the File menu
            // Closes down this window and ends the program

            this.Close();
        }

        private void mnu_startGame_Click(object sender, RoutedEventArgs e)
        {
            // Event handler for when the user clicks Start Game
            // from the File menu

            StartGame();
        }

        private void mnu_adminTools_Click(object sender, RoutedEventArgs e)
        {
            // Event handler for when the user clicks Admin Tools
            // from the File menu
            // Opens a new AdminWindow instance and closes down this window

            AdminWindow admin_window = new AdminWindow();
            admin_window.Show();
            window_main.Close();
        }

        private void mnu_howToPlay_Click(object sender, RoutedEventArgs e)
        {
            // Event handler for when the user clicks How to Play
            // from the Help menu
            // Opens a new HowToPlayWindow


            InstructionWindow in_window=new InstructionWindow();
            in_window.Show();
            
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

        private void btn_howToPlay_Click(object sender, RoutedEventArgs e)
        {
            // Event handler for when the user clicks the How to Play button
            // Opens a new HowToPlayWindow

            

            InstructionWindow in_window = new InstructionWindow();
            in_window.Show();
            //this.Close();
        }
       

        
    }
}
