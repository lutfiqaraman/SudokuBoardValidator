using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuValidator
{
    class Program
    {
        static void Main(string[] args)
        {
            char[,] grid =
            {
                {'5', '3', '4', '6', '7', '8', '9', '1', '2' },
                {'6', '7', '2', '1', '9', '5', '3', '4', '8' },
                {'1', '9', '8', '3', '4', '2', '5', '6', '7' },
                {'8', '5', '9', '7', '6', '1', '4', '2', '3' },
                {'4', '2', '6', '8', '5', '3', '7', '9', '1' },
                {'7', '1', '3', '9', '2', '4', '8', '5', '6' },
                {'9', '6', '1', '5', '3', '7', '2', '8', '4' },
                {'2', '8', '7', '4', '1', '9', '6', '3', '5' },
                {'3', '4', '5', '2', '8', '6', '1', '7', '9' }
            };

            var sudokuBoard = new SudokuChecker(grid);

            bool ValidBoard = sudokuBoard.IsValid();

            if (ValidBoard == true)
                Console.WriteLine("Numbers of Sudoku board are correct");
            else
                Console.WriteLine("Numbers of Sudoku board are incorrect, please check your board");
        }

        public class SudokuChecker
        {
            char[,] _grid;

            public SudokuChecker(char[,] grid)
            {
                _grid = grid;
            }

            public bool IsValid()
            {
                bool case1 = CheckNumbersinEachRow();
                bool case2 = CheckNumbersinEachColumn();
                bool case3 = CheckNumbersinEachSquare();

                return case1 && case2 && case3;
            }

            bool CheckNumbersinEachRow()
            {
                return Validate(GetNumberFromRow);
            }

            bool CheckNumbersinEachColumn()
            {
                return Validate(GetNumberFromColumn);
            }

            bool CheckNumbersinEachSquare()
            {
                return Validate(GetNumberFromSquare);
            }

            bool Validate(Func<int, int, int> numberGetter)
            {
                for (var row = 0; row < 9; row++)
                {
                    var usedNumbers = new bool[10];

                    for(var column = 0; column < 9; column++)
                    {
                        var number = numberGetter(row, column);

                        if (number == 0 || usedNumbers[number] == true)
                        {
                            return false;
                        }

                        usedNumbers[number] = true;
                    }
                }

                return true;

            }

            int GetNumberFromRow(int row, int column)
            {
                return ToNumber(_grid[row, column]);
            }

            int GetNumberFromColumn(int row, int column)
            {
                return ToNumber(_grid[row, column]);
            }

            int GetNumberFromSquare(int block, int index)
            {
                var column = 3 * (block % 3) + (index % 3);
                var row = (index / 3) + (3 * (block / 3));
                return ToNumber(_grid[row, column]);
            }

            int ToNumber(char c)
            {
                if (c == '.')
                    return 0;

                return (int)(c - '0');
            }

        }
    }
}
