
/* Question:
You have token bucket initialized with TokenBucket(int tokens, int refreshrate, int capacity);
You have initial tokens then for every second refreshrate tokens are added and the capacity is max tokens a bucket can have.
Write a void get(int amount) multithreaded function for TokenBucket class.
*/
public class TokenBucket {

	private readonly int MAX_CAPACITY;
	private readonly int FILL_RATE;	

	private List<int> bucket;
	private long lastTimestamp;
	Random rand = new Random();

	private static object _lock = new object();
	


	public TokenBucket(int tokens, int refreshrate, int capacity) {
		MAX_CAPACITY = capacity;
		FILL_RATE = refreshrate;
		lastTimestamp = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
		
		bucket = new List<int>();
		// initialzied bucket with tokens
		// bucket = new List<int>(MAX_CAPACITY);		
		// for (int i = 0; i < MAX_CAPACITY; i++) {
		// 	bucket.Add( (int)(rand.NextDouble()*100)+1 );
		// }
	}

	public void Fill() {
		lock(_lock) {
			while (bucket.Count == MAX_CAPACITY) {
				Console.WriteLine("Bucket is full.");
				Monitor.Wait(_lock);
			}

			long now = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
			int bucketRemaining = MAX_CAPACITY - bucket.Count;

			long timeDiffCount = (now - lastTimestamp) * FILL_RATE / 1000; // convert to milisec
			Console.WriteLine("bucketRemaining - " + bucketRemaining + " | timeDiffCount - " + timeDiffCount);
			long numTokensToAdd = Math.Min(bucketRemaining, timeDiffCount);

			// update lastTimestamp
			lastTimestamp = now;			
			for (int i = 0; i < numTokensToAdd; i++) { // add tokens
				bucket.Add((int) (rand.NextDouble() * 100) + 1);
				Console.WriteLine("token added");
			}

			Monitor.PulseAll(_lock);
			Console.WriteLine("Inside Fill() - After Monitor.PulseAll(_lock);");
		}
	}

	// public void Get(int amount) {
	public List<int> Get(int amount) {

		Fill();
		
		var result = new List<int>();
		if (amount <= 0) return result; // or throw exception
		if (amount >= MAX_CAPACITY) return result; // or 'bucket is full' exception

		int count = 0; // token count

		lock(_lock) {

			Console.WriteLine("count = " + count + " | amount = " + amount);

			while (count < amount) {
				
				while (bucket.Count == 0) { // bucket is empty
					Monitor.Wait(_lock);
				}

				result.Add(bucket[bucket.Count - 1]);
				bucket.RemoveAt(bucket.Count - 1);				
				count++;
				Console.WriteLine("count = " + count);
				
				Monitor.PulseAll(_lock);

			}
		}

		return result;
	}

	
	// static void Main(string[] args){

	// 	TokenBucket bucket = new TokenBucket(7,100,8);
		
	// 	var tasks = new List<Task>();
	// 	Thread.Sleep(1000);
		
	// 	// # scenario 1
	// 	// tasks.Add(Task.Run( () => {
			
	// 	// 	bucket.Fill();			
	// 	// 	Console.WriteLine("-- FINISHED: bucket.Fill(); --");
	// 	// 	Thread.Sleep(1000);
			
	// 	// }));

	// 	tasks.Add(Task.Run( () => {

	// 		for (int i = 1; i < 9; i=i+2) {
	// 			var result = bucket.Get(i*10);			
	// 			Console.WriteLine("-- FINISHED: bucket.Get(7); --" + " result = " + result.ToString());
	// 			foreach(var item in result) Console.Write("item: " + item + ", ");
	// 			Thread.Sleep(3000);
	// 		}
	// 	}));

	// 	// Wait for all 2 tasks to complete.
	// 	Task.WaitAll(tasks.ToArray());

	// }
	
	
}