using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolver.Service.Entities
{
    public enum SudokuSolverResponseStatus : byte
    {
        Success,
        Error,
        NotSolved,
    }
}
