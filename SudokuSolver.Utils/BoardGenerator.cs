using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolver.Utils
{
    /// <summary>
    /// Generates some random sample sudoku boards
    /// </summary>
    public static class BoardGenerator
    {
        private static readonly List<int[,]> boards = new List<int[,]>();
        private static readonly Random random = new Random((int)(DateTime.Now.Ticks % int.MaxValue));

        static BoardGenerator()
        {
            boards.Add(new int[,]
            {
                {3, 0, 6, 5, 0, 8, 4, 0, 0},
                {5, 2, 0, 0, 0, 0, 0, 0, 0},
                {0, 8, 7, 0, 0, 0, 0, 3, 1},
                {0, 0, 3, 0, 1, 0, 0, 8, 0},
                {9, 0, 0, 8, 6, 3, 0, 0, 5},
                {0, 5, 0, 0, 9, 0, 6, 0, 0},
                {1, 3, 0, 0, 0, 0, 2, 5, 0},
                {0, 0, 0, 0, 0, 0, 0, 7, 4},
                {0, 0, 5, 2, 0, 6, 3, 0, 0}
            });

            boards.Add(new int[,]
            {
                {6, 0, 2, 5, 0, 0, 4, 0, 0},
                {5, 0, 0, 0, 0, 0, 0, 1, 0},
                {0, 8, 0, 0, 0, 0, 0, 0, 2},
                {0, 0, 3, 0, 1, 0, 0, 8, 0},
                {9, 0, 0, 8, 0, 0, 0, 0, 5},
                {0, 5, 0, 0, 2, 0, 7, 0, 0},
                {0, 3, 0, 0, 0, 0, 1, 5, 0},
                {0, 0, 0, 0, 0, 0, 0, 7, 0},
                {0, 0, 8, 2, 0, 0, 3, 0, 0}
            });

            boards.Add(new int[,]
            {
                {1, 0, 2, 0, 0, 0, 4, 0, 0},
                {5, 0, 0, 0, 0, 0, 0, 7, 0},
                {0, 8, 4, 0, 0, 0, 0, 0, 2},
                {4, 0, 3, 0, 1, 0, 0, 8, 0},
                {7, 0, 0, 6, 0, 4, 0, 0, 5},
                {0, 5, 0, 0, 0, 0, 9, 0, 0},
                {0, 3, 0, 4, 0, 0, 1, 5, 0},
                {0, 0, 0, 0, 5, 0, 0, 6, 4},
                {0, 0, 0, 1, 0, 0, 8, 0, 0}
            });

            boards.Add(new int[,]
            {
                {1, 0, 5, 0, 0, 0, 4, 0, 0},
                {0, 0, 0, 0, 0, 0, 0, 0, 0},
                {0, 8, 4, 0, 0, 0, 0, 0, 2},
                {0, 0, 3, 0, 1, 0, 0, 8, 0},
                {8, 0, 0, 6, 0, 4, 0, 0, 5},
                {0, 5, 0, 0, 0, 0, 7, 0, 0},
                {0, 3, 0, 4, 0, 0, 1, 0, 0},
                {0, 0, 0, 0, 2, 0, 0, 6, 4},
                {0, 0, 0, 0, 3, 0, 9, 0, 0}
            });
        }

        public static int[,] GenerateBoard()
        {
            return boards[random.Next(boards.Count)];
        }

    }
}
