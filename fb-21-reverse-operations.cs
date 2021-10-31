// [FB Coding Practice] Reverse Operations
// fb: https://www.facebookrecruiting.com/portal/coding_practice_question/?problem_id=623634548182866&c=900979800845961&ppid=454615229006519&practice_plan=0
// LC: https://leetcode.com/discuss/interview-question/688086/fb-online-assessment-question

/*
Reverse Operations
You are given a singly-linked list that contains N integers. A subpart of the list is a contiguous set of even elements, bordered either by either end of the list or an odd element. For example, if the list is [1, 2, 8, 9, 12, 16], the subparts of the list are [2, 8] and [12, 16].
Then, for each subpart, the order of the elements is reversed. In the example, this would result in the new list, [1, 8, 2, 9, 16, 12].
The goal of this question is: given a resulting list, determine the original order of the elements.
Implementation detail:
You must use the following definition for elements in the linked list:
class Node {
    int data;
    Node next;
}
Signature
Node reverse(Node head)
Constraints
1 <= N <= 1000, where N is the size of the list
1 <= Li <= 10^9, where Li is the ith element of the list
Example
Input:
N = 6
list = [1, 2, 8, 9, 12, 16]
Output:
[1, 8, 2, 9, 16, 12]
*/
using System;


class Solution
{
    static void Main(string[] args)
    {
        int[] list = new int[]{1,2,8,9,12,16};
        
        Node head = new Node(1);
        Reverse(head);
    }
    
    // Returns the last odd node as new head after reverse.
    // 2 -> 4 -> 6 -> 7
    // 6 -> 4 -> 2 -> 7
        
    static Node Reverse(Node head) {
    
        Node dummy = new Node(0);
        dummy.next = head;
        
        Node prev = dummy;
        Node cur = head;
        
        while (cur != null) {
            
            if (cur.data % 2 == 0) { // even
                prev.next = ReverseOdds(cur);
            }
            
            prev = cur;
            cur = cur.next;
        }
        
        return dummy.next;
                
    }
    
    static Node ReverseOdds(Node head) {
        
        Node prev = null;
        Node cur = head;
        
        while (cur != null && cur.data % 2 == 0) {
            Node t = cur.next;
            cur.next = prev;
            
            prev = cur;
            cur = t;
        }
        
        head.next = cur;
        return prev;
        
    }
    
    static void printLinkedList(Node head) {
        Console.Write("[");
        while (head != null) {
            Console.Write(head.data);
            head = head.next;
        if (head != null)
            Console.Write(" ");
        }
        Console.Write("]");
    }
    
    class Node {
        public int data;
        public Node next;
        public Node(int data = 0, Node next = null){
            this.data = data;
            this.next = next;
        }
        
    }
}
