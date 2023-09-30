using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace TableParser
{
    [TestFixture]
    public class QuotedFieldTaskTests
    {
        [TestCase("' '",0,  " ", 3)]
        [TestCase("'x y'",0,"x y",5)]
        [TestCase("''", 0, "", 2)]
        [TestCase("\"a \'b\' \'c\' d\" \'\"1\" \"2\" \"3\"\'", 0, "a 'b' 'c' d", 13)]
        public void Test(string line, int startIndex, string expectedValue, int expectedLength)
        {
            var actualToken = QuotedFieldTask.ReadQuotedField(line, startIndex);
            Assert.AreEqual(new Token(expectedValue, startIndex, expectedLength), actualToken);
        }

        // Добавьте свои тесты
    }

    class QuotedFieldTask
    {
        public static Token ReadQuotedField(string line, int startIndex)
        {
            string startSymbol = " ";
            var stopSymbol = new string[] { "\'", "\"" };
            int length = 1;
            if (stopSymbol.Any(line[startIndex].ToString().Contains))
                startSymbol = stopSymbol.Single(x => x == line[startIndex].ToString());
            var resultString = new StringBuilder();
            for (int i = startIndex + 1; i < line.Length; i++)
            {
                if (line[i].ToString() == startSymbol && line[i - 1] != '\\')
                {
                    length++;
                    break;
                }
                else if (line[i] != '\\')
                    resultString.Append(line[i]);
                length++;
            }
            return new Token(resultString.ToString(), startIndex, length);
        }
    }
}