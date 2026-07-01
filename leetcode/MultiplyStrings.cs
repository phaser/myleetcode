using System.Text;

namespace leetcode;

public class MultiplyStringsSolver(string num1, string num2)
{
    private readonly char[] _num1 = num1.Reverse().ToArray() ?? throw new ArgumentException("null not allowed");
    private readonly char[] _num2 = num2.Reverse().ToArray() ?? throw new ArgumentException("null not allowed");

    public string Multiply()
    {
        var result = new StringBuilder();
        var carry = 0;
        
        return new string(result.ToString().Reverse().ToArray());
    }
}

public class Solution {
    public string Multiply(string num1, string num2)
    {
        return "";
    }
}