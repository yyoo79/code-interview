/*
Dropbox
	Find Byte in File
	Given a pattern of bytes, return true if the pattern is a subarray of the file content.
	The brute force solution is to try to match the pattern to a subset of the text as shown in the function
	FindByteInFile.containsBytes below. (This problem is also called "Finding a Needle in a Haystack" and "StrStr")
	This will lead to lots of wasted time pursuing partial matches. To reduce this, we can use a rolling hash
	to find all the candidate spots in the string. 
	Look up Rabin-Karp's algorithm for more background on rolling hashes.
	Your interviewer might not even want you to actually write a rolling hash function. Ask if you can assume
	that there's a rolling hash function that lets you get hashes of length pattern.length from the file.
	Your interviewer might also want to you to mimic the process of reading and opening the file. 
*/

public class Dropbox_FindBytePattern {
	
	/*
	public static void Main(string[] arg) {
		
		Console.WriteLine("hello Dropbox_FindBytePattern");
		
		// simulating a new random file (text)
		// int sizeInMb = 1;
		// byte[] text = new byte[sizeInMb * 1024 * 1024];
		// Random rand = new Random();
		// rand.NextBytes(text);
		byte[] text = new byte[10] {1, 2, 3, 4, 5, 99, 7, 99, 9, 10};

		// simulating a new pattern
		// byte[] pattern = new byte[5];
		// rand.NextBytes(pattern);
		byte[] pattern = new byte[3] {99, 7, 98};
		
		FindByteInFile findByteInFile = new FindByteInFile();

		// var result = findByteInFile.ContainsBytes(pattern, text);
		// Console.WriteLine("ContainsBytes: Result = " + result);
		
		var result = findByteInFile.ContainsBytesRollingHash(pattern, text);
		Console.WriteLine("ContainsBytesRollingHash: Result = " + result);
		
	}
	*/
	
	// public bool ContainBytes(byte[] pattern, byte[] text) {		
	// 	for (int i = 0; i < text.Length - pattern.Length; i++) {	
	// 		int j = 0;
	// 		while (j < pattern.Length && pattern[j] == text[i+j]) {
	// 			j++;
	// 		}
	// 		if (j == pattern.Length) return true;
	// 	}
	// 	return false;
	// }
}

public class FindByteInFile {

	// Simple substring match    
    public bool ContainsBytes(byte[] pattern, byte[] file) {
        
		// null check
		if (pattern == null && file == null) return true;
		if (file == null && pattern != null) return false;        

		// base check
        if (pattern != null && file != null && pattern.Length > file.Length) return false;
        
		if (file != null && pattern != null) {
			for (int start = 0; start <= file.Length - pattern.Length; start++) {
				int end = 0;
				while (end < pattern.Length && pattern[end] == file[start + end]) {
					end++;
				}
				if (end == pattern.Length) {
					return true;
				}
			}
		}
        return false;
    }

	/*
		file = [1, 2, 3, 4, 3, 4, 5]
		pattern = [4, 3, 4]

	*/

    // -----------------------
    // ALTERNATIVE IMPLEMENTATION WITH ROLLING HASH
    // -----------------------
    public bool ContainsBytesRollingHash(byte[] pattern, byte[] text) {
	
		// boundary check
        if (text.Length < pattern.Length) return false;

        int m = pattern.Length;
        int n = text.Length; 
		
		byte[] initialBytes = new byte[m]; // = Arrays.copyOfRange(text, 0, m);
		Array.Copy(text, initialBytes, m);
		
        RollingHash hashFun = new RollingHash(31, initialBytes);
        long patternHashVal = hashFun.Hash(pattern);

        for (int start = 0; start <= n - m; start++) {
            if (patternHashVal == hashFun.getCurrHashValue()) {
                //need to check byte by byte to ensure 
                int end = 0; 
                while (end < m && pattern[end] == text[start + end]) {
                    end++;
                }
                if (end == m) {
                    return true;
                }
            }
			// update
            if (start < n - m) {
                hashFun.Update(text[start], text[start + m]);
            }
        }
        return false;
    }
}

class RollingHash {
    // Ask if you can assume that you have a rolling hash class before you start implementing
    // your own rolling hash. Most interviewers won't make you create your own.
    private readonly int LARGE_PRIME = 105613;
	private int WINDOW_LENGTH;
    private long currHashValue;

	private int a;
	private int h = 1;

    public RollingHash(int a, byte[] initialBytes) {
        this.a = a;
        this.WINDOW_LENGTH = initialBytes.Length;
        // The value of h would be "pow(a, WINDOW_LENGTH - 1) % q 
        for (int i = 0; i < WINDOW_LENGTH - 1; i++) {
            //a^n % p = (a^n-1 % p * a%p)%p; 
            h = (h * a) % LARGE_PRIME;
        }
        currHashValue = Hash(initialBytes);
    }

    public long Hash(byte[] bytes) {
        int hashVal = 0;

        for (int i = 0; i < bytes.Length; i++) {
            hashVal = (a * hashVal + bytes[i]) % LARGE_PRIME;
        }
        return hashVal;
    }

    public long Update(byte removed, byte incoming) {
        // Relevant math:
        // (a + b) % p = (a % p + b % p) % p
        // (a - b) % p = (a % p - b % p) % p might give negative
        // (a * b) % p = (a %p  * b % p) % p
        currHashValue = (a * (currHashValue - removed * h) + incoming) % LARGE_PRIME;

        // We might get negative value of t, converting it to positive
        if (currHashValue < 0) {
            currHashValue += LARGE_PRIME;
        }
        return currHashValue;
    }

    public long getCurrHashValue() {
        return currHashValue;
    }
}

