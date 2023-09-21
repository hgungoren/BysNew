namespace ToksozBysNew.Doctors
{
    public static class DoctorConsts
    {
        private const string DefaultSorting = "{0}IsActive asc";

        public static string GetDefaultSorting(bool withEntityName)
        {
            return string.Format(DefaultSorting, withEntityName ? "Doctor." : string.Empty);
        }

    }
}