using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Dartin.Managers;
using Dartin.Models;
using Newtonsoft.Json;

namespace UnitTests
{
    public class ParserTest
    {
        [Theory]
        [InlineData(1, 20, "20")]
        [InlineData(2, 20, "D20")]
        [InlineData(3, 20, "T20")]
        [InlineData(1, 50, "Bull")]
        [InlineData(1, 25, "OBull")]

        [InlineData(null, null, "jgskgjskl")]
        [InlineData(null, null, "D80")]
        [InlineData(null, null, "D-19")]

        public void Parsetest(int? multiplier, int? points, string input)
        {
            if (multiplier != null && points != null)
            {
                var testInput = JsonConvert.SerializeObject(Parser.ParseThrow(input));
                string expected = JsonConvert.SerializeObject(new Toss { Score = points.Value, Multiplier = multiplier.Value });
                Assert.Equal(expected, testInput);
            }
            else
            {
                var testInput = Parser.ParseThrow(input);
                Assert.Null(testInput);
            }
        }
    }
}
