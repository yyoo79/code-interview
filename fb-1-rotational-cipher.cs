// [FB Coding Practice] Rotational Cipher

using System;

class HelloWorld {
        
    static void Main() {
        string answer = rotationalCipher("Zebra-493?",3);
        Console.WriteLine(answer);
        answer = rotationalCipher("abcdefghijklmNOPQRSTUVWXYZ0123456789",39);
        Console.WriteLine(answer);
    }
    
    private static string rotationalCipher(String input, int rotationFactor) {
        // Write your code here
        StringBuilder sb = new StringBuilder();

        // (F:3) a -> d, Z -> C, 9 -> 2, 3 -> 6
        //
        string lowers = "abcdefghijklmnopqrstuvwxyz";
        string uppers = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        int alphaF = rotationFactor > 25 ? rotationFactor % 26 : rotationFactor;
        int numF = rotationFactor > 9 ? rotationFactor % 10 : rotationFactor;

        foreach(char c in input) {

          if (lowers.Contains(c)) {
            int newC = c + alphaF;
            newC = (newC > 'z') ? newC - 26: newC;
            sb.Append((char)newC);
          }
          else if (uppers.Contains(c)) {
            int newC = c + alphaF;
            newC = (newC > 'Z') ? newC - 26: newC;
            sb.Append((char)newC);
          }
          else if (Char.IsNumber(c)) {
            int newC = c + numF;
            newC = (newC > '9') ? newC - 10: newC;
            sb.Append((char)newC);
          }
          else sb.Append(c);

        }
    
        return sb.ToString();
  }
}
