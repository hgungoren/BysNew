using DevExpress.XtraPrinting.Native;
using System;

namespace ToksozBysNew.Web.Pages.DevExtreme
{
    public class EmployeeTask
    {
        public int ID { get; set; }
        public string Subject { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime DueDate { get; set; }
        public string Status { get; set; }
        public Priority Priority { get; set; }
        public int Completion { get; set; }
    }
}
