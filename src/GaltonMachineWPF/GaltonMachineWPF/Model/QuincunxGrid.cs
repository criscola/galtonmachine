using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GaltonMachineWPF.Model
{
    public class QuincunxGrid
    {

        private Ball[][] grid;
        private int size;

        public int GetSize()
        {
            return size;
        }

        public int GetRowSize(int rowNumber)
        {
            return grid[rowNumber].Length;
        }

        public QuincunxGrid(int size)
        {
            if (size > 2)
            {
                int count = 1;
                this.size = size;
                // Crea un int con colonne
                grid = new Ball[size][];
                for (int i = 0; i < size; i++)
                {
                    grid[i] = new Ball[count];
                    // Popola l'array con int di valore 0
                    for (int j = 0; j < grid[i].Length - 1; j++)
                    {
                        grid[i][j] = null;
                    }
                    count++;
                }
            }
        }

        public Ball GetCell(int x, int y)
        {
            return grid[x][y];
        }

        public void SetCell(int x, int y, Ball value)
        {
            grid[x][y] = value;
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
