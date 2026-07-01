namespace leetcode.tests;

public class MultiplyStringsTests
{
    [Fact]
    public void SmokeTest()
    {
        var num1 = "2";
        var num2 = "3";
        var solver = new MultiplyStringsSolver(num1, num2);
        Assert.Equal("6", solver.Multiply());

        num1 = "123";
        num2 = "456";
        solver = new MultiplyStringsSolver(num1, num2);
        Assert.Equal("56088", solver.Multiply());
    }
}