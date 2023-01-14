using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OmegaSudoku
{
    public class Node   //this class represents a Node in a double linked lists
    {
        public Node left;   //the node to the left
        public Node right;  //the node to the right
        public Node up; //the node above
        public Node down;   //the node bellow
        public ColumnNode head; //the column of the node
    }
}
