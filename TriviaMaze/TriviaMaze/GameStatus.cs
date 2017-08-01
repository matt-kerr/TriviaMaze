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
using System.Windows.Shapes;

//Creating an object that will monitor the player's progress and then be able to be save to a data file. Clearly incomplete.
namespace TriviaMaze
{   
    [Serializable]
    public class GameStatus
    {
        
        public int player_loc_row { get; set; }
        public int player_loc_col { get; set; }
        public int[,] exit_grid { get; set; }

        public int[,] horiz_lock_Status { get; set; }
        public int[,] vert_lock_Status { get; set; }
        public List<Question> questions;

        public GameStatus()
        {

        }

        public GameStatus(int player_loc_row, int player_loc_col, int[,] exit_grid, List<Question> questions,
            int[,] horiz_lock_Status, int[,] vert_lock_Status) {

                this.player_loc_row = player_loc_row;
                this.player_loc_col = player_loc_col;
                this.exit_grid = exit_grid;
                this.questions = questions;
                this.horiz_lock_Status = horiz_lock_Status;
                this.vert_lock_Status = vert_lock_Status;
                
     

        }

        public void saveGame(String FileName)
        {
            // Serializes the GameWindow to a file

            Stream TestFileStream = File.Create(FileName);
            BinaryFormatter serializer = new BinaryFormatter();
            serializer.Serialize(TestFileStream, this);
            TestFileStream.Close();
        }

    }
}
