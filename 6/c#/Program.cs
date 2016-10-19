using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CodeCata
{
    public class Program
    {   

        public static List<string> readFile () 
        {
            // Word list location
            var wordFile = System.IO.Directory.GetCurrentDirectory() + "/wordList.txt";

            List<string> wordList = new List<string>();

            using (var reader = new StreamReader(File.OpenRead(wordFile))) 
            {
                string line;
                while ((line = reader.ReadLine()) != null) 
                {
                    wordList.Add(line);
                }
                return wordList;
            }
        }

        public static Dictionary<string, List<string>> groupAnagrams (List<string> words) 
        {
            var anagrams = new Dictionary<string, List<string>>();
            foreach (string word in words) {
                var sortedWord = String.Concat(word.OrderBy(c => c));

                if (anagrams.ContainsKey(sortedWord)) 
                {
                    anagrams[sortedWord].Add(word);
                } 
                else 
                {
                    anagrams.Add(sortedWord, new List<string>{word});
                }
            }
            return anagrams;
        }

        public static Dictionary<string, List<string>> cleanupAnagrams (Dictionary<string, List<string>> anagrams) 
        {   
            // Keys to be removed
            var singleWords = anagrams.Where(pair => pair.Value.Count < 2)
                        .Select(pair => pair.Key)
                        .ToList();
            
            // Remove single words
            foreach (var singleWord in singleWords)
            {
                anagrams.Remove(singleWord);
            }

            return anagrams;
        }

        public static void Main (string[] args)
        {   
            
            // Get word list
            var wordList = readFile();

            // Sort into anagrams
            var anagrams = groupAnagrams(wordList);
            
            // Remove single words
            var cleanedAnagrams = cleanupAnagrams(anagrams);

            // Print result
            foreach (KeyValuePair<string, List<string>> pair in cleanedAnagrams)
            {   
                Console.WriteLine("Sorted String: " + pair.Key);
                pair.Value.ForEach(i => Console.Write("{0}\t", i));
                Console.WriteLine(System.Environment.NewLine);
            }
            Console.WriteLine("Total anagram groups found: " + anagrams.Count);
        }
    }
}
