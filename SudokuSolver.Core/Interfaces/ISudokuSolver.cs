using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolver.Core.Interfaces
{
    public interface ISudokuSolver
    {
        bool Solve(int[,] board);
    }
}
