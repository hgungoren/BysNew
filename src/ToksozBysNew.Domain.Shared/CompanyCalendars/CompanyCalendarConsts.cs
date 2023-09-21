namespace ToksozBysNew.CompanyCalendars
{
    public static class CompanyCalendarConsts
    {
        private const string DefaultSorting = "{0}CompanyCalendarDate asc";

        public static string GetDefaultSorting(bool withEntityName)
        {
            return string.Format(DefaultSorting, withEntityName ? "CompanyCalendar." : string.Empty);
        }

    }
}