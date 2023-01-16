using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OmegaSudoku
{
    public static class Printer
    {
        public static void printSudoku(int schemeInt,int[,] board) //this method prints out a given sudoku board, it uses colors to display which numbers have been inputted and which are not
                                                                    //and also seperates the boxes with color
        {
            int sqrtSize = (int)Math.Sqrt(schemeInt);
            int rowBoxes = sqrtSize - 1;
            for (int row = 0; row < schemeInt; row++)
            {
                int columnBoxes = sqrtSize - 1;
                
                for (int col = 0; col < schemeInt; col++)
                {
                    if (board[row, col] != 0)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write(string.Format("{0}", Convert.ToChar(board[row, col] + '0')));
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write(string.Format("{0}", Convert.ToChar(board[row, col] + '0')));
                    }
                    if(col + 1 != schemeInt)
                    {
                        if (col == columnBoxes)
                        {
                            Console.ForegroundColor = ConsoleColor.DarkCyan;
                            Console.Write("  |  ");
                            columnBoxes += sqrtSize;
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.Write("  |  ");
                        }
                        
                    }
                    Console.ForegroundColor = ConsoleColor.White;

                }

                Console.WriteLine("");
                if(row+1 != schemeInt)
                {
                    if(row == rowBoxes)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkCyan;
                        rowBoxes += sqrtSize;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.White;

                    }
                    for (int j = 0; j < schemeInt * 6 - 4; j++)
                    {
                        Console.Write("-");
                    }
                    Console.WriteLine("");
                }
                
                Console.ForegroundColor = ConsoleColor.White;

            }
            Console.WriteLine("");
        }
    }
        
}
