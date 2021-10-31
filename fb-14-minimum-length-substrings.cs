// [FB Coding Practice] Minimum Length Substrings
// FB: https://www.facebookrecruiting.com/portal/coding_practice_question/?problem_id=2237975393164055&c=900979800845961&ppid=454615229006519&practice_plan=0

// LC: https://leetcode.com/discuss/interview-question/1045207/facebook-online-minimum-length-substrings

/*
Minimum Length Substrings
You are given two strings s and t. You can select any substring of string s and rearrange the characters of the selected substring. Determine the minimum length of the substring of s such that string t is a substring of the selected substring.
Signature
int minLengthSubstring(String s, String t)
Input
s and t are non-empty strings that contain less than 1,000,000 characters each
Output
Return the minimum length of the substring of s. If it is not possible, return -1
Example
s = "dcbefebce"
t = "fd"
output = 5
Explanation:
Substring "dcbef" can be rearranged to "cfdeb", "cefdb", and so on. String t is a substring of "cfdeb". Thus, the minimum length required is 5.
*/

using System;
using System.Collections.Generic;


public class MinimumLengthSubstrings {
    static void Main(String[] args) {
        // Call minLengthSubstring with test cases here
        Console.WriteLine(minLengthSubstring("dcbefebce","fd"));
        
        Console.WriteLine(minLengthSubstring("ADOBECODEBANC","ABC"));
    
    }
    
    // f: 4 d: 0
    
    // e: 3, 5, 8 | d: 0, 9 | f: 1
    
    private static int minLengthSubstring(String s, String t) {
        
        int l = 0;;
        int r = 0;
        
        var dictT = new Dictionary<char, int>();
        foreach(char c in t) {
            if (dictT.ContainsKey(c)) dictT[c]++;
            else dictT[c] = 1;
        }
        
        int required = dictT.Count;
        int formed = 0;
        
        // cur window
        var windowCounts = new Dictionary<char, int>();
        
        // ans list of the form (window length, left, right)
        int[] ans = {-1, 0, 0};
        
        while (r < s.Length) {
            
            char c = s[r];
            if (windowCounts.ContainsKey(c)) windowCounts[c]++;
            else windowCounts[c] = 1;
            
            int countDictT;
            dictT.TryGetValue(c, out countDictT);
            int countWindow;
            windowCounts.TryGetValue(c, out countWindow);
                
            if (dictT.ContainsKey(c) && countDictT == countWindow)
                formed++;
            
            // Try and contract the window till the point where it ceases to be 'desirable'.
            while (l <= r && formed == required) {
                
                c = s[l];
                
                // Save the smallest window until now.
                if (ans[0] == -1 || r - l + 1 < ans[0]) {
                    ans[0] = r - l + 1;
                    ans[1] = l;
                    ans[2] = r;
                }
                
                // The character at the position pointed by the
                // `Left` pointer is no longer a part of the window.
                windowCounts[c]--;
                                
                dictT.TryGetValue(c, out countDictT);                
                windowCounts.TryGetValue(c, out countWindow);

                if (dictT.ContainsKey(c) && countWindow < countDictT)
                    formed--;
                
                //move
                l++;                
            }
            
            r++; // move r
            
        }
                        
        // Console.WriteLine(ans[0] == -1 ? "" : s.Substring(ans[1], ans[2] - ans[1]+1));
        
        return ans[2] - ans[1]+1;
        
    }
}
