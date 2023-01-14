using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OmegaSudoku
{
    public class ColumnID   //this class represents the information that is saved on each column
    {
        public int constraint = -1; //what constrait is this column
        public int number = -1; //what number is this column on
        public int position = -1;   //what is the position of this column between all the other columns
    }
}
