using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SudokuSolver.Web.ModelBinders
{
    /// <summary>
    /// Extracts the sudoku board matrix received from frontend and makes it available to the controller action method.
    /// </summary>
    public class SudokuBoardModelBinder : IModelBinder
    {
        private const string KEY_FORMAT_STRING = "cell_{0}_{1}";
        private const int BOARD_WIDTH = 9;

        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            var form = controllerContext.HttpContext.Request.Form;

            string[,] model = new string[BOARD_WIDTH, BOARD_WIDTH];          
            string currentKey = null;

            try
            {
                for (int row = 0; row < BOARD_WIDTH; row++)
                {
                    for (int col = 0; col < BOARD_WIDTH; col++)
                    {
                        currentKey = string.Format(KEY_FORMAT_STRING, row, col);
                        model[row, col] = form[currentKey].Trim();
                    }
                }

                return model;
            }
            catch (Exception)
            {
                bindingContext.ModelState.AddModelError(currentKey, "Data not in correct format.");  
            }

            return null;
        }
    }
}