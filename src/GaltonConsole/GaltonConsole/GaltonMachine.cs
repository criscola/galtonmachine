using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GaltonConsole
{
    public class GaltonMachine
    {
        #region =================== costanti ===================
        public const int REFRESH_TIME = 50;
        public const int CYCLE_PAUSE = 800;
        #endregion

        #region =================== membri statici =============
        #endregion

        #region =================== membri e proprietà ===========
        public QuincunxGrid Grid { get; private set; }
        public Ball Ball { get; private set; }

        #endregion

        #region =================== costruttori ================

        public GaltonMachine(int width)
        {
            Grid = new QuincunxGrid(width);
            Ball = new Ball();  
        }

        #endregion

        #region =================== metodi privati ===============

        #endregion

        #region =================== metodi pubblici ============
        public void StartSimulation(int simulations)
        {
            for (int i = 0; i < simulations; i++)
            {
                Ball.X = 0;
                Ball.Y = 0;
                
                PrintGrid();
                Thread.Sleep(CYCLE_PAUSE);
                Grid.IncrementCell(0, 0);
                for (int j = 0; j < Grid.Size - 1; j++) {
                    // Fa scendere la pallina di 1
                    Ball.X++;
                    // Guarda dove rimbalza la pallina
                    if (Ball.Bounce())
                    {
                        Ball.Y++;
                    }
                    Grid.IncrementCell(Ball.X, Ball.Y);
                    //Console.WriteLine(Ball.X + " -- " + Ball.Y);
                    PrintGrid();
                    Thread.Sleep(REFRESH_TIME);
                }
            }
        }
        
        public void PrintGrid()
        {
            Console.Clear();
            for (int i = 0; i < Grid.Size; i++)
            {
                for (int j = 0; j < Grid.Size; j++)
                {
                    // Se siamo alla cella in cui è attualmente presente la pallina, stampare un carattere speciale
                    if (Ball.X == i && Ball.Y == j)
                    {
                        Console.Write("X\t");
                        
                    }
                    else
                    {
                        Console.Write(Grid.GetCell(i, j) + "\t");
                    }
                }
                Console.WriteLine();
            }
            for (int i = 0; i < Grid.Size; i++)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write(Grid.GetResults()[i] + "\t");
                Console.ForegroundColor = ConsoleColor.White;
            }
            
        }
        #endregion
    }

}
