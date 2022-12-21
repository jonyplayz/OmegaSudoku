using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//800000070006010053040600000000080400003000700020005038000000800004050061900002000
//100000027000304015500170683430962001900007256006810000040600030012043500058001000

namespace OmegaSudoku
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("welcome to the Omega Sudoku Solver\nPlease enter the sudoku data bellow");
            string sudokuData = Console.ReadLine();
            foreach (char c in sudokuData)
            {
                if (c > '9' || c < '0')
                {
                    Console.WriteLine("data is not valid");
                    return;
                }
            }
            double scheme = Math.Sqrt(sudokuData.Length);
            if(scheme % 1 != 0)
            {
                Console.WriteLine("data is not valid");
                return;
            }
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
                board[Ycounter,Xcounter] = Convert.ToInt32(c-'0');
                Xcounter++;

            }
            for(int i = 0; i < schemeInt; i++)
            {
                for (int j = 0; j < schemeInt; j++)
                {
                    if(board[i, j] != 0)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write(string.Format("{0}", board[i, j]));
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write(string.Format("{0}", board[i, j]));
                    }
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write("  |  ", board[i, j]);
                }
                Console.WriteLine("");
                for(int j = 0; j < schemeInt*6; j++)
                {
                    Console.Write("-");
                }
                Console.WriteLine("");
            }

            List<int>[,] options = new List<int>[schemeInt, schemeInt];
            for (int i = 0; i < schemeInt; i++)
            {
                for (int j = 0; j < schemeInt; j++)
                {
                    options[i, j] = new List<int>();
                }

            }

            for (int i = 0; i < schemeInt; i++)
            {
                for (int j = 0; j < schemeInt; j++)
                {
                    if(board[i,j] == 0)
                    {
                        options[i,j] = searchOptions(board, i, j, schemeInt);
                    }
                }
                
            }
            //Console.WriteLine(options[0, 1].ToString());
            foreach(int i in options[0, 1])
            {
                Console.WriteLine(i);
            }



        }

        static List<int> searchOptions(int[,] board, int row, int column, int scheme)
        {
            int[] indexes = new int[scheme];
            for (int i = 0; i < indexes.Length; i++)
            {
                //Console.WriteLine(row + " " + column + " " + i);
                //Console.WriteLine(indexes[board[i, column]] + " " + board[i, column]);
                if (board[row, i] != 0)
                {
                    if(indexes[board[row, i]-1] == 0)
                    {
                        indexes[board[row, i]-1] = 1;
                    }
                }
                if (board[i, column] != 0)
                {
                    if (indexes[board[i, column]-1] == 0)
                    {
                        indexes[board[i, column]-1] = 1;
                    }
                }
            }
            List<int> optionsList = new List<int>();
            for(int i = 0; i < indexes.Length; i++)
            {
                if(indexes[i] == 0)
                {
                    optionsList.Add(i + 1);
                }
            }
            return optionsList;
        }




    }
}
