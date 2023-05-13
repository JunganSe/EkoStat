using EkoStatLibrary.Enums;
using System.ComponentModel.DataAnnotations;

namespace EkoStatRp.Models;

public class ReportSettingsViewModel
{
    [Display(Name = "Cost limit")]
    public decimal? CostLimit { get; set; }

    [Display(Name = "Type")]
    public LimitType CostLimitType { get; set; }

    [Display(Name = "Segment by")]
    public SegmentSize SegmentBy { get; set; }
}
