using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AcraBackend.Common.Database.Model
{
    public class BaseReport
    {
        [Key] 
        public Guid Id { get; set; } = Guid.NewGuid();

        public string Product { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public int AndroidVersion { get; set; }

        public string PackageName { get; set; }
        public string VersionName { get; set; }
        public int VersionCode { get; set; }

        public int Occurences { get; set; } = 1;
        public DateTime LastOccurence { get; set; } = DateTime.UtcNow;

        public int ClosedVersionCode { get; set; }
        public DateTime? Closed { get; set; }

        public int DaysClosed => Closed.HasValue ? (int)(DateTime.UtcNow - Closed.Value).TotalDays : 0;
    }

    [Table("FatalReports")]
    public class FatalReport : BaseReport
    {
        public double AvailableMemorySizeGb { get; set; }

        public double TotalMemorySizeGb { get; set; }

        public string Environment { get; set; }

        public string Display { get; set; }

        public string Logcat { get; set; }

        public string StackTrace { get; set; }

        public string StackTraceHash { get; set; }

        public DateTime AppStartDate { get; set; }

        public DateTime CrashDate { get; set; }



        public int TimeToCrash => (int)(CrashDate - AppStartDate).TotalMinutes;
        
        public int RoundedAvailableGbs => (int)Math.Round(AvailableMemorySizeGb);

        public int RoundedTotalGbs => (int)Math.Round(TotalMemorySizeGb);
    }

    public class SilentReport : BaseReport
    {
        public string CustomData { get; set; }

        public string IsSilent { get; set; }
    }
}
