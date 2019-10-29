using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolver.Service.Entities
{
    public class SudokuSolverResponse
    {
        public SudokuSolverResponseStatus Status { get; set; }
        public string ErrorMessage { get; set; }
        public int[,] Board { get; set; }
    }
}
