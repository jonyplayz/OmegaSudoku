using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OmegaSudoku
{
    class Exceptions
    {

        public Boolean checkForExceptions(string sudokuData)
        {
            Boolean errorOccured = false;
            if (sudokuData.Length == 0)
            {
                Console.WriteLine("the data you provided is not valid, you entered an empty string\n");
                errorOccured = true;
            }


            double scheme = Math.Sqrt(sudokuData.Length);
            if ((scheme != 1 && scheme != 4 && scheme != 9 && scheme != 16 && scheme != 25) && !errorOccured)
            {
                Console.WriteLine("the data you provided is not valid, the sudoku size can only be 1x1, 4x4, 9x9, 16x16, 25x25\n");
                errorOccured = true;
            }
            if (!errorOccured)
            {
                foreach (char c in sudokuData)
                {
                    if ((c > (48 + scheme) || c < '0') && !errorOccured)
                    {
                        Console.WriteLine("the data you provided is not valid, you can only input these characters:");
                        Console.Write("0 ");
                        for (int i = 0; i < scheme; i++)
                        {
                            Console.Write(Convert.ToChar(49 + i) + " ");
                        }
                        Console.WriteLine("\n");
                        errorOccured = true;
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
                foreach (char c in sudokuData)
                {
                    if (Xcounter == scheme)
                    {
                        Xcounter = 0;
                        Ycounter++;
                    }
                    board[Ycounter, Xcounter] = Convert.ToInt32(c - '0');
                    Xcounter++;

                }


                for (int i = 0; i < scheme; i++)
                {
                    for (int j = 0; j < scheme; j++)
                    {
                        if (!errorOccured)
                        {
                            if (board[i, j] != 0)
                            {
                                if (appearancesY[board[i, j] - 1] == 0)
                                {
                                    appearancesY[board[i, j] - 1] = 1;
                                }
                                else
                                {
                                    Console.WriteLine("the data you provided is not valid, the sudoku data has the same number in the same row");
                                    errorOccured = true;
                                }
                            }
                            if(board[j, i] != 0 && !errorOccured)
                            {
                                if (appearancesX[board[j, i] - 1] == 0)
                                {
                                    appearancesX[board[j, i] - 1] = 1;
                                }
                                else
                                {
                                    Console.WriteLine("the data you provided is not valid, the sudoku data has the same number in the same column");
                                    errorOccured = true;
                                }
                                
                            }
                            
                        }
                        
                    }
                    appearancesX = new int[schemeInt];
                    appearancesY = new int[schemeInt];
                            
                }
            }

            return errorOccured;

        }


 
    }
}
