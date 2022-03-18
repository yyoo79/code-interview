
/* Question:
You have token bucket initialized with TokenBucket(int tokens, int refreshrate, int capacity);
You have initial tokens then for every second refreshrate tokens are added and the capacity is max tokens a bucket can have.
Write a void get(int amount) multithreaded function for TokenBucket class.
*/

/*
	Idea is to keep critical section as small as possible, to increse mutithreading.
	So instead of synchronizing whole block, I tried to separate updates to two variables.
	[1] LastTimeUpdate
	[2] TokenCount
	Using two different mutex to protect both variables separately.

	Algorithm : 
		- Calculate the time passed since last update
		- calculate the tokens gained
		- try to use newly gained tokens before touching the current available ones.

		- if we ran short then look into available ones
		- else if we gained more even after using it, add it back to available ones

		- if we still need more, then sleep for 1 second and try again.
*/

public class TokenBucket2 {


	int tokenCount;

	int rate;
	int maxCapacity;
	private static object tokenObj = new object();
	
	private long lastUpdatetime;
	private static object updateTimeObj = new object();

	public TokenBucket2(int tokens, int refreshrate, int capacity) {
		tokenCount = tokens;
		rate = refreshrate;
		maxCapacity = capacity;
		lastUpdatetime = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
	}

	private int UpdateTime() {
		long currentTime = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
		int count = 0;

		lock(updateTimeObj) {
			count = (int)((currentTime - lastUpdatetime) / 1000);
			lastUpdatetime += count * 1000;
			Monitor.Wait(updateTimeObj);
		}
		return count;
	}

	public void Get(int amount) {
		// boundary check
		// reject if invalid request 
		if (amount > maxCapacity) return;

		var result = new List<int>();

		while (amount > 0) {
			int count = UpdateTime();
			int bucketGain = count * rate;

			// if more token gained than needed 
			if (bucketGain >= amount) {
				bucketGain -= amount;
				amount = 0;
			} else {
				amount -= bucketGain;
				bucketGain = 0;
			}

			// if we still need more tokens, check the available ones
			if (amount > 0) {
				lock(tokenObj) {
					tokenCount -= amount;
					if (tokenCount < 0) {
						amount = tokenCount * (-1);
						tokenCount = 0;
					} else {
						amount = 0;
					}
				}
			} else if (bucketGain > 0) { // put extra tokens back 
				lock(tokenObj) {
					tokenCount += bucketGain;
					if (tokenCount > maxCapacity) tokenCount = maxCapacity;
				}
			}

			if (amount == 0) return; // if completed, exit out
			Thread.Sleep(1000); // sleep for 1 second

		}
	}

	
	static void Main(string[] args){

		TokenBucket2 bucket2 = new TokenBucket2(7,100,8);
		
		var tasks = new List<Task>();
		Thread.Sleep(1000);
		
		tasks.Add(Task.Run( () => {

			for (int i = 1; i < 9; i=i+2) {				
				bucket2.Get(i*10);

				Console.WriteLine("-- FINISHED: bucket.Get({0}); --", i*10);
				// foreach(var item in result) Console.Write("item: " + item + ", ");

				Thread.Sleep(3000);
			}
		}));

		// Wait for all 2 tasks to complete.
		Task.WaitAll(tasks.ToArray());

	}
	
	
}