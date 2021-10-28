// [FB Coding Practice] Contiguous SubArrays
// https://leetcode.com/discuss/interview-question/742523/facebook-prep-question-contiguous-subarrays-on-solution

using System;
using System.Collections.Generic;

class ContiguousSubArrays {
    static void Main(string[] args) {
        // Call countSubarrays() with test cases here
          var test1 = new int[5]{3,4,1,6,2};
          var result1 = countSubarrays(test1);
          foreach (int item in result1) Console.Write(item + ", ");
        
        // arr = [3, 4, 1, 6, 2]
        // output = [1, 3, 1, 5, 1]
                
        
    }
    /*
              0  1  2  3  4
    arr = [3, 4, 1, 6, 2]
              i
       s = [3,1]
       L = [1, 2, 1, 4, 1]
       R = [1, 3, 1, 5, 1]
    
    */
    private static int[] countSubarrays(int[] arr) {
        // Write your code here
        Stack<int> s = new Stack<int>();
        int n = arr.Length;
        int[] L = new int[n];
        L[0] = 1;
        s.Push(0);
        for (int i  = 1; i < n ; i++) {
            while (s.Count != 0 && arr[s.Peek()] < arr[i]) s.Pop();
            if (s.Count == 0)
                L[i] = i + 1;
            else
                L[i] = i - s.Peek();
            s.Push(i);
        }
        int[] R = new int[n];
        R[n-1] = L[n-1];
        s.Clear();
        s.Push(n-1);
        for (int i = n-2; i >= 0 ;i--) {
            while (s.Count != 0 && arr[s.Peek()] < arr[i]) s.Pop();
            if (s.Count == 0)
                R[i] = (n-i) + L[i] -1;
            else
                R[i] = (s.Peek() - i) + L[i] -1;
            s.Push(i);
        }        
        return R;
    }
    
}
