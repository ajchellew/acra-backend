using System;
using System.Collections.Generic;
using System.Linq;
using AcraBackend.Common.Database.Model;

namespace AcraBackend.Common.Database.Dao
{
    public class ReportsDao
    {
        public bool OnFatal(FatalReport fatalReport)
        {
            bool newReport = false;

            using var context = new DatabaseContext();
            var fatalReportInDb = context.FatalReports.FirstOrDefault(report =>
                report.PackageName == fatalReport.PackageName && report.StackTraceHash == fatalReport.StackTraceHash);
            if (fatalReportInDb == null)
            {
                context.FatalReports.Add(fatalReport);
                newReport = true;
            }
            else
            {
                var fatal = fatalReportInDb;
                fatal.Occurences += 1;
                fatal.LastOccurence = DateTime.UtcNow;

                if (fatal.ClosedVersionCode != 0)
                {
                    fatal.Closed = null;
                    fatal.ClosedVersionCode = 0;
                    fatal.VersionCode = fatalReport.VersionCode;
                    fatal.VersionName = fatalReport.VersionName;
                }
            }
            context.SaveChanges();
            context.Dispose();

            return newReport;
        }

        public void OnSilent(SilentReport silentReport)
        {
            
        }

        public List<string> GetPackageNames()
        {
            using var db = new DatabaseContext();
            return db.FatalReports.Select(fatal => fatal.PackageName).Distinct().OrderBy(s => s).ToList();
        }

        public List<FatalReport> GetFatalReports(string packageName)
        {
            using var db = new DatabaseContext();
            return db.FatalReports.Where(fatal => fatal.PackageName == packageName).OrderBy(fatal => fatal.LastOccurence).ToList();
        }

        public void CloseReport(FatalReport report)
        {
            using var db = new DatabaseContext();
            var dbReport = db.FatalReports.First(fr => fr.Id == report.Id);
            dbReport.Closed = DateTime.UtcNow;
            dbReport.ClosedVersionCode = report.VersionCode;
            db.SaveChanges();
        }

        public void DeleteReport(FatalReport report)
        {
            using var db = new DatabaseContext();
            db.FatalReports.Remove(report);
            db.SaveChanges();
        }
    }
}
