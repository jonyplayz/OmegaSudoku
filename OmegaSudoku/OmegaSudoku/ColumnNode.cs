using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OmegaSudoku
{
    public class ColumnNode : Node  //this class represents a column that inherets from Node. it has all the variables from node and adds some special ones for a column
    {
        public int size = 0;    //the size of the column, how many nodes are in the column
        public ColumnID info;   //the information about the column
    }
}
