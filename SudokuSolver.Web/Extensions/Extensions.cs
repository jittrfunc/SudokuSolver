using SudokuSolver.Service.Entities;
using SudokuSolver.Web.Utils;
using SudokuSolver.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SudokuSolver.Web.Extensions
{
    public static class Extensions
    {
        /// <summary>
        /// Generates the UI viewmodel from the response received from the sudokusolver service.
        /// </summary>
        /// <param name="response"></param>
        /// <returns></returns>
        public static SudokuSolverViewModel ToViewModel(this SudokuSolverResponse response)
        {
            return new SudokuSolverViewModel
            {
                Board = response.Board.ToStringBoard(),
                ErrorMessage = response.ErrorMessage
            };
        }

        /// <summary>
        /// Generates the sudoku board for service consumption from the board received from UI
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static SudokuBoardParseResult ToIntBoard(this string[,] input)
        {
            var board = new int[9, 9];
            var parseResult = new SudokuBoardParseResult() { Board = board };

            try
            {
                for (int row = 0; row < 9; row++)
                {
                    for (int col = 0; col < 9; col++)
                    {
                        var current = input[row, col];
                        board[row, col] = string.IsNullOrWhiteSpace(current) ? 0 : int.Parse(current);
                    }
                }
            }
            catch (Exception)
            {
                parseResult.ErrorMessage = "Board could not be parsed. Please provide numeric values only.";
            }
            
            return parseResult;
        }

        /// <summary>
        /// Generates the sudoku board for UI consumption from the board received from service response.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string[,] ToStringBoard(this int[,] input)
        {
            var board = new string[9, 9];

            for (int row = 0; row < 9; row++)
            {
                for (int col = 0; col < 9; col++)
                {
                    var current = input[row, col];
                    board[row, col] = current == 0 ? string.Empty : current.ToString();
                }
            }

            return board;
        }
    }
}