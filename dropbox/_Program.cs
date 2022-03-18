// using System;
// using System.Threading;
// using System.Threading.Tasks;

// namespace MyApp // Note: actual namespace depends on the project name.
// {
//     internal class Program
//     {
//         static void Main(string[] args)
//         {
//             var sc = new SynchronizedCache();
// 			var tasks = new List<Task>();
// 			int itemsWritten = 0;

// 			// Execute a writer.
// 			tasks.Add(Task.Run( () => { String[] vegetables = { "broccoli", "cauliflower",
// 																"carrot", "sorrel", "baby turnip",
// 																"beet", "brussel sprout",
// 																"cabbage", "plantain",
// 																"spinach", "grape leaves",
// 																"lime leaves", "corn",
// 																"radish", "cucumber",
// 																"raddichio", "lima beans" };
// 										for (int ctr = 1; ctr <= vegetables.Length; ctr++)
// 											sc.Add(ctr, vegetables[ctr - 1]);

// 										itemsWritten = vegetables.Length;
// 										Console.WriteLine("Task {0} wrote {1} items\n",
// 															Task.CurrentId, itemsWritten);
// 										} ));

// 			// Execute two readers, one to read from first to last and the second from last to first.
// 			for (int ctr = 0; ctr <= 1; ctr++) {
// 				bool desc = ctr == 1;
// 				tasks.Add(Task.Run( () => { int start, last, step;
// 											int items;
// 											do {
// 												String output = String.Empty;
// 												items = sc.Count;
// 												if (! desc) {
// 												start = 1;
// 												step = 1;
// 												last = items;
// 												}
// 												else {
// 												start = items;
// 												step = -1;
// 												last = 1;
// 												}

// 												for (int index = start; desc ? index >= last : index <= last; index += step)
// 												output += String.Format("[{0}] ", sc.Read(index));

// 												Console.WriteLine("Task {0} read {1} items: {2}\n",
// 																Task.CurrentId, items, output);
// 											} while (items < itemsWritten | itemsWritten == 0);
// 									} ));
// 			}
// 			// Execute a red/update task.
// 			tasks.Add(Task.Run( () => { Thread.Sleep(100);
// 										for (int ctr = 1; ctr <= sc.Count; ctr++) {
// 											String value = sc.Read(ctr);
// 											if (value == "cucumber")
// 												if (sc.AddOrUpdate(ctr, "green bean") != SynchronizedCache.AddOrUpdateStatus.Unchanged)
// 												Console.WriteLine("Changed 'cucumber' to 'green bean'");
// 										}
// 										} ));

// 			// Wait for all three tasks to complete.
// 			Task.WaitAll(tasks.ToArray());

// 			// Display the final contents of the cache.
// 			Console.WriteLine();
// 			Console.WriteLine("Values in synchronized cache: ");
// 			for (int ctr = 1; ctr <= sc.Count; ctr++)
// 				Console.WriteLine("   {0}: {1}", ctr, sc.Read(ctr));
//         }
//     }
// }

// public class SynchronizedCache 
// {
//     private ReaderWriterLockSlim cacheLock = new ReaderWriterLockSlim();
//     private Dictionary<int, string> innerCache = new Dictionary<int, string>();

//     public int Count
//     { get { return innerCache.Count; } }

//     public string Read(int key)
//     {
//         cacheLock.EnterReadLock();
//         try
//         {
//             return innerCache[key];
//         }
//         finally
//         {
//             cacheLock.ExitReadLock();
//         }
//     }

//     public void Add(int key, string value)
//     {
//         cacheLock.EnterWriteLock();
//         try
//         {
//             innerCache.Add(key, value);
//         }
//         finally
//         {
//             cacheLock.ExitWriteLock();
//         }
//     }

//     public bool AddWithTimeout(int key, string value, int timeout)
//     {
//         if (cacheLock.TryEnterWriteLock(timeout))
//         {
//             try
//             {
//                 innerCache.Add(key, value);
//             }
//             finally
//             {
//                 cacheLock.ExitWriteLock();
//             }
//             return true;
//         }
//         else
//         {
//             return false;
//         }
//     }

//     public AddOrUpdateStatus AddOrUpdate(int key, string value)
//     {
//         cacheLock.EnterUpgradeableReadLock();
//         try
//         {
//             string result = null;
//             if (innerCache.TryGetValue(key, out result))
//             {
//                 if (result == value)
//                 {
//                     return AddOrUpdateStatus.Unchanged;
//                 }
//                 else
//                 {
//                     cacheLock.EnterWriteLock();
//                     try
//                     {
//                         innerCache[key] = value;
//                     }
//                     finally
//                     {
//                         cacheLock.ExitWriteLock();
//                     }
//                     return AddOrUpdateStatus.Updated;
//                 }
//             }
//             else
//             {
//                 cacheLock.EnterWriteLock();
//                 try
//                 {
//                     innerCache.Add(key, value);
//                 }
//                 finally
//                 {
//                     cacheLock.ExitWriteLock();
//                 }
//                 return AddOrUpdateStatus.Added;
//             }
//         }
//         finally
//         {
//             cacheLock.ExitUpgradeableReadLock();
//         }
//     }

//     public void Delete(int key)
//     {
//         cacheLock.EnterWriteLock();
//         try
//         {
//             innerCache.Remove(key);
//         }
//         finally
//         {
//             cacheLock.ExitWriteLock();
//         }
//     }

//     public enum AddOrUpdateStatus
//     {
//         Added,
//         Updated,
//         Unchanged
//     };

//     ~SynchronizedCache()
//     {
//        if (cacheLock != null) cacheLock.Dispose();
//     }
// }

// class ReadWriteLock{
//     public int readers = 0;
//     public int writers = 0;
//     public int writeRequests = 0;
    
//     public void LockRead() {
//         lock(this) {
//             while(writers > 0 || writeRequests > 0){
//                 Monitor.Wait(this);
//             }
//             readers++;
//         }
//     }
    
//     public void UnlockRead() {
//         lock(this) {
//             readers--;
//             Monitor.PulseAll(this);
//         }
//     }
    
//     public void LockWrite() {
//         lock(this) {
//             writeRequests++;
//             while (readers > 0 || writers > 0) {
//                 Monitor.Wait(this);
//             }
//             writeRequests--;
//             writers++;
//         }
//     }
    
//     public void UnlockWrite() {
//         lock(this) {
//             writers--;
//             Monitor.PulseAll(this);
//         }
//     }
        
//     static object locker = new object();
//     static Queue<int> numbers = new Queue<int>();
//     static int[] vec = {1, 2, 3, 4, 5, 6, 7};
    
//     public void ReadTest() {
//         // int counter = 0;
//         // while(counter != 7) {
//         //     lock(locker){
//         //         while (numbers.Count==0) {
//         //             Monitor.Wait(locker);
//         //         }
//         //         Console.WriteLine("numbers.Dequeue() = " + numbers.Dequeue());
//         //     }
//         //     // Thread.Sleep(500);
//         //     counter++;
//         // }
//         Console.WriteLine("inside ReadTest() before UnlockRead()");
//         UnlockRead();
//         Console.WriteLine("inside ReadTest() after UnlockRead()");
        
//     }
//     /*
//     public int readers = 0;
//     public int writers = 0;
//     public int writeRequests = 0;
//     */
//     public void WriteTest() {        
//         readers = 1;
//         writers = 1;
//         Console.WriteLine("inside WriteTest() before LockWrite()");
//         LockWrite();
//         Console.WriteLine("inside WriteTest() after LockWrite()");
//         // for(int i = 0; i < vec.Length; i++) {
//         //     lock(locker) {
//         //         numbers.Enqueue(vec[i]);
//         //         Monitor.Pulse(locker);
//         //     }
//         //     // Thread.Sleep(1000);
//         // }
//     }
    
//     // static void Main() {
//     //     var rwl = new ReadWriteLock();        
//     //     new Thread(rwl.WriteTest()).Start();
//     //     new Thread(rwl.ReadTest()).Start();
//     // }
    
//     // static void Main() {
//     //     var rwl = new ReadWriteLock();
//     //     // rwl.writers = 1;
//     //     rwl.LockRead();
//     //     Console.WriteLine("rwl.readers: " + rwl.readers);
//     // }
// }