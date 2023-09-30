using System;
using System.Collections.Generic;
using System.Linq;
 
namespace PocketGoogle
{
    public class Indexer : IIndexer
    {
        private readonly Dictionary<int, Dictionary<string, List<int>>> wordsIndex = new Dictionary<int, Dictionary<string, List<int>>>();
        
        private readonly Dictionary<string, HashSet<int>> positionOfWords = new Dictionary<string, HashSet<int>>();

        public void Add(int id, string documentText)
        {
            var splitWords = documentText.Split(new char[] { ' ', '.', ',', '!', '?', ':', '-', '\r', '\n' });
            wordsIndex.Add(id, new Dictionary<string, List<int>>());
 
            var position = 0;
            
            foreach (var word in splitWords)
            {
                AddWordsInContains(positionOfWords, word, id);

                AddIndexesWordCondition(wordsIndex, word, id, position);
                
                position += word.Length + 1;
            }
        }
 
        public List<int> GetIds(string word)
        {
            if (!positionOfWords.ContainsKey(word))
                return new List<int>();
            else
                return positionOfWords[word].ToList();
        }
 
        public List<int> GetPositions(int id, string word)
        {
            var positions = new List<int>();
            
            if (wordsIndex.ContainsKey(id) && wordsIndex[id].ContainsKey(word))
                positions = wordsIndex[id][word];
 
            return positions;  
        }
 
        public void Remove(int id)
        {
            var words = wordsIndex[id].Keys.ToArray();
 
            foreach (var word in words)            
                positionOfWords[word].Remove(id);
            
            wordsIndex.Remove(id);
        }

        private static void AddWordsInContains( IDictionary<string, HashSet<int>> positionOfWords, string word, int id)
        {
            if (!positionOfWords.ContainsKey(word))
                positionOfWords[word] = new HashSet<int>();
 
            if (!positionOfWords[word].Contains(id))
                positionOfWords[word].Add(id);
        }
        
        private static void AddIndexesWordCondition( IReadOnlyDictionary<int, Dictionary<string, List<int>>> wordsIndex, string word, int id,int position)
        {
            if (!wordsIndex[id].ContainsKey(word))
                wordsIndex[id].Add(word, new List<int>());
            
            wordsIndex[id][word].Add(position);
        }
    }
}