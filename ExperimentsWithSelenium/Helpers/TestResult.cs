using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Numerics;

namespace ExperimentsWithSelenium.Helpers
{
    class TestResult
    {
        public string InputData;
        public string ResultData;
        private bool _isResultCorrect = false;
        public bool IsResultCorrect => _isResultCorrect;

        public TestResult(string inputData, string resultData)
        {
            this.InputData = inputData;
            this.ResultData = resultData;
        }

        public void Validate()
        {
            this._isResultCorrect = false;

            //Dangerous, Very bad "result collection" aproach, xUnit would be better for this
            if (ResultData.StartsWith("True") || ResultData.StartsWith("Please enter an integer"))
                this._isResultCorrect = true;
            else
            {

                try
                {
                    var number = Convert.ToUInt64(InputData);

                    /// Might be not optimized
                    /// BUT: Readabity is key here
                    if ((number < 0 || number > 170) && !ResultData.Contains("Please enter an integer"))
                    {
                        this._isResultCorrect = false;

                        /* Infinity is not a valid response and system shouldnt allow for numbers > 170 but if it changes than this:
                        
                        if (number > 170 && ResultData.Contains("Infinity"))
                            this._isResultCorrect = true;
                        */
                    }
                    else
                    {
                        var factorial = CalculateFactorial(number);

                        if (EqualsStringInScientificNotation(factorial, ResultData))
                            this._isResultCorrect = true;
                    }

                }
                catch (FormatException e)
                {
                    this._isResultCorrect = false;
                }
                catch (Exception e)
                {
                    this._isResultCorrect = false;
                }
            }
        }

        /// <summary>
        /// This should be handled by Formater but Deadline aproaches 
        /// </summary>
        public bool EqualsStringInScientificNotation(BigInteger bigInteger, string formatedStringResult)
        {
            bool equals = false;
            var sb = new StringBuilder(formatedStringResult);

            var index = -1;
            index = sb.ToString().IndexOf(',');
            if (index >= 0)
                sb.Remove(index, 1);

            index = -1;
            index = sb.ToString().IndexOf('.');
            if (index >= 0)
                sb.Remove(index, 1);

            index = sb.ToString().IndexOf('e');
            if (index >= 0)
                sb.Remove(index - 1, sb.Length - index + 1);  // last digit removed to accomodate rounding error

            
            equals = bigInteger.ToString().StartsWith(sb.ToString().Trim());

            return equals;
        }

        public BigInteger CalculateFactorial(BigInteger number)
        {
            if (number == 1 || number == 0)
                return 1;
            else
                return number * CalculateFactorial(number - 1);
        }

        public override string ToString()
        {
            var output = new StringBuilder();

            if (this._isResultCorrect)
                output.Append("PASSED ");
            else
                output.Append("FAILED ");

            output.Append("Output: ").Append(ResultData).Append(" for Input: ").Append(InputData);

            return output.ToString();
        }
    }
}
