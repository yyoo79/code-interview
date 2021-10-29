// [FB Coding Practice] Largest Triple Products

using System;
using System.Collections.Generic;
using System.Linq;

class LargestTripleProducts {

    static void Main(string[] args) {
        
        int[] test;
        int[] result;
        
        test = new int[5]{1, 2, 3, 4, 5};
        result = findMaxProduct(test);
        foreach(int item in result) Console.Write(item + ", ");
        Console.WriteLine("");
        
        test = new int[5]{2, 1, 2, 1, 2};
        result = findMaxProduct(test);
        foreach(int item in result) Console.Write(item + ", ");
        Console.WriteLine("");
        
        test = new int[7]{1, 2, 3, 4, 3, 2, 1};
        result = findMaxProduct(test);
        foreach(int item in result) Console.Write(item + ", ");
        Console.WriteLine("");
    }
  
    private static int[] findMaxProduct(int[] arr) {
        
        // edgecase
        if (arr.Length == 0) return new int[0];
        if (arr.Length == 1) return new int[]{-1};
        if (arr.Length == 2) return new int[]{-1,-1};
        
        var res = new int [arr.Length];
        res[0] = res[1] = -1;
        res[2] = arr[0] * arr[1] * arr[2];
        
        
        var heap = new MinHeap();
        
        heap.Add(arr[0]);
        heap.Add(arr[1]);
        heap.Add(arr[2]);
        
        for (int i = 3; i < arr.Length; i++) {
            
            heap.Add(arr[i]);
            var li = new int[3];            
            li[0] = heap.PopMax();
            li[1] = heap.PopMax();
            li[2] = heap.PopMax();
            res[i] = li[0] * li[1] * li[2]; 
            heap.Add(li[0]);
            heap.Add(li[1]);
            heap.Add(li[2]);
            
        }
        
        return res;
        
    }
    
    
    
    
    /// <summary>
    /// Define my own minimum heap class MinHeap
    /// </summary>
    public class MinHeap
    {
        public SortedDictionary<int, int> sorted = new SortedDictionary<int, int>();

        public void Add(int val)
        {
            if (sorted.ContainsKey(val))
            {
                sorted[val]++;
            }
            else
            {
                sorted.Add(val, 1); 
            }
        }
        
        public int PopMax() {
            int maxKey = sorted.Keys.Last();
            var count = sorted[maxKey];
            if (count == 1) sorted.Remove(maxKey);
            else sorted[maxKey]--;
            return maxKey;            
        }

        /// <summary>
        /// SortedDictionary<int> default is in ascending order. 
        /// 
        /// </summary>
        /// <returns></returns>
        public int PopMin()
        {
            int minKey = sorted.Keys.First();

            var count = sorted[minKey];
            if (count == 1)
            {
                sorted.Remove(minKey);
            }
            else
            {
                sorted[minKey]--;
            }

            return minKey;
        }
    }
    
}
