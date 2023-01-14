using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OmegaSudoku
{
    public static class MakeCoverMatrix
    {
        public static byte[,] createCoverMat(int[,] iniMat, int size)   //this method creates the cover matrix while considering the clues from the original board
        {
            // initialize the cover matrix
            byte[,] coverMatrix = new byte[size * size * size, size * size * 4];
            int sqrtSize = (int)Math.Sqrt(size);
            for (int row = 0; row < size; row++)    //go over all the rows
            {
                for (int col = 0; col < size; col++)    //go over all the columns
                {
                    for (int number = 0; number < size; number++)   //go over all the possible numbers
                    {

                        if (iniMat[row, col] == 0 || iniMat[row, col] == number + 1)    //if this is a possible number in the current cell in the original board
                                                                                        //or if its the only option for the cell
                        {
                            // the row index to cover
                            int rowPlace = row * size * size + col * size + number;

                            // setting the cover matrix according to the calculations
                            coverMatrix[rowPlace, row * size + col] = 1;

                            coverMatrix[rowPlace, size * size + row * size + number] = 1;

                            coverMatrix[rowPlace, size * size * 2 + col * size + number] = 1;

                            coverMatrix[rowPlace, size * size * 3 + (row / sqrtSize * sqrtSize + col / sqrtSize) * size + number] = 1;
                        }
                    }
                }
            }
            return coverMatrix;
        }
    }
}
