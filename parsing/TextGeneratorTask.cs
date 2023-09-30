using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace TextAnalysis
{
    static class TextGeneratorTask
    {
        public static string ContinuePhrase(
            Dictionary<string, string> nextWords,
            string phraseBeginning,
            int wordsCount)
        {
            StringBuilder fullPhrase = new StringBuilder(phraseBeginning);
            for (int i = 0; i < wordsCount; i++)
            {
                
            }
            return fullPhrase.ToString();
        }
    }
}