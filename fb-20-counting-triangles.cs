// [FB Coding Practice] Counting Triangles
// FB: https://www.facebookrecruiting.com/portal/coding_practice_question/?problem_id=720422605157879&c=900979800845961&ppid=454615229006519&practice_plan=0
// LC: https://leetcode.com/discuss/interview-question/922155/facebook-recruiting-portal-counting-triangles

using System;
using System.Collections.Generic;

class CountingTriangles {
    static void Main(string[] args) {

        // Call countDistinctTriangles() with test cases here
        int[][] arr = new int[][]{
            new int[]{2, 2, 3},
            new int[]{3, 2, 2},
            new int[]{2, 5, 6}
        };
        Console.WriteLine(countDistinctTriangles(arr));
        
        arr = new int[][]{
            new int[]{8, 4, 6},
            new int[]{100, 101, 102},
            new int[]{84, 93, 173}
        };
        Console.WriteLine(countDistinctTriangles(arr));
        
    }
  
    private static int countDistinctTriangles(int[][] arr) {
        // Write your code here
        int n = arr.Length;
        // int count = 0;
        var set = new HashSet<string>();
                
        for (int i = 0; i < n; i++) {
            
            var sides = arr[i];
            Array.Sort(sides);
            
            string key = sides[0].ToString() +
                sides[1].ToString() + sides[2].ToString();
            
            set.Add(key);
            
        }
        
        return set.Count;
            
    }
        
}
