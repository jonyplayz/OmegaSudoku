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

            for (int i = 0; i < schemeInt; i++)
            {
                for (int j = 0; j < schemeInt; j++)
                {
                    if (board[i, j] == 0)
                    {
                        emptyFlag = true;
                        options[i, j] = searchOptions(board, i, j, schemeInt);
                        if(options[i,j].Count() == 0)
                        {
                            return null;
                        }
                    }
                }

            }

            if (emptyFlag)
            {
                for (int i = 0; i < schemeInt; i++)
                {
                    for (int j = 0; j < schemeInt; j++)
                    {
                        options[i, j] = searchOptions(board, i, j, schemeInt);
                        if (options[i, j].Count == 1 && board[i,j]==0)
                        {

                            board[i, j] = options[i, j][0];
                            placedFlag = true;
                            //Console.WriteLine("printing from single option " + i + " " + j);
                            //Printer printer = new Printer();
                            //printer.printSudoku(schemeInt, board);

                            board = solve(schemeInt, board, options);
                            if(board == null)
                            {
                                return null;
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
                                //Console.WriteLine("printing from around option " + i + " " + j);
                                //Printer printer = new Printer();
                                //printer.printSudoku(schemeInt, board);
                                placedFlag = true;

  
                                board = solve(schemeInt, board, options);
                                if (board == null)
                                {
                                    return null;
                                }

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
                        int[] toRemove = new int[schemeInt];
                        options[i, j] = searchOptions(board, i, j, schemeInt);

                        for (int k = 0; k < schemeInt; k++)
                        {
                            if (board[i, k] == 0 && k != j)
                            {

                                options[i, k] = searchOptions(board, i, k, schemeInt);
                                foreach (int n in options[i, k])
                                {
                                    if (options[i, j].Contains(n))
                                    {
                                        toRemove[n - 1] = 1;
                                    }
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
                            board[i, j] = options[i, j][0];
                            //Console.WriteLine("printing from row option " + i + " " + j);
                            //Printer printer = new Printer();
                            //printer.printSudoku(schemeInt, board);
                            placedFlag = true;

                            board = solve(schemeInt, board, options);

                            if (board == null)
                            {
                                return null;
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
                        int[] toRemove = new int[schemeInt];

                        options[i, j] = searchOptions(board, i, j, schemeInt);

                        for (int k = 0; k < schemeInt; k++)
                        {
                            if (board[k, j] == 0 && k != i)
                            {
                                options[k, j] = searchOptions(board, k, j, schemeInt);
                                
                                foreach (int n in options[k, j])
                                {
                                    if (options[i, j].Contains(n))
                                    {
                                        toRemove[n - 1] = 1;
                                    }
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

                            board[i, j] = options[i, j][0];
                            //Console.WriteLine("printing from column option " + i + " " + j);
                            //Printer printer = new Printer();
                            //printer.printSudoku(schemeInt, board);
                            placedFlag = true;

                            board = solve(schemeInt, board, options);
                            if(board == null)
                            {
                                return null;
                            }

                          
                            
                            


                        }
                    }
                }
            }

            if (!placedFlag && emptyFlag)
            {
                int[] bestOptionsIndex = new int[2];
                int minimalOptions = schemeInt;
                for (int i = 0; i < schemeInt; i++)
                {
                    for (int j = 0; j < schemeInt; j++)
                    {
                        if(board[i,j] == 0)
                        {
                            
                            options[i, j] = searchOptions(board, i, j, schemeInt);
                            if (options[i,j].Count() < minimalOptions)
                            {
                                minimalOptions = options[i, j].Count();
                                bestOptionsIndex[0] = i;
                                bestOptionsIndex[1] = j;
                            }
                        }
                    }
                }

                int[,] copyBoard = new int[schemeInt, schemeInt];
                for (int i = 0; i < schemeInt; i++)
                {
                    for (int j = 0; j < schemeInt; j++)
                    {
                        copyBoard[i, j] = board[i, j];
                    }
                }

                for (int optionIndex = 0; optionIndex < options[bestOptionsIndex[0], bestOptionsIndex[1]].Count(); optionIndex++)
                {
                    int num = options[bestOptionsIndex[0], bestOptionsIndex[1]][optionIndex];
                    
                    
                    board[bestOptionsIndex[0], bestOptionsIndex[1]] = num;
                    if (solve(schemeInt, board, options) == null)
                    {
                        for (int i = 0; i < schemeInt; i++)
                        {
                            for (int j = 0; j < schemeInt; j++)
                            {
                                board[i, j] = copyBoard[i, j];
                            }
                        }
                        for (int i = 0; i < schemeInt; i++)
                        {
                            for (int j = 0; j < schemeInt; j++)
                            {
                                options[i, j] = searchOptions(board, i, j, schemeInt);
                            }
                        }
                    }
                    
                }
                for (int i = 0; i < schemeInt; i++)
                {
                    for (int j = 0; j < schemeInt; j++)
                    {
                        if(board[i,j] == 0)
                        {
                            return null;
                        }
                    }
                }
                return board;

               


            }
            return board;
        }

        static List<int> searchOptions(int[,] board, int row, int column, int scheme)
        {
            int[] indexes = new int[scheme];
            int sqrtScheme = (int)Math.Sqrt(scheme);
            int startPoint_row = row - (row % (sqrtScheme));
            int startPoint_column = column - (column % (sqrtScheme));
            for (int i = 0; i < (sqrtScheme); i++)
            {
                for (int j = 0; j < (sqrtScheme); j++)
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
            

            List<int> optionsList = new List<int>();
            for (int i = 0; i < indexes.Length; i++)
            {
                if (indexes[i] == 0)
                {
                    optionsList.Add(i + 1);
                }
            }
            return optionsList;
        }

    }
}
