using System;

namespace MyService
{
    public class SampleCalcUtil
    {
        public static bool IsEven(int num)
        {
            if (num <= 3)
                return num % 2 == 0;

            // for TDD as per the official doc
            throw new NotImplementedException("Not implemented.");
        }
    }
}
