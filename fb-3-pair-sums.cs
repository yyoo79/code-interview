// [FB Coding Practice] Pair Sums 

/*
Pair Sums
Given a list of n integers arr[0..(n-1)], determine the number of different pairs of elements within it which sum to k.
If an integer appears in the list multiple times, each copy is considered to be different; that is, two pairs are considered different if one pair includes at least one array index which the other doesn't, even if they include the same values.
Signature
int numberOfWays(int[] arr, int k)
Input
n is in the range [1, 100,000].
Each value arr[i] is in the range [1, 1,000,000,000].
k is in the range [1, 1,000,000,000].
Output
Return the number of different pairs of elements which sum to k.
Example 1
n = 5
k = 6
arr = [1, 2, 3, 4, 3]
output = 2
The valid pairs are 2+4 and 3+3.
Example 2
n = 5
k = 6
arr = [1, 5, 3, 3, 3]
output = 4
There's one valid pair 1+5, and three different valid pairs 3+3 (the 3rd and 4th elements, 3rd and 5th elements, and 4th and 5th elements).
*/
class PairSums {
    static void Main(string[] args) {
        // Call numberOfWays() with test cases here
        int[] test1 = new int[5] {1, 2, 3, 4, 3};
        Console.WriteLine(numberOfWays(test1, 6));
        int[] test2 = new int[5] {1, 5, 3, 3, 3}; // 2 + 3 -1 + 3-1 + 3-1 = /2
        Console.WriteLine(numberOfWays(test2, 6));
        int[] test3 = new int[4] {3,3,3,3};
        Console.WriteLine(numberOfWays(test3, 6)); // 4-1 + 4-1 + 4-1 + 4-1 / 12/2 = 6
    }
    /*
    k = 6 | arr = [1, 2, 3, 4, 3] | output = 2
    {
        1: 1
        2: 1
        3: 2
        4: 1
    }
    k = 6 | arr = [1, 5, 3, 3, 3] | output = 4
    {
        1: 1
        5: 1
        3: 3
    }
    what if there 4 3's? [3, 3, 3, 3] - output = 6
    {
        3: 4
    }
    3,3, - 1 | 3,3,3 - 3 | 3,3,3,3 - 6
    count: 2 - 1
    count: 3 - 3
    count: 4 - 6
    option1 - mark item2
    
    optino2 - mark key
    */
    private static int numberOfWays(int[] arr, int k) {
        // Write your code here
        int twiceCount = 0;
        var dict = new Dictionary<int, int>();
        for (int i = 0; i < arr.Length; i++) {
            if (dict.ContainsKey(arr[i])) dict[arr[i]]++;
            else dict[arr[i]] = 1;
        }
        
        for (int i = 0; i < arr.Length; i++) {
            int item2 = k - arr[i]; // k - item1
            if (dict.ContainsKey(item2)) {
                twiceCount += dict[item2];
            }
            if (item2 == arr[i]) twiceCount--;
        }
        
        return twiceCount/2;
        
        
//         foreach(var kv in dict) {
//             int key = kv.Key;
//             int val = kv.Value;
//             int key2 = k - key;
            
//             if (dict.ContainsKey(key2)) {
//                 if (dict[key2] > 0) {
//                     dict[key]--;
//                     count++;
//                 }
//             }
            
//         }
        
        // return count;
    }
}
