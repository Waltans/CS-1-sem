using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace Autocomplete
{
    internal class AutocompleteTask
    {
        public static string FindFirstByPrefix(IReadOnlyList<string> phrases, string prefix)
        {
            var index = LeftBorderTask.GetLeftBorderIndex(phrases, prefix, -1, phrases.Count) + 1;
            if (index < phrases.Count && phrases[index].StartsWith(prefix, StringComparison.OrdinalIgnoreCase))
                return phrases[index];
            
            return null;
        }
        public static string[] GetTopByPrefix(IReadOnlyList<string> phrases, string prefix, int count)
        {
            var leftIndex = LeftBorderTask.GetLeftBorderIndex(phrases, prefix, -1, phrases.Count) + 1;
            var totalCountElements =
                (GetCountByPrefix(phrases, prefix)) > count ? count : GetCountByPrefix(phrases, prefix);
            var resultArray = new string[totalCountElements];
            
            for (int i = 0; i < totalCountElements; i++)
            {
                resultArray[i] = phrases[leftIndex + i];
            }
            
            return resultArray;
        }
        
        public static int GetCountByPrefix(IReadOnlyList<string> phrases, string prefix)
        {
            var phrasesCount = RightBorderTask.GetRightBorderIndex(phrases, prefix, -1, phrases.Count) -
                               LeftBorderTask.GetLeftBorderIndex(phrases, prefix, -1, phrases.Count);
            return phrasesCount -1 ;
        }
    }

    [TestFixture]
    public class AutocompleteTests
    {
        [Test]
        public void TopByPrefix_IsEmpty_WhenNoPhrases()
        {
            var phrases = new List<string>();
            string prefix = "";
            var count = 0;
            var actualTopWords = AutocompleteTask.GetTopByPrefix(phrases, prefix, count);
            CollectionAssert.IsEmpty(actualTopWords);
        }

        [Test]
        public void CountByPrefix_IsTotalCount_WhenEmptyPrefix()
        {
            var phrases = new List<string>{"aa", "ab"};
            string prefix = "";
            var actualCount = AutocompleteTask.GetCountByPrefix(phrases, prefix);
            var expectedCount = 2;
            Assert.AreEqual(expectedCount, actualCount);
        }
        
        [Test]
        public void CountByPrefix_IsTotalCount_WhenPrefix()
        {
            var phrases = new List<string>{"aa", "ab"};
            string prefix = "a";
            var actualCount = AutocompleteTask.GetCountByPrefix(phrases, prefix);
            var expectedCount = 2;
            Assert.AreEqual(expectedCount, actualCount);
        }
        
        [Test]
        public void CountByPrefix_IsTotalCount_WhenAnotherPrefix()
        {
            var phrases = new List<string>{"aa", "ab"};
            string prefix = "b";
            var actualCount = AutocompleteTask.GetCountByPrefix(phrases, prefix);
            var expectedCount = 0;
            Assert.AreEqual(expectedCount, actualCount);
        }
    }
}