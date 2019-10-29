using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SudokuSolver.Web.ViewModels
{
    public class SudokuSolverViewModel
    {
        public string[,] Board { get; set; }
        public string ErrorMessage { get; set; }
    }
}