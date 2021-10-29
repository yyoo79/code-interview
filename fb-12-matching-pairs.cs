// [FB Coding Practice] Matching Pairs
// https://leetcode.com/discuss/interview-question/632717/facebook-recruiting-portal-matching-pairs

using System;
using System.Collections.Generic;
using System.Linq;

class MatchingPairs {
    static void Main(string[] args) {
        string s, t;
        // Example 1 s = "abcd" t = "adcb" output = 4
        s = "abcd";
        t = "adcb";
        Console.WriteLine(matchingPairs(s, t));
        
        // Example 2 s = "mno" t = "mno" output = 1
        s = "mno";
        t = "mno";
        Console.WriteLine(matchingPairs(s, t));
        
        Console.WriteLine(matchingPairs("aa", "aa"));
        
        Console.WriteLine(matchingPairs("ax", "ay"));
        Console.WriteLine(matchingPairs("axb", "ayb"));
        Console.WriteLine(matchingPairs("axa", "aya"));
        Console.WriteLine(matchingPairs("abx", "abb"));
        Console.WriteLine(matchingPairs("abb", "axb"));
        Console.WriteLine(matchingPairs("ax", "ya"));
                
        
    }
  
    private static int matchingPairs(string s, string t) {
        
        var nomatch = new HashSet<string>();
        var match = new HashSet<char>();
        int count = 0;
        bool isDup = false;
        int n = s.Length;
        for (int i=0; i < n; i++) {
            if (s[i] == t[i]) {
                count++;
                if (match.Contains(s[i])) isDup = true;
                match.Add(s[i]);
            } else {
                nomatch.Add(s[i] + "" + t[i]);
            }
        }
        
        if (count == n) return isDup ? count : count - 2;
        
        if (count == n -1) {
            string temp = (string)nomatch.ToArray()[0];
            if (isDup || match.Contains(temp[0]) || match.Contains(temp[1]))
                return count;
            return count -1;
        }
            
        foreach (string nm in nomatch) {
            if (nomatch.Contains(nm[1]+""+nm[0])) return count + 2;
        }
        
        var nomatchS = new HashSet<char>();
        var nomatchT = new HashSet<char>();
        
        foreach (string nm in nomatch) {
            
            if (nomatchS.Contains(nm[1]) || nomatchT.Contains(nm[0])) return count + 1;
            nomatchS.Add(nm[0]);
            nomatchT.Add(nm[1]);

        }
        
        return count;
        
        
    }
}
