
// source: https://riptutorial.com/dot-net/example/230/basic-producer-consumer-loop--blockingcollection-
using System.Collections.Concurrent;

public class Sample_BlockingCollection1 {
	/*
	public static void Main(string[] args) {

		var collection = new BlockingCollection<int>(5);
		var random = new Random();

		var producerTask = Task.Run(() => {
			for(int item=1; item<=10; item++) 
			{
				collection.Add(item);
				Console.WriteLine("Produced: " + item);
				Thread.Sleep(random.Next(10,1000));
			}
			collection.CompleteAdding();
			Console.WriteLine("Producer completed!");
		});

		var consumerTask = Task.Run(() => {
			foreach(var item in collection.GetConsumingEnumerable())
			{
				Console.WriteLine("Consumed: " + item);
				Thread.Sleep(random.Next(10,1000));
			}
			Console.WriteLine("Consumer completed!");
		});
		
		Task.WaitAll(producerTask, consumerTask);
			
		Console.WriteLine("Everything completed!");
	}
	*/	
}


/*
using System.Collections.Concurrent;

public class Sample_BlockingCollection1 {

	public static void Main(string[] args) {

		// source: https://docs.microsoft.com/en-us/dotnet/standard/collections/thread-safe/blockingcollection-overview

		// A bounded collection. It can hold no more // than 100 items at once.
		BlockingCollection<Data> dataItems = new BlockingCollection<Data>(100);

		// A simple blocking consumer with no cancellation.
		Task.Run(() =>
		{
			while (!dataItems.IsCompleted)
			{

				Data data = null;
				// Blocks if dataItems.Count == 0.
				// IOE means that Take() was called on a completed collection.
				// Some other thread can call CompleteAdding after we pass the
				// IsCompleted check but before we call Take.
				// In this example, we can simply catch the exception since the
				// loop will break on the next iteration.
				try
				{
					data = dataItems.Take();
				}
				catch (InvalidOperationException) { }

				if (data != null)
				{
					Process(data);
				}
			}
			Console.WriteLine("\r\nNo more items to take.");
		});

		// A simple blocking producer with no cancellation.
		Task.Run(() =>
		{
			while (moreItemsToAdd)
			{
				Data data = GetData();
				// Blocks if numbers.Count == dataItems.BoundedCapacity
				dataItems.Add(data);
			}
			// Let consumer know we are done.
			dataItems.CompleteAdding();
		});

	}
	
}

public class Data {
	public Data(){

	}
}
*/