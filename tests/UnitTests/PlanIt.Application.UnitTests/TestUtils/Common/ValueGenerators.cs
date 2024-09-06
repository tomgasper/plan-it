namespace PlanIt.Application.UnitTests.TestUtils.Common;

public static class ValueGeneratorsUtils
{
    public static string GenerateSequentialGuid(int number)
    {
        if (number < 0)
        {
            throw new ArgumentException("Number can't be negative.", nameof(number));
        }

        string guidFormat = "00000000-0000-0000-0000-{0:D12}";
        string sequentialPart = number.ToString("D12");
        return string.Format(guidFormat, sequentialPart);
    }
}