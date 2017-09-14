using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GaltonConsole
{
    public class QuincunxGrid
    {
        private int[][] grid;

        public int Width { get; private set; }

        public QuincunxGrid(int width)
        {
            if (width > 2)
            {
                // Inizializza l'array della griglia
                grid = new int[width][];

                for (int i = 0; i < width; i++)
                {
                    grid[i] = new int[width + 1];
                    // Popola l'array con int di valore 0
                    for (int j = 0; j < grid[i].Length - 1; j++)
                    {
                        grid[i][j] = 0;
                    }
                }
                Width = width;
            }
            else
            {
                Debug.WriteLine("Specified width must be >= 2.");
            }
        }

        /// <summary>
        /// Riceve la cella specificata
        /// </summary>
        /// <param name="x">L'indice della riga</param>
        /// <param name="y">L'indice della colonna</param>
        /// <returns></returns>
        public int GetCell(int x, int y)
        {
            return grid[x][y];
        }

        /// <summary>
        /// Setta la cella specificata con un valore specificato 
        /// </summary>
        /// <param name="x">L'indice della riga</param>
        /// <param name="y">L'indice della colonna</param>
        /// <param name="value">Il valore della cella</param>
        public void SetCell(int x, int y, int value)
        {
            grid[x][y] = value;
        }

        /// <summary>
        /// Incrementa di 1 la cella specificata
        /// </summary>
        /// <param name="x">L'indice della riga</param>
        /// <param name="y">L'indice della colonna</param>
        public void IncrementCell(int x, int y)
        {
            grid[y][x] = grid[y][x] + 1;
        }

        /// <summary>
        /// Ritorna i "risultati" dell'ultima riga, ovvero il conteggio delle volte in cui la pallina è arrivata in una determinata posizione
        /// </summary>
        /// <returns>L'array di risultati (copia dell'ultima riga della griglia)</returns>
        public int[] GetResults()
        {
            int[] lastRow = new int[Width];
            for (int i = 0; i < grid[Width - 1].Length; i++)
            {
                lastRow[i] = grid[Width - 1][i];
            }
            return lastRow;
        }
    }
}
