// /*
// Dropbox

// ## Space Panorama
// This question is usually used in a phone screen. 
// The sky is divided into a big grid and we are snapping pictures of the grid pieces.
// Each image has a row number and a column number corresponding to its place in the grid.
// We want to save the images to a disk, and read them too!
// Assume each piece of data is 1 MB. 
// Write an API to do this.

// We also want to have constant-time access to the last file we saved. If we haven't saved all the files yet,
// then just return any file that we haven't saved. Assume the sky is 1000 x 1000.


// ## JAVA - from PDF file

// NASA selects Dropbox as its official partner, and we’re tasked with managing a panorama for the universe.
// The Hubble telescope (or some other voyager we have out there) will occasionally snap a photo of a sector of the universe,
// and transmit it to us. You are to help write a data structure to manage this.

// For the purpose of this problem, assume that the observable universe has been divided into 2D sectors.
// Sectors are indexed by x- and y-coordinates.

// */

// public class Dropbox_SpacePanorama {
	
// 	public static void Main(string[] arg) {
// 		Console.WriteLine("hello Dropbox_SpacePanorama");
// 	}

// 	public Dropbox_SpacePanorama(){

// 	}

	

// }

// import java.io.*;
// import java.util.*;

// /**
// * NASA selects Dropbox as its official partner, and we’re tasked with managing 
// * a panorama for the universe. The Hubble telescope (or some other voyager we 
// * have out there) will occasionally snap a photo of a sector of the universe, 
// * and transmit it to us. You are to help write a data structure to manage this.
// * For the purpose of this problem, assume that the observable universe has been . 
// * divided into 2D sectors. Sectors are indexed by x- and y-coordinates.
// */
// public File {
//     public File(String path) {}
//     public Boolean exists() {}
//     public byte[] read() {}
//     public void write(bytes[] bytes) {}
// }

// public Image {
//     public Image(byte[] bytes) {}
//     byte[] getBytes() {} // no more than 1MB in size
// }

// public Sector { 
//     public Sector(int x, int y) {}
//     int getX() {}
//     int getY() {}

//     // @Override
//     // public boolean equals(Object o) {
//     //     if(o==this) return true;

//     //     if(!(o instanceof Sector)){
//     //         return false;
//     //     }

//     //     Sector that = (Sector) o;

//     //     return this.x == that.getX() && this.y == that.getY();
//     // }

//     // @Override
//     // public int hashCode() {
//     //     int prime = 31;
//     //     int result = 1;
//     //     result = prime*result + this.x;
//     //     result = prime*result + this.y;
//     //     return result;
//     // }
// }

// /**
//  * row-major indexing to be consistent.
//  */
// public class SpacePanorama {
//     /**
//      * initializes the data structure. rows x cols is the sector layout.
//      * width, height can be as large as 1K each.
//      */
//     public SpacePanorama(int rows, int cols) {}

//     /**
//      * The Hubble will occasionally call this (via some radio wave communication)
//      * to report new imagery for the sector at (y, x)
//      * Images can be up to 1MB in size.
//      */
//     public void update(int y, int x, Image image) {}

//     /**
//      * NASA will occasionally call this to check the view of a particular sector.
//      */
//     public Image fetch(int y, int x) {}

//     /**
//      * return the 2D index of the sector that has the stalest data.
//      * the idea is that this may help the telescope decide where to aim next.
//      */
//     public Sector getStalestSector() {}
// }

// /**
// * row-major indexing to be consistent.
// */
// if
//     public void removeHead() {
//         locMap.put(head.next.key, null);
//         head.next = head.next.next;
//         head.next.prev = head;
//     }
    
//     public void addTail(int key, int value) {
//         DLinkedList newEle = new DLinkedList(key, value);
//         moveToTail(newEle);
//         locMap.put(key, newEle);
//     }
    
//     public void moveToTail(DLinkedList e) {
//         e.prev = tail.prev;
//         tail.prev.next = e;
//         tail.prev = e;
//         e.next = tail;
//     }
// }

// LRU
// class LRUCache {
//     private int capacity;
//     private LinkedHashMap<Integer, Integer> leastRecentUsedList;
    
//     public LRUCache(int capacity) {
//         this.capacity = capacity;
//         leastRecentUsedList = new LinkedHashMap<>();
//     }
    
//     public int get(int key) {
//         if(leastRecentUsedList.containsKey(key)) {
//             int value = leastRecentUsedList.get(key);
//             leastRecentUsedList.remove(key);
//             leastRecentUsedList.put(key, value);
//             return value;
//         }
        
//         return -1;

//     }
    
//     public void put(int key, int value) {
//         if(leastRecentUsedList.containsKey(key)) {
//             leastRecentUsedList.remove(key);
//         } else if(leastRecentUsedList.size()==capacity) {
//             leastRecentUsedList.remove(leastRecentUsedList.keySet().iterator().next());
//         }
//         leastRecentUsedList.put(key, value);
//     }
    
   
// }

// /*class LRUCache {
//     class DLinkedList {
//         int key, val;
//         DLinkedList prev, next;
        
//         public DLinkedList(int _key, int _val) {
//             key = _key; val = _val;
//         }
//     }
    
//     private int capacity, count = 0;
//     private DLinkedList head, tail;
//     private Map<Integer, DLinkedList> locMap = new HashMap<Integer, DLinkedList>();
    
//     public LRUCache(int capacity) {
//         this.capacity = capacity;
//         head = new DLinkedList(-1, -1);//dummy for easier pointer manipulation
//         tail = new DLinkedList(-1, -1);
//         head.next = tail;
//         tail.prev = head;
//     }
    
//     public int get(int key) {
//         DLinkedList e = getNode(key);
//         return  e == null? -1: e.val;
//     }
    
//     public DLinkedList getNode(int key) {
//         if(locMap.get(key)!=null) {
//             DLinkedList e = locMap.get(key);
//             e.next.prev = e.prev;
//             e.prev.next = e.next;
//             moveToTail(e);
//             return e;
//         } else {
//             return null;
//         }
//     }
    
//     public void put(int key, int value) {
//         DLinkedList e = getNode(key);
//         if(e!=null) {
//            e.val = value;
//         } else {
//             if(count==capacity) { // remove LRU
//                 removeHead();
//             }
//             addTail(key, value);
//             count++;
//         }

//     }
    
//     public void removeHead() {
//         locMap.put(head.next.key, null);
//         head.next = head.next.next;
//         head.next.prev = head;
//         count--;
//     }
    
//     public void addTail(int key, int value) {
//         DLinkedList newEle = new DLinkedList(key, value);
//         moveToTail(newEle);
//         locMap.put(key, newEle);
//     }
    
//     public void moveToTail(DLinkedList e) {
//         e.prev = tail.prev;
//         tail.prev.next = e;
//         tail.prev = e;
//         e.next = tail;
//     }
// }*/

// /**
//  * Your LRUCache object will be instantiated and called as such:
//  * LRUCache obj = new LRUCache(capacity);
//  * int param_1 = obj.get(key);
//  * obj.put(key,value);
//  */