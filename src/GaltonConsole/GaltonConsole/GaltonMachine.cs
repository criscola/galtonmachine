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
                Thread.Sleep(1500);
                Grid.IncrementCell(0, 0);
                for (int j = 0; j < Grid.Width - 1; j++) {
                    // Fa scendere la pallina di 1
                    Ball.Y++;
                    // Guarda dove rimbalza la pallina
                    if (Ball.Bounce())
                    {
                        Ball.X++;
                    }
                    Grid.IncrementCell(Ball.X, Ball.Y);
                    //Console.WriteLine(Ball.X + " -- " + Ball.Y);
                    PrintGrid();
                    Thread.Sleep(1500);
                }
            }
        }
        
        public void PrintGrid()
        {
            Console.Clear();
            for (int i = 0; i < Grid.Width; i++)
            {
                for (int j = 0; j < Grid.Width; j++)
                {
                    // Se siamo alla cella in cui è attualmente presente la pallina, stampare un carattere speciale
                    if (Ball.X == j && Ball.Y == i)
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
            Console.WriteLine();
            Console.WriteLine("----------------------------------------------------------------");
        }
        #endregion
    }

}
