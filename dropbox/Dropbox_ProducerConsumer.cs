/*

Design a messaging queue for processing images.
A producer will use the queue to send image data to a consumer for processing.

= STEP1 =
Start with one producer and one consumer.
1. Producer POST the image to the storage service
2. Return OK response with image location
3. Producer message with image metadata and location info
4. Return response indicating the queue got msg
5. Consumer tries to GET a msg from the queue. WAIT if there is no msg in queue
6. If msg is available, return msg to Consumer.

Write the application methods for each service that you used.
Expect the candidate to discuss when failure happens. 

If the candidate talks about persisting each message to the disk when sent to the queue service, then redirect them to assume it's already saved.

Producer:
save_to_storage_service(byte[] image_data) throws FailedToPostImageException {}
send_message(string message_with_image_information_from_storage_service) throws CannotPostToQueueException {}

Messaging Queue:

Consumer:
request_message(_queueServices) throws EmptyQueueException

Follow up questions:

What if you have defective data in the queue that can't be processed by the consumers?
- When the queue gives a message to the consumer, it should store that message in a cache (save to disk)
- Have a retry policy and a queue manager that tracks the retries on the data. If the data can't be processed correctly, then the producer tells the 
What if we have duplicate images being processed by the consumers?
- That isn't an issue. Unless it's mass duplication, it won't really matter if we have a few duplicates. 
Can you think of a scenario where it would be a problem for the consumers to process duplicate information?
- Finance / a bank.
How would you solve that problem?
- Have an orchestrator (like ZooKeeper) check the consumers for which jobs they are processing. If two or more are processing the same job, then you need to kill the job on all of but one of the consumers.


Testing:
Unit test everything in each service. 
Do regression testing, end-to-end tests, and performance testing. 
Test for each type of error that the whole system can throw. 

*/

using System.Collections.Concurrent;
// using Microsoft.Azure.Storage;
// using Microsoft.Azure.Storage.Blob;

using System.Net;

public class Dropbox_ProducerConsumer {
	
	/*
	public static void Main(string[] arg) {
		
		ImageProducerConsumer_Step1 ipc = new ImageProducerConsumer_Step1(5);

		int totalRun = 5;
		
		var producerTask = Task.Run(() => {
			for (int item = 1; item <= totalRun; item++) 
			{
				// simulating a new random file creation
				var image = new byte[7000];
				for (int i = 0; i < image.Length; i++)
					image[i] = 0x20;
				
				// produce image
				ipc.Produce(image);
				Console.WriteLine("-> [Producer Task] Done. item#" + item);
				// Console.WriteLine("--Should some kind of wait occurs here after Produce() call --");

			}			
			Console.WriteLine("=> Producer completed!");
		});

		var consumerTask = Task.Run(() => {

			for (int item = 1; item <= totalRun; item++) 
			{
				// ipc.Consume();
				string fileInfo = ipc.Request_message();

				// download file based on fileInfo
				// download process simulation
				// var info = item.Split("|"); // info[0] = filename, info[1] = filelocation
				// var client = new HttpClient();
				// var response = await client.GetAsync(info[1]);
				// using (var stream = await response.Content.ReadAsStreamAsync())
				// {
				// 	var fileInfo = new FileInfo(info[0]);
				// 	using (var fileStream = fileInfo.OpenWrite()) {
				// 		await stream.CopyToAsync(fileStream);
				// 	}
				// }

				Console.WriteLine("<- [Consumer Task] fileInfo - " + fileInfo);
				// Console.WriteLine("--Should some kind of wait occurrs here after Consume() call --");

			}

			Console.WriteLine("<= Consumer completed!");						

		});
		
		Task.WaitAll(producerTask, consumerTask);
		
		Console.WriteLine("Everything completed!");

	}
	*/
	
}

public class ImageMessage {
	public int id {get; set;}
	public string fileLocation {get; set;}
	public ImageMessage(int _id, string _fileLocation){
		_id = id;
		fileLocation = _fileLocation;
	}

}

public class ImageProducerConsumer_Step1 {
	
	BlockingCollection<string> q; // queue	
	AzureStorageBlob blockBlob; //Azure blockBlob - fake one

	public ImageProducerConsumer_Step1(int messageQueueSize){
		q = new BlockingCollection<string>(messageQueueSize);
		blockBlob = new AzureStorageBlob();
	}
	
	public void Consume() {
		
		// get item from Queue
	}

	// Consumer
	// request_message(_queueServices) throws EmptyQueueException
	public string Request_message() {
		string? fileInfo;
		try {
			
			// note: Take vs TryTake
			// Take -  A call to Take may block until an item is available to be removed.
			// TryTake - If the collection is empty, this method immediately returns false.
			// https://stackoverflow.com/questions/48301027/difference-between-take-trytake-and-add-tryadd-on-a-blocking-collection
			
			fileInfo = q.Take();

			Console.WriteLine("Step6: Consumed fileInfo " + fileInfo);

			var random = new Random(); // simulating delay
			Thread.Sleep(random.Next(10,1000));
			// }			

		}
		catch (Exception ex) {
			throw new Exception("EmptyQueueException: " + ex.Message);
		}

		return fileInfo != null ? fileInfo : "";

	}

	public void Produce(byte[] image_data){
		
		HttpResponseMessage response = Save_to_storage_service(image_data);
		// string filelocation = httpMsg.Content.ToString() == null ? "invalid" : httpMsg.Content.ToString();

		var filelocation = response.Content.ReadAsStringAsync().Result;

		// string? filelocation = httpMsg.Content.ToString();

		if (filelocation != null) {			
			// ImageMessage imgMsg = new ImageMessage(idGenerator++, filelocation);
			string msgWithImgInfo = idGenerator + " | " + filelocation;
			idGenerator++;
			var responseStr = Send_message(msgWithImgInfo);
		}			
		
	}

	int idGenerator = 1;
	private HttpResponseMessage Save_to_storage_service(byte[] image_data) {  //throws FailedToPostImageException
		try{
			// upload image_data - just simulation
			using(var stream = new MemoryStream(image_data, writable: false)) {
				blockBlob.UploadFromStream(stream);
			}
			
			var random = new Random(); // simulating delay
			Thread.Sleep(random.Next(10,1000));

		}
		catch (Exception ex) {
			throw new Exception("FailedToPostImageException: " + ex.Message);
		}		
		string fileLocation = "http://www.filelocation.com/file" + idGenerator.ToString();		
		var content = new StringContent(fileLocation);
		HttpResponseMessage msg = new HttpResponseMessage {
			StatusCode = HttpStatusCode.OK, Content = content};
		return msg;
	}

	private HttpResponseMessage Send_message(string message_with_image_information_from_storage_service) { //throws CannotPostToQueueException {}
		try{			
			// send message to queue
			q.Add(message_with_image_information_from_storage_service);
			Console.WriteLine("Msg Added to Q: " + message_with_image_information_from_storage_service);

			var random = new Random(); // simulating delay
			Thread.Sleep(random.Next(10,1000));
		}
		catch (Exception ex) {
			throw new Exception("CannotPostToQueueException: " + ex.Message);
		}		
		HttpResponseMessage msg = new HttpResponseMessage {StatusCode = HttpStatusCode.OK};
		return msg;
	} 

}


public class AzureStorageBlob {
	public AzureStorageBlob() {}
	public void UploadFromStream(MemoryStream? ms) {
		// fake
	}
}