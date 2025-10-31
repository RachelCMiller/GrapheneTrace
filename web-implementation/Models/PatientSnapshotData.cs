//Model for PatientSnapshotData (32x32 comma separated ints, a freeze frame of the heatmap) that will be in the database
//Author: 2414111
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
namespace GrapheneTrace.Web.Models;

public class PatientSnapshotData
{
    [Key]
    public int SnapshotId { get; set; }
    public int SessionId { get; set; }
    public string DeviceId { get; set; } = string.Empty;
    public int UserId { get; set; }
    public DateTime? SnapshotTime { get; set; }
    public string SnapshotData { get; set; } = string.Empty;
    public int? PeakSnapshotPressure { get; set; }
    public float? ContactAreaPercent { get; set; }
    public float? CoefficientOfVariation { get; set; }
}