using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OmegaSudoku
{
    class Printer
    {
        public void printSudoku(int schemeInt,int[,] board) //this method prints out a given sudoku board, it uses colors to display which numbers are have been inputted and which are not
        {
            for (int i = 0; i < schemeInt; i++)
            {
                int boxes = (int)Math.Sqrt(schemeInt) - 1;
                for (int j = 0; j < schemeInt; j++)
                {
                    if (board[i, j] != 0)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write(string.Format("{0}", Convert.ToChar(board[i, j] + '0')));
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write(string.Format("{0}", Convert.ToChar(board[i, j] + '0')));
                    }

                    if(j == boxes)
                    {
                        Console.ForegroundColor = ConsoleColor.Gray;
                        Console.Write("  |  ");
                        boxes += 3;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write("  |  ");
                    }
                    Console.ForegroundColor = ConsoleColor.White;
                }

                Console.WriteLine("");
                for (int j = 0; j < schemeInt * 6; j++)
                {
                    Console.Write("-");
                }
                Console.WriteLine("");
            }
            Console.WriteLine("");
        }
    }
        
}
