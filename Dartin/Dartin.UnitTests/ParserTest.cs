using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Dartin.Managers;

namespace UnitTests
{
    public class ParserTest
    {
        [Fact]
        public void Parsetest()
        {
            Assert.Equal(20 ,Parser.ParseThrow("20"));
            Assert.Equal(40 ,Parser.ParseThrow("D20"));
            Assert.Equal(60 ,Parser.ParseThrow("T20"));
            Assert.Equal(50 ,Parser.ParseThrow("Bull"));
            Assert.Equal(25 ,Parser.ParseThrow("OBull"));

            Assert.Equal(-1 ,Parser.ParseThrow("ghsjkghskj"));
            Assert.Equal(-1 ,Parser.ParseThrow("D80"));
            Assert.Equal(-1 ,Parser.ParseThrow("-10"));
        }
    }
}
