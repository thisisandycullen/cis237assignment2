//Andy Cullen
//Assignment 2
//Due: 10/6/15

//PROGRAM CLASS

//THIS CLASS DEFINES THE MAZE ARRAY AND THE STARTING POSITION.
//IT CREATES A MAZESOLVER OBJECT WHICH DOES THE WORK OF SOLVING THE MAZE.
//IT TRANSPOSES THE ORIGINAL MAZE AND SAVES IT AS A SECOND MAZE.
//THE SECOND MAZE IS SOLVED WITH THE MAZESOLVER AS WELL.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cis237assignment2
{
    class Program
    {   // This is the main entry point for the program.
        
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            /// Starting Coordinates.
            const int X_START = 1;
            const int Y_START = 1;

            /// The first maze that needs to be solved.
            char[,] maze1 = 
            { 
            { '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#' },
            { '#', '.', '.', '.', '#', '.', '.', '.', '.', '.', '.', '#' },
            { '#', '.', '#', '.', '#', '.', '#', '#', '#', '#', '.', '#' },
            { '#', '#', '#', '.', '#', '.', '.', '.', '.', '#', '.', '#' },
            { '#', '.', '.', '.', '.', '#', '#', '#', '.', '#', '.', '.' },
            { '#', '#', '#', '#', '.', '#', '.', '#', '.', '#', '.', '#' },
            { '#', '.', '.', '#', '.', '#', '.', '#', '.', '#', '.', '#' },
            { '#', '#', '.', '#', '.', '#', '.', '#', '.', '#', '.', '#' },
            { '#', '.', '.', '.', '.', '.', '.', '.', '.', '#', '.', '#' },
            { '#', '#', '#', '#', '#', '#', '.', '#', '#', '#', '.', '#' },
            { '#', '.', '.', '.', '.', '.', '.', '#', '.', '.', '.', '#' },
            { '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#' } 
            };

            /// Create a new instance of a mazeSolver.
            MazeSolver mazeSolver = new MazeSolver();

            /// Create the second maze by transposing the first maze
            char[,] maze2 = transposeMaze(maze1);

            /// Tell the instance to solve the first maze with the passed maze, and start coordinates.
            mazeSolver.SolveMaze(maze1, X_START, Y_START);

            /// Give the user a chance to see the first finished maze before continuing to the transposition
            Console.WriteLine("Press ENTER to transpose this maze.");
            Console.ReadLine();

            /// Solve the transposed maze.
            mazeSolver.SolveMaze(maze2, X_START, Y_START);

        }//END OF MAIN


        /// This method will take in a 2 dimensional char array and return
        /// a char array maze that is flipped along the diagonal, or in mathematical
        /// terms, transposed.
        static char[,] transposeMaze(char[,] maze)
        {
            //Create a transposed maze.
            char[,] transposedMaze = new char[maze.GetLength(0), maze.GetLength(1)];

            //go through a row...
            for (int row = 0; row < maze.GetLength(0); row++)
            {   //then through each column...
                for (int column = 0; column < maze.GetLength(1); column++)
                {   //swap the column and row values
                    transposedMaze[row, column] = maze[column, row];
                }
                //repeat until finished
            }

            return transposedMaze;  //return the transposed maze so it can be saved as maze2
        }

    }//END OF PROGRAM CLASS
}//END OF NAMESPACE
