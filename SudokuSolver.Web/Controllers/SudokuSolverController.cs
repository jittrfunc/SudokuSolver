using NLog;
using SudokuSolver.Core.Implementation;
using SudokuSolver.Service.Entities;
using SudokuSolver.Service.Implementation;
using SudokuSolver.Utils;
using SudokuSolver.Web.Extensions;
using SudokuSolver.Web.ModelBinders;
using SudokuSolver.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace SudokuSolver.Web.Controllers
{
    public class SudokuSolverController : Controller
    {
        #region Private Fields

        private readonly SudokuSolverService sudokuSolverService;
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        #endregion

        #region Constructor

        public SudokuSolverController(SudokuSolverService sudokuSolverService)
        {
            this.sudokuSolverService = sudokuSolverService;
        } 

        #endregion

        #region Public Endpoints

        /// <summary>
        /// Renders the initial empty board for the users
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Index()
        {
            var viewModel = new SudokuSolverViewModel() { Board = BoardGenerator.GenerateBoard().ToStringBoard() };
            return View(viewModel);
        }

        /// <summary>
        /// Solves the sudoku board received from the user and displays it
        /// </summary>
        /// <param name="board"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Solve([ModelBinder(typeof(SudokuBoardModelBinder))] string[,] board)
        {
            try
            {
                var parseResult = board.ToIntBoard();

                if(parseResult.ErrorMessage != null)
                {
                    return View("Index", new SudokuSolverViewModel { Board = board, ErrorMessage = parseResult.ErrorMessage });
                }

                var response = sudokuSolverService.Solve(new SudokuSolverRequest { Board = parseResult.Board });
                return View("Index", response.ToViewModel());
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError); //log and reply with 500 for any other types of errors
            }
        } 

        #endregion
    }
}