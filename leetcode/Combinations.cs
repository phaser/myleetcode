namespace leetcode;

public class CombinationsSolver(int n, int k)
{
    private readonly int _n = n;
    private readonly int _k = k;

    public IList<IList<int>> GetCombinations()
    {
        var solutions = new List<IList<int>>();
        var solution = new int[_k];

        for (var i = 0; i < solution.Length; i++)
            solution[i] = i + 1;
        
        while (true)
        {
            solutions.Add(solution.ToList());
            var i = _k - 1;
            while (i >= 0 && solution[i] == _n - _k + i + 1)
                i--;

            if (i < 0)
                break;

            solution[i]++;
            for (var j = i + 1; j < _k; j++)
                solution[j] = solution[j - 1] + 1;
        }
        return solutions;
    }
}

public class Solution {
    public IList<IList<int>> Combine(int n, int k)
    {
        return new CombinationsSolver(n, k).GetCombinations();
    }
}