using System;
using System.Threading;
using System.Threading.Tasks;

namespace MyApp // Note: actual namespace depends on the project name.
{
    internal class Dropbox_ReadWriteLock
    {
        /*
        static void Main(string[] args)
        {
            // var rwl = new ReadWriteLock_Dropbox();
            var rwl = new ReadWriteLock_Dropbox_WriteFirst();
            var tasks = new List<Task>();
            
            // # scenario 1
            tasks.Add(Task.Run( () => {
                // there is 1 writer, so it will be locked until release occurs
                rwl.Read_Acquire_Test();
                Console.WriteLine("-- FINISHED: rwl.LockReadTest(); --");
            }));
            tasks.Add(Task.Run( () => {
                // now existing writer is exited from the doc (release). now it's open to read.
                rwl.Write_Release_Test();
                Console.WriteLine("-- FINISHED: rwl.UnlockWriteTest(); --");
            }));

            // Wait for all three tasks to complete.
            Task.WaitAll(tasks.ToArray());
        }
        */
    }
}

public class ReadWriteLock_Dropbox_WriteFirst {

    private Dictionary<Thread, int> readingTreads = new Dictionary<Thread, int>();
    private int writeAccesses = 0;
    private int writeRequests = 0;
    private Thread? writingThread = null;

    public void Read_Acquire() {
        lock(this) {
            Thread cur = Thread.CurrentThread;
            while (!CanGrantReadAccess(cur)) Monitor.Wait(this);

            if (!readingTreads.ContainsKey(cur)) readingTreads[cur] = 0;
            readingTreads[cur] = GetReadAccessCount(cur) + 1;
        }
    }

    private bool CanGrantReadAccess(Thread cur) {
        // check if t = writer or t = reader => true;
        // check if writerExists or writeRequestExists => false;
        
        // check #1 - IsWriter? - check if cur is writer
        if (IsWriter(cur)) return true;
        // check #2 - HasWriter? - check if writingThread exists
        if (ExistWritingThread()) return false;
        // check #3 - IsReader? - readingThreads(dict) contains cur
        if (IsReader(cur)) return true;
        // check #4 - HasWriteRequest? - writeRequest > 0
        if (ExistWriteRequest()) return false;
        // if all pass => true;
        return true;
    }

    private bool IsWriter(Thread thread) { return thread == writingThread; }
    private bool ExistWritingThread() { return writingThread != null; }
    private bool IsReader(Thread thread) { return readingTreads.ContainsKey(thread); }
    private bool ExistWriteRequest() { return writeRequests > 0;}
    private int GetReadAccessCount(Thread thread) {
        return readingTreads.ContainsKey(thread) ? readingTreads[thread] : 0;
    }

    public void Read_Release() {        
        lock(this) {
            Thread cur = Thread.CurrentThread;
            // check cur = reader, if not, throw exception
            if (!IsReader(cur)) throw new Exception("The current Thread is not a reader");
            
            // update count (decrease)
            int count = GetReadAccessCount(cur);
            if (count <= 0) readingTreads[cur] = 0;
            else readingTreads[cur] = count - 1;

            Monitor.PulseAll(this);
        }
    }

    public void Write_Acquire() {
        lock(this) {
            writeRequests++;
            Thread cur = Thread.CurrentThread;
            while (!CanGrantWriteAccess(cur))
                Monitor.Wait(this);
            writeRequests--;
            writeAccesses++;
            writingThread = cur;
        }
    }

    private bool CanGrantWriteAccess(Thread cur) {
        // 1. IsOnlyReader? 2. ExistReader? 3. ExistWritingTread? 4. IsWriter?
        if (IsOnlyReader(cur)) return true;
        if (ExistReader()) return false;
        if (writingThread == null) return true;
        if (!IsWriter(cur)) return false;
        return true;
    }

    private bool IsOnlyReader(Thread thread) { 
        return readingTreads.Count == 1 && readingTreads[thread] > 0;
    }
    private bool ExistReader() { return readingTreads.Count > 0; }

    public void Write_Release() {
        lock(this) {
            Thread cur = Thread.CurrentThread;
            if (!IsWriter(cur)) throw new Exception("This current thread is not a writer");
            writeAccesses--;
            if (writeAccesses == 0) writingThread = null;
            Monitor.PulseAll(this);
        }
    }


    public void Write_Release_Test() {
        Thread.Sleep(1000);
        Console.WriteLine("inside UnlockWriteTest() before UnlockWrite()");
        Write_Release();        
        Console.WriteLine("inside UnlockWriteTest() after UnlockWrite()");
    }
    public void Read_Acquire_Test() {
        // writers = 1; // 1 writer
        Console.WriteLine("inside LockReadTest() before LockRead()");
        Read_Acquire();
        Console.WriteLine("inside LockReadTest() after LockRead()");
    }

}

public class ReadWriteLock_Dropbox { // Read Preferring Lock
    public int readers = 0;
    public int writers = 0;
    public int writeRequests = 0;
    
    public void Read_Acquire() {
        lock(this) {
            while(writers > 0 || writeRequests > 0){
                Monitor.Wait(this);
            }
            readers++;
        }
    }
    
    public void Read_Release() {
        lock(this) {
            readers--;
            Monitor.PulseAll(this);
        }
    }
    
    public void Write_Acquire() {
        lock(this) {
            writeRequests++;
            while (readers > 0 || writers > 0) {
                Monitor.Wait(this);
            }
            writeRequests--;
            writers++;
        }
    }
    
    public void Write_Release() {
        lock(this) {
            writers--;
            Monitor.PulseAll(this);
        }
    }
            
    public void Write_Release_Test() {
        Thread.Sleep(1000);
        Console.WriteLine("inside UnlockWriteTest() before UnlockWrite()");
        Write_Release();        
        Console.WriteLine("inside UnlockWriteTest() after UnlockWrite()");
    }
    public void Read_Acquire_Test() {
        writers = 1; // 1 writer
        Console.WriteLine("inside LockReadTest() before LockRead()");
        Read_Acquire();
        Console.WriteLine("inside LockReadTest() after LockRead()");
    }

}