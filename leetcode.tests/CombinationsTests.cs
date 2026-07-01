namespace leetcode.tests;

public class CombinationsTests
{
    [Fact]
    public async Task TestBacktrack()
    {
        var solver = new CombinationsSolver(2, 2);
        var solutions = solver.GetCombinations();
        Assert.Single(solutions);
        
        solver = new CombinationsSolver(4, 2);
        solutions = solver.GetCombinations();
        Assert.Equal(6, solutions.Count);
        Assert.Equal([[1, 2], [1, 3], [1, 4], [2, 3], [2, 4], [3, 4]],  solutions);
    }
}