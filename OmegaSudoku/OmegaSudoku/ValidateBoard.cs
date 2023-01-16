using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OmegaSudoku
{
    public static class ValidateBoard
    {

        public static bool validate(string sudokuData)    //this function checks for any exceptions from a given input of sudoku data.
        {
            bool errorOccured = false;
            if (sudokuData.Length == 0) //check for empty data
            {
                errorOccured = true;
                throw new EmptyBoardDataException("the data you provided is not valid, you entered an empty string\n");
            }


            double scheme = Math.Sqrt(sudokuData.Length);
            if ((scheme != 1 && scheme != 4 && scheme != 9 && scheme != 16 && scheme != 25) && !errorOccured)   //check for wrong size
            {
                errorOccured = true;
                throw new InvalidBoardSizeException("the data you provided is not valid, the sudoku size can only be 1x1, 4x4, 9x9, 16x16, 25x25\n");
            }
            if (!errorOccured)
            {
                foreach (char c in sudokuData)
                {
                    if ((c > (48 + scheme) || c < '0') && !errorOccured)    //check for wrong charecters
                    {
                        string errormsg = "the data you provided is not valid, you can only input these characters:\n0 ";
                        for (int i = 0; i < scheme; i++)
                        {
                            errormsg += (Convert.ToChar(49 + i) + " ");
                        }
                        errormsg += "\n";
                        errorOccured = true;
                        throw new InvalidCharacterException(errormsg);
                    }
                }
            }
            if (!errorOccured)
            {
                int[] appearancesX = new int[(int)scheme];  
                int[] appearancesY = new int[(int)scheme];

                int schemeInt = Convert.ToInt32(scheme);
                int[,] board = new int[schemeInt, schemeInt];
                int Xcounter = 0;
                int Ycounter = 0;
                foreach (char c in sudokuData)  //this loop translates the given sudokudata into a visible sudoku board
                {
                    if (Xcounter == scheme)
                    {
                        Xcounter = 0;
                        Ycounter++;
                    }
                    board[Ycounter, Xcounter] = Convert.ToInt32(c - '0');
                    Xcounter++;

                }


                for (int row = 0; row < scheme; row++)
                {
                    for (int col = 0; col < scheme; col++)
                    {
                        if (!errorOccured)
                        {
                            if (board[row, col] != 0)
                            {
                                if (appearancesY[board[row, col] - 1] == 0) //checks for the same number in the same row
                                {
                                    appearancesY[board[row, col] - 1] = 1;
                                }
                                else
                                {
                                    
                                    errorOccured = true;

                                    throw new SameNumberInTheSameRowException("the data you provided is not valid, the sudoku data has the same number in the same row");
                                }
                            }
                            if(board[col, row] != 0 && !errorOccured)
                            {
                                if (appearancesX[board[col, row] - 1] == 0) //checks for the same number in the same column
                                {
                                    appearancesX[board[col, row] - 1] = 1;
                                }
                                else
                                {
                                    errorOccured = true;

                                    throw new SameNumberInTheSameColumnException("the data you provided is not valid, the sudoku data has the same number in the same column");
                                }
                                
                            }
                            
                        }
                        
                    }
                    appearancesX = new int[schemeInt];
                    appearancesY = new int[schemeInt];
                            
                }
            }

            if (!errorOccured)  //this checks for the same number in the same box
            {
                int schemeInt = Convert.ToInt32(scheme);
                int sqrtSchemeInt = (int)Math.Sqrt(schemeInt);
                int[,] board = new int[schemeInt, schemeInt];
                int Xcounter = 0;
                int Ycounter = 0;
                foreach (char c in sudokuData)  //this builds the sudoku board from the sudokudata
                {
                    if (Xcounter == scheme)
                    {
                        Xcounter = 0;
                        Ycounter++;
                    }
                    board[Ycounter, Xcounter] = Convert.ToInt32(c - '0');
                    Xcounter++;

                }
                for (int row = 0; row < schemeInt; row++)
                {
                    for(int col = 0; col < schemeInt; col++)
                    {
                        if (board[row, col] != 0)
                        {
                            int startPoint_row = row - (row % sqrtSchemeInt);
                            int startPoint_column = col - (col % sqrtSchemeInt);

                            for (int rowOffset = 0; rowOffset < sqrtSchemeInt; rowOffset++)
                            {
                                for (int colOffset = 0; colOffset < sqrtSchemeInt; colOffset++)
                                {
                                    if (board[startPoint_row + rowOffset, startPoint_column + colOffset] == board[row, col] && ((startPoint_row + rowOffset) != row && (startPoint_column + colOffset) != col))
                                    {
                                        errorOccured = true;
                                        throw new SameNumberInTheSameBoxException("the data you provided is not valid, the sudoku data has the same number in the same box");
                                    }
                                }
                            }
                        }
                    }
                }
            }

            return errorOccured;

        }
    }
}
