namespace ToksozBysNew.InvoiceDetails
{
    public static class InvoiceDetailConsts
    {
        private const string DefaultSorting = "{0}InvoiceDetailQuantity asc";

        public static string GetDefaultSorting(bool withEntityName)
        {
            return string.Format(DefaultSorting, withEntityName ? "InvoiceDetail." : string.Empty);
        }

        public const int InvoiceDetailQuantityMinLength = 1;
        public const int InvoiceDetailQuantityMaxLength = 9999999;
        public const decimal InvoiceDetailPriceMinLength = 0;
        public const decimal InvoiceDetailPriceMaxLength = 99999999; 
    }
}