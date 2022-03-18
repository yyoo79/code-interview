public class TopKPhoto {

	// public static void Main(string[] args) {

	// 	Console.WriteLine("Hello TopKPhoto!");

	// }

	// ** For non-streaming **

	// Approach #1: Count freq + Bucket sort
	// O(n), cons is the bucket is sparse, if there is one extreme freq, inefficient space usage
	public List<int> TopKViewPhoto(int[] photoIds, int k) {
    	List<int> result = new List<int>();
    	if (photoIds == null || photoIds.Length < 1) return result;

		Dictionary<int, int> freqMap = new Dictionary<int, int>(); // key:id, value: count
    	int maxFreq = 0;
    	foreach (int id in photoIds) {
			if (!freqMap.ContainsKey(id)) freqMap[id] = 0;
			freqMap[id]++;			
			maxFreq = Math.Max(maxFreq, freqMap[id]);			
		}

		List<int>[] buckets = new List<int>[maxFreq + 1];
		foreach (var kv in freqMap) {
			if (buckets[kv.Value] == null)
				buckets[kv.Value] = new List<int>();
			buckets[kv.Value].Add(kv.Key);
		}

		for (int i = buckets.Length - 1; i >= 0; i++) {
			if (buckets[i] != null) {
				var ids = buckets[i];
				if (k >= buckets[i].Count) {
					result.AddRange(ids);
					k -= buckets[i].Count;
				} else {
					for (int j = 0; j < k; j++) {
						result.Add(ids[j]);
					}
					break;
				}
			}
		}
		return result;
	}


	// Approach #2: using max heap - PriorityQueue
	// using k-max heap to maintain k most viewed. T: O(nlogk)
	public List<int> topKViewPhoto_PQ(int[] photoIds, int k) {
		List<int> result = new List<int>();
		if (photoIds == null || photoIds.Length < 1) return result;

		Dictionary<int, int> freqMap = new Dictionary<int, int>(); // key:id, value: count
		foreach (int id in photoIds) {
			if (!freqMap.ContainsKey(id)) freqMap[id] = 0;
			freqMap[id]++;
		}

		// PriorityQueue<View> topKView = new PriorityQueue<>((a,b)->a.freq - b.freq);
		PriorityQueue<int, int> topKView = new PriorityQueue<int, int>();

		foreach(var kv in freqMap) {
			if (topKView.Count < k) {
				topKView.Enqueue(kv.Key, -1 * kv.Value); // 1, 2, 3 => -3, -2, -1
			} else {
				
			}
		}

		// [todo] translate this to C#
		// for(Map.Entry<Integer, Integer> freqEntry: freqMap.entrySet()) {
		// 	View view = new View(freqEntry.getKey(), freqEntry.getValue());
		// 	if(topKView.size()<k) {
		// 		topKView.add(view);
		// 	} else {
		// 		if(freqEntry.getValue()>topKView.peek().freq) {
		// 			topKView.poll();
		// 			topKView.offer(view);
		// 		}
		// 	}
		// }

		// while(!topKView.isEmpty()) {
		// 	result.add(topKView.poll().id);
		// }

		return result;

	}

	class View {
		int id;
		int freq;
		public View(int id, int freq) {
			this.id = id; this.freq = freq;
		}
	}

}