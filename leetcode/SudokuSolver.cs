public interface IFindUniqueSolution
{
    public char Find(char[][] board, int x, int y);
}

public static class SudokuUtils
{
    extension(char[][] board)
    {
        private void AssertGoodXY(int x, int y)
        {
            if (x < 0 || x >= board.Length)
                throw new ArgumentOutOfRangeException(nameof(x));
            if (y < 0 || y >= board[0].Length)
                throw new ArgumentOutOfRangeException(nameof(y));
        }

        public IList<char> GetQuadrantValues(int x, int y)
        {
            board.AssertGoodXY(x, y);
            var quadrantValues = new List<char>();
            var xb = x / 3;
            var yb = y / 3;
            var xbb = xb * 3;
            var ybb = yb * 3;
            var xbe = xb * 3 + 3;
            var ybe = yb * 3 + 3;
            for (var i = xbb; i < xbe; i++)
            for (var j = ybb; j < ybe; j++)
                if (board[i][j] != '.') quadrantValues.Add(board[i][j]);
            return quadrantValues;
        }

        public IList<(int x, int y)> GetQuadrantEmpties(int x, int y)
        {
            board.AssertGoodXY(x, y);
            var quadrantEmpties = new List<(int, int)>();
            var xb = x / 3;
            var yb = y / 3;
            var xbb = xb * 3;
            var ybb = yb * 3;
            var xbe = xb * 3 + 3;
            var ybe = yb * 3 + 3;
            for (var i = xbb; i < xbe; i++)
            for (var j = ybb; j < ybe; j++)
                if (board[i][j] == '.') quadrantEmpties.Add((i, j));
            return quadrantEmpties;
        }
        
        public IList<char> GetColumnValues(int x, int y)
        {
            board.AssertGoodXY(x, y);
            var columnValues = new List<char>();
            for (var i = 0; i < board.Length; i++)
            {
                if (board[i][y] != '.')
                {
                    columnValues.Add(board[i][y]);
                }
            }

            return columnValues;
        }

        public IList<char> GetRowValues(int x, int y)
        {
            board.AssertGoodXY(x, y);
            var rowValues = new List<char>();
            for (var i = 0; i < board[x].Length; i++)
                if (board[x][i] != '.')
                    rowValues.Add(board[x][i]);
            return rowValues;
        }

        public bool IsAcceptable(int x, int y, char value = '.')
        {
            var qv = board.GetQuadrantValues(x, y);
            var rv = board.GetRowValues(x, y);
            var cv = board.GetColumnValues(x, y);
            return !qv.Contains(value) && !rv.Contains(value) && !cv.Contains(value);
        }
        
        public void PrintBoard()
        {
            Console.Write("[");
            for (var i = 0; i < board.Length; i++)
            {
                Console.Write("[");
                for (var j = 0; j < board[i].Length; j++)
                {
                    Console.Write($"\"{board[i][j]}\"");
                    if (j < board[i].Length - 1)
                    {
                        Console.Write(",");
                    }
                }
                Console.Write("]");
                if (i < board.Length - 1)
                {
                    Console.Write(",");
                }
            }
            Console.Write("]");
        }
    }
}

public class GenericConstraintFinder : IFindUniqueSolution
{
    public char Find(char[][] board, int x, int y)
    {
        char[] whole = ['1', '2', '3', '4', '5', '6', '7', '8', '9'];
        var qv = board.GetQuadrantValues(x, y);
        var rv = board.GetRowValues(x, y);
        var cv = board.GetColumnValues(x, y);
        var intersect = whole
            .Except(qv)
            .Except(rv)
            .Except(cv)
            .ToList();
        return intersect.Count == 1 ? intersect.First() : '.';
    }
}

public class PerSymbolQuadrantConstraintFinder : IFindUniqueSolution
{
    public char Find(char[][] board, int x, int y)
    {
        char[] whole = ['1', '2', '3', '4', '5', '6', '7', '8', '9'];
        var qv = board.GetQuadrantValues(x, y);
        var toSearch = whole.Except(qv).ToList();
        var empties = board.GetQuadrantEmpties(x, y);
        foreach (var ts in toSearch.Where(ts => empties.Count(a => !board.GetRowValues(a.x, a.y).Contains(ts) && !board.GetColumnValues(a.x, a.y).Contains(ts)) == 0))
        {
            if (!board.GetRowValues(x, y).Contains(ts) && !board.GetColumnValues(x, y).Contains(ts))
                return ts;
        }
        return '.';
    }
}

public class BacktrackingSolver(char[][] board)
{
    private char[][] _board = board;
    private record Cell(int x, int y, char value = '.');

    public void Solve()
    {
        var solution = GetEmpties();
        var i = 0;
        do
        {
            if (solution[i].value != '9')
            {
                do
                {
                    solution[i] = solution[i] with
                    {
                        value = solution[i].value switch
                        {
                            '.' => '1',
                            '1' => '2',
                            '2' => '3',
                            '3' => '4',
                            '4' => '5',
                            '5' => '6',
                            '6' => '7',
                            '7' => '8',
                            '8' => '9',
                            _ => '.'
                        }
                    };
                    if (solution[i].value == '.')
                        break;
                } while (!board.IsAcceptable(solution[i].x, solution[i].y,  solution[i].value));
                _board[solution[i].x][solution[i].y] = solution[i].value;
                i += solution[i].value == '.' ? -1 : 1;
            }
            else
            {
                _board[solution[i].x][solution[i].y] = '.';
                solution[i] = solution[i] with { value = '.' };
                i--;
            }
        } while (i < solution.Length);
    }

    private Cell[] GetEmpties()
    {
        var empties = new List<Cell>();
        for (var i = 0; i < _board.Length; i++)
        for (var j = 0; j < _board[i].Length; j++)
        {
            if (_board[i][j] == '.')
                empties.Add(new Cell(i, j));
        }
        return empties.ToArray();
    }

    public void PrintBoard()
    {
        _board.PrintBoard();
    }
}

public class SudokuSolver(char[][] board)
{
    private char[][] _board = board;

    private IList<IFindUniqueSolution> _solutions = new List<IFindUniqueSolution>()
    {
        new GenericConstraintFinder(),
        new PerSymbolQuadrantConstraintFinder()
    };

    public void Solve()
    {
        var boardCompleted = true;
        var iterations = 0;
        var discoveredThisRound = false;
        do
        {
            iterations++;
            discoveredThisRound = false;
            boardCompleted = true;
            for (var i = 0; i < board.Length; i++)
            for (var j = 0; j < board[i].Length; j++)
            {
                if (board[i][j] != '.') continue;
                boardCompleted = false;
                foreach (var finder in _solutions)
                {
                    var found = finder.Find(board, i, j);
                    if (found == '.') continue;
                    discoveredThisRound = true;
                    _board[i][j] = found;
                }
            }
        } while (!boardCompleted && discoveredThisRound);

        if (!_board.Any(a => a.Contains('.')))
        {
            PrintBoard();
            return;
        }
        var bsolver = new BacktrackingSolver(_board);
        bsolver.Solve();
        bsolver.PrintBoard();
    }
    
    public void PrintBoard()
    {
        _board.PrintBoard();
    }
}

public class Solution {
    public void SolveSudoku(char[][] board)
    {
        var solver = new SudokuSolver(board);
        solver.Solve();
        solver.PrintBoard();
    }
}