using System;

namespace GaltonMachineWPF.Model
{
    public class QuincunxGrid
    {

        private Ball[][] grid;

        private int size;

        public int Size
        {
            get { return size; }
            set
            {
                size = value;
                GenerateGrid();
            }
        }

        public QuincunxGrid(int size)
        {
            this.size = size;
            GenerateGrid();
        }

        public int GetRowSize(int rowNumber)
        {
            return grid[rowNumber].Length;
        }

        public Ball GetCell(int x, int y)
        {
            return grid[x][y];
        }

        public Ball GetCell(Cell currentCell)
        {
            Console.WriteLine("CurrentBall: {0},{1}", currentCell.Row, currentCell.Column);
            Console.WriteLine("Lunghezza righe {0}, Lunghezza colonne {1}", grid.Length, grid[currentCell.Row].Length);
            return grid[currentCell.Row][currentCell.Column];
        }

        public void SetCell(int x, int y, Ball value)
        {
            grid[x][y] = value;
        }

        private void GenerateGrid()
        {
            if (size > 2)
            {
                int count = 1;
                
                grid = new Ball[size][];
                for (int i = 0; i < size; i++)
                {
                    grid[i] = new Ball[count];
                    count++;
                }
            }
        }


        /*
        public void IncrementCell(int x, int y)
        {
            grid[x][y] = grid[x][y] + 1;
        }*/
        /*
        public int[] GetResults()
        {
            int[] lastRow = new int[size];

            for (int i = 0; i < size; i++)
            {
                lastRow[i] = grid[size - 1][i];
            }
            return lastRow;
        }*/
    }
}
