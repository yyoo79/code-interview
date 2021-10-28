// [FB Coding Practice] Slow Sums
// FB: https://www.facebookrecruiting.com/portal/coding_practice_question/?problem_id=836241573518034&c=900979800845961&ppid=454615229006519&practice_plan=0
// LC: https://leetcode.com/discuss/general-discussion/590101/slow-sum
// others: https://www.johncanessa.com/2020/12/12/slow-sum/

class SlowSums {
    static void Main() {
        // Call getTotalTime() with test cases here
        int[] test1 = new int[5]{1,2,3,4,5}; 
        Console.WriteLine(getTotalTime(test1));
        // 1,2,3,9 >> 9
        // 1,2,12  >> 21
        // 1 14    >> 35
        // 15      >> 50
        
    }
    
    private static int getTotalTime(int[] arr) {
        // Write your code here
        List<int> sorted = new List<int>(arr);
        sorted.Sort();
        sorted.Reverse();
        
        // int[] dp = new int[arr.Length];
        // dp[0] = sorted[0];
        // dp[1] = sorted[1] + dp[0];
        
        int prev = sorted[0] + sorted[1];
        int total = prev;
        
        for (int i = 2; i < sorted.Count; i++) {
            prev += sorted[i];
            total += prev;
        }
        
        return total;
      }
}
