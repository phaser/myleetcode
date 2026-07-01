using System.Text;

namespace leetcode;

public class MultiplyStringsSolver(string num1, string num2)
{
    private char[] _num1 = num1.Reverse().ToArray() ?? throw new ArgumentException("null not allowed");
    private char[] _num2 = num2.Reverse().ToArray() ?? throw new ArgumentException("null not allowed");

    public static string Multiply(char[] reversedNum, char c)
    {
        var result = new StringBuilder();
        var carry = 0;
        foreach (var t in reversedNum)
        {
            var d = (c - 48) * (t - 48) + carry;
            carry = d / 10;
            d = d % 10;
            result.Append((char)(d + 48));
        }

        if (carry > 0)
            result.Append((char)(48 + carry));
        return result.ToString();
    }

    public static char[] Add(char[] num1, char[] num2, int offset)
    {
        var ml = Math.Max(num1.Length, num2.Length);
        var result = new StringBuilder();

        for (var i = 0; i < offset; i++)
        {
            result.Append(i < num1.Length ? num1[i] : (char)48);
        }

        var carry = 0;
        for (var i = 0; i < ml; i++)
        {
            var d1 = offset + i < num1.Length ? num1[offset + i] : 48;
            var d2 = i < num2.Length ? num2[i] : 48;
            var d = d1 + d2 - 96 + carry;
            carry = d / 10;
            d %= 10;
            result.Append((char)(d + 48));
        }
        
        if (carry > 0)
            result.Append((char)(48 + carry));
        
        while (result.Length > 1 && result[result.Length - 1] == 48)
            result.Remove(result.Length - 1, 1);
        
        return result.ToString().ToCharArray();
    }
    
    public string Multiply()
    {
        if (num1.Length < num2.Length)
        {
            (_num1, _num2) = (_num2, _num1);
        }

        var results = new List<string>();
        char[] intr = [];
        for (var i = 0; i < _num2.Length; i++)
        {
            intr = Add(intr, Multiply(_num1, _num2[i]).ToCharArray(), i);
        }
        
        return new string(intr.Reverse().ToArray());
    }
}

public class MultiplyStringsSolution {
    public string Multiply(string num1, string num2)
    {
        return new MultiplyStringsSolver(num1, num2).Multiply();
    }
}