// [FB Coding Practice] Encrypted Words
// FB: https://www.facebookrecruiting.com/portal/coding_practice_question/?problem_id=223547538732703&c=900979800845961&ppid=454615229006519&practice_plan=0

using System;

class Solution
{
    static void Main(string[] args)
    {
        string S;
        S = "abd"; // bac
        Console.WriteLine(findEncryptedWord(S));
        S = "abcd"; // bacd
        Console.WriteLine(findEncryptedWord(S));
        S = "abcxcba"; // xbacbca
        Console.WriteLine(findEncryptedWord(S));
        S = "facebook"; // eafcobok
        Console.WriteLine(findEncryptedWord(S));
    }
        
    private static String findEncryptedWord(String s) {

        var res = Build(s);
        return res;
    }
    
    private static String Build(String s) {
        
        if (s.Length <= 2) return s;
        
        int n = s.Length; // 2=> X, 3=>1, 4 => 1, 5 => 2
        int mid = (n % 2 == 0) ? (n/2)-1 : n/2;
        
        string middle = s.Substring(mid,1);
        string s1 = s.Substring(0,mid);
        string s2 = s.Substring(mid+1, n-mid-1);
             
        return middle + Build(s1) + Build(s2);
        
    }
}

