using SudokuSolver.Service.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolver.Service.Interfaces
{
    public interface ISudokuSolverService
    {
        SudokuSolverResponse Solve(SudokuSolverRequest request);
    }
}
