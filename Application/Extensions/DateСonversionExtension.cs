namespace Application.Extensions
{
    public static class DateСonversionExtension
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
    }
}
