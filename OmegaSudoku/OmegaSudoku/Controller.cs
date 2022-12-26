using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//800000070006010053040600000000080400003000700020005038000000800004050061900002000 HARD
//100000027000304015500170683430962001900007256006810000040600030012043500058001000 EASY
//800500674796010253040600189009080400083000700027005938000000892274050361908002547
//800000070006010053040600000000080400003000700020005038000000800004050061900002040

//094000600053986041082013975000160307900002000030000012560041000010000700300290050 EASY

namespace OmegaSudoku
{
    class Controller
    {
        public void startSudokuSolver()
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
            if (scheme % 1 != 0)
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
                board[Ycounter, Xcounter] = Convert.ToInt32(c - '0');
                Xcounter++;

            }

            Printer printer = new Printer();
            printer.printSudoku(schemeInt, board);



            List<int>[,] options = new List<int>[schemeInt, schemeInt];
            for (int i = 0; i < schemeInt; i++)
            {
                for (int j = 0; j < schemeInt; j++)
                {
                    options[i, j] = new List<int>();
                }

            }

            int[,] result = new int[schemeInt, schemeInt];
            Solver solver = new Solver();
            result = solver.solve(schemeInt, board, options);
            Console.WriteLine("and the answer is:");
            printer.printSudoku(schemeInt, result);

            

            
        }
        
    }
}
