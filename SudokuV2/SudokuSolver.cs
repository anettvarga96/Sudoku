using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuV2
{
    class SudokuSolver
    {
        public Cell[,] board { get; set; }

        public SudokuSolver(Cell[,] board)
        {
            this.board = board;
        }

        public void solveSudoku()
        {
            if (board == null || board.Length == 0)
                return;
            solve();
        }

        private bool solve()
        {
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if (board[i, j].value == null)
                    {
                        for (int c = 1; c <= 9; c++)
                        {
                            if (isValid(i, j, c))
                            {
                                board[i, j].value = c;

                                if (solve())
                                    return true;
                                else
                                    board[i, j].value = null;
                            }
                        }
                        return false;
                    }
                }
            }
            return true;
        }

        private bool isValid(int row, int col, int c)
        {
            for (int i = 0; i < 9; i++)
            {
                //check row  
                if (board[i, col].value != null && board[i, col].value == c)
                    return false;
                //check column  
                if (board[row, i].value != null && board[row, i].value == c)
                    return false;
                //check 3*3 block  
                if (board[3 * (row / 3) + i / 3, 3 * (col / 3) + i % 3].value != null && board[3 * (row / 3) + i / 3, 3 * (col / 3) + i % 3].value == c)
                    return false;
            }
            return true;
        }
    }
}
