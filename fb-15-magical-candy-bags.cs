// [FB Coding Practice] Magical Candy Bags
// FB: https://www.facebookrecruiting.com/portal/coding_practice_question/?problem_id=513590792640579&c=900979800845961&ppid=454615229006519&practice_plan=0
// LC: https://leetcode.com/discuss/interview-question/1184501/facebook-recruiting-portal-magical-candy-bags

/*
Magical Candy Bags
You have N bags of candy. The ith bag contains arr[i] pieces of candy, and each of the bags is magical!
It takes you 1 minute to eat all of the pieces of candy in a bag (irrespective of how many pieces of candy are inside), and as soon as you finish, the bag mysteriously refills. If there were x pieces of candy in the bag at the beginning of the minute, then after you've finished you'll find that floor(x/2) pieces are now inside.
You have k minutes to eat as much candy as possible. How many pieces of candy can you eat?
Signature
int maxCandies(int[] arr, int K)
Input
1 ≤ N ≤ 10,000
1 ≤ k ≤ 10,000
1 ≤ arr[i] ≤ 1,000,000,000
Output
A single integer, the maximum number of candies you can eat in k minutes.
Example 1
N = 5 
k = 3
arr = [2, 1, 7, 4, 2]
output = 14
In the first minute you can eat 7 pieces of candy. That bag will refill with floor(7/2) = 3 pieces.
In the second minute you can eat 4 pieces of candy from another bag. That bag will refill with floor(4/2) = 2 pieces.
In the third minute you can eat the 3 pieces of candy that have appeared in the first bag that you ate.
In total you can eat 7 + 4 + 3 = 14 pieces of candy.
*/

using System;
using System.Collections.Generic;
using System.Linq;


class MagicalCandyBags {
    static void Main(string[] args) {
        // Call maxCandies() with test cases here
        Console.WriteLine(maxCandies(new int[5]{2, 1, 7, 4, 2},3));
        
    }
  
    private static int maxCandies(int[] arr, int k) {
        
        var maxHeap = new MinHeap();        
        foreach(int num in arr) maxHeap.Add(num); // nlog(n)
        
        int sum = 0;
        
        // O(klog(n))
        for (int i = 0; i < k; i++) {
            
            int pop = maxHeap.PopMax();
            sum += pop;
            maxHeap.Add((int)pop/2);            
            
        }
        
        return sum;
        
    }
    
    
    public class MinHeap
    {
        public SortedDictionary<int, int> sorted = new SortedDictionary<int, int>();
        
        public void Add(int val) {
            if (sorted.ContainsKey(val)) sorted[val]++;
            else sorted.Add(val, 1);
        }
        
        public int PopMax() {
            int maxKey = sorted.Keys.Last();
            var count = sorted[maxKey];
            if (count == 1) sorted.Remove(maxKey);
            else sorted[maxKey]--;
            return maxKey;            
        }
        
        public int PopMin()
        {
            int minKey = sorted.Keys.First();
            var count = sorted[minKey];
            if (count == 1) sorted.Remove(minKey);
            else sorted[minKey]--;
            return minKey;
        }
    }
    
}
