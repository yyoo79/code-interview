// [FB Coding Practice] Revenue Milestones 

using System;
using System.Collections.Generic;
using System.Collections;

// To execute C#, please define "static void Main" on a class
// named Solution.

class Solution
{
    static void Main(string[] args)
    {
        // int[] t1r = new int[10]{10, 20, 30, 40, 50, 60, 70, 80, 90, 100};
        // int[] t1m = new int[3]{100,200,500};
        // int[] result1 = getMilestoneDays(t1r,t1m);
        // foreach(int item in result1) Console.WriteLine(item);
        
        int[] t1r = new int[6]{700, 800, 600, 400, 600, 700};
        int[] t1m = new int[5]{3100, 2200, 800, 2100, 1000};
        int[] result1 = getMilestoneDays(t1r,t1m);
        foreach(int item in result1) Console.WriteLine(item);
            
    }
    
    
    private static int[] getMilestoneDays(int[] revenues, int[] milestones) {
        
        int n = revenues.Length;
        int m = milestones.Length;
        int[] cumsum = new int[n];
        
        var res = new List<int>();
        
        cumsum[0] = revenues[0];
        for (int i = 1; i < n ; i++) {
            cumsum[i] = cumsum[i-1] + revenues[i];
        }
                
        for (int i = 0; i < m; i++) {
            int idx = Search(cumsum, milestones[i]);
            res.Add(idx+1);
        }
        
        return res.Count > 0 ? res.ToArray() : new int[1]{-1};
        
    }

    // binary search
    private static int Search(int[] arr, int val){
        int left = 0;
        int right = arr.Length -1;
        while (left < right) {
            int mid = left + (right - left)/2;
            if (val == arr[mid]) return mid;
            else if (val > arr[mid]) left = mid+1;
            else right = mid;
        }
        return left;
    }
        
}


