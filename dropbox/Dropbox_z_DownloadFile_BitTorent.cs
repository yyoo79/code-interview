
// STEP#1: if chucks are given - just do something like interval merge
public class Dropbox_z_DownloadFile_BitTorent {
	public Dropbox_z_DownloadFile_BitTorent() { }
		
	public bool IsFileDone(List<Chunk> chunks, int size) {
		if (chunks == null || chunks.Count == 0) return false;
		//Array.Sort(chunks, (a, b) -> a.start - b.start);
		chunks.Sort();
		if(chunks[0].start != 0) return false;
		
		int end = chunks[0].end;
		for(int i = 1; i < chunks.Count; i++) {
			Chunk chunk = chunks[i];
			if(chunk.start > end)
				return false;
			else
				end = Math.Max(end, chunk.end);
		}
		return end == size;
	}
}

// STEP#2: if chunks are coming in stream - Use PriorityQueue
public class Dropbox_z_DownloadFile_BitTorent_PQ {
	
	private PriorityQueue<Chunk, int> chunks; // (Chunk, Chunk.size)
	int size;

	public Dropbox_z_DownloadFile_BitTorent_PQ(int size) {
		chunks = new PriorityQueue<Chunk, int>();
		this.size = size;
	}

	public void AddBlock(Chunk chunk) {
		chunks.Enqueue(chunk, chunk.size);
		if (chunks.Count > 1) {
			Chunk smallest = chunks.Dequeue();
			// keep mering with continous chunks
			while (chunks.Count > 0 && chunks.Peek().start <= smallest.end) {
				Chunk c = chunks.Dequeue();
				smallest.end = Math.Max(smallest.end, c.end);
			}
			chunks.Enqueue(smallest, smallest.size);
		}
	}
		
	public bool IsFileDone_Stream() {
		if (chunks.Count > 0 && chunks.Peek().start == 0 && chunks.Peek().end == size)
			return true;
		return false;
	}
	

}

public class Chunk {
	public int start;
	public int end;
	public int size;
	public Chunk() {}
}