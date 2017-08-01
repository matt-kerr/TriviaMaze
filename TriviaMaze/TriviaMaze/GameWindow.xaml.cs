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
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
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
    /// Interaction logic for GameWindow.xaml
    /// </summary>
    public partial class GameWindow : Window
    {
        

        //these 5 are for Serialization 
        private int [,] horiz_lock_Status;
        private int [,] vert_lock_Status;
        // cur_lock_type - 1 is vert, 2 is horiz 
        private int cur_lock_type;
        private int cur_lock_x;
        private int cur_lock_y;

        private Room[,] room_layout;
        private Rectangle[,] room_players;
        private Lock[,] vert_locks;
        private Lock[,] horiz_locks;
        private String curr_type;
        private String curr_question_string;
        private String curr_optionA;
        private String curr_optionB;
        private String curr_optionC;
        private String curr_optionD;
        private String curr_answer;
        private Question curr_question;
        private bool question_active;
        private Lock curr_lock;
        private Room curr_room;
        public String user_answer;
        public int player_loc_row;
        public int player_loc_col;
        private int[,] exit_grid;
        private int TRIED = 3;
        private int PATH = 7;
        private QuestionWindow question_window;
        private List<Question> questions;

        public GameWindow()
        {
            InitializeComponent();
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            // this.category = category;
            InitializeQuestionElements();
            InitializeExitPath();
            InitializeGameRooms();
            InitializeLocks();

            questions = DatabaseConnectionQuery.fillQuestionList();

            StartGame();
        }

        public GameWindow(int num)
        {
            // used for unit testing
            this.player_loc_row = 3;
            this.player_loc_col = 3;
            this.user_answer = "";
        }

        private void InitializeQuestionElements()
        {
            // Initializes question elements to a blank state

            question_active = false;
            curr_type = "";
            curr_question_string = "";
            curr_optionA = "";
            curr_optionB = "";
            curr_optionC = "";
            curr_optionD = "";
            curr_answer = "";
        }

        private void InitializeExitPath()
        {
            // Sets up a blank grid for determining
            // if there is a valid exit path
            // 1 = valid path
            // 0 = blocked
            // Several spots are blocked by default because of
            // how the game board is laid out

            exit_grid = new int[7, 7] {{1, 1, 1, 1, 1, 1, 1},
                                       {1, 0, 1, 0, 1, 0, 1},
                                       {1, 1, 1, 1, 1, 1, 1},
                                       {1, 0, 1, 0, 1, 0, 1},
                                       {1, 1, 1, 1, 1, 1, 1},
                                       {1, 0, 1, 0, 1, 0, 1},
                                       {1, 1, 1, 1, 1, 1, 1}};
        }

        private void InitializeGameRooms()
        {
            // Initializes Room arrays and player Rectangle arrays

            room_layout = new Room[4, 4];
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    room_layout[i, j] = new Room(i, j);
                }
            }

            room_layout[0, 0].SetRoomCanvas(room_0_0);
            room_layout[0, 1].SetRoomCanvas(room_0_1);
            room_layout[0, 2].SetRoomCanvas(room_0_2);
            room_layout[0, 3].SetRoomCanvas(room_0_3);
            room_layout[1, 0].SetRoomCanvas(room_1_0);
            room_layout[1, 1].SetRoomCanvas(room_1_1);
            room_layout[1, 2].SetRoomCanvas(room_1_2);
            room_layout[1, 3].SetRoomCanvas(room_1_3);
            room_layout[2, 0].SetRoomCanvas(room_2_0);
            room_layout[2, 1].SetRoomCanvas(room_2_1);
            room_layout[2, 2].SetRoomCanvas(room_2_2);
            room_layout[2, 3].SetRoomCanvas(room_2_3);
            room_layout[3, 0].SetRoomCanvas(room_3_0);
            room_layout[3, 1].SetRoomCanvas(room_3_1);
            room_layout[3, 2].SetRoomCanvas(room_3_2);
            room_layout[3, 3].SetRoomCanvas(room_3_3);

            room_players = new Rectangle[4, 4];
            room_players[0, 0] = rect_player_0_0;
            room_players[0, 1] = rect_player_0_1;
            room_players[0, 2] = rect_player_0_2;
            room_players[0, 3] = rect_player_0_3;
            room_players[1, 0] = rect_player_1_0;
            room_players[1, 1] = rect_player_1_1;
            room_players[1, 2] = rect_player_1_2;
            room_players[1, 3] = rect_player_1_3;
            room_players[2, 0] = rect_player_2_0;
            room_players[2, 1] = rect_player_2_1;
            room_players[2, 2] = rect_player_2_2;
            room_players[2, 3] = rect_player_2_3;
            room_players[3, 0] = rect_player_3_0;
            room_players[3, 1] = rect_player_3_1;
            room_players[3, 2] = rect_player_3_2;
            room_players[3, 3] = rect_player_3_3;

            rect_player_0_0.Visibility = Visibility.Hidden;
            rect_player_0_1.Visibility = Visibility.Hidden;
            rect_player_0_2.Visibility = Visibility.Hidden;
            rect_player_0_3.Visibility = Visibility.Hidden;
            rect_player_1_0.Visibility = Visibility.Hidden;
            rect_player_1_1.Visibility = Visibility.Hidden;
            rect_player_1_2.Visibility = Visibility.Hidden;
            rect_player_1_3.Visibility = Visibility.Hidden;
            rect_player_2_0.Visibility = Visibility.Hidden;
            rect_player_2_1.Visibility = Visibility.Hidden;
            rect_player_2_2.Visibility = Visibility.Hidden;
            rect_player_2_3.Visibility = Visibility.Hidden;
            rect_player_3_0.Visibility = Visibility.Hidden;
            rect_player_3_1.Visibility = Visibility.Hidden;
            rect_player_3_2.Visibility = Visibility.Hidden;
            rect_player_3_3.Visibility = Visibility.Hidden;
        }

        private void InitializeLocks()
        {
            // Initializes Lock arrays and assigns a Question to each lock

            Question debug_question_vert = new Question("MC", "", "Temp MC Question Answer is A", "Temp Option A", "Temp Option B", "Temp Option C", "Temp Option D", "A");

            Question debug_question_horiz = new Question("TF", "", "Temp TF Question Answer is true", "Temp Option true", "Temp Option false", "Temp Option C TF", "Temp Option D TF", "true");

            vert_locks = new Lock[4, 3];
            vert_lock_Status = new int[4,3];

            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    vert_locks[i, j] = new Lock(debug_question_vert);
                    vert_lock_Status[i, j] = 0;
                }
            }
            
            vert_locks[0, 0].SetLockCanvas(lock_vert_0_0);
            vert_locks[0, 1].SetLockCanvas(lock_vert_0_1);
            vert_locks[0, 2].SetLockCanvas(lock_vert_0_2);
            vert_locks[1, 0].SetLockCanvas(lock_vert_1_0);
            vert_locks[1, 1].SetLockCanvas(lock_vert_1_1);
            vert_locks[1, 2].SetLockCanvas(lock_vert_1_2);
            vert_locks[2, 0].SetLockCanvas(lock_vert_2_0);
            vert_locks[2, 1].SetLockCanvas(lock_vert_2_1);
            vert_locks[2, 2].SetLockCanvas(lock_vert_2_2);
            vert_locks[3, 0].SetLockCanvas(lock_vert_3_0);
            vert_locks[3, 1].SetLockCanvas(lock_vert_3_1);
            vert_locks[3, 2].SetLockCanvas(lock_vert_3_2);

            vert_locks[0, 0].SetExitPathRow(0);
            vert_locks[0, 0].SetExitPathCol(1);
            vert_locks[0, 1].SetExitPathRow(0);
            vert_locks[0, 1].SetExitPathCol(3);
            vert_locks[0, 2].SetExitPathRow(0);
            vert_locks[0, 2].SetExitPathCol(5);
            vert_locks[1, 0].SetExitPathRow(2);
            vert_locks[1, 0].SetExitPathCol(1);
            vert_locks[1, 1].SetExitPathRow(2);
            vert_locks[1, 1].SetExitPathCol(3);
            vert_locks[1, 2].SetExitPathRow(2);
            vert_locks[1, 2].SetExitPathCol(5);
            vert_locks[2, 0].SetExitPathRow(4);
            vert_locks[2, 0].SetExitPathCol(1);
            vert_locks[2, 1].SetExitPathRow(4);
            vert_locks[2, 1].SetExitPathCol(3);
            vert_locks[2, 2].SetExitPathRow(4);
            vert_locks[2, 2].SetExitPathCol(5);
            vert_locks[3, 0].SetExitPathRow(6);
            vert_locks[3, 0].SetExitPathCol(1);
            vert_locks[3, 1].SetExitPathRow(6);
            vert_locks[3, 1].SetExitPathCol(3);
            vert_locks[3, 2].SetExitPathRow(6);
            vert_locks[3, 2].SetExitPathCol(5);

            horiz_locks = new Lock[3, 4];
            horiz_lock_Status = new int[3, 4];
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    horiz_locks[i, j] = new Lock(debug_question_horiz);
                    horiz_lock_Status[i, j] = 0;
                }
            }
            
            horiz_locks[0, 0].SetLockCanvas(lock_horiz_0_0);
            horiz_locks[0, 1].SetLockCanvas(lock_horiz_0_1);
            horiz_locks[0, 2].SetLockCanvas(lock_horiz_0_2);
            horiz_locks[0, 3].SetLockCanvas(lock_horiz_0_3);
            horiz_locks[1, 0].SetLockCanvas(lock_horiz_1_0);
            horiz_locks[1, 1].SetLockCanvas(lock_horiz_1_1);
            horiz_locks[1, 2].SetLockCanvas(lock_horiz_1_2);
            horiz_locks[1, 3].SetLockCanvas(lock_horiz_1_3);
            horiz_locks[2, 0].SetLockCanvas(lock_horiz_2_0);
            horiz_locks[2, 1].SetLockCanvas(lock_horiz_2_1);
            horiz_locks[2, 2].SetLockCanvas(lock_horiz_2_2);
            horiz_locks[2, 3].SetLockCanvas(lock_horiz_2_3);

            horiz_locks[0, 0].SetExitPathRow(1);
            horiz_locks[0, 0].SetExitPathCol(0);
            horiz_locks[0, 1].SetExitPathRow(1);
            horiz_locks[0, 1].SetExitPathCol(2);
            horiz_locks[0, 2].SetExitPathRow(1);
            horiz_locks[0, 2].SetExitPathCol(4);
            horiz_locks[0, 3].SetExitPathRow(1);
            horiz_locks[0, 3].SetExitPathCol(6);
            horiz_locks[1, 0].SetExitPathRow(3);
            horiz_locks[1, 0].SetExitPathCol(0);
            horiz_locks[1, 1].SetExitPathRow(3);
            horiz_locks[1, 1].SetExitPathCol(2);
            horiz_locks[1, 2].SetExitPathRow(3);
            horiz_locks[1, 2].SetExitPathCol(4);
            horiz_locks[1, 3].SetExitPathRow(3);
            horiz_locks[1, 3].SetExitPathCol(6);
            horiz_locks[2, 0].SetExitPathRow(5);
            horiz_locks[2, 0].SetExitPathCol(0);
            horiz_locks[2, 1].SetExitPathRow(5);
            horiz_locks[2, 1].SetExitPathCol(2);
            horiz_locks[2, 2].SetExitPathRow(5);
            horiz_locks[2, 2].SetExitPathCol(4);
            horiz_locks[2, 3].SetExitPathRow(5);
            horiz_locks[2, 3].SetExitPathCol(6);
            
        }

        private void StartGame()
        {
            // Starts the game after the Rooms, Rectangles, and Locks have been initialized

            // Player always starts at [0, 0]
            player_loc_row = 0;
            player_loc_col = 0;
            curr_room = room_layout[player_loc_row, player_loc_col];

            MovePlayer();
        }

        private void MovePlayer()
        {
            // Draws the player's location on the game board

            // Hide the previous location
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    room_players[i, j].Visibility = Visibility.Hidden;
                }
            }

            room_players[player_loc_row, player_loc_col].Visibility = Visibility.Visible;

            if (player_loc_row == 3 && player_loc_col == 3)
            {
                MessageBox.Show("Found the exit!\n\nPress OK to return to Main Menu.", "Winner!");
                this.Close();
            }
        }

        public bool IsValidMovement(int row_dest, int col_dest)
        {
            // Returns true if [row_dest, col_dest] is a valid place for the player to move
            // from their current position
            // The player can only move left/right/up/down when the Lock in between the Rooms is open

            // Check if the player can move left
            if ((player_loc_row - row_dest == 1) && (player_loc_col - col_dest == 0))
            {
                // Check horizontal lock at [row_dest, y]
                return horiz_locks[row_dest, player_loc_col].IsLocked();
                
            }
            // Check if the player can move right
            else if ((player_loc_row - row_dest == -1) && (player_loc_col - col_dest == 0))
            {
                // Check horizontal lock at [player_loc_row, y]
                return horiz_locks[player_loc_row, player_loc_col].IsLocked();
            }
            // Check if the player can move up
            else if ((player_loc_col - col_dest == 1) && (player_loc_row - row_dest == 0))
            {
                // Check vertical lock at [x, col_dest]
                return vert_locks[player_loc_row, col_dest].IsLocked();
            }
            // Check if the player can move down
            else if ((player_loc_col - col_dest == -1) && (player_loc_row - row_dest == 0))
            {
                // Check vertical lock at [x, player_loc_col]
                return vert_locks[player_loc_row, player_loc_col].IsLocked();
            }
            else
            {
                return false;
            }

        }

        public bool IsAdjacentLockVert(int row_dest, int col_dest)
        {
            // Determines if the vertical Lock that got clicked is a valid one based on
            // which Room the player is currently in
            // First row checks the Lock to the left of the Room
            // Second row checks the Lock to the right of the Room

            return ((player_loc_col - col_dest == 1) && (player_loc_row == row_dest))
                || ((player_loc_row == row_dest) && (player_loc_col == col_dest));
        }

        public bool IsAdjacentLockHoriz(int row_dest, int col_dest)
        {
            // Determines if the horizontal Lock that got clicked is a valid one based on
            // which Room the player is currently in
            // First row checks the Lock above the Room
            // Fecond row checks the Lock below the Room

            return ((player_loc_row - row_dest == 1) && (player_loc_col == col_dest))
                || ((player_loc_row == row_dest) && (player_loc_col == col_dest));
        }


        private void StartQuestion()
        {
            // Starts a question

            Random rand = new Random();
            int index = rand.Next(0, questions.Count);
            this.curr_question = questions[index];
            questions.RemoveAt(index);
            question_window = new QuestionWindow(this.curr_question,this);
            question_window.Show();
            this.Hide();
            question_active = true;

            // Retrieve the Question from the Lock the user clicked
            this.curr_type = curr_question.GetQuestionType();
            this.curr_answer = curr_question.GetAnswer();
            this.curr_question_string = curr_question.GetQuestionString();

            if (curr_question.GetQuestionType() == "MC"||curr_question.GetQuestionType() == "SHORT")
            {
                // Multiple choice
                this.curr_optionA = curr_question.GetOptionA();
                this.curr_optionB = curr_question.GetOptionB();
                this.curr_optionC = curr_question.GetOptionC();
                this.curr_optionD = curr_question.GetOptionD();

            }
            else if (curr_question.GetQuestionType() == "TF")
            {
                // True/false
                this.curr_optionA = curr_question.GetOptionA();
                this.curr_optionB = curr_question.GetOptionB();

            }
        }

        public void SetUserAnswer(string answer)
        {
            // Assigns the user's answer
            // Called from QuestionWindow

            this.user_answer = answer;
        }

        public void EndQuestion()
        {
            // Ends a question after the user submits an answer
            // Unlocks the current Lock if the answer is correct
            // Locks the current Lock if the answer is incorrect

            if (question_window.set_answer)
            {
                question_window.Close();
            }
            IncorrectWindow incorrect = null;
            CorrectWindow correct = null;

            question_active = false;
            if (this.curr_type == "MC" || this.curr_type == "TF")
            {
                if (this.curr_answer == this.user_answer)
                {
                    // Correct answer
                    correct=new CorrectWindow(this,curr_question);
                    correct.Show();
                    curr_lock.Unlock();
                    if(cur_lock_type == 1)
                    {
                        vert_lock_Status[cur_lock_x, cur_lock_y] = 1;
                    }
                    else if (cur_lock_type == 2)
                    {
                        horiz_lock_Status[cur_lock_x, cur_lock_y] = 1;
                    }

                }
                else
                {
                    // Incorrect answer

                    // Activate the Lock
                    curr_lock.LockForever();
                    incorrect = new IncorrectWindow(GetAnswer(),this,this.curr_question);
                    incorrect.Show();
                    if (cur_lock_type == 1)
                    {
                        vert_lock_Status[cur_lock_x, cur_lock_y] = 2;
                    }
                    else if (cur_lock_type == 2)
                    {
                        horiz_lock_Status[cur_lock_x, cur_lock_y] = 2;
                    }
                    
                    // Block the pathway in the exit path
                    exit_grid[curr_lock.GetExitPathRow(), curr_lock.GetExitPathCol()] = 0;
                }
            }
            if (this.curr_type == "SHORT")
            {
                if (this.user_answer.ToLower().Equals(this.curr_optionA.ToLower()) || this.user_answer.ToLower().Equals(this.curr_optionB.ToLower()) || this.user_answer.ToLower().Equals(this.curr_optionC.ToLower()) || this.user_answer.ToLower().Equals(this.curr_optionD.ToLower()))
                {
                    // Correct answer
                    correct = new CorrectWindow(this, curr_question);
                    correct.Show();
                    curr_lock.Unlock();
                    if (cur_lock_type == 1)
                    {
                        vert_lock_Status[cur_lock_x, cur_lock_y] = 1;
                    }
                    else if (cur_lock_type == 2)
                    {
                        horiz_lock_Status[cur_lock_x, cur_lock_y] = 1;
                    }
                }
                else
                {
                    // Incorrect answer
                    incorrect = new IncorrectWindow(GetAnswer(),this,this.curr_question);
                    incorrect.Show();
                    //  Activate the lock
                    curr_lock.LockForever();
                    if (cur_lock_type == 1)
                    {
                        vert_lock_Status[cur_lock_x, cur_lock_y] = 2;
                    }
                    else if (cur_lock_type == 2)
                    {
                        horiz_lock_Status[cur_lock_x, cur_lock_y] = 2;
                    }
                    // Block the pathway in the exit path
                    exit_grid[curr_lock.GetExitPathRow(), curr_lock.GetExitPathCol()] = 0;
                }
            }

            ResetExitPathValues();

            // Check if there is still a valid path to the exit
            if (!Traverse(player_loc_row * 2, player_loc_col * 2))
            {
                if (correct != null)
                {
                    correct.Close();
                }
                if (incorrect != null)
                {
                    incorrect.Close();
                }
                MessageBox.Show("No valid path to exit!\n\nPress OK to return to Main Menu.", "Game Over");
                this.Close();
            }
        }

        private string GetAnswer()
        {
            // Returns the answer to the current question

            if (this.curr_answer == "B" || this.curr_answer == "false")
            {
                return this.curr_optionB;
            }
            if (this.curr_answer=="C")
            {
                return this.curr_optionC;
            }
            if (this.curr_answer=="D")
            {
                return this.curr_optionD;
            }
            else // a, true, or short answer
            {
                return this.curr_optionA;
            }
        }

        private bool Traverse(int row, int col)
        {
            // Maze traversal algorithm for determining
            // if the user has a valid path to the exit

            bool done = false;

            if (Valid(row, col))
            {
                exit_grid[row, col] = TRIED;

                if (row == exit_grid.GetLength(0) - 1 && col == exit_grid.GetLength(1) - 1)
                {
                    done = true;
                }
                else
                {
                    done = Traverse(row + 1, col); // down
                    if (!done)
                    {
                        done = Traverse(row, col + 1); // right
                    }
                    if (!done)
                    {
                        done = Traverse(row - 1, col); // up
                    }
                    if (!done)
                    {
                        done = Traverse(row, col - 1);
                    }
                }
                if (done)
                {
                    exit_grid[row, col] = PATH;
                }
 
            }
            return done;
        }


        private bool Valid(int row, int col)
        {
            // Returns true if the destination [row, col] is a
            // legal spot for Traverse algorithm to use
            bool result = false;

            if (row >= 0 && row < exit_grid.GetLength(0) && col >= 0 && col < exit_grid.GetLength(1))
            {
                if (exit_grid[row, col] == 1)
                {
                    result = true;
                }
            }
            return result;
        }

        private void ResetExitPathValues()
        {
            // Reset all 7s and 3s to 1s in the exit grid
            // so that the Traverse algorithm can be reused

            for (int i = 0; i < exit_grid.GetLength(0); i++)
            {
                for (int j = 0; j < exit_grid.GetLength(1); j++)
                {
                    if (exit_grid[i, j] == 3 || exit_grid[i, j] == 7)
                    {
                        exit_grid[i, j] = 1;
                    }
                }
            }
        }

        private void EndGame()
        {
            // Creates and shows a new MainWindow when the user exits the game

            MainWindow main_window = new MainWindow();
            main_window.Show();
        }

        private void window_game_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // Event handler for when the user closes the GameWindow

            EndGame();
        }

        private void room_layout_MouseUp(object sender, MouseButtonEventArgs e)
        {
            // Event handler for when the user clicks on a Room
            // Searches through the array of Room's Canvas' until it finds
            // the one the user clicked on
            // Then checked if it is a valid location to move to
            // If it is a valid location to move to, then it moves the player there

            if (!question_active)
            {
                Canvas temp_canvas = (Canvas)sender;

                for (int i = 0; i < room_layout.GetLength(0); i++)
                {
                    for (int j = 0; j < room_layout.GetLength(1); j++)
                    {

                        if (temp_canvas.Equals(room_layout[i, j].GetCanvas()))
                        {
                            if (IsValidMovement(i, j))
                            {
                                player_loc_row = i;
                                player_loc_col = j;
                                curr_room = room_layout[i, j];
                                MovePlayer();
                            }
                        }
                    }
                }
            }
        }

        private void lock_vert_MouseUp(object sender, MouseButtonEventArgs e)
        {
            // Event handler for when the user clicks on a vertical Lock
            // searches through the array of vertical Locks until it finds
            // the one that the user clicked on
            // Then checks if it is adjacent to the user's current Room
            // If it is adjacent then it will ask the Question from that lock

            if (!question_active)
            {
                Canvas temp_canvas = (Canvas)sender;

                for (int i = 0; i < vert_locks.GetLength(0); i++)
                {
                    for (int j = 0; j < vert_locks.GetLength(1); j++)
                    {

                        if (temp_canvas.Equals(vert_locks[i, j].GetCanvas()))
                        {
                            if (IsAdjacentLockVert(i, j))
                            {
                                curr_lock = vert_locks[i, j];
                                temp_canvas.MouseUp -= lock_vert_MouseUp;
                                StartQuestion();
                                cur_lock_type = 1;
                                cur_lock_x = i;
                                cur_lock_y = j;
                            }
                        }
                    }
                }
            }
        }

        private void lock_horiz_MouseUp(object sender, MouseButtonEventArgs e)
        {
            // Event handler for when the user clicks on a horizontal Lock
            // Searches through the array of vertical Locks until it finds
            // the one that the user clicked on
            // Then checks if it is adjacent to the user's current Room
            // If it is adjacent then it will ask the question from that Lock

            if (!question_active)
            {
                Canvas temp_canvas = (Canvas)sender;

                for (int i = 0; i < horiz_locks.GetLength(0); i++)
                {
                    for (int j = 0; j < horiz_locks.GetLength(1); j++)
                    {

                        if (temp_canvas.Equals(horiz_locks[i, j].GetCanvas()))
                        {
                          
                            if (IsAdjacentLockHoriz(i, j))
                            {
                                curr_lock = horiz_locks[i, j];
                                temp_canvas.MouseUp -= lock_horiz_MouseUp;
                                StartQuestion();
                                cur_lock_type = 2;
                                cur_lock_x = i;
                                cur_lock_y = j;
                            }
                        }
                    }
                }
            }
        }

        private void mnu_howToPlay_Click(object sender, RoutedEventArgs e)
        {
            // Event handler for when the user clicks How to Play
            // from the Help menu
            // Opens a new HowToPlayWindow

            InstructionWindow in_window = new InstructionWindow();
            in_window.Show();
            //this.Close();
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

        private void mnu_exit_Click(object sender, RoutedEventArgs e)
        {
            // Event handler for when the user clicks Exit
            // from the File menu

            this.Close();
        }

        private void mnu_loadGame_Click(object sender, RoutedEventArgs e)
        {
            // Event handler for when the user clicks Load Game
            // from the File Menu
            // Loads the game from a serialized state
            // if a saved game exists

            this.InitializeExitPath();
            this.InitializeLocks();
            this.InitializeGameRooms();
            this.InitializeQuestionElements();
            // load the game from a serialized state
            GameStatus toLoad = new GameStatus();
            if (File.Exists("Test.dat"))
            {
                
                Stream TestFileStream = File.OpenRead("Test.dat");
                BinaryFormatter deserializer = new BinaryFormatter();
                toLoad = (GameStatus)deserializer.Deserialize(TestFileStream);
                TestFileStream.Close();
            }
            this.questions = toLoad.questions;
            this.exit_grid = toLoad.exit_grid;
            
            this.player_loc_col = toLoad.player_loc_col;
            this.player_loc_row = toLoad.player_loc_row;
           
            this.vert_lock_Status = toLoad.vert_lock_Status;
            this.horiz_lock_Status = toLoad.horiz_lock_Status;
         
            for (int i = 0; i < horiz_lock_Status.GetLength(0); i++)
            {
                for (int j = 0; j < horiz_lock_Status.GetLength(1); j++)
                {
                    curr_lock = horiz_locks[i, j];
                   
                    if(horiz_lock_Status[i,j] == 2)
                    {
                        curr_lock.LockForever();
                        exit_grid[curr_lock.GetExitPathRow(), curr_lock.GetExitPathCol()] = 0;
                        curr_lock.GetCanvas().MouseUp -= lock_horiz_MouseUp;
                    }
                    else if(horiz_lock_Status[i,j] == 1)
                    {
                        curr_lock.Unlock();
                        curr_lock.GetCanvas().MouseUp -= lock_horiz_MouseUp;    
                    }   
                }
            }

            for ( int i = 0; i < vert_lock_Status.GetLength(0); i++)
            {
                for (int j = 0; j < vert_lock_Status.GetLength(1); j++)
                {
                    curr_lock = vert_locks[i, j];
                    
                    if (vert_lock_Status[i, j] == 2)
                    {
                        curr_lock.LockForever();
                        exit_grid[curr_lock.GetExitPathRow(), curr_lock.GetExitPathCol()] = 0;
                        curr_lock.GetCanvas().MouseUp -= lock_vert_MouseUp;
                    }
                    else if (vert_lock_Status[i, j] == 1)
                    {
                        curr_lock.Unlock();
                        curr_lock.GetCanvas().MouseUp -= lock_vert_MouseUp;
                    }
                   
                }
            }
            this.room_players[this.player_loc_row, this.player_loc_col].Visibility = Visibility.Visible;
            curr_room = this.room_layout[this.player_loc_row, this.player_loc_col];
        }

        private void mnu_saveGame_Click(object sender, RoutedEventArgs e)
        {
            // Event handler for when the user clicks Save Game
            // from the File menu
            // Serializes the game

            GameStatus toSave = new GameStatus(player_loc_row, player_loc_col, exit_grid, questions, horiz_lock_Status, vert_lock_Status);
            toSave.saveGame("Test.dat");  
        }
    }
}
