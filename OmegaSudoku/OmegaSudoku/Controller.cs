using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//800000070006010053040600000000080400003000700020005038000000800004050061900002000 HARD
//100000027000304015500170683430962001900007256006810000040600030012043500058001000 EASY
//800500674796010253040600189009080400083000700027005938000000892274050361908002547
//800000070006010053040600000000080400003000700020005038000000800004050061900002040
//000000000000000000000000000000000000000000000000000000000000000000000000000000000
//094000600053986041082013975000160307900002000030000012560041000010000700300290050 EASY

namespace OmegaSudoku
{
    class Controller
    {
        public void startSudokuSolver() //this function handles the start and the continuity of the sudoku solver
        {
            var watch = new System.Diagnostics.Stopwatch();
            
            Boolean quitting = false;   //a flag to indicate if the user wants to quit the app
            Boolean errorOccured = false;   //a flag to indicate if an error has been occurred
            Console.WriteLine("welcome to the Omega Sudoku Solver\n");
            
            while (!quitting)
            {
                Console.WriteLine("please enter the sudoku data to start solving or 'quit' to quit\n");
                errorOccured = false;
                Console.SetIn(new StreamReader(Console.OpenStandardInput(8192)));   //a change so we can input more then 253 charecters
                string sudokuData = Console.ReadLine();
                watch.Restart();
                watch.Start();
                if (sudokuData != "quit")
                {
                    Exceptions exceptions = new Exceptions();
                    errorOccured = exceptions.checkForExceptions(sudokuData);   //checks for any exceptions

                    if (!errorOccured)  //coverts the sudoku data into a sudoku board
                    {
                        double scheme = Math.Sqrt(sudokuData.Length);
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
                        printer.printSudoku(schemeInt, board);  //prints the original sudoku board

                        int[,] result = new int[schemeInt, schemeInt];  //the solved board
                        Solver solver = new Solver();
                        testDLX tdlx = new testDLX();
                        //result = tdlx.run(board, schemeInt);
                        result = solver.solve(board, schemeInt);    //a call to the solve method to start solving the board
                        if (result == null) //if the result is null then the board is unsolveable
                        {
                            watch.Stop();
                            Console.WriteLine("the board is not valid and cannot be solved");
                        }
                        else
                        {
                            Console.WriteLine("and the answer is:");
                            printer.printSudoku(schemeInt, result); //prints the solved board
                            watch.Stop();
                            Console.WriteLine($"Execution Time: {watch.ElapsedMilliseconds} ms");   //prints how many miliseconds it took to solve
                            string resultInString = "";
                            for (int i = 0; i < schemeInt; i++) //inputs the solved board into a string in a sudoku data form
                            {
                                for (int j = 0; j < schemeInt; j++)
                                {
                                    resultInString += Convert.ToChar(result[i, j] + '0');
                                }

                            }
                        }
                    }
                    
                }
                else
                {
                    quitting = true;
                    Console.WriteLine("thank you for using OmegaSudoku Solver, goodbye");
                }
            }
        }
        
    }
}
