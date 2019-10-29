using NLog;
using SudokuSolver.Core.Exceptions;
using SudokuSolver.Core.Interfaces;
using SudokuSolver.Service.Entities;
using SudokuSolver.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolver.Service.Implementation
{
    /// <summary>
    /// Implementation of the public API contract for SudokuSolver Service
    /// </summary>
    public class SudokuSolverService : ISudokuSolverService
    {
        #region Private Fields

        private readonly ISudokuSolver sudokuSolver;
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        #endregion

        #region Constructor

        public SudokuSolverService(ISudokuSolver sudokuSolver)
        {
            this.sudokuSolver = sudokuSolver;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// API method that takes in the sudoku board request from client and
        /// returns sudoku board response with appropriate status code and error message (if any).
        /// </summary>
        /// <param name="request"></param>
        /// <returns>SudokuSolverResponse</returns>
        public SudokuSolverResponse Solve(SudokuSolverRequest request)
        {
            var response = new SudokuSolverResponse { Board = request.Board };

            try
            {
                if (sudokuSolver.Solve(request.Board))
                    response.Status = SudokuSolverResponseStatus.Success;
                else
                {
                    response.Status = SudokuSolverResponseStatus.NotSolved;
                    response.ErrorMessage = "Board could not be solved with the given inputs.";
                }
            }
            catch (BoardInvalidException ex)
            {
                response.Status = SudokuSolverResponseStatus.Error;
                response.ErrorMessage = ex.Message;
            }
            catch (Exception ex)
            {
                logger.Error(ex);

                response.Status = SudokuSolverResponseStatus.Error;
                response.ErrorMessage = "Something unexpected happened. Please try again and if error persists contact admin.";
            }

            return response;
        }

        #endregion
    }
}
