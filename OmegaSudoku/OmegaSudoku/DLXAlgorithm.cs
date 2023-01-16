using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OmegaSudoku
{
    public class DLXAlgorithm
    {
        public int[,] board; //this is the solved sudoku board
        Boolean isSolved = false; //this is a flag that marks if we solved the board already or not. this is used to optimize the 'search' function
        ArrayList solution = new ArrayList(); //this is the arraylist that saves the solution nodes from the 'search' function

        public void search(int k, int schemeInt, ColumnNode root) //this is the recursive method that is the core of the dancing links (DLX) algorithm.
                                                                  //it searches for the exact cover in the double linked lists.
        {
            if (root.right == root) //if we covered all the columns of the toridol matrix succesfully then we found the exact cover and the solution to the sudoku board.
            {
                mapSolvedToGrid(schemeInt, solution); //map the solution arraylist into a regular sudoku board.
                isSolved = true;
                return;
            }

            if (isSolved) //skip if we solved the board
            {
                return;
            }
            ColumnNode chosenCol = choose(root);    //chooses the best column to start covering from
            Node firstNodeOfCol = chosenCol.down; //goes to the first node of the column
            cover(chosenCol); //covering the chosen column
            while (firstNodeOfCol != chosenCol)
            {
                solution.Add(firstNodeOfCol); //adding the solution nodes to the arraylist
                Node nextNode = firstNodeOfCol.right; //getting the node to the right of the current node
                while (nextNode != firstNodeOfCol) //covering all the nodes in the same row as the current node
                {
                    cover(nextNode.head);
                    nextNode = nextNode.right;
                }
                search(k + 1, schemeInt, root); //calling the search function again to continue to discover the toridoal list

                firstNodeOfCol = (Node)solution[k]; //if we didnt find the exact cover we need to backtrack so we remove the node from the solution list and start to uncover everything that we covered
                solution.RemoveAt(k);
                chosenCol = firstNodeOfCol.head;
                nextNode = firstNodeOfCol.left;
                while (nextNode != firstNodeOfCol)
                {
                    uncover(nextNode.head);
                    nextNode = nextNode.left;
                }
                firstNodeOfCol = firstNodeOfCol.down;

            }
            uncover(chosenCol);
            return;


        }


        // this allows us to map the solved linked list to the Grid
        public void mapSolvedToGrid(int schemeInt, ArrayList solution)
        {

            int[,] solvedBoard = new int[schemeInt, schemeInt]; //the solved board
            if (solution == null)
            {
                solvedBoard = null;
                return;
            }

            for (int i = 0; i < solution.Count; i++)  // we iterate over every single element of the ArrayList
                                                      // we stop iterating once we run out of elements in the list
            {
                

                Node element = (Node)solution[i]; //getting the current node in the solution arraylist
                Node next = element;

                int smallestColPos = schemeInt * schemeInt * 4; 
                while (next.head.info.position < smallestColPos)    //getting the left most node in the current row
                {
                    smallestColPos = next.head.info.position;
                    next = next.left;
                }
                next = next.right;

                int row = next.head.info.position / schemeInt;  //calculate the row index
                int col = next.head.info.position % schemeInt;  //calculate the column index
                int number = next.right.head.info.position % schemeInt + 1; //calculate the number that need to be inputted

                solvedBoard[row, col] = number;

            }
            board = solvedBoard;
            return;

        }


        public ColumnNode choose(ColumnNode root)   //this method chooses the best column to start the covering with
        {
            ColumnNode rightOfRoot = (ColumnNode)root.right; // we cast the node to the right of the root to be a ColumnNode
            ColumnNode smallest = rightOfRoot;
            while (rightOfRoot.right != root)
            {
                rightOfRoot = (ColumnNode)rightOfRoot.right;
                if (rightOfRoot.size < smallest.size) // choosing which column has the lowest size
                {
                    smallest = rightOfRoot;
                }
            }

            return smallest;
        }

        public void cover(Node column)  //this function covers the given column
        {
            // we remove the column head by remapping the node to its left to the node to its right; thus, the linked list no longer contains
            // a way to access the column head. Later when we uncover it, we can easily do so by just reversing this process.
            column.right.left = column.left;
            column.left.right = column.right;

            // We also have to do this covering for all the rows in the column
            Node curRow = column.down;
            while (curRow != column) 
            {
                Node curNode = curRow.right;
                while (curNode != curRow)
                {
                    curNode.down.up = curNode.up;
                    curNode.up.down = curNode.down;
                    curNode.head.size--;
                    curNode = curNode.right;
                }
                curRow = curRow.down;
            }
        }

        // uncovers the column i.e. adds back all the nodes of the column to the linked list
        public void uncover(Node column)
        {
            Node curRow = column.up;
            while (curRow != column) // do this for all the nodes of the column to be uncovered first, and then reinsert the columnHead
            {
                Node curNode = curRow.left;
                while (curNode != curRow)
                {
                    curNode.head.size++;
                    curNode.down.up = curNode; // reinserts node into linked list
                    curNode.up.down = curNode;
                    curNode = curNode.left;
                }
                curRow = curRow.up;
            }
            column.right.left = column; // reinserts column head
            column.left.right = column;
        }
    }
}
