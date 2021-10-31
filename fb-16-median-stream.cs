// [FB Coding Practice] Median Stream
// FB: https://www.facebookrecruiting.com/portal/coding_practice_question/?problem_id=547645422524434&c=900979800845961&ppid=454615229006519&practice_plan=0
// LC: https://leetcode.com/discuss/interview-question/1006825/Facebook-or-Phone-Screen-or-Median-Stream

/*
Median Stream
You're given a list of n integers arr[0..(n-1)]. You must compute a list output[0..(n-1)] such that, for each index i (between 0 and n-1, inclusive), output[i] is equal to the median of the elements arr[0..i] (rounded down to the nearest integer).
The median of a list of integers is defined as follows. If the integers were to be sorted, then:
If there are an odd number of integers, then the median is equal to the middle integer in the sorted order.
Otherwise, if there are an even number of integers, then the median is equal to the average of the two middle-most integers in the sorted order.
Signature
int[] findMedian(int[] arr)
Input
n is in the range [1, 1,000,000].
Each value arr[i] is in the range [1, 1,000,000].
Output
Return a list of n integers output[0..(n-1)], as described above.
Example 1
n = 4
arr = [5, 15, 1, 3]
output = [5, 10, 5, 4]
The median of [5] is 5, the median of [5, 15] is (5 + 15) / 2 = 10, the median of [5, 15, 1] is 5, and the median of [5, 15, 1, 3] is (3 + 5) / 2 = 4.
Example 2
n = 2
arr = [1, 2]
output = [1, 1]
The median of [1] is 1, the median of [1, 2] is (1 + 2) / 2 = 1.5 (which should be rounded down to 1).
*/

using System;
using System.Collections.Generic;
using System.Linq;

class MedianStream {
    static void Main(string[] args) {
        // Call findMedian() with test cases here
        int[] result;
        result = findMedian(new int[4]{5, 15, 1, 3});
        foreach(int num in result) Console.Write(num + ", ");
        Console.WriteLine("");
        
        result = findMedian(new int[2]{1, 2});
        foreach(int num in result) Console.Write(num + ", ");
        Console.WriteLine("");
    }

    private static int[] findMedian(int[] arr) {
        // Write your code here
        
        int[] res = new int[arr.Length];
        var MF = new MedianFinder();
        
        
        for (int i = 0; i < arr.Length; i++) {
            
            MF.AddNum(arr[i]);
            res[i] = (int)MF.FindMedian();
        }
        
        return res;
    }
    
    public class MedianFinder {
    
        private MinMaxHeap small;
        private MinMaxHeap large;
        private bool even;

        public MedianFinder() {
            small = new MinMaxHeap();
            large = new MinMaxHeap();
            even = true;
        }

        public void AddNum(int num) {
            if (even) {
                large.Add(num);
                small.Add(large.PopMin());
            } else {
                small.Add(num);
                large.Add(small.PopMax());
            }
            even = !even;
        }

        public double FindMedian() {
            if (even)
                return (small.PeekMax() + large.PeekMin()) / 2.0;
            else
                return (small.PeekMax());
        }
        
    }
    
    public class MinMaxHeap
    {
        public SortedDictionary<int, int> sorted = new SortedDictionary<int, int>();
        
        public void Add(int val) {
            if (sorted.ContainsKey(val)) sorted[val]++;
            else sorted.Add(val, 1);
        }
        
        public int PeekMax() {
            return sorted.Keys.Last();
        }
        
        public int PeekMin() {
            return sorted.Keys.First();
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
