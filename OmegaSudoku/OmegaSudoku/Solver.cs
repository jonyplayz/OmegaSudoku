using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OmegaSudoku
{
    class Solver
    {
        public int[,] solve(int[,] initialMatrix, int schemeInt)
        {
            byte[,] coverMatrix = MakeCoverMatrix.createCoverMat(initialMatrix, schemeInt); //this function creates the cover matrix which is a matrix that represents
            //all the constrains that need to be satisfied in a sudoku board for each cell in the sudoku board, having 1 on costrains that are satisfied
            //and 0 on those that are not satisfied
            

            
            ColumnNode doubleLinkedList = MakeDoubleLinkedList.createDoubleLinkedLists(coverMatrix, schemeInt);   // create the circular doubly-linked toroidal list from the cover matrix
            DLXAlgorithm DLXA = new DLXAlgorithm();
            DLXA.search(0, schemeInt, doubleLinkedList);// start the Dancing Links process of searching and covering and uncovering recursively

            return DLXA.board;



        }

    }
}
