using GaltonMachineWPF.Helpers;

namespace GaltonMachineWPF.Model
{
    public class Cell : BindableBase
    {
        private int row;

        public int Row
        {
            get { return row; }
            set
            {
                row = value;
                OnPropertyChanged(() => Column);
            }
        }

        private int column;

        public int Column
        {
            get { return column; }
            set
            {
                column = value;
                OnPropertyChanged(() => Column);
            }
        }

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
