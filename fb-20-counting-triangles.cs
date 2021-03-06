// [FB Coding Practice] Counting Triangles
// FB: https://www.facebookrecruiting.com/portal/coding_practice_question/?problem_id=720422605157879&c=900979800845961&ppid=454615229006519&practice_plan=0
// LC: https://leetcode.com/discuss/interview-question/922155/facebook-recruiting-portal-counting-triangles
/*
Counting Triangles
Given a list of N triangles with integer side lengths, determine how many different triangles there are. Two triangles are considered to be the same if they can both be placed on the plane such that their vertices occupy exactly the same three points.
Signature
int countDistinctTriangles(ArrayList<Sides> arr)
or 
int countDistinctTrianges(int[][] arr)
Input
In some languages, arr is an Nx3 array where arr[i] is a length-3 array that contains the side lengths of the ith triangle. In other languages, arr is a list of structs/objects that each represent a single triangle with side lengths a, b, and c.
It's guaranteed that all triplets of side lengths represent real triangles.
All side lengths are in the range [1, 1,000,000,000]
1 <= N <= 1,000,000
Output
Return the number of distinct triangles in the list.
Example 1
arr = [[2, 2, 3], [3, 2, 2], [2, 5, 6]]
output = 2
The first two triangles are the same, so there are only 2 distinct triangles.
Example 2
arr = [[8, 4, 6], [100, 101, 102], [84, 93, 173]]
output = 3
All of these triangles are distinct.
Example 3
arr = [[5, 8, 9], [5, 9, 8], [9, 5, 8], [9, 8, 5], [8, 9, 5], [8, 5, 9]]
output = 1
All of these triangles are the same.

*/
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
