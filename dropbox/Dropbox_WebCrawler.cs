/*
(From python code)
Given a URL, crawl that webpage for URLs, and then continue crawling until you've visited all URLs
Assume you have an API with two methods:
	get_html_content(url) -> returns html of the webpage of url
	get_links_on_page(html) -> returns array of the urls in the html
Do this in a breadth-first style manner (it's just easier).

Walkthrough: (from java code)
First, single thread version…
Talked a bit about DFS, BFS, stack overflow… endless depth etc problem…
Implemented simple BFS, using result set to do visisted checking as well
 
Multithreading:
Which part is bottle neck: the getURL part… network latency etc… parser etc
I mentioned Master / slave model, and how it can be achieved using ThreadPool, Callable, and check futures…
and metioned it is even more efficient if we don’'t do the syn of these queue, set checking logic just let master manage all these… he seems like this…
 
Then, he asked, in some language/machine, there is no support of threadPool, how would we do it …
He gave an API… to let you avoid writing some setup code///
setThread(THREAD_COUNT, method)…
implement the method… actually it is the BFS method… but with right locking
 
There is some back and forth about where to put lock, and
also asked the terminating conditions… (queue is empty and no working thread)

*/
public class WebCrawler_SingleThread {

	public List<string> get_links_on_page(string html) { // THIS IS API Test
		return new List<string>{"aa","bb","cc"};
	}
	HashSet<string> visited;
	Queue<string> q;

	public WebCrawler_SingleThread(string url){ // url might not be available at init
		visited = new HashSet<string>();
		q = new Queue<string>();
		// visited.Add(url);
		// q.Enqueue(url);		
	}
	
	public List<string> CrawlBFS(string url){
		
		visited.Add(url);
		q.Enqueue(url);

		while (q.Any()) {

			string cur = q.Dequeue();
			var urlList = get_links_on_page(cur); // Interviewer asks which line is the bottleneck. IT'S THIS ONE!

			foreach(string each in urlList) {
				if (!visited.Contains(each)) {
					q.Enqueue(each);
					visited.Add(each);
				}
			}

		}

		return visited.ToList();

	}

	public List<string> CrawlDFS(string url) {
		DFS(url);
		return visited.ToList();
	}
	private void DFS(string url) {

		// exit case - null or already visited
		if (url == null || visited.Contains(url)) return;

		visited.Add(url);
		var urlList = get_links_on_page(url); // Interviewer asks which line is the bottleneck. IT'S THIS ONE!

		foreach(string each in urlList) {
			DFS(each);
		}

		return;
	}


}