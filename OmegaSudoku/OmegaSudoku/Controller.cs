using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace OmegaSudoku
{
    public static class Controller
    {

        public static void startSudokuSolver() //this function handles the start and the continuity of the sudoku solver
        {

            bool quitting = false;   //a flag to indicate if the user wants to quit the app
            Console.WriteLine("welcome to the Omega Sudoku Solver\n");
            string[] returnedItems = null;

            while (!quitting)
            {
                string chosenMethod = null;
                try
                {
                    returnedItems = getInput(); //getting the input from the user
                }
                catch(FileDoesntExistException FDEE)
                {
                    Console.WriteLine(FDEE.Message);
                }
                catch(WrongFileTypeException WFTE)
                {
                    Console.WriteLine(WFTE.Message);
                }
               
                if(returnedItems != null)
                {
                    if (returnedItems[1] == "false")    //if we got input and there hasnt been an error
                    {
                        chosenMethod = returnedItems[2];
                        string result = null;
                        try
                        {
                            result = beginSolving(returnedItems[0]);    //solve the board
                        }
                        catch (EmptyBoardDataException EBE)
                        {
                            Console.WriteLine(EBE.Message);
                        }
                        catch (InvalidBoardSizeException IBSE)
                        {
                            Console.WriteLine(IBSE.Message);
                        }
                        catch (InvalidCharacterException ICE)
                        {
                            Console.WriteLine(ICE.Message);
                        }
                        catch (SameNumberInTheSameRowException SNITSRE)
                        {
                            Console.WriteLine(SNITSRE.Message);
                        }
                        catch (SameNumberInTheSameColumnException SNITSCE)
                        {
                            Console.WriteLine(SNITSCE.Message);
                        }
                        catch(UnsolveableBoardException UBE)
                        {
                            Console.WriteLine(UBE.Message);
                        }
                        catch(SameNumberInTheSameBoxException SNITSBE)
                        {
                            Console.WriteLine(SNITSBE.Message);
                        }
                        if(result != null)
                        {
                            if (result == "quit")
                            {
                                quitting = true;
                            }
                            else if (returnedItems[2] == "2")   //if the user wanted to read from a file then append the naswer to that same file
                            {
                                string filePath = returnedItems[3];
                                File.AppendAllText(filePath, ("\nthe answer:\n" + result));
                            }
                        }
                        
                    }
                }
                
            }
            
        }

        public static string[] getInput()   //get the input from the user
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
                    case "1":   //if the user wanted to input from console
                        Console.WriteLine("please enter the sudoku data to start solving or 'quit' to quit\n");
                        Console.SetIn(new StreamReader(Console.OpenStandardInput(8192)));   //a change so we can input more then 253 charecters
                        sudokuData = Console.ReadLine();
                        break;
                    case "2":   //if the user wanted to input from a text file
                        
                        Console.WriteLine("please enter the full path to the sudoku data to start solving or 'quit' to quit\n");
                        filePath = Console.ReadLine();
                        sudokuData = inputFromFile(filePath);
                        if(sudokuData == null)
                        {
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

        public static string inputFromFile(string filePath) //this function gets an input from a text file
        {
            if (!filePath.EndsWith(".txt"))
            {
                throw new WrongFileTypeException("the file type is wrong. it can only be a .txt file");
            }
            else if (!File.Exists(filePath))
            {
                throw new FileDoesntExistException("the file path you enetered is not valid");
            }
            
            string sudokuData = File.ReadAllText(filePath);
            return sudokuData;
        }

        public static string beginSolving(string sudokuData)    //this function begins the solver program
        {
            var watch = new System.Diagnostics.Stopwatch();
            bool errorOccured = false;   //a flag to indicate if an error has been occurred

            watch.Restart();
            watch.Start();
            if (sudokuData != "quit")
            {
                if (sudokuData != null)
                {
                    try
                    {
                        errorOccured = ValidateBoard.validate(sudokuData); //checks for any exceptions
                    }
                    catch (EmptyBoardDataException EBE)
                    {
                        throw EBE;
                    }
                    catch (InvalidBoardSizeException IBSE)
                    {
                        throw IBSE;
                    }
                    catch (InvalidCharacterException ICE)
                    {
                        throw ICE;
                    }
                    catch (SameNumberInTheSameRowException SNITSRE)
                    {
                        throw SNITSRE;
                    }
                    catch (SameNumberInTheSameColumnException SNITSCE)
                    {
                        throw SNITSCE;
                    }
                    catch (SameNumberInTheSameBoxException SNITSBE)
                    {
                        throw SNITSBE;
                    }

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

                    Printer.printSudoku(schemeInt, board);  //prints the original sudoku board

                    Solver solver = new Solver();

                    int[,] result = solver.solve(board, schemeInt);    //a call to the solve method to start solving the board
                    if (result == null) //if the result is null then the board is unsolveable
                    {
                        watch.Stop();
                        throw new UnsolveableBoardException("the board is not valid and cannot be solved");

                    }
                    else
                    {
                        Console.WriteLine("and the answer is:\n");
                        Printer.printSudoku(schemeInt, result); //prints the solved board
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
    }
}
