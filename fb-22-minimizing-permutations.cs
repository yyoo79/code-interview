// [FB Coding Practice] Minimizing Permutations
// fb: https://www.facebookrecruiting.com/portal/coding_practice_question/?problem_id=292715105029046&c=900979800845961&ppid=454615229006519&practice_plan=0
// LC: https://leetcode.com/discuss/interview-question/1137426/Facebook-or-Minimizing-Permutations

/*
Minimizing Permutations
In this problem, you are given an integer N, and a permutation, P of the integers from 1 to N, denoted as (a_1, a_2, ..., a_N). You want to rearrange the elements of the permutation into increasing order, repeatedly making the following operation:
Select a sub-portion of the permutation, (a_i, ..., a_j), and reverse its order.
Your goal is to compute the minimum number of such operations required to return the permutation to increasing order.
Signature
int minOperations(int[] arr)
Input
Array arr is a permutation of all integers from 1 to N, N is between 1 and 8
Output
An integer denoting the minimum number of operations required to arrange the permutation in increasing order
Example
If N = 3, and P = (3, 1, 2), we can do the following operations:
Select (1, 2) and reverse it: P = (3, 2, 1).
Select (3, 2, 1) and reverse it: P = (1, 2, 3).
output = 2
*/

using System;
using System.Collections.Generic;
using System.Linq;

class MinimizingPermutations {
    static void Main(string[] args) {
        // Call minOperations() with test cases here
        int[] test = new int[]{3,1,2};
        Console.WriteLine(minOperations(test));
    }
  
    private static int minOperations(int[] arr) {
        // Write your code here
        int ret = 0;
        int n = arr.Length;
        int[] target = new int[n];
        for (int i = 0; i < n; i++) target[0] = i + 1;
        
        var seen = new HashSet<string>();
        var queue = new Queue<int[]>();
        queue.Enqueue(arr);
        seen.Add(string.Join("",arr));
        
        while (queue.Count > 0) {
            int size = queue.Count;
            for (int i = 0; i < size; i++) {
                int[] cur = queue.Dequeue();
                
                // check if arrays are same
                if (target.Length == cur.Length &&
                    target.Intersect(cur).Count() == target.Length)
                    return ret;
                
                for (int j = 0; j < cur.Length; j++) {
                    
                    for (int k = j + 1; k < cur.Length; k++) {
                        
                        int[] next = (int[])cur.Clone();
                        next = Reverse(next, j, k);
                        string strNext = string.Join("",next);
                        if (!seen.Contains(strNext)) {
                            queue.Enqueue(next);
                            seen.Add(strNext);
                        }
                        
                    }
                }
                
            }
            
            ret++;
        }
        
        return ret;
    }
    
    private static int[] Reverse(int[] arr, int from, int to) {
        for(; from < to; from++, to--) {
            int tmp = arr[from];
            arr[from] = arr[to];
            arr[to] = tmp;
        }
        return arr;
    }
    
    
}
