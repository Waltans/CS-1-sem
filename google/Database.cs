using System.Collections.Generic;
using System.Linq;

namespace PocketGoogle
{
    class Database : Dictionary<string, Dictionary<int, List<int>>>
    {
        public void Add(int documentId, int position, string word)
        {
            var docDict = ContainsKey(word) ? this[word]
                : this[word] = new Dictionary<int, List<int>>();
            
            var docList = docDict.ContainsKey(documentId) ? docDict[documentId]
                : docDict[documentId] = new List<int>();
            docList.Add(position);
        }

        public List<int> GetIds(string word) =>
            ContainsKey(word) ? this[word].Keys.ToList() : new List<int>();

        public List<int> GetPositions(int documentId, string word) =>
            ContainsKey(word) && this[word].ContainsKey(documentId) ? this[word][documentId]
                : new List<int>();

        public void RemoveDocument(int documentId)
        {
            foreach (var docDict in this.Values) docDict.Remove(documentId);
        }
    }
}