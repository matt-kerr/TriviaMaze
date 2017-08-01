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
using System.Windows.Controls;
using System.Windows.Media;

namespace TriviaMaze
{
    [Serializable]
    public class Lock
    {
        private bool is_passable;
        private Question question;
        [NonSerialized]
        private Canvas lock_canvas;
        private int exit_path_row;
        private int exit_path_col;
        private bool is_locked;

        public Lock(Question question)
        {
            this.is_passable = false;
            // temp question
            this.question = question;
            this.exit_path_row = -1;
            this.exit_path_col = -1;
            this.is_locked = false;
        }

        public int GetExitPathRow()
        {
            // Returns the exit path row stored by this Lock
            // Used by the GameWindow for determining if the user
            // still has a valid path to the exit

            return this.exit_path_row;
        }

        public void SetExitPathRow(int exit_path_row)
        {
            // Sets the exit path row of this Lock

            this.exit_path_row = exit_path_row;
        }

        public int GetExitPathCol()
        {
            // Returns the exit path col stored by this Lock
            // Used by the GameWindow for determining if the user
            // still has a valid path to the exit
            return this.exit_path_col;
        }

        public void SetExitPathCol(int exit_path_col)
        {
            // Sets the exit path col of this Lock
            this.exit_path_col = exit_path_col;
        }

        public void SetLockCanvas(Canvas new_canvas)
        {
            // Sets the Canvas for this lock
            // The canvas is used by GameWindow and is clicked by the user
            // when they play the game to get asked the Question from this Lock

            this.lock_canvas = new_canvas;
        }

        public Canvas GetCanvas()
        {
            // Returns the Canvas owned by this Lock

            return this.lock_canvas;
        }

        public Question GetQuestion()
        {
            // returns the Question owned by this Lock
            return this.question;
        }

        public void Unlock()
        {
            // Makes it so the user can pass through this Lock in GameWindow
            // changes the color of the Canvas to Green to signify that it is unlocked

            this.is_passable = true;
            this.lock_canvas.Background = new SolidColorBrush(Colors.Green);
        }

        public void LockForever()
        {
            // Changes the color of the Canvas to Red to signify that it is locked

            this.lock_canvas.Background = new SolidColorBrush(Colors.Red);
            this.is_locked = true;
        }

        public bool IsLocked()
        {
            // Returns whether the user can currently pass through this Lock

            return this.is_passable;
        }
    }
}
