using System.Collections.Generic;
using System.Text;

namespace TextAnalysis
{
    static class FrequencyAnalysisTask
	{
		const int TwoWords = 2;
		const int Frequency = 1;
		const int OneWord  = 1;
		

        public static Dictionary<string, string> GetMostFrequentNextWords(List<List<string>> text)
		{
			var startDictionary = new Dictionary<string, Dictionary<string, int>>();			
			startDictionary = MakeDictionary(text, startDictionary,  OneWord);
			var lastDictionary = MakeDictionary(text, startDictionary, TwoWords);
			var result = new Dictionary<string, string>();
			var beginOfGramms = new StringBuilder();

            return TakeDictionaryWithFrequencyNgrams(lastDictionary, result, beginOfGramms);
		}

        public static Dictionary<string, string> TakeDictionaryWithFrequencyNgrams (Dictionary<string, Dictionary<string, int>> lastDictionary, Dictionary<string, string> result,
StringBuilder beginOfGramms)
        {
            var maximum = 0; 
			var endCombination = "";
            MakeResult(lastDictionary, maximum, endCombination, beginOfGramms, result);
            return result;
		}

		public static Dictionary<string, Dictionary<string, int>> MakeDictionary(List<List<string>> text, Dictionary<string, Dictionary<string, int>> startDictionary, int lastWord)
        {
            foreach (var sentence in text)
                for (var i = 0; i < sentence.Count - lastWord; i++) 
                {
					var beginning = ""; 
					var valueOfContinning = "";
					var firstSymbol = sentence[i];
					var secondSymbol = sentence[i + OneWord]; 

					if (lastWord == TwoWords) 
					{ 
						beginning = firstSymbol + " " + secondSymbol; 
						valueOfContinning = sentence[i + TwoWords]; 
					} 
					else
					{
						beginning = firstSymbol;
						valueOfContinning = secondSymbol; 
					} 
					
					MakeStartDictionary(startDictionary, beginning, valueOfContinning);
				}

            return startDictionary;
		}
		
		public static void MakeStartDictionary(Dictionary<string, Dictionary<string, int>> startDictionary, string beginning, string valueOfContinning)
		{ 
			if (startDictionary.ContainsKey(beginning))
            	if (startDictionary[beginning].ContainsKey(valueOfContinning)) 
                	startDictionary[beginning][valueOfContinning]++; 
				else 
					startDictionary[beginning][valueOfContinning] =  OneWord ; 
			else 			
				startDictionary[beginning] = new Dictionary<string, int> { { valueOfContinning, Frequency } }; 
		}

		public static void MakeResult(Dictionary<string, Dictionary<string, int>> lastDictionary, int maximum, string endCombination, StringBuilder beginOfGramms, Dictionary<string, string> result)
		{
			foreach (var pair in lastDictionary)
            {
                beginOfGramms.Append(pair.Key); 
                foreach (var value in lastDictionary[pair.Key]) 
                    if (value.Value > maximum) 
                    {
                        maximum = value.Value;
                        endCombination = value.Key; 
                    }
                    else if (value.Value == maximum)
						if (string.CompareOrdinal(value.Key, endCombination.ToString()) < 0) 								endCombination = value.Key;
 
                result.Add(beginOfGramms.ToString(), endCombination.ToString()); 
                maximum = 0;
                endCombination = "";
                beginOfGramms.Clear(); 
			}
        }
	}
}
