// [FB Coding Practice] Change in a Foreign Currency
// FB: https://www.facebookrecruiting.com/portal/coding_practice_question/?problem_id=2903692913051025&c=900979800845961&ppid=454615229006519&practice_plan=0
// LC: https://leetcode.com/discuss/interview-question/999637/facebook-online-change-in-a-foreign-currency

using System;

/*
Change in a Foreign Currency
You likely know that different currencies have coins and bills of different denominations. In some currencies, it's actually impossible to receive change for a given amount of money. For example, Canada has given up the 1-cent penny. If you're owed 94 cents in Canada, a shopkeeper will graciously supply you with 95 cents instead since there exists a 5-cent coin.
Given a list of the available denominations, determine if it's possible to receive exact change for an amount of money targetMoney. Both the denominations and target amount will be given in generic units of that currency.
Signature
boolean canGetExactChange(int targetMoney, int[] denominations)
Input
1 ≤ |denominations| ≤ 100
1 ≤ denominations[i] ≤ 10,000
1 ≤ targetMoney ≤ 1,000,000
Output
Return true if it's possible to receive exactly targetMoney given the available denominations, and false if not.
Example 1
denominations = [5, 10, 25, 100, 200]
targetMoney = 94
output = false
Every denomination is a multiple of 5, so you can't receive exactly 94 units of money in this currency.
Example 2
denominations = [4, 17, 29]
targetMoney = 75
output = true
You can make 75 units with the denominations [17, 29, 29].

*/

class ForeignCurrency {
    static void Main(string[] args) {
        
        int[] denominations = new int[5]{5, 10, 25, 100, 200};
        int targetMoney = 94;
        Console.WriteLine(canGetExactChange(targetMoney, denominations));
        
        denominations = new int[3]{4,17,29};
        targetMoney = 75;
        Console.WriteLine(canGetExactChange(targetMoney, denominations));
        
    }
  
    private static bool canGetExactChange(int targetMoney, int[] denominations) {
        // Write your code here
        
        bool[][] dp = new bool[denominations.Length][];
        for (int i = 0; i < denominations.Length; i++) {
            bool[] t = new bool[targetMoney + 1];
            dp[i] = t;
        }
        
        for (int i = 0; i < dp.Length; i++)
            dp[i][0] = true;
        
        for (int i = 0; i < dp.Length; i++) {
            for (int j = 1; j < dp[i].Length; j++) {
                
                int coin = denominations[i];
                
                if (j-coin < 0)
                    dp[i][j] = false;                
                else if (i == 0){                    
                    dp[i][j] = dp[i][j-coin];
                }
                else
                    dp[i][j] = dp[i][j-coin] || dp[i-1][j];
                
            }
        }
            
                
        return dp[denominations.Length-1][targetMoney];
    }
}
