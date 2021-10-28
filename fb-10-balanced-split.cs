// [FB Coding Practice] Balanced Split

using System;
using System.Collections.Generic;

// We don’t provide test cases in this language yet, but have outlined the signature for you. Please write your code below, and don’t forget to test edge cases!

//FB: https://www.facebookrecruiting.com/portal/coding_practice_question/?problem_id=226994905008716&c=900979800845961&ppid=454615229006519&practice_plan=0

class BalancedSplit {

    static void Main(string[] args) {
        // Call balancedSplitExists() with test cases here
        int[] test;
        
        // Example 1: arr = [1, 5, 7, 1] | output = true
        // We can split the array into A = [1, 1, 5] and B = [7].
        test = new int[4]{1, 5, 7, 1};
        Console.WriteLine(balancedSplitExists(test));
        
        // Example 2: arr = [12, 7, 6, 7, 6] | output = false
        test = new int[5]{12, 7, 6, 7, 6};
        Console.WriteLine(balancedSplitExists(test));
    }
  
    // O(n) = selection sort
    private static bool balancedSplitExists(int[] arr) {
        var sum = 0;
        foreach (var num in arr) sum += num;
        if (sum % 2 != 0) return false;
        return partition(arr, 0, sum/2, 0, arr.Length-1);
    }
    
    private static bool partition(int[] arr, int sum,
                                  int target, int l, int r) {
        if (r < l) return false;
        var pivotLoc=1;
        int tmp;
        for (int i=1;i<r;i++){
            if (arr[i] <= arr[r]) {
                tmp = arr[i];
                arr[i]= arr[pivotLoc];
                arr[pivotLoc]=tmp;
                sum+= tmp;
                pivotLoc++;
            }
        }
        
        tmp = arr[r];
        arr[r] = arr[pivotLoc];
        arr[pivotLoc] = tmp;
        sum += tmp;
        
        if (sum == target)
            return arr[pivotLoc] != arr[pivotLoc + 1];
        else if (sum < target)
            return partition(arr, sum, target, pivotLoc+1, arr.Length-1);
        else
            return partition(arr, 0, target, 0, pivotLoc - 1);
            
            
    }
    
    
    
    /*
    // O(nlogn) = sort
    private static bool balancedSplitExists(int[] arr) {
        // Write your code here
        var li = new List<int>(arr);
        
        // sort
        li.Sort();
        
        // 2 pointers
        int i = 0, j = li.Count-1;
        int sumA = li[i++];
        int sumB = li[j--];
        
        if (sumA == sumB) return false;
        // 6, 6, 7, 7, 12 i = 2 , j = 2, sumA = 2, sumB = 7

        while(i <= j){
            
            Console.WriteLine("sumA = " + sumA);
                                    
            if (sumA < sumB)
                sumA += li[i++];
            else
                sumB += li[j--];
            
            if (i-1 >= 0 && j+1 <= li.Count-1 && li[i-1] == li[j+1])
                return false; // when the number got split between A and B

        }
        
        return sumA == sumB;
    }
    */
    
}
