using ExperimentsWithSelenium.Data;
using ExperimentsWithSelenium.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ExperimentsWithSelenium
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Artur Gunia 2020");
            ExperimentsWithSelenium.Tests.Tests tests = new Tests.Tests();
            tests.SetUp();
            List<TestResult> validResults = tests.CalculateFactorialUsingHomePage(ITestData.GetValidTestData());
            List<TestResult> invalidResults = tests.CalculateFactorialUsingHomePage(ITestData.GetInvalidTestData());
            List<TestResult> areSubPagesValid = tests.VisitSubPages();

            Console.WriteLine();
            Console.WriteLine("Results for factorial calculation test, using VALID data");
            foreach (TestResult testResult in validResults)
            {
                testResult.Validate();
                Console.WriteLine(testResult);
            }
            
            Console.WriteLine();
            Console.WriteLine("Results for factorial calculation test, using INVALID data");
            foreach (TestResult testResult in invalidResults)
            {
                testResult.Validate();
                Console.WriteLine(testResult);
            }

            Console.WriteLine();
            Console.WriteLine("Results for subpage visits");
            foreach (TestResult testResult in areSubPagesValid)
            {
                testResult.Validate();
                Console.WriteLine(testResult);
            }

            Console.WriteLine("---The End---");
            Console.WriteLine("----------");

            tests.TearDown();
        }
        
    }
}
