public class Allocator {
   
    static Queue<int> idList = new Queue<int>();
    static HashSet<int> taken = new HashSet<int>();
    static int MAX_ID;
   
    public Allocator(int maxId) {
        MAX_ID = maxId;
        // idList = new Queue<int>();
        for (int i = 0; i < maxId; i++) idList.Enqueue(i);        
        // taken = new HashSet<int>();
    }
   
    public int Allocate() {
        int id = -1;
        if (idList.Any()) {
            id = idList.Dequeue();
            taken.Add(id);
        } // else { throw new Exception("All IDs are used up"); }
        return id;
    }
   
    public void Release(int id) {
        if (id < 0 || id >= MAX_ID || !taken.Contains(id)) return;
		// throw new Exception ("Invalid ID");
        taken.Remove(id);
        idList.Enqueue(id);            
    }
   
    public bool Check(int id) {
        return !taken.Contains(id);
    }
   
    // static void Main() {
    //     Allocator allocator = new Allocator(10);
    //     int id1 = allocator.Allocate();
    //     int id2 = allocator.Allocate();
    //     int id3 = allocator.Allocate();
    //     Console.WriteLine(id1 + ", " + id2 + ", " + id3);
    //     Console.WriteLine(allocator.Check(id1));
    //     Console.WriteLine(allocator.Check(id2));
    //     Console.WriteLine(allocator.Check(id3));
    //     Console.WriteLine(allocator.Check(11));
    //     Console.WriteLine(allocator.Check(-1));
    //     allocator.Release(2);
    //     Console.WriteLine(allocator.Check(id3));
    // }
}
