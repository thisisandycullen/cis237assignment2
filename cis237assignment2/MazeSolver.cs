//Andy Cullen
//Assignment 2
//Due: 10/6/15

//MAZESOLVER CLASS

//THE PURPOSE OF THIS CLASS IS TO SOLVE A MAZE ARRAY MADE UP OF '#' AND '.' CHARS.

//FROM A PREDEFINED STARTING POINT ON A PREDEFINED MAZE, BOTH DECLARED IN THE PROGRAM CLASS,
//MAZESOLVER SEARCHES NEIGHBORING ARRAY ITEMS FOR EMPTY SPACES MARKED AS '.'
//BEFORE IT CHECKS FOR EMPTY SPACES, HOWEVER, IT FIRST CHECKS TO SEE THAT THE STARTING POINT
//HAS BEEN PLACED ON AN EMPTY SPACE. ELSE, IT THROWS AN ERROR MESSAGE AND DOES NOT PROCEED.

//IF A NEIGHBORING SPACE IS 'EMPTY', IT WILL PLACE AN 'X' OVER ITSELF (EVEN IF THERE IS AN
//'X' ALREADY THERE---THIS WILL BE EXPLAINED BELOW), AND ALSO PLACE AN 'X' OVER THE NEIGHBORING
//SPACE. FROM THERE, IT WILL MOVE ITS FOCUS TO THAT NEIGHBORING SPACE TO PERFORM ANOTHER
//SEARCH FOR AN EMPTY SPACE FROM THERE, AND SO ON. 

//IF NO EMPTY SPACES CAN BE FOUND (A DEAD END IS REACHED), THE MAZESOLVER WILL TRAVERSE BACK
//THROUGH PREVIOUS X-MARKED SPACES TO FIND ANOTHER EMPTY SPACE, AND REPLACE EACH OF THESE
//Xs WITH A '0'. (WHEN AN X-MARKED SPACE IS FOUND WITH A NEIGHBORING EMPTY SPACE, IT WILL
//BE CHANGED BACK FROM A '0' TO AN 'X' VIA THE PROCESS MENTIONED EARLIER, CONTINUING THE
//LINE OF Xs THAT SHOULD EVENTUALLY LEAD TO THE END OF THE MAZE.

//THE MAZE HAS BEEN SOLVED WHEN THE MAZESOLVER CHECKS A SPACE THAT IS OUTSIDE OF THE ARRAY.
//THIS IS, ESSENTIALLY, THE PROGRAM FINDING A MAZE EXIT.

//IF THE MAZESOLVER TRAVERSES THE WHOLE MAZE WITHOUT FINDING AN EXIT, THE MAZESOLVER INFORMS THE
//USER THAT THERE IS NO EXIT. IN THIS CASE, ALL SPACES WILL AUTOMATICALLY END UP MARKED WITH
//RED 0s BECAUSE EVERY DIRECTION LEADS TO A DEAD END.

//THE MAZESOLVER ALSO HAS A PRINT METHOD THAT PRINTS OUT THE MAZE TO THE CONSOLE AS A VISUAL DISPLAY
//OF ITS PROGRESS. THIS METHOD ALSO ALTERS TEXT COLOR APPROPRIATELY BASED ON CHAR VALUES. #s REMAIN
//THE DEFAULT COLOR, Xs ARE DISPLAYED AS GREEN, 0s ARE DISPLAYED AS RED, AND .s ARE DISPLAYED AS GRAY
//TO CONTRAST THE WALLS.

//THE PROCESS ALSO HAS APPROPRIATELY TIMED USER PROMPTS THAT ALLOW THE PROGRAM TO STOP AT IMPORTANT
//POINTS OF OUTPUT SO THAT THE USER MAY SEE RESULTS. THE USER MAY THEN CONTINUE BY PRESSING 'ENTER'.


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cis237assignment2
{
    /// This class is used for solving a char array maze.
    class MazeSolver
    {
        // Class level member variables for the mazesolver class
        char[,] maze;
        int xStart;
        int yStart;

        //initialize move # variable
        int moveNumber = 0;

        //neighboring tile assigners
        int upTile;
        int rightTile;
        int downTile;
        int leftTile;

        //progress booleans
        bool mazeStarted = false;
        bool mazeFinished = false;

        /// Default Constuctor to setup a new maze solver. 
        public MazeSolver()
        {}

        /// This is the public method that will allow someone to use this class to solve the maze.
        public void SolveMaze(char[,] maze, int xStart, int yStart)
        {
            //Assign passed in variables to the class level ones. It was not done in the constuctor so that
            //a new maze could be passed in to this solve method without having to create a new instance.
            //The variables are assigned so they can be used anywhere they are needed within this class. 
            this.maze = maze;
            this.xStart = xStart;
            this.yStart = yStart;

            //Begin solving the maze
            MazeTraversal(xStart,yStart);
        }

        ///This is the recursive method that gets called to solve the maze.
        private void MazeTraversal(int currentCol, int currentRow)
        {   //Implement maze traversal recursive call

            //assign/reassign "neighboring tiles"
            upTile = currentRow - 1;
            leftTile = currentCol - 1;
            downTile = currentRow + 1;
            rightTile = currentCol + 1;

            //try/catch is used to handle the IndexOutOfRangeException
            try
            {
                //if this is the first pass-through...
                if (!mazeStarted)
                {
                    //make sure the starting tile is open
                    if (maze[currentCol, currentRow] == '.')
                    {   //mark an X on the tile if it is
                        maze[currentCol, currentRow] = 'X';

                        //print the initial maze to the console
                        PrintMaze(moveNumber, currentRow, currentCol);

                        //the maze has now officially been started
                        mazeStarted = true;

                        //prompt user to begin mazesolver
                        Console.WriteLine("Press ENTER to solve this maze.");
                        Console.ReadLine();
                        
                    }
                    else
                    {
                        //print the initial maze to the console
                        PrintMaze(moveNumber, currentRow, currentCol);

                        //if the starting tile is not open, print a failure notice to the console
                        Console.WriteLine("Maze solver Failed." + Environment.NewLine +
                                          "The starting position at [" + currentRow + "," + currentCol + "] is not an open space." +
                                          Environment.NewLine);
                    }
                }

                if (mazeStarted)
                {
                    //search up for next open tile
                    if (maze[upTile, currentCol] == '.')
                    {   //mark an X on the above tile if it is open
                        maze[upTile, currentCol] = 'X';
                        //mark an X on the current tile (solver may be retracing its steps)
                        maze[currentRow, currentCol] = 'X';

                        currentRow = upTile;
                    }
                    else
                    {   //search right for next open tile
                        if (maze[currentRow, rightTile] == '.')
                        {   //mark an X on the right tile if it is open
                            maze[currentRow, rightTile] = 'X';
                            //mark an X on the current tile (solver may be retracing its steps)
                            maze[currentRow, currentCol] = 'X';

                            currentCol = rightTile;
                        }
                        else
                        {   //search down for next open tile
                            if (maze[downTile, currentCol] == '.')
                            {   //mark an X on the tile below if it is open
                                maze[downTile, currentCol] = 'X';
                                //mark an X on the current tile (solver may be retracing its steps)
                                maze[currentRow, currentCol] = 'X';

                                currentRow = downTile;
                            }
                            else
                            {   //search left for next open tile
                                if (maze[currentRow, leftTile] == '.')
                                {   //mark an X on the left tile if it is open
                                    maze[currentRow, leftTile] = 'X';
                                    //mark an X on the current tile (solver may be retracing its steps)
                                    maze[currentRow, currentCol] = 'X';

                                    currentCol = leftTile;
                                }
                                else
                                {   //THE MAZE SOLVER HAS REACHED A DEAD END
                                    //mark the current space with a 0
                                    maze[currentRow, currentCol] = '0';

                                    //search up for an X to retrace steps
                                    if (maze[upTile, currentCol] == 'X')
                                    {   //mark a 0 on the above tile
                                        maze[upTile, currentCol] = '0';

                                        currentRow = upTile;
                                    }
                                    else
                                    {   //search right for an X to retrace steps
                                        if (maze[currentRow, rightTile] == 'X')
                                        {   //mark a 0 on the right tile
                                            maze[currentRow, rightTile] = '0';

                                            currentCol = rightTile;
                                        }
                                        else
                                        {   //search down for an X to retrace steps
                                            if (maze[downTile, currentCol] == 'X')
                                            {   //mark a 0 on the tile below
                                                maze[downTile, currentCol] = '0';

                                                currentRow = downTile;
                                            }
                                            else
                                            {   //search left for an X to retrace steps
                                                if (maze[currentRow, leftTile] == 'X')
                                                {   //mark a 0 on the left tile
                                                    maze[currentRow, leftTile] = '0';

                                                    currentCol = leftTile;
                                                }
                                                else
                                                {   //THE MAZE SOLVER COULD NOT FIND AN EXIT AND IS FINISHED
                                                    Console.WriteLine("This maze has no reachable exit." + Environment.NewLine);
                                                    mazeFinished = true;
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }

                    //As long as the maze has not been finished...
                    if (!mazeFinished)
                    {
                        moveNumber++;                                   //accumulate the move #
                        PrintMaze(moveNumber, currentRow, currentCol);  //show progress on the current maze
                        MazeTraversal(currentCol, currentRow);          //RECURSION METHOD - start again from the top
                    }
                    else
                    {
                        Reset();    //reset booleans and move # for the next maze
                    }
                }

            }
            catch (IndexOutOfRangeException)
            {

                if (mazeStarted)
                {   //if the maze has been officially started, then reaching this exception means the maze was solved
                    Console.WriteLine("This maze has been solved!" + Environment.NewLine);
                    Reset();    //reset booleans and move # for the next maze
                }
                else
                {   //print the initial maze to the console
                    PrintMaze(moveNumber, currentRow, currentCol);

                    //if the maze was never started, then reaching this exception means the starting row/column constants
                    Console.WriteLine("Maze solver Failed." + Environment.NewLine +
                                      "The starting position at [" + currentRow + "," + currentCol +
                                      "] is outside of the maze." + Environment.NewLine);
                }

                mazeFinished = true;
                Reset();    //reset booleans and move # for the next maze
            }

        }

        private void PrintMaze(int moveNumber, int currentRow, int currentCol)
        {
            // get the dimensions of the maze array
            int mazeWidth = maze.GetLength(0);
            int mazeHeight = maze.GetLength(1);

            //display the move number, or "Maze start" if at the start
            if (moveNumber == 0)
            {
                Console.WriteLine("Maze start");
            }
            else
            {
                Console.WriteLine("Move #" + moveNumber);
            }

            //show the current tile coordinates the maze solver is focused on
            Console.WriteLine("Current position: " + currentRow + "," + currentCol);

            //build the maze by printing the value of each array item
            for (int column = 0; column < mazeWidth; column++)
            {
                for (int row = 0; row < mazeHeight; row++)
                {   
                    //before printing each char, assign corresponding output color
                    switch (maze[column, row])
                    {
                        case '.':
                            Console.ForegroundColor = ConsoleColor.DarkGray; break;
                        case 'X':
                            Console.ForegroundColor = ConsoleColor.DarkGreen; break;
                        case '0':
                            Console.ForegroundColor = ConsoleColor.DarkRed; break;
                        default:
                            Console.ForegroundColor = ConsoleColor.White; break;
                    }

                    //print the current char to the console
                    Console.Write(maze[column, row] + " ");
                }

                Console.WriteLine();
            }

            Console.WriteLine();
        }

        private void Reset()
        {   //reset booleans and move # for the next maze
            mazeStarted = false;
            mazeFinished = false;
            moveNumber = 0;
        }

    }//END OF MAZESOLVER CLASS
}//END OF NAMESPACE
