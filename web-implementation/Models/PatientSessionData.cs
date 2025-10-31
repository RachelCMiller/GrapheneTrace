//model for PatientSessionData (all data from csv file) that will be in the database
//Author: 2414111

using System.ComponentModel.DataAnnotations;

namespace GrapheneTrace.Web.Models;

public class PatientSessionData
{
    [Key]
    public int SessionId { get; set; }
    public string DeviceId { get; set; } = string.Empty;
    public int UserId { get; set; }
    public DateTime Start { get; set; }
    public DateTime? End { get; set; }
    public string RawData { get; set; } = string.Empty;
    public int? PeakSessionPressure { get; set; }
    public bool ClinicianFlag { get; set; } = false;
}
