// [FB Coding Practice] Element Swapping
// https://www.facebookrecruiting.com/portal/coding_practice_question/?problem_id=838749853303393&c=900979800845961&ppid=454615229006519&practice_plan=0
// LC: // https://leetcode.com/discuss/interview-question/848430/element-swapping-facebook-coding-practice-2020


using System;

// We don’t provide test cases in this language yet, but have outlined the signature for you. Please write your code below, and don’t forget to test edge cases!

/*
Example 1
n = 3
k = 2
arr = [5, 3, 1]
output = [1, 5, 3]
We can swap the 2nd and 3rd elements, followed by the 1st and 2nd elements, to end up with the sequence [1, 5, 3]. This is the lexicographically smallest sequence achievable after at most 2 swaps.
Example 2
n = 5
k = 3
arr = [8, 9, 11, 2, 1]
output = [2, 8, 9, 11, 1]
We can swap [11, 2], followed by [9, 2], then [8, 2].
*/


class ElementSwapping {
    static void Main(string[] args) {
        // Call findMinArray() with test cases here
        int[] result;
        result = findMinArray(new int[3]{5,3,1}, 2);
        foreach(int num in result) Console.Write(num + ", ");
        Console.WriteLine("");
        
        result = findMinArray(new int[5]{8,9,11,2,1}, 3);
        foreach(int num in result) Console.Write(num + ", ");
        Console.WriteLine("");
                
    }
  
    private static int[] findMinArray(int[] arr, int k) {
        
        int cur = 0;
        int n = arr.Length;
        while (k > 0 && cur < n) {
         
            int minVal = Int32.MaxValue;
            int min = 0;
            int limit = Math.Min(k, n);
            
            // find and set min + minVal
            for (int i = cur; i <= cur + limit; i++) {
                if (arr[i] < minVal)
                {
                    minVal = arr[i];
                    min = i;
                }
            }
            
            // swap backward from min to cur
            for (int i = min; i > cur; i--) {
                //swap
                int temp = arr[i-1];
                arr[i-1] = arr[i];
                arr[i] = temp;
                k--;
            }
        
            cur++;
            
        }
        
        return arr;
                
    }
}
