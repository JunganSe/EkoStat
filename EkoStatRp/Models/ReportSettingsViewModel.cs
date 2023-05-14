using EkoStatLibrary.Enums;
using System.ComponentModel.DataAnnotations;

namespace EkoStatRp.Models;

public class ReportSettingsViewModel
{
    [Display(Name = "Highlight cost at or above")]
    [Range(0, 100, ErrorMessage = "Must be {1} to {2}.")]
    public decimal? CostLimit { get; set; }

    [Display(Name = "Type")]
    public LimitType CostLimitType { get; set; }

    [Display(Name = "Segment by")]
    public SegmentSize SegmentBy { get; set; }
}
