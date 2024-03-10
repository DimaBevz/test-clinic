using Application.Common.Enums;

namespace Application.Extensions
{
    public static class OperationsWithDatesExtension
    {
        public static int GetSubstractionInYears(this DateTime minuendDate, DateTime subtrahendDate)
        {
            int yearsDifference = minuendDate.Year - subtrahendDate.Year;

            if (minuendDate.Month < subtrahendDate.Month || (minuendDate.Month == subtrahendDate.Month && minuendDate.Day < subtrahendDate.Day))
            {
                yearsDifference--;
            }

            return yearsDifference;
        }

        public static int GetSubstractionInYears(this DateOnly minuendDate, DateOnly subtrahendDate)
        {
            int yearsDifference = minuendDate.Year - subtrahendDate.Year;

            if (minuendDate.Month < subtrahendDate.Month || (minuendDate.Month == subtrahendDate.Month && minuendDate.Day < subtrahendDate.Day))
            {
                yearsDifference--;
            }

            return yearsDifference;
        }

        public static DateComparisonResult CompareWith(this DateTime date1, DateTime date2)
        {
            var comparisonResult = date1.CompareTo(date2);

            DateComparisonResult resultInterpretation = comparisonResult switch
            {
                0 => DateComparisonResult.TheSame,
                -1 => DateComparisonResult.Earlier,
                1 => DateComparisonResult.Later,
                _ => throw new ArgumentException($"Argument value should be 0 || -1 || 1. Actual value: {comparisonResult}", nameof(comparisonResult)),
            };

            return resultInterpretation;
        }

        public static DateComparisonResult CompareWith(this DateOnly date1, DateOnly date2)
        {
            var comparisonResult = date1.CompareTo(date2);

            DateComparisonResult resultInterpretation = comparisonResult switch
            {
                0 => DateComparisonResult.TheSame,
                -1 => DateComparisonResult.Earlier,
                1 => DateComparisonResult.Later,
                _ => throw new ArgumentException($"Argument value should be 0 || -1 || 1. Actual value: {comparisonResult}", nameof(comparisonResult)),
            };

            return resultInterpretation;
        }
    }
}
