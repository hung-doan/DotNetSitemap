using DotNetSitemap.Core.Constrains;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetSitemap.Core.Test.Constrains
{
    [TestFixture]
    public class ChangeFreqTest
    {
        static object[] ToString_ReturnCorrectValueSource = new object[] {
            new object [] { ChangeFreq.Always, "always" },
            new object [] { ChangeFreq.Daily, "daily" },
            new object [] { ChangeFreq.Hourly, "hourly" },
            new object [] { ChangeFreq.Monthly, "monthly" },
            new object [] { ChangeFreq.Never, "never" },
            new object [] { ChangeFreq.Weekly, "weekly" },
            new object [] { ChangeFreq.Yearly, "yearly" },
        };
        static object[] Value_ReturnCorrectValueSource = new object[] {
            new object [] { ChangeFreq.Always, "always" },
            new object [] { ChangeFreq.Daily, "daily" },
            new object [] { ChangeFreq.Hourly, "hourly" },
            new object [] { ChangeFreq.Monthly, "monthly" },
            new object [] { ChangeFreq.Never, "never" },
            new object [] { ChangeFreq.Weekly, "weekly" },
            new object [] { ChangeFreq.Yearly, "yearly" },
        };

        [TestCaseSource("ToString_ReturnCorrectValueSource")]
        public void ToString_ReturnCorrectValue(ChangeFreq input, string expected)
        {
            Assert.AreEqual(input.ToString(), expected);

        }
        [TestCaseSource("Value_ReturnCorrectValueSource")]
        public void Value_ReturnCorrectValue(ChangeFreq input, string expected)
        {
            Assert.AreEqual(input.Value, expected);
        }
    }
}
