using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SudokuSolver.Core.Exceptions;
using SudokuSolver.Core.Implementation;

namespace SudokuSolver.Core.Tests
{
    [TestClass]
    public class SudokuSolverCoreTests
    {
        [TestMethod]
        public void SolveBoardWithValidInput()
        {
            int[,] board = new int[,]
            {
                {3, 0, 6, 5, 0, 8, 4, 0, 0},
                {5, 2, 0, 0, 0, 0, 0, 0, 0},
                {0, 8, 7, 0, 0, 0, 0, 3, 1},
                {0, 0, 3, 0, 1, 0, 0, 8, 0},
                {9, 0, 0, 8, 6, 3, 0, 0, 5},
                {0, 5, 0, 0, 9, 0, 6, 0, 0},
                {1, 3, 0, 0, 0, 0, 2, 5, 0},
                {0, 0, 0, 0, 0, 0, 0, 7, 4},
                {0, 0, 5, 2, 0, 6, 3, 0, 0}
            };

            var solved = new SudokuSolverCore().Solve(board);

            int[,] expected = new int[,]
            {
                {3, 1, 6, 5, 7, 8, 4, 9, 2},
                {5, 2, 9, 1, 3, 4, 7, 6, 8},
                {4, 8, 7, 6, 2, 9, 5, 3, 1},
                {2, 6, 3, 4, 1, 5, 9, 8, 7},
                {9, 7, 4, 8, 6, 3, 1, 2, 5},
                {8, 5, 1, 7, 9, 2, 6, 4, 3},
                {1, 3, 8, 9, 4, 7, 2, 5, 6},
                {6, 9, 2, 3, 5, 1, 8, 7, 4},
                {7, 4, 5, 2, 8, 6, 3, 1, 9}
            };

            if (!solved) Assert.Fail("SolveBoardWithValidInput: Board could not be solved with valid input.");

            for (int row = 0; row < 9; row++)
            {
                for (int col = 0; col < 9; col++)
                {
                    if (board[row, col] != expected[row, col])
                        Assert.Fail("SolveBoardWithValidInput: Solved board does not match expected values.");
                }
            }
        }

        [TestMethod]
        public void BoardCannotBeSolved()
        {
            int[,] board = new int[,]
            {
                {6, 0, 2, 5, 0, 0, 4, 9, 0}, //first 0 in this row cannot be filled with any valid value (1-9), as all 1-9 exists in either row, col or 3x3 block
                {5, 1, 0, 0, 0, 0, 0, 0, 0},
                {0, 8, 7, 0, 0, 0, 0, 0, 2},
                {0, 0, 3, 0, 1, 0, 0, 8, 0},
                {9, 0, 0, 8, 0, 4, 0, 0, 5},
                {0, 5, 0, 0, 2, 0, 9, 0, 0},
                {0, 3, 0, 0, 0, 0, 1, 5, 0},
                {0, 0, 0, 0, 0, 0, 0, 7, 4},
                {0, 0, 8, 2, 0, 0, 3, 0, 0}
            };

            var solved = new SudokuSolverCore().Solve(board);

            Assert.AreEqual(solved, false);
        }

        [TestMethod]
        public void BoardProvidedWithNegativeCellValue()
        {
            int[,] board = new int[,]
            {
                {3, 0, 6, 5, 0, 8, 4, 0, 0},
                {5, 2, 0, 0, 0, 0, 0, 0, 0},
                {0, 8, 7, 0, 0, 0, 0, -3, 1}, //-ve cell value (-3)
                {0, 0, 3, 0, 1, 0, 0, 8, 0},
                {9, 0, 0, 8, 6, 3, 0, 0, 5},
                {0, 5, 0, 0, 9, 0, 6, 0, 0},
                {1, 3, 0, 0, 0, 0, 2, 5, 0},
                {0, 0, 0, 0, 0, 0, 0, 7, 4},
                {0, 0, 5, 2, 0, 6, 3, 0, 0}
            };

            var ex = Assert.ThrowsException<BoardInvalidException>(() => new SudokuSolverCore().Solve(board));

            Assert.AreEqual(ex.Message, "Each cell must contain values between 0-9.");
        }

        [TestMethod]
        public void BoardProvidedWithCellValueGreaterThanNine()
        {
            int[,] board = new int[,]
            {
                {3, 0, 6, 5, 0, 8, 4, 0, 0},
                {5, 2, 0, 0, 0, 0, 0, 0, 0},
                {0, 8, 7, 0, 0, 0, 0, 10, 1}, //cell value > 9 (10)
                {0, 0, 3, 0, 1, 0, 0, 8, 0},
                {9, 0, 0, 8, 6, 3, 0, 0, 5},
                {0, 5, 0, 0, 9, 0, 6, 0, 0},
                {1, 3, 0, 0, 0, 0, 2, 5, 0},
                {0, 0, 0, 0, 0, 0, 0, 7, 4},
                {0, 0, 5, 2, 0, 6, 3, 0, 0}
            };

            var ex = Assert.ThrowsException<BoardInvalidException>(() => new SudokuSolverCore().Solve(board));

            Assert.AreEqual(ex.Message, "Each cell must contain values between 0-9.");
        }

        [TestMethod]
        public void BoardProvidedWithDuplicateRowValue()
        {
            int[,] board = new int[,]
            {
                {3, 0, 6, 5, 0, 8, 4, 0, 0},
                {5, 2, 0, 0, 0, 2, 0, 0, 0}, //2 is duplicate in row
                {0, 8, 7, 0, 0, 0, 0, 3, 1},
                {0, 0, 3, 0, 1, 0, 0, 8, 0},
                {9, 0, 0, 8, 6, 3, 0, 0, 5},
                {0, 5, 0, 0, 9, 0, 6, 0, 0},
                {1, 3, 0, 0, 0, 0, 2, 5, 0},
                {0, 0, 0, 0, 0, 0, 0, 7, 4},
                {0, 0, 5, 2, 0, 6, 3, 0, 0}
            };

            var ex = Assert.ThrowsException<BoardInvalidException>(() => new SudokuSolverCore().Solve(board));

            Assert.AreEqual(ex.Message, "Each provided input must be unique for the row, column and 3x3 block.");
        }

        [TestMethod]
        public void BoardProvidedWithDuplicateColValue()
        {
            int[,] board = new int[,]
            {
                {3, 0, 6, 5, 0, 8, 4, 0, 0},
                {5, 2, 0, 0, 0, 0, 0, 0, 0},
                {0, 8, 7, 0, 0, 0, 0, 3, 1},
                {0, 0, 3, 0, 1, 0, 4, 8, 0}, //4 is duplicate in row/col
                {9, 0, 0, 8, 6, 3, 0, 0, 5},
                {0, 5, 0, 0, 9, 0, 6, 0, 0},
                {1, 3, 0, 0, 0, 0, 2, 5, 0},
                {0, 0, 0, 0, 0, 0, 0, 7, 4},
                {0, 0, 5, 2, 0, 6, 3, 0, 0}
            };

            var ex = Assert.ThrowsException<BoardInvalidException>(() => new SudokuSolverCore().Solve(board));

            Assert.AreEqual(ex.Message, "Each provided input must be unique for the row, column and 3x3 block.");
        }

        [TestMethod]
        public void BoardProvidedWithDuplicateBlockValue()
        {
            int[,] board = new int[,]
            {
                {3, 0, 6, 5, 0, 8, 4, 0, 0},
                {5, 2, 0, 0, 0, 0, 0, 1, 0}, //1 is duplicate in the 3x3 block
                {0, 8, 7, 0, 0, 0, 0, 3, 1},
                {0, 0, 3, 0, 1, 0, 0, 8, 0},
                {9, 0, 0, 8, 6, 3, 0, 0, 5},
                {0, 5, 0, 0, 9, 0, 6, 0, 0},
                {1, 3, 0, 0, 0, 0, 2, 5, 0},
                {0, 0, 0, 0, 0, 0, 0, 7, 4},
                {0, 0, 5, 2, 0, 6, 3, 0, 0}
            };

            var ex = Assert.ThrowsException<BoardInvalidException>(() => new SudokuSolverCore().Solve(board));

            Assert.AreEqual(ex.Message, "Each provided input must be unique for the row, column and 3x3 block.");
        }
    }
}
