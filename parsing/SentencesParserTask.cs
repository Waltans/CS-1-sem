using System.Collections.Generic;
using System.Text;

namespace TextAnalysis
{
    static class SentencesParserTask
    {
        public static List<List<string>> ParseSentences(string text)
        {
            var textList = new List<List<string>>();

            var stopSymbol = text.Split(new string[] { ".", "?", "!", ";", "(", ")", ":" }, 
                System.StringSplitOptions.RemoveEmptyEntries);

            List<string> words = new List<string>();

            foreach (var sentens in stopSymbol)
            {
                words = BuilderAppend(sentens);
                if (words != null)
                    textList.Add(words);
            }

            return textList;
        }

        public static List<string> BuilderAppend(string sentens)
        {
            StringBuilder builder = new StringBuilder();
            var words = new List<string>();

            builder.Append(sentens);

            for(int i = 0; i < builder.Length; i++)
            {
                if ((char.IsLetter(builder[i]) || builder[i] == '\''))
                {
                    words.Add(builder[i].ToString().ToLower());
                }
                else
                {
                    builder[i] = ' ';
                }
            }
            return words;
        }
    }
}