using SudokuSolver.Core.Exceptions;
using SudokuSolver.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolver.Core.Implementation
{
    /// <summary>
    /// This class is the core implementation of the sudoku solver algorithm.
    /// This uses backtracking to check each valid digit between 1-9 is placable in the cell.
    /// 
    /// Step 1. A value can be placed in a cell if the row, column and 3x3 block none contain that particular value.
    /// Step 2. If such a valid value is found for the cell, then we recurse for the remaining cells to fill them up.
    /// Step 3. If the remaining cells cannot be filled with 1-9, we try a different value for the cell and repeat from step 1.
    /// Step 4. When we have tried placing all possible values between 1-9 and still failed to fill up the whole board, 
    ///         we return false to indicate the board cannot be filled with the given numeric combinations.
    /// </summary>
    public class SudokuSolverCore : ISudokuSolver
    {
        #region Private Constants

        private const int BOARD_WIDTH = 9; //represents the width of the board (9x9)
        private const int BLOCK_WIDTH = 3; //represents width of each 3x3 blocks inside 9x9 board 

        #endregion

        #region Public Methods

        /// <summary>
        /// Public interface for SodokuSolver
        /// </summary>
        /// <param name="board"></param>
        /// <returns></returns>
        public bool Solve(int[,] board)
        {
            ensureInitialStateIsValid(board);
            return solveInternal(board);
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Performs validation on the initial state of the board as received from the client.
        /// </summary>
        /// <param name="board"></param>
        private void ensureInitialStateIsValid(int[,] board)
        {
            //Validation #1 - No repeated non-zero numbers in row, column or 3x3 block
            //Validation #2 - No cell should contain numbers that are -ve or greater than 9

            for (int row = 0; row < BOARD_WIDTH; row++)
            {
                for (int col = 0; col < BOARD_WIDTH; col++)
                {
                    if (board[row, col] != 0)
                    {
                        if (board[row, col] < 0 || board[row, col] > 9)
                            throw new BoardInvalidException("Each cell must contain values between 0-9.");

                        if (!isValidPlacement(board, board[row, col], row, col))
                            throw new BoardInvalidException("Each provided input must be unique for the row, column and 3x3 block.");
                    }
                }
            }
        }

        /// <summary>
        /// Method that solves the sudoku board
        /// </summary>
        /// <param name="board"></param>
        /// <returns></returns>
        private bool solveInternal(int[,] board)
        {
            int rowToFill = -1, colToFill = -1;
            bool boardFull = true;

            for (int row = 0; row < BOARD_WIDTH; row++)
            {
                for (int col = 0; col < BOARD_WIDTH; col++)
                {
                    if (board[row, col] == 0)
                    {
                        rowToFill = row;
                        colToFill = col;
                        boardFull = false;
                    }
                }
            }

            //all the cells have been sucessfully filled i.e. board is solved.
            if (boardFull)
                return true;

            for (int val = 1; val <= BOARD_WIDTH; val++)
            {
                if (isValidPlacement(board, val, rowToFill, colToFill))
                {
                    board[rowToFill, colToFill] = val;

                    if (solveInternal(board))
                        return true;

                    board[rowToFill, colToFill] = 0; //board could not be solved with this value, revert
                }
            }

            return false;
        }

        /// <summary>
        /// Checks if the current placing of number (1-9) is valid for this cell i.e. no other cell from the same row, col or 3x3 block should have this number.
        /// </summary>
        /// <param name="board"></param>
        /// <param name="val"></param>
        /// <param name="currentRow"></param>
        /// <param name="currentCol"></param>
        /// <returns></returns>
        private bool isValidPlacement(int[,] board, int val, int currentRow, int currentCol)
        {

            for (int row = 0; row < BOARD_WIDTH; row++)
            {
                if (board[row, currentCol] == val && row != currentRow)
                    return false;
            }

            for (int col = 0; col < BOARD_WIDTH; col++)
            {
                if (board[currentRow, col] == val && col != currentCol)
                    return false;
            }

            //find which 3x3 square block the current placement lies in (value can be between 0-2 inclusive)
            int rowOfBlock = currentRow / BLOCK_WIDTH;
            int colOfBlock = currentCol / BLOCK_WIDTH;

            //Get the starting index for the row and column of the block
            int blockRowStartIdx = rowOfBlock * BLOCK_WIDTH;
            int blockColStartIdx = colOfBlock * BLOCK_WIDTH;

            for (int row = blockRowStartIdx; row < blockRowStartIdx + BLOCK_WIDTH; row++)
            {
                for (int col = blockColStartIdx; col < blockColStartIdx + BLOCK_WIDTH; col++)
                {
                    if (board[row, col] == val && row != currentRow && col != currentCol)
                        return false;
                }
            }

            return true;
        }

        #endregion
    }
}
