// [FB Coding Practice] Seating Arrangements
// FB: https://www.facebookrecruiting.com/portal/coding_practice_question/?problem_id=2444722699191194&c=900979800845961&ppid=454615229006519&practice_plan=0
// LC: https://leetcode.com/discuss/interview-question/709517/facebook-recruiting-portal-seating-arrangements

/*
Seating Arrangements
There are n guests attending a dinner party, numbered from 1 to n. The ith guest has a height of arr[i-1] inches.
The guests will sit down at a circular table which has n seats, numbered from 1 to n in clockwise order around the table. As the host, you will choose how to arrange the guests, one per seat. Note that there are n! possible permutations of seat assignments.
Once the guests have sat down, the awkwardness between a pair of guests sitting in adjacent seats is defined as the absolute difference between their two heights. Note that, because the table is circular, seats 1 and n are considered to be adjacent to one another, and that there are therefore n pairs of adjacent guests.
The overall awkwardness of the seating arrangement is then defined as the maximum awkwardness of any pair of adjacent guests. Determine the minimum possible overall awkwardness of any seating arrangement.
Signature
int minOverallAwkwardness(int[] arr)
Input
n is in the range [3, 1000].
Each height arr[i] is in the range [1, 1000].
Output
Return the minimum achievable overall awkwardness of any seating arrangement.
Example
n = 4
arr = [5, 10, 6, 8]
output = 4
If the guests sit down in the permutation [3, 1, 4, 2] in clockwise order around the table (having heights [6, 5, 8, 10], in that order), then the four awkwardnesses between pairs of adjacent guests will be |6-5| = 1, |5-8| = 3, |8-10| = 2, and |10-6| = 4, yielding an overall awkwardness of 4. It's impossible to achieve a smaller overall awkwardness.
*/

using System;
using System.Collections.Generic;

/*
Great Explanation
Steps to do :

-> Sort the Array in ascending order
-> Visualize that we need to arrange this data as a normal distribution (max comes in middle, mins goes in the edges) like

 A[0], A[2], A[4], ....A[n-2], A[n], A[n-1],......A[5], A[3], A[1]
 As this is a circle A[0] and A[1] are adjacent, and make note that A[n] and A[n-1] are adjacent
-> Calculate difference between every other item

 diff = A[i] - A[i -2]
-> special case diffs

 A[1]-A[0]
 A[n]-A[n-1]
-> return max of diffs
*/

class SeatingArrangements {
    static void Main(string[] args) {
        
        // test 1
        Console.WriteLine(minOverallAwkwardness(new int[4]{5,10,6,8}));
        
        // test 2
        Console.WriteLine(
            minOverallAwkwardness(new int[9]{1, 2, 3, 4, 5, 6, 7, 8, 9}));
        
    }
  
    private static int minOverallAwkwardness(int[] arr) {
        
        // sort
        Array.Sort(arr);
        int min = arr[1] - arr[0];
                
        for (int i = 2; i < arr.Length; i+=2) {
            min = Math.Max(min, arr[i] - arr[i-2]);
        }
        
        for (int i = 3; i < arr.Length; i+=2) {
            min = Math.Max(min, arr[i] - arr[i-2]);
        }
        
        return min;
    }
}
