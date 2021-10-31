// [FB Coding Practice] 1 Billion Users
// FB: https://www.facebookrecruiting.com/portal/coding_practice_question/?problem_id=951929261870357&c=900979800845961&ppid=454615229006519&practice_plan=0
// LC: https://leetcode.com/discuss/interview-question/746520/facebook-recruiting-portal-1-billion-users

/*
1 Billion Users
We have N different apps with different user growth rates. At a given time t, measured in days, the number of users using an app is g^t (for simplicity we'll allow fractional users), where g is the growth rate for that app. These apps will all be launched at the same time and no user ever uses more than one of the apps. We want to know how many total users there are when you add together the number of users from each app.
After how many full days will we have 1 billion total users across the N apps?
Signature
int getBillionUsersDay(float[] growthRates)
Input
1.0 < growthRate < 2.0 for all growth rates
1 <= N <= 1,000
Output
Return the number of full days it will take before we have a total of 1 billion users across all N apps.
Example 1
growthRates = [1.5]
output = 52
Example 2
growthRates = [1.1, 1.2, 1.3]
output = 79
Example 3
growthRates = [1.01, 1.02]
output = 1047
*/

using System;

class BillionUsers {
    static void Main(string[] args) {
        
        Console.WriteLine(GetBillionUsersDay(new float[1]{1.5f}));
        
        Console.WriteLine(GetBillionUsersDay(new float[3]{1.1f, 1.2f, 1.3f}));
                          
    }
  
    private static int GetBillionUsersDay(float[] growthRates) {
        // Write your code here
        int start = 1;
        int end = 1_000; // considering this to be the upper_limit; can be discussed with the interviewer
        double target = 1_000_000_000;

        while (start < end) {
            double total = 0;
            int mid = start + (end - start) / 2;
            
            // calculate mid value
            foreach (float growthRate in growthRates) {
                total += userOnDay(growthRate, mid);
            }
            
            if (total == target) {
                return mid;
            }
            if (total > target) {
                end = mid;
            } else {
                start = mid + 1;
            }
        }
        return start;
    }
    
    // GP n-th element formula: [a * (r^ (n- 1)]
    // In this case as a == r, it simplifies to r^n
    private static double userOnDay(float rate, int day) {
        return Math.Pow(rate, day);
    }
}
