// [FB Coding Practice] Balance Brackets
// FB - https://www.facebookrecruiting.com/portal/coding_practice_question/?problem_id=211548593612944&ppid=454615229006519&practice_plan=0

// LC - https://leetcode.com/problems/valid-parentheses/
// LC solution - https://leetcode.com/problems/valid-parentheses/discuss/9290/C-Solution

using System;
using System.Collections.Generic;

// To execute C#, please define "static void Main" on a class
// named Solution.

class Solution
{
    static void Main(string[] args)
    {
        string s;
        s = "{[()]}";
        Console.WriteLine(isBalanced(s));
        s = "{}()";
        Console.WriteLine(isBalanced(s));
        s = "{(})";
        Console.WriteLine(isBalanced(s));
        s = ")";
        Console.WriteLine(isBalanced(s));
        
    }
    
    private static bool isBalanced(string s) {
        // Write your code here
        var bracket = new Stack<char>();
        
        foreach (char ch in s) {
            if (ch == '{') bracket.Push('}');
            else if (ch == '[') bracket.Push(']');
            else if (ch == '(') bracket.Push(')');
            else if (bracket.Count == 0 || ch != bracket.Pop())
                return false;
        }
                
        return bracket.Count == 0;
    }
}
