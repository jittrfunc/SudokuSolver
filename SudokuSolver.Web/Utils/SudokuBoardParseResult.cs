using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SudokuSolver.Web.Utils
{
    public class SudokuBoardParseResult
    {
        public int[,] Board { get; set; }
        public string ErrorMessage { get; set; }
    }
}