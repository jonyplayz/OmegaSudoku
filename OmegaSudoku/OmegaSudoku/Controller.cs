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
    public class Controller
    {

        public void startSudokuSolver() //this function handles the start and the continuity of the sudoku solver
        {

            Boolean quitting = false;   //a flag to indicate if the user wants to quit the app
            Console.WriteLine("welcome to the Omega Sudoku Solver\n");

            while (!quitting)
            {
                string chosenMethod = null;
                string[] returnedItems = getInput();
                if(returnedItems[1] == "false")
                {
                    chosenMethod = returnedItems[2];
                    string result = beginSolving(returnedItems[0]);
                    if(result == "quit")
                    {
                        quitting = true;
                    }
                    else if(result != null && returnedItems[2] == "2")
                    {
                        string filePath = returnedItems[3];
                        File.AppendAllText(filePath, ("\nthe answer:\n" + result));
                    }
                }
            }
            
        }




        public string beginSolving(string sudokuData)
        {
            var watch = new System.Diagnostics.Stopwatch();
            Boolean errorOccured = false;   //a flag to indicate if an error has been occurred

            watch.Restart();
            watch.Start();
            if (sudokuData != "quit")
            {
                if (sudokuData != null)
                {
                    Exceptions exceptions = new Exceptions();
                    errorOccured = exceptions.checkForExceptions(sudokuData);   //checks for any exceptions
                }

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

                    Solver solver = new Solver();
                    //testDLX tdlx = new testDLX();
                    //result = tdlx.run(board, schemeInt);
                    int[,] result = solver.solve(board, schemeInt);    //a call to the solve method to start solving the board
                    if (result == null) //if the result is null then the board is unsolveable
                    {
                        watch.Stop();
                        Console.WriteLine("the board is not valid and cannot be solved");
                        return null;
                    }
                    else
                    {
                        Console.WriteLine("and the answer is:");
                        printer.printSudoku(schemeInt, result); //prints the solved board
                        watch.Stop();
                        Console.WriteLine($"Execution Time: {watch.ElapsedMilliseconds} ms\n");   //prints how many miliseconds it took to solve
                       
                        string resultInString = "";
                        for (int i = 0; i < schemeInt; i++) //inputs the solved board into a string in a sudoku data form
                        {
                            for (int j = 0; j < schemeInt; j++)
                            {
                                resultInString += Convert.ToChar(result[i, j] + '0');
                            }

                        }

                        Console.WriteLine("this is the input in format " + resultInString);
                        return resultInString;
                    }

                }
                return null;

            }
            else
            {
            
                Console.WriteLine("thank you for using OmegaSudoku Solver, goodbye");
                    return "quit";
            }
        }


        public string[] getInput()
        {

            string[] returnItems = new string[4];
            Boolean errorOccured = false;   //a flag to indicate if an error has been occurred
            string sudokuData = null;
            string filePath = "";

            Console.WriteLine("please choose the method of input for the sudoku data\n1 - from the console\n2 - from a text file\n'quit' to quit");
            string chosenMethod = Console.ReadLine();
            if (chosenMethod == "quit")
            {
                sudokuData = "quit";
            }
            else
            {
                switch (chosenMethod)
                {
                    case "1":
                        Console.WriteLine("please enter the sudoku data to start solving or 'quit' to quit\n");
                        Console.SetIn(new StreamReader(Console.OpenStandardInput(8192)));   //a change so we can input more then 253 charecters
                        sudokuData = Console.ReadLine();
                        break;
                    case "2":
                        Console.WriteLine("please enter the full path to the sudoku data to start solving or 'quit' to quit\n");
                        filePath = Console.ReadLine();
                        if (File.Exists(filePath) && filePath.EndsWith(".txt"))
                        {
                            Console.WriteLine("keep in mind that the answer will be written in the same text file");
                            sudokuData = File.ReadAllText(filePath);
                        }
                        else
                        {
                            Console.WriteLine("the file path you enetered is not valid, please ensure that the path is valid and that the fileis a .txt file");
                            errorOccured = true;
                        }
                        break;
                    case "quit":
                        sudokuData = "quit";
                        break;
                    default:
                        Console.WriteLine("you entered an invalid input, please try again");
                        errorOccured = true;
                        break;
                }
            }
            returnItems[0] = sudokuData;
            if (errorOccured)
            {
                returnItems[1] = "true";
            }
            else
            {
                returnItems[1] = "false";
            }
            returnItems[2] = chosenMethod;
            returnItems[3] = filePath;
            return returnItems;
        }


        //public void startSudokuSolver() //this function handles the start and the continuity of the sudoku solver
        //{
        //    var watch = new System.Diagnostics.Stopwatch();
            
        //    Boolean quitting = false;   //a flag to indicate if the user wants to quit the app
        //    Boolean errorOccured = false;   //a flag to indicate if an error has been occurred
        //    Console.WriteLine("welcome to the Omega Sudoku Solver\n");
            
        //    while (!quitting)
        //    {
        //        errorOccured = false;
        //        string sudokuData = null;
        //        string filePath = "";

        //        Console.WriteLine("please choose the method of input for the sudoku data\n1 - from the console\n2 - from a text file\n'quit' to quit");
        //        string chosenMethod = Console.ReadLine();
        //        if(chosenMethod == "quit")
        //        {
        //            sudokuData = "quit";
        //        }
        //        else
        //        {
        //            switch (chosenMethod)
        //            {
        //                case "1":
        //                    Console.WriteLine("please enter the sudoku data to start solving or 'quit' to quit\n");
        //                    Console.SetIn(new StreamReader(Console.OpenStandardInput(8192)));   //a change so we can input more then 253 charecters
        //                    sudokuData = Console.ReadLine();
        //                    break;
        //                case "2":
        //                    Console.WriteLine("please enter the full path to the sudoku data to start solving or 'quit' to quit\n");
        //                    filePath = Console.ReadLine();
        //                    if (File.Exists(filePath) && filePath.EndsWith(".txt"))
        //                    {
        //                        Console.WriteLine("keep in mind that the answer will be written in the same text file");
        //                        sudokuData = File.ReadAllText(filePath);
        //                    }
        //                    else
        //                    {
        //                        Console.WriteLine("the file path you enetered is not valid, please ensure that the path is valid and that the fileis a .txt file");
        //                        errorOccured = true;
        //                    }
        //                    break;
        //                case "quit":
        //                    sudokuData = "quit";
        //                    break;
        //                default:
        //                    Console.WriteLine("you entered an invalid input, please try again");
        //                    errorOccured = true;
        //                    break;
        //            }
        //        }
                
                
        //        watch.Restart();
        //        watch.Start();
        //        if (sudokuData != "quit")
        //        {
        //            if(sudokuData != null)
        //            {
        //                Exceptions exceptions = new Exceptions();
        //                errorOccured = exceptions.checkForExceptions(sudokuData);   //checks for any exceptions
        //            }
                    
        //            if (!errorOccured)  //coverts the sudoku data into a sudoku board
        //            {
        //                double scheme = Math.Sqrt(sudokuData.Length);
        //                int schemeInt = Convert.ToInt32(scheme);
        //                int[,] board = new int[schemeInt, schemeInt];
        //                int Xcounter = 0;
        //                int Ycounter = 0;
        //                foreach (char c in sudokuData)
        //                {
        //                    if (Xcounter == scheme)
        //                    {
        //                        Xcounter = 0;
        //                        Ycounter++;
        //                    }
        //                    board[Ycounter, Xcounter] = Convert.ToInt32(c - '0');
        //                    Xcounter++;

        //                }

        //                Printer printer = new Printer();
        //                printer.printSudoku(schemeInt, board);  //prints the original sudoku board

        //                Solver solver = new Solver();
        //                //testDLX tdlx = new testDLX();
        //                //result = tdlx.run(board, schemeInt);
        //                int[,]  result = solver.solve(board, schemeInt);    //a call to the solve method to start solving the board
        //                if (result == null) //if the result is null then the board is unsolveable
        //                {
        //                    watch.Stop();
        //                    Console.WriteLine("the board is not valid and cannot be solved");
        //                }
        //                else
        //                {
        //                    Console.WriteLine("and the answer is:");
        //                    printer.printSudoku(schemeInt, result); //prints the solved board
        //                    watch.Stop();
        //                    Console.WriteLine($"Execution Time: {watch.ElapsedMilliseconds} ms\n");   //prints how many miliseconds it took to solve
        //                    if(chosenMethod == "2") //if we want to write the answer in the txt file
        //                    {
        //                        string resultInString = "";
        //                        for (int i = 0; i < schemeInt; i++) //inputs the solved board into a string in a sudoku data form
        //                        {
        //                            for (int j = 0; j < schemeInt; j++)
        //                            {
        //                                resultInString += Convert.ToChar(result[i, j] + '0');
        //                            }

        //                        }
        //                        File.AppendAllText(filePath, ("\nthe answer:\n" + resultInString));
        //                    }
                            
        //                }
        //            }
                    
        //        }
        //        else
        //        {
        //            quitting = true;
        //            Console.WriteLine("thank you for using OmegaSudoku Solver, goodbye");
        //        }
        //    }
        //}
        
    }
}
