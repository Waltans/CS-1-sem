using System;
using System.Collections.Generic;
using System.Linq;

namespace Passwords
{
    public class CaseAlternatorTask
    {
        //Тесты будут вызывать этот метод
        public static List<string> AlternateCharCases(string lowercaseWord)
        {
            var result = new List<string>();
            AlternateCharCases(lowercaseWord.ToCharArray(), 0, result);
            result = result.Union(result).ToList();
            return result;
        }

        static void AlternateCharCases(char[] word, int startIndex, List<string> result)
        {
            if (startIndex == word.Length)
            {
                result.Add(new string(word));
                return;
            }
            
            AlternateCharCases(word, startIndex + 1, result);
            word[startIndex] = char.ToUpper(word[startIndex]);
            
            if (char.IsLetter(word[startIndex]))
            {
                if (word[startIndex] == char.ToUpper(word[startIndex]))
                {
                    AlternateCharCases(word, startIndex + 1, result);
                    word[startIndex] = char.ToLower(word[startIndex]);
                }
            }
        }
    }
}