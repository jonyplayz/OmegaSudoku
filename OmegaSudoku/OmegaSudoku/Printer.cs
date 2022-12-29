using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OmegaSudoku
{
    class Printer
    {
        public void printSudoku(int schemeInt,int[,] board)
        {
            for (int i = 0; i < schemeInt; i++)
            {
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
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write("  |  ");
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
