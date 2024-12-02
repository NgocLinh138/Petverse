using Contract.Constants;
using Contract.Enumerations;
using Domain.Abstractions.EntityBase;

namespace Domain.Entities
{
    public class ReportImage : EntityBase<int>, IDateTracking
    {
        public int ReportId { get; set; }
        public MediaType Type { get; set; }
        public string URL { get; set; }
        public DateTime CreatedDate { get; set; } = TimeZones.GetSoutheastAsiaTime();
        public DateTime? UpdatedDate { get; set; }

        public virtual Report Report { get; set; } = null!;
    }
}
