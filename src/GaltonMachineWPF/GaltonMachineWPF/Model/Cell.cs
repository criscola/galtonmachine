using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GaltonMachineWPF.Model
{
    public class Cell
    {
        public int Row { get; set; }
        public int Column { get; set; }
        public Ball Content { get; set; }

        public Cell()
        {

        }

        public Cell(int row, int column, Ball content)
        {
            Row = row;
            Column = column;
            Content = content;
        }
    }
}
