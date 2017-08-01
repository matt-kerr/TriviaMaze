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

namespace TriviaMaze
{  
    [Serializable]
    public class Room
    {
        private int row;
        private int col;
        [NonSerialized]
        private Canvas room_canvas;

        public Room(int row, int col)
        {
            this.row = row;
            this.col = col;
            this.room_canvas = null;
        }

        public int GetRow()
        {
            // Returns which row this Room is in

            return this.row;
        }

        public int GetCol()
        {
            // Returns which col this Room is in

            return this.col;
        }

        public Canvas GetCanvas()
        {
            // Returns the Canvas owned by this Room
            return this.room_canvas;
        }

        public void SetRoomCanvas(Canvas new_canvas)
        {
            // Sets the Canvas owned by this Room
            // The canvas is used by GameWindow and is clicked by the user
            // when they play the game to move around the game environment

            this.room_canvas = new_canvas;
        }
    }
}
