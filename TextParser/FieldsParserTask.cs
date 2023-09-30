using System.Collections.Generic;
using System.Text;
using NUnit.Framework;

namespace TableParser
{
    [TestFixture]
    public class FieldParserTaskTests
    {
        
        [TestCase("text", new[] {"text"})]
        [TestCase("hello world", new[] {"hello", "world"})]
        [TestCase("text", new[] {"text"})]
        [TestCase("hello world", new[] {"hello", "world"})]
        [TestCase("'a b'",  new[]{"a b"})]
        [TestCase("hello  world", new[] {"hello", "world"})]
        [TestCase(" ' ' ", new[] {" "})]
        [TestCase(" '' ", new[] {""})]
        [TestCase("", new string[0])]
        [TestCase("'a ", new[] {"a "})]
        [TestCase(@"'x\'x\'x'", new[] {"x'x'x"})]
        [TestCase(@"'""x""", new[] {@"""x"""})]
        [TestCase(@"""'a' 'b' 'c' 'd'""", new[] {"'a' 'b' 'c' 'd'"})]
        [TestCase("\"a \'b\' \'c\' d\"", new[] {"a 'b' 'c' d"})]
        [TestCase(@"""a b c d""e", new []{"a b c d","e"})]
        [TestCase(@"a""b c d e""", new []{"a", "b c d e"})]
        [TestCase(@"""\\""", new[] { "\\" })]
        [TestCase(@"""a \""b\""""", new []{@"a ""b"""})]
        public static void Test(string input, string[] expectedResult)
        {
            var actualResult = FieldsParserTask.ParseLine(input);
            Assert.AreEqual(expectedResult.Length, actualResult.Count);
            for (int i = 0; i < expectedResult.Length; ++i)
            {
                Assert.AreEqual(expectedResult[i], actualResult[i].Length);
            }
        }
    }

    public class FieldsParserTask
    {
        public static List<string> ParseLine(string line)
        {
            var fields = new List<string>();

            for (int pos = 0; ;)
            {
                var token = ReadField(line, pos);
                if (0 == token.Length) break;
                fields.Add(token.Value);
                pos = token.GetIndexNextToToken();
            }
            return fields;
        }

        private static Token ReadField(string line, int startIndex)
        {
            var pos = startIndex;
            while (pos < line.Length && line[pos] == ' ') ++pos;
            if (line.Length <= pos) return new Token("", pos, 0);
            if (line[pos] == '"' || line[pos] == '\'')
                return ReadQuotedField(line, pos);
            else
                return ReadSimpleField(line, pos);
        }

        private static Token ReadSimpleField(string line, int startIndex)
        {
            int stopIndex = startIndex + 1;
            while (stopIndex < line.Length &&
                    line[stopIndex] != ' ' &&
                    line[stopIndex] != '"' &&
                    line[stopIndex] != '\'') ++stopIndex;
            var length = stopIndex - startIndex;
            return new Token(line.Substring(startIndex, length),
                             startIndex, length);
        }

        private static Token ReadQuotedField(string line, int startIndex)
        {
            var ix = startIndex + 1;
            bool escape = false;
            var builder = new StringBuilder();
            for (; ix < line.Length; ++ix)
            {
                if (escape)
                {
                    if (line[ix] != '\\' && line[ix] != line[startIndex])
                    { 
                        builder.Append('\\'); 
                    }
                    builder.Append(line[ix]);
                    escape = false;
                }
                else if (line[ix] == '\\') escape = true;
                else if (line[ix] == line[startIndex]) break;
                else builder.Append(line[ix]);
            }
            return new Token(builder.ToString(), startIndex, ix - startIndex + 1);
        }
    }
}