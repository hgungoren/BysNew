namespace ToksozBysNew.Clinics
{
    public static class ClinicConsts
    {
        private const string DefaultSorting = "{0}ClinicName asc";

        public static string GetDefaultSorting(bool withEntityName)
        {
            return string.Format(DefaultSorting, withEntityName ? "Clinic." : string.Empty);
        }

    }
}