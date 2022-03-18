/*
Question from PDF file:

This round is to design the log monitor. The interviewer specifies the format of the log.
{action: "view", page: "www.dropbox.com", location: "GB", time stamp: xxxx}
{action: "download", file: "hello.txt", time stamp: xxxx}
In the log, there must be an action and a time stamp, and then the others are determined regardless of what the action is. You
How to design the interface to create log, what functions are needed, and then discuss the monitor service and
The two web servers do not matter how they interact, how to store logs, what bottlenecks will be, how to improve, how to
What kind of scale, and then asked if you want to design a metric to monitor the health of your monitor service, you
What data will be collected. That's about it.
 
He didn't ask me to specifically calculate how much storage and how many servers were needed, but he said it was okay when scale up later.
How to sharding these.
The monitor service interacts with the web server. The monitor is how to receive the log data and how to get it.
After that, you need to do some aggregation, and then update the monitor data.


Question from LC post:

System Design : Logging system where all dropbox teams would call this api and log the event/something.
The log will be a list of key value pairs from different teams.
Later on all these teams can query on logs saying give all the entries where key=value. You have to return team specific results.
Every log can have different keys and values.
For eg:
team1 entry 1 : { id : "user1", key1 : 123, key2: 12.67 }
team2 entry 1 : { userguid : "HAK8916", key22 : 123.34}



*/
public class Dropbox_LoggingSystem {
	
	/*
	public static void Main(string[] arg) {
		Console.WriteLine("hello Dropbox_LoggingSystem");
	}
	*/
}