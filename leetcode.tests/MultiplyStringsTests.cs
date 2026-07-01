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

    [Fact]
    public void TestBuggyMultiply()
    {
        var num1 = "9";
        var num2 = "99";
        var solver = new MultiplyStringsSolver(num1, num2);
        var result = solver.Multiply();
        Assert.Equal("891", result);
    }
    
    [Fact]
    public void TestMultiplyByChar()
    {
        var num1 = "98123";
        char num2 = '7';
        
        var solver = new MultiplyStringsSolver(num1, num1);
        var result = MultiplyStringsSolver.Multiply(num1.Reverse().ToArray(), num2);
        //686861
        Assert.Equal("168686", result);
    }

    [Fact]
    public void TestAdd()
    {
        char[] num1 = [];
        char[] num2 = ['3', '2', '1', '9'];
        var solver = new MultiplyStringsSolver("", "");
        var r = MultiplyStringsSolver.Add(num1, num2, 0);
        var r1 = MultiplyStringsSolver.Add(num1, num2, 1);
        var r2 = MultiplyStringsSolver.Add(num1, num2, 2);
        
        Assert.Equal(['3', '2', '1', '9'], r);
        Assert.Equal(['0', '3', '2', '1', '9'], r1);
        Assert.Equal(['0', '0', '3', '2', '1', '9'], r2);
    }
}