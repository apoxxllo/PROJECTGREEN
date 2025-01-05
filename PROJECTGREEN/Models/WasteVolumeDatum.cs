using System;
using System.Collections.Generic;

namespace PROJECTGREEN.Models;

public partial class WasteVolumeDatum
{
    public int Id { get; set; }

    public int? BiodegradableCollected { get; set; }

    public int? NonbioCollected { get; set; }

    public int? RecyclablesCollected { get; set; }

    public int? MonthlyRecycled { get; set; }

    public int? UncollectedWaste { get; set; }

    public int? AverageHousehouldRecyclingRate { get; set; }

    public int? EwasteRecycled { get; set; }

    public int? PlasticfreeReductionImpact { get; set; }

    public int? CompostableWaste { get; set; }

    public int? MrfProcessingCapacity { get; set; }

    public string? BiodegradableRemarks { get; set; }

    public string? NonbioRemarks { get; set; }

    public string? RecyclablesRemarks { get; set; }

    public string? MonthlyRecycledRemarks { get; set; }

    public string? UncollectedRemarks { get; set; }

    public string? AvarageHouseHoldRemarks { get; set; }

    public string? EwasteRemarks { get; set; }

    public string? PlasticFreeRemark { get; set; }

    public string? CompostableRemarks { get; set; }

    public string? MrfRemarks { get; set; }

    public int? RecyclingSuccessRate { get; set; }

    public int? TotalPopulation { get; set; }

    public int? TotalHouseholds { get; set; }

    public int? DailyWasteBio { get; set; }

    public int? DailyWasteNonbio { get; set; }

    public int? DailyWasteRecyclable { get; set; }

    public int? HouseholdSegregation { get; set; }

    public int? CommunityComposting { get; set; }
}
