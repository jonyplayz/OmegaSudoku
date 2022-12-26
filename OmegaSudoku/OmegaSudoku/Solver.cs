using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OmegaSudoku
{
    class Solver
    {
        
        public int[,] solve(int schemeInt, int[,] board, List<int>[,] options)
        {
            Boolean emptyFlag = false;
            Boolean placedFlag = false;
            List<int>[,] optionsCopy = (List<int>[,])options.Clone();
            //this is answer for easy 2
            //int[,] test = new int[9, 9] { { 1, 9, 4, 7, 2, 5, 6, 8, 3 }, { 7, 5, 3, 9, 8, 6, 2, 4, 1 }, { 6, 8, 2, 4, 1, 3, 9, 7, 5 }, { 8, 2, 5, 1, 6, 4, 3, 9, 7 }, { 9, 7, 1, 5, 3, 2, 4, 6, 8 }, { 4,3,6,8,7,9,5,1,2},{ 5,6,7,3,4,1,8,2,9},{ 2,1,9,6,5,8,7,3,4},{ 3,4,8,2,9,7,1,5,6} };
            //this is answer for hard given
            int[,] test = new int[9, 9] { {  8,3,1,5,2,9,6,7,4}, {  7,9,6,8,1,4,2,5,3}, {  5,4,2,6,3,7,1,8,9}, {  1,5,9,7,8,3,4,2,6}, { 4,8,3,2,9,6,7,1,5}, {  6,2,7,1,4,5,9,3,8}, {  3,6,5,4,7,1,8,9,2}, {  2,7,4,9,5,8,3,6,1}, {  9,1,8,3,6,2,5,4,7} };

            for (int i = 0; i < schemeInt; i++)
            {
                for (int j = 0; j < schemeInt; j++)
                {
                    if (board[i, j] == 0)
                    {
                        emptyFlag = true;
                        options[i, j] = searchOptions(board, i, j, schemeInt);
                    }
                }

            }

            if (emptyFlag)
            {
                for (int i = 0; i < schemeInt; i++)
                {
                    for (int j = 0; j < schemeInt; j++)
                    {
                        if (options[i, j].Count == 1 && board[i,j]==0)
                        {

                            board[i, j] = options[i, j][0];
                            placedFlag = true;
                            options = resetOptions(schemeInt);
                            Console.WriteLine("printing from single option " + i + " " + j);
                            Printer printer = new Printer();
                            printer.printSudoku(schemeInt, board);

                            if (board[i, j] != test[i, j])
                            {
                                Console.WriteLine("error occured in single op in " + i + " " + j);

                            }
                            else
                            {
                                board = solve(schemeInt, board, options);
                            }
                        }

                    }

                }
            }

            if (!placedFlag && emptyFlag)
            {
                for (int i = 0; i < schemeInt; i++)
                {
                    for (int j = 0; j < schemeInt; j++)
                    {

                        if (board[i, j] == 0)
                        {
                            options[i, j] = searchOptions(board, i, j, schemeInt);
                            int startPoint_row = i - (i % ((int)Math.Sqrt(schemeInt)));
                            int startPoint_column = j - (j % ((int)Math.Sqrt(schemeInt)));
                            int[] toRemove = new int[schemeInt];
                            //Console.WriteLine(startPoint_row + " " + startPoint_column);
                            for (int k = 0; k < ((int)Math.Sqrt(schemeInt)); k++)
                            {
                                for (int n = 0; n < ((int)Math.Sqrt(schemeInt)); n++)
                                {
                                    if (board[startPoint_row + k, startPoint_column + n] == 0)
                                    {
                                        options[(startPoint_row + k), (startPoint_column + n)] = searchOptions(board, (startPoint_row + k), (startPoint_column + n), schemeInt);
                                        foreach (int num in options[(startPoint_row + k), (startPoint_column + n)])
                                        {
                                            if (options[i, j].Contains(num))
                                            {
                                                toRemove[num - 1] = 1;
                                            }
                                        }
                                        for (int y = 0; y < schemeInt; y++)
                                        {
                                            if (toRemove[y] == 1)
                                            {
                                                options[i, j].Remove(y + 1);
                                            }
                                        }
                                    }
                                }
                            }
                            if (options[i, j].Count == 1)
                            {
                                board[i, j] = options[i, j][0];
                                options = resetOptions(schemeInt);
                                Console.WriteLine("printing from around option " + i + " " + j);
                                Printer printer = new Printer();
                                printer.printSudoku(schemeInt, board);
                                placedFlag = true;

                                if (board[i, j] != test[i, j])
                                {
                                    Console.WriteLine("error occured in around op in " + i + " " + j);

                                }
                                else
                                {
                                    board = solve(schemeInt, board, options);
                                }

                                
                                
                                
                            }
                        }
                    }
                }
            }



            if (!placedFlag && emptyFlag)
            {
                //Console.ForegroundColor = ConsoleColor.Red;
                //Console.WriteLine("before option 4 6 ");
                //foreach (int n in options[4, 6])
                //{
                //    Console.WriteLine(n);
                //}
                //Console.WriteLine("after option 4 6 " + board[4, 6]);
                //Console.ForegroundColor = ConsoleColor.White;


                for (int i = 0; i < schemeInt; i++)
                {
                    for (int j = 0; j < schemeInt; j++)
                    {



                        //List<int> holderOptions = options[i, j];
                        int[] toRemove = new int[schemeInt];
                        options = (List<int>[,])optionsCopy.Clone();

                        options[i, j] = searchOptions(board, i, j, schemeInt);


                        //int[] guide = new int[schemeInt];
                        //int guideCounter = 0;
                        //foreach (int n in options[i, j])
                        //{
                        //    guide[n - 1] = 1;
                        //    guideCounter++;
                        //}
                        //for (int k = 0; k < schemeInt; k++)
                        //{
                        //    Console.WriteLine(guide[k]);
                        //}
                        //Console.WriteLine("bruh");
                        for (int k = 0; k < schemeInt; k++)
                        {
                            if (board[i, k] == 0 && k != j)
                            {

                                options[i, k] = searchOptions(board, i, k, schemeInt);
                                if (searchHidden(options, i, k, board) != null)
                                {
                                    List<List<int>> resultHolder = searchHidden(options, i, k, board);
                                    options[i, j] = resultHolder[0];
                                    int foundIndexX = resultHolder[2][0];
                                    int foundIndexY = resultHolder[2][1];
                                    options[foundIndexX, foundIndexY] = resultHolder[1];
                                }

                                foreach (int l in options[3, 5])
                                {
                                    Console.WriteLine(l + " " + 3 + " " + 5);
                                }
                                foreach (int n in options[i, k])
                                {
                                    //Console.WriteLine(n + " " + i + " " + k);
                                    if (options[i, j].Contains(n))
                                    {
                                        toRemove[n - 1] = 1;
                                    }
                                }
                                Console.WriteLine("---------------");
                                
                                //Console.WriteLine("\n");
                            }
                        }
                        for (int y = 0; y < schemeInt; y++)
                        {
                            //Console.Write(toRemove[y] + " ");
                            if (toRemove[y] == 1)
                            {

                                options[i, j].Remove(y + 1);
                            }
                        }
                        //Console.WriteLine("\n");
                        if (options[i, j].Count == 1)
                        {
                            board[i, j] = options[i, j][0];
                            //Console.WriteLine("this is i and j " + i + " " + j);
                            //foreach (int n in options[i, j])
                            //{
                            //    Console.WriteLine(n);
                            //}
                            options = resetOptions(schemeInt);
                            Console.WriteLine("printing from row option " + i + " " + j);
                            Printer printer = new Printer();
                            printer.printSudoku(schemeInt, board);
                            placedFlag = true;
                            if (board[i, j] != test[i, j])
                            {
                                Console.WriteLine("error occured in row op in " + i + " " + j);

                            }
                            else
                            {
                                board = solve(schemeInt, board, options);
                            }


                           
                            
                        }
                    }
                }
            }
            if(!placedFlag && emptyFlag)  //------------------------------------------------------------------------------------------------
            {
               
                for (int i = 0; i < schemeInt; i++)
                {
                    for (int j = 0; j < schemeInt; j++)
                    {
                        options = (List<int>[,])optionsCopy.Clone();
                        int[] toRemove = new int[schemeInt];

                        options[i, j] = searchOptions(board, i, j, schemeInt);

                        //guide = new int[schemeInt];
                        //guideCounter = 0;
                        //foreach (int n in options[i, j])
                        //{
                        //    guide[n - 1] = 1;
                        //    guideCounter++;
                        //}
                        for (int k = 0; k < schemeInt; k++)
                        {
                            if (board[k, j] == 0 && k != i)
                            {
                                options[k, j] = searchOptions(board, k, j, schemeInt);
                                if (searchHidden(options, k, j, board) != null)
                                {
                                    List<List<int>> resultHolder = searchHidden(options, k, j, board);
                                    options[i, j] = resultHolder[0];
                                    int foundIndexX = resultHolder[2][0];
                                    int foundIndexY = resultHolder[2][1];
                                    options[foundIndexX, foundIndexY] = resultHolder[1];
                                }
                                foreach (int l in options[k, j])
                                {
                                    //Console.WriteLine(l);
                                }
                                
                                foreach (int n in options[k, j])
                                {
                                    if (options[i, j].Contains(n))
                                    {
                                        toRemove[n - 1] = 1;
                                    }
                                    //if (guide[n - 1] == 1)
                                    //{
                                    //    guide[n - 1] = 0;
                                    //    guideCounter--;
                                    //}
                                }
                            }
                        }
                        for (int y = 0; y < schemeInt; y++)
                        {
                            if (toRemove[y] == 1)
                            {

                                options[i, j].Remove(y + 1);
                            }
                        }
                        if (options[i, j].Count == 1)
                        {
                            //for (int k = 0; k < schemeInt; k++)
                            //{
                            //    Console.WriteLine(guide[k]);
                            //}

                            board[i, j] = options[i, j][0];
                            options = resetOptions(schemeInt);
                            Console.WriteLine("printing from column option " + i + " " + j);
                            Printer printer = new Printer();
                            printer.printSudoku(schemeInt, board);
                            placedFlag = true;
                            if (board[i, j] != test[i, j])
                            {
                                Console.WriteLine("error occured in column op in " + i + " " + j);

                            }
                            else
                            {
                                board = solve(schemeInt, board, options);
                            }

                          
                            
                            


                        }
                    }
                }
            }
            //if (!placedFlag && emptyFlag)
            //{
            //    for (int i = 0; i < schemeInt; i++)
            //    {
            //        for (int j = 0; j < schemeInt; j++)
            //        {
            //            for (int k = 0; k < schemeInt; k++)
            //            {
            //                if (searchHidden(options, i, k, board) != null)
            //                {
            //                    List<List<int>> resultHolder = searchHidden(options, k, j, board);
            //                    options[i, j] = resultHolder[0];
            //                    int foundIndexX = resultHolder[2][0];
            //                    int foundIndexY = resultHolder[2][1];
            //                    options[foundIndexX, foundIndexY] = resultHolder[1];
            //                }
            //            }
            //        }
            //    }
            //}

            return board;
        }

        static List<int> searchOptions(int[,] board, int row, int column, int scheme)
        {
            int[] indexes = new int[scheme];
            int startPoint_row = row - (row % ((int)Math.Sqrt(scheme)));
            int startPoint_column = column - (column % ((int)Math.Sqrt(scheme)));
            for (int i = 0; i < ((int)Math.Sqrt(scheme)); i++)
            {
                for (int j = 0; j < ((int)Math.Sqrt(scheme)); j++)
                {
                    if(board[startPoint_row + i, startPoint_column + j] != 0)
                    {
                        if (indexes[board[startPoint_row + i, startPoint_column + j] - 1] == 0)
                        {
                            indexes[board[startPoint_row + i, startPoint_column + j] - 1] = 1;
                        }
                    }
                }
            }


            for (int i = 0; i < indexes.Length; i++)
            {
                if (board[row, i] != 0)
                {
                    if (indexes[board[row, i] - 1] == 0)
                    {
                        indexes[board[row, i] - 1] = 1;
                    }
                }
                if (board[i, column] != 0)
                {
                    if (indexes[board[i, column] - 1] == 0)
                    {
                        indexes[board[i, column] - 1] = 1;
                    }
                }
            }
            //if(indexes[0] == 0)
            //{
            //    Console.WriteLine("this is indexes:");
            //    for (int i = 0; i < indexes.Length; i++)
            //    {
            //        Console.WriteLine(indexes[i]);
            //    }
                
            //    Console.WriteLine("-------------------");
            //}
            

            List<int> optionsList = new List<int>();
            for (int i = 0; i < indexes.Length; i++)
            {
                if (indexes[i] == 0)
                {
                    optionsList.Add(i + 1);
                }
            }
            //Console.WriteLine(row + " " + column);
            //    foreach (int n in optionsList)
            //    {
            //        Console.WriteLine(n);
            //    }
            //    Console.WriteLine("-------------------");

            

            return optionsList;
        }

        static List<List<int>> searchHidden(List<int>[,] options, int row, int column, int[,] board)
        {
            int scheme = board.GetLength(0);
            //int starterOptions = options[row, column].Count()-1;
            //int dupCounter = 0;
            List<int> dupNumbers = new List<int>();
            Boolean matched = true;
            
                int startPoint_row = row - (row % ((int)Math.Sqrt(scheme)));
                int startPoint_column = column - (column % ((int)Math.Sqrt(scheme)));
                for (int i = 0; i < ((int)Math.Sqrt(scheme)); i++)
                {
                    for (int j = 0; j < ((int)Math.Sqrt(scheme)); j++)
                    {
                        if (board[startPoint_row + i, startPoint_column + j] != 0 && (startPoint_row + i != row || startPoint_column + j != column))
                        {
                            foreach(int num1 in options[row, column])
                            {
                                foreach(int num2 in options[startPoint_row + i, startPoint_column + j])
                                {
                                    if(num1 == num2)
                                    {
                                        dupNumbers.Add(num1);
                                    }
                                }
                            }
                            if(dupNumbers.Count() == 2)
                            {
                                for (int n1 = 0; n1 < ((int)Math.Sqrt(scheme)); n1++)
                                {
                                    for (int n2 = 0; n2 < ((int)Math.Sqrt(scheme)); n2++)
                                    {
                                        if (board[startPoint_row + n1, startPoint_column + n2] != 0 && (startPoint_row + n1 != row || startPoint_column + n2 != column) && (n1 != i || n2 != j))
                                        {
                                            foreach(int num in options[startPoint_row + n1, startPoint_column + n2])
                                            {
                                                dupNumbers.Remove(num);
                                            }
                                            if(dupNumbers.Count() != 2)
                                            {
                                                matched = false;
                                            }

                                        }
                                    }
                                }
                                if (matched)
                                {
                                    int[] numbersToRemove1 = new int[scheme];
                                    int[] numbersToRemove2 = new int[scheme];
                                    foreach (int num in options[row, column])
                                    {
                                        if (!dupNumbers.Contains(num))
                                        {
                                            numbersToRemove1[num - 1] = 1;
                                        }
                                    }
                                    foreach (int num in options[startPoint_row + i, startPoint_column + j])
                                    {
                                        if (!dupNumbers.Contains(num))
                                        {
                                            numbersToRemove2[num - 1] = 1;
                                        }
                                    }
                                    for (int indexNumToRemove = 0; indexNumToRemove < scheme; indexNumToRemove++)
                                    {
                                        if(numbersToRemove1[indexNumToRemove] == 1)
                                        {
                                            options[row, column].Remove(indexNumToRemove + 1);
                                        }
                                        if (numbersToRemove2[indexNumToRemove] == 1)
                                        {
                                            options[startPoint_row + i, startPoint_column + j].Remove(indexNumToRemove + 1);
                                        }
                                    }
                                List<int> temp = new List<int>() { startPoint_row + i, startPoint_column + j };
                                foreach(int kaka in options[row, column])
                                {
                                    Console.WriteLine(kaka + " " + row + " " + column + " yeet");
                                }
                                Console.WriteLine("ok");
                                    return new List<List<int>> { options[row, column], options[startPoint_row + i, startPoint_column + j] , temp };
                                }
                            }
                        }
                    }
                }
            return null;
        }




        static List<int>[,] resetOptions(int schemeInt)
        {
            List<int>[,] options = new List<int>[schemeInt, schemeInt];
            for (int i = 0; i < schemeInt; i++)
            {
                for (int j = 0; j < schemeInt; j++)
                {
                    options[i, j] = new List<int>();
                }

            }

            return options;
        }
    }
}
