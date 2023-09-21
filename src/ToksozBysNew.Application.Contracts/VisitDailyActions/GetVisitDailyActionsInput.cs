using Volo.Abp.Application.Dtos;
using System;

namespace ToksozBysNew.VisitDailyActions
{
    public class GetVisitDailyActionsInput : PagedAndSortedResultRequestDto
    {
        public string FilterText { get; set; }

        public DateTime? VisitDailyDateMin { get; set; }
        public DateTime? VisitDailyDateMax { get; set; }
        public decimal? VisitDaily1Min { get; set; }
        public decimal? VisitDaily1Max { get; set; }
        public decimal? VisitDaily2Min { get; set; }
        public decimal? VisitDaily2Max { get; set; }
        public decimal? VisitDaily3Min { get; set; }
        public decimal? VisitDaily3Max { get; set; }
        public decimal? VisitDaily4Min { get; set; }
        public decimal? VisitDaily4Max { get; set; }
        public decimal? VisitDaily5Min { get; set; }
        public decimal? VisitDaily5Max { get; set; }
        public decimal? VisitDaily6Min { get; set; }
        public decimal? VisitDaily6Max { get; set; }
        public decimal? VisitDaily7Min { get; set; }
        public decimal? VisitDaily7Max { get; set; }
        public decimal? VisitDaily8Min { get; set; }
        public decimal? VisitDaily8Max { get; set; }
        public decimal? VisitDaily9Min { get; set; }
        public decimal? VisitDaily9Max { get; set; }
        public decimal? VisitDaily10Min { get; set; }
        public decimal? VisitDaily10Max { get; set; }
        public decimal? VisitDaily11Min { get; set; }
        public decimal? VisitDaily11Max { get; set; }
        public decimal? VisitDaily12Min { get; set; }
        public decimal? VisitDaily12Max { get; set; }
        public decimal? VisitDaily13Min { get; set; }
        public decimal? VisitDaily13Max { get; set; }
        public decimal? VisitDaily14Min { get; set; }
        public decimal? VisitDaily14Max { get; set; }
        public decimal? VisitDaily15Min { get; set; }
        public decimal? VisitDaily15Max { get; set; }
        public DateTime? VisitDailyCloseDateMin { get; set; }
        public DateTime? VisitDailyCloseDateMax { get; set; }
        public string VisitDailyNote { get; set; }
        public Guid? IdentityUserId { get; set; }

        public GetVisitDailyActionsInput()
        {

        }
    }
}