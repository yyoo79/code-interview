// [FB Coding Practice] Passing Yearbooks
// FB: https://www.facebookrecruiting.com/portal/coding_practice_question/?problem_id=146466059993201&c=900979800845961&ppid=454615229006519&practice_plan=0
// Leetcode: https://leetcode.com/discuss/interview-question/614096/facebook-interview-preparation-question-passing-yearbooks
// Thanks to @xiaoxiang615 for good solution

// using System.Collections.Generic;

class PassingYearbooks {
    static void Main() {
        // Call findSignatureCounts() with test cases here
        int[] test1 = new int[4]{3, 2, 4, 1};
        var result1 = findSignatureCounts(test1);
        foreach(var item in result1) Console.Write(item + ", ");
        Console.WriteLine("");
        
        int[] test2 = new int[2]{2, 1};
        var result2 = findSignatureCounts(test2);
        foreach(var item in result2) Console.Write(item + ", ");
        Console.WriteLine("");
        
        int[] test3 = new int[2]{1, 2};
        var result3 = findSignatureCounts(test3);
        foreach(var item in result3) Console.Write(item + ", ");
        Console.WriteLine("");
        
        int[] test4 = new int[9]{5,3,9,4,1,8,6,2,7};
        var result4 = findSignatureCounts(test4);
        foreach(var item in result4) Console.Write(item + ", ");
    }
    /*
        1. Create a HashMap(dict)
        {
            3: 1
            2: 2
            4: 3
            1: 4
        }
        2. Create a set - visited
        3. Create a set - round(cycle)
        4. loop through hashmap (if not visited)
            while not visited(kv.Key)
                add to round  // 3, 1, 4, 3(X)
                add to visited // 3, 1, 4, 3(X)
                kv.Key <= k.Value
            loop round
                add round size to i-1 to res(ans)
        5. 
    
    */
    private static int[] findSignatureCounts(int[] arr) {
        // Write your code here
    
        int[] ans = new int[arr.Length];
        
        Dictionary<int,int> dict = new Dictionary<int,int>();
        for (int i = 0; i < arr.Length ; i++) dict[arr[i]] = i+1;
                
        HashSet<int> visited = new HashSet<int>();
        
        foreach (var kv in dict) {
            int key = kv.Key;
            if (!visited.Contains(key)) {
                var round = new HashSet<int>();
                while (!visited.Contains(key)) {
                    visited.Add(key);
                    round.Add(key);
                    key = dict[key];
                }
                foreach (int k in round) {
                    ans[k-1] = round.Count;
                }
            }
        }
        
        return ans;
    }
}
