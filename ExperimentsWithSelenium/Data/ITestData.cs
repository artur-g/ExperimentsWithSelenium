using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ExperimentsWithSelenium.Data
{
    interface ITestData
    {
        public static List<string> GetValidTestData()
        {
            var Data = GetTestData(ExperimentsWithSelenium.Resource.ValidData);
            Random rand = new Random();

            for (int i = 0; i< 5; i++)
            {
                Data.Add(rand.Next(0, 171).ToString());
            }
            return Data;
        }
        public static List<string> GetInvalidTestData()
        {
            var Data = GetTestData(ExperimentsWithSelenium.Resource.InvalidData);
            Random rand = new Random();

            for (int i = 0; i < 5; i++)
            {
                Data.Add(rand.Next(171, 9999).ToString());
            }
            return Data;
        }

        private static List<string> GetTestData(string inputData)
        {
            var Data = inputData.Split("\r\n", StringSplitOptions.RemoveEmptyEntries).ToList<string>();
            return Data;
        }
    }
}
