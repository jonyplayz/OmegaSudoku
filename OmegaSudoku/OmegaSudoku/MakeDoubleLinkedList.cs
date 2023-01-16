using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OmegaSudoku
{
    public static class MakeDoubleLinkedList
    {
        // the method to convert the cover matrix Exact Cover problem to a doubly-linked list, which will allow us to later
        // perform our Dancing Links X algorithm.
        public static ColumnNode createDoubleLinkedLists(byte[,] coverMat, int schemeInt)
        {
            ColumnNode root = new ColumnNode(); // the root is used as an entry-way to the linked list i.e. we access the list through the root
                                                // create the column heads
            ColumnNode curColumn = root;
            for (int col = 0; col < schemeInt * schemeInt * 4; col++) // getting the column heads from the cover matrix and filling in the information about the 
                                                                      // constraints. We iterate for all the column heads,
                                                                      // thus going through all the items in the first row of the cover matrix
            {

                // We create the ColumnID that will store the information. We will later map this ID to the current curColumn
                ColumnID id = new ColumnID();
                id.position = col;

                if (col < 3 * schemeInt * schemeInt)
                {

                    // identifying the digit
                    int digit = (col / (3 * schemeInt)) + 1;
                    id.number = digit;

                    // is it for a row, column or block?
                    int index = col - (digit - 1) * 3 * schemeInt;
                    if (index < schemeInt)
                    {
                        id.constraint = 0; // we're in the row constraint
                    }
                    else if (index < 2 * schemeInt)
                    {
                        id.constraint = 1; // we're in the column constraint
                    }
                    else
                    {
                        id.constraint = 2; // we're in the block constraint
                    }
                }
                else
                {
                    id.constraint = 3; // we're in the cell constraint
                }
                curColumn.right = new ColumnNode();
                curColumn.right.left = curColumn;
                curColumn = (ColumnNode)curColumn.right;
                curColumn.info = id; // the information about the column is set to the new column
                curColumn.head = curColumn;
            }
            curColumn.right = root; // making the list circular i.e. the right-most ColumnHead is linked to the root
            root.left = curColumn;

            // Once all the ColumnHeads are set, we iterate over the entire matrix

            // Iterate over all the rows
            for (int row = 0; row < coverMat.GetLength(0); row++)
            {
                
                curColumn = (ColumnNode)root.right; //this is the current column we are on
                Node lastCreatedElement = null;
                Node firstElement = null;

                // iterator over all the columns
                for (int col = 0; col < coverMat.GetLength(1); col++)
                {
                    if (coverMat[row, col] == 1)  // if the cover matrix element has a 1 i.e. there is a clue here i.e. we were given this value fromt he original board
                    {
                        // create a new data element and link it in the double linked list
                        Node colElement = curColumn;
                        while (colElement.down != null)
                        {
                            colElement = colElement.down;
                        }
                        colElement.down = new Node();
                        if (firstElement == null)
                        {
                            firstElement = colElement.down;
                        }
                        colElement.down.up = colElement;
                        colElement.down.left = lastCreatedElement;
                        colElement.down.head = curColumn;
                        if (lastCreatedElement != null)
                        {
                            colElement.down.left.right = colElement.down;
                        }
                        lastCreatedElement = colElement.down;
                        curColumn.size++;
                    }
                    curColumn = (ColumnNode)curColumn.right;
                }
                // link the first and the last element, again making it circular
                if (lastCreatedElement != null)
                {
                    lastCreatedElement.right = firstElement;
                    firstElement.left = lastCreatedElement;
                }
            }
            curColumn = (ColumnNode)root.right;

            // link the last column elements with the corresponding columnHeads
            for (int col = 0; col < coverMat.GetLength(1); col++)
            {
                Node colElement = curColumn;
                while (colElement.down != null)
                {
                    colElement = colElement.down;
                }
                colElement.down = curColumn;
                curColumn.up = colElement;
                curColumn = (ColumnNode)curColumn.right;
            }

            return root; //return the root of the double linked list
        }
    }
}
