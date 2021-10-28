// [FB Coding Practice] Queue Removals
// FB: https://www.facebookrecruiting.com/portal/coding_practice_question/?problem_id=229890198389794&c=900979800845961&ppid=454615229006519&practice_plan=0

using System;
using System.Collections.Generic;

// FB: https://www.facebookrecruiting.com/portal/coding_practice_question/?problem_id=229890198389794&c=900979800845961&ppid=454615229006519&practice_plan=0

class QueueRemovals {
    public class Position {
        public int index;
        public int val;
        public Position(int index, int val){
            this.index = index;
            this.val = val;
        }
    }
    static void Main(string[] args) {
        // Call findPositions() with test cases here
        int[] test;
        int x;
        int[] result;
        test = new int[6]{1, 2, 2, 3, 4, 5};
        x = 5;
        result = findPositions(test, x);
        foreach (int num in result) Console.WriteLine(num);
    }
  
    private static int[] findPositions(int[] arr, int x) {
        
        int[] res = new int[x];
        Queue<Position> q = new Queue<Position>();
        
        //build Queue
        int n = arr.Length;
        for (int i = 0; i < n; i++) {
            q.Enqueue(new Position(i + 1,arr[i]));
        }
        
        List<Position> popped;
        int add = 0;
        
        // iteration x
        for (int it = 0; it < x; it++) {
            
            popped = new List<Position>();
            int max = Int32.MinValue;
            int maxIdx = Int32.MinValue;
            
            // 1. pop x times
            for (int i = 0; i < x && q.Count > 0; i++) {
                var p = q.Dequeue();
                popped.Add(p);                                   
                if (p.val > max) { // find max
                    max = p.val;
                    maxIdx = p.index;
                }
            }
                        
            // 2. add to res
            res[add++] = maxIdx;
            
            // 3. decrease by 1 & remove maxIdx, then add back
            foreach(var p in popped){
                if (p.index != maxIdx) {
                    q.Enqueue(new Position(p.index, (p.val == 0) ? p.val : p.val - 1));
                }
            }
            
        }
                
        return res;
        
    }
}

