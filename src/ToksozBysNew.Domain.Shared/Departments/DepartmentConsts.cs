namespace ToksozBysNew.Departments
{
    public static class DepartmentConsts
    {
        private const string DefaultSorting = "{0}DepartmentName asc";

        public static string GetDefaultSorting(bool withEntityName)
        {
            return string.Format(DefaultSorting, withEntityName ? "Department." : string.Empty);
        }

        public const int DepartmentNameMaxLength = 150;
    }
}