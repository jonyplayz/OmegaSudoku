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
                        if (options[i, j].Count == 1)
                        {
                            board[i, j] = options[i, j][0];
                            options = resetOptions(schemeInt);

                            board = solve(schemeInt, board, options);
                        }

                    }

                }
            }
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
