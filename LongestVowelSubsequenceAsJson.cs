using System.Linq;
using System.Text.Json;
using System.Collections.Generic;
using System.Text;

public static string LongestVowelSubsequenceAsJson(List<string> words)
{
    HashSet<char> vowels = new HashSet<char> { 'a', 'e', 'i', 'o', 'u' };

    if (words.Count == 0)
    {
        return "[]";
    }

    //Save longest vowel sequence of each word
    List<string> longestVowelSequences = new List<string>();

    //Save vowel subSequences of current word
    List<string> vowelSubSequences = new List<string>();

    //Track all possible vowel subSequences for each word 
    StringBuilder sequence = new StringBuilder("");
    string currentWord;
    char currentLetter;
    string longestSubSequence;

    for (int wordCounter = 0; wordCounter < words.Count; wordCounter++)
    {
        currentWord = words[wordCounter];

        for (int letterCounter = 0; letterCounter < currentWord.Length; letterCounter++)
        {
            currentLetter = currentWord[letterCounter];

            if (vowels.Contains(currentLetter))
            {
                sequence.Append(currentLetter);
            }

            //Save the current sequence when the consonant letter is seen
            else
            {
                vowelSubSequences.Add(sequence.ToString());
                sequence.Clear();
            }
        }

        //Save the word that ends with vowel letters, or all letters are vowels
        vowelSubSequences.Add(sequence.ToString());    

        //Reset sequence that ends with vowel letters, or all letters are vowels
        sequence.Clear();        

        //Find the longest vowel subSeqeunce and save
        longestSubSequence = vowelSubSequences.MaxBy(s => s.Length);            
        longestVowelSequences.Add(longestSubSequence);

        //Reset the vowelSubSequences for the next word 
        vowelSubSequences.Clear();
    }

    return JsonSerializer.Serialize(longestVowelSequences);
}
