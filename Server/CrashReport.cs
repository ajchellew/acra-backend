using System;
using AcraBackend.Common.Database.Model;
using Newtonsoft.Json.Linq;

namespace AcraBackend.Server
{
    /// <summary>
    /// Reads a JSON crash report into an object
    /// </summary>
    public class CrashReport
    {
        private const string IdField = "ID";
        private const string ProductField = "PRODUCT";
        private const string BrandField = "BRAND";
        private const string PhoneModelField = "PHONE_MODEL";
        private const string AndroidVersionField = "ANDROID_VERSION";
        private const string AvailableMemorySizeField = "AVAILABLE_MEM_SIZE";
        private const string TotalMemorySizeField = "TOTAL_MEM_SIZE";
        private const string EnvironmentField = "ENVIRONMENT";
        private const string DisplayField = "DISPLAY";
        private const string LogcatField = "LOGCAT";
        private const string StackTraceField = "STACK_TRACE";
        private const string StackTraceHashField = "STACK_TRACE_HASH";
        private const string AppStartDateField = "USER_APP_START_DATE";
        private const string CrashDateField = "USER_CRASH_DATE";
        private const string PackageNameField = "PACKAGE_NAME";
        private const string VersionNameField = "APP_VERSION_NAME";
        private const string VersionCodeField = "APP_VERSION_CODE";
        private const string IsSilentField = "IS_SILENT";
        private const string CustomDataField = "CUSTOM_DATA";

        public string Product { get; }
        public string Brand { get; }
        public string Model { get; }
        public int AndroidVersion { get; }
        public long AvailableMemorySize { get; }
        public long TotalMemorySize { get; }
        public string Environment { get; }
        public string Display { get; }
        public string Logcat { get; }
        public string StackTrace { get; }
        public string StackTraceHash { get; }
        public DateTime AppStartDate { get; }
        public DateTime CrashDate { get; }
        public string PackageName{ get; }
        public string VersionName { get; }
        public int VersionCode { get; }
        public string CustomData { get; }
        public bool IsSilent { get; }

        public CrashReport(string json)
        {
            var jObject = JObject.Parse(json);
            foreach (var keyValuePair in jObject)
            {
                if (keyValuePair.Value == null)
                    continue;

                var value = keyValuePair.Value.ToString();

                switch (keyValuePair.Key)
                {
                    case ProductField:
                        Product = value;
                        break;
                    case BrandField:
                        Brand = value;
                        break;
                    case PhoneModelField:
                        Model = value;
                        break;
                    case AndroidVersionField:
                        AndroidVersion = int.Parse(value);
                        break;
                    case AvailableMemorySizeField:
                        AvailableMemorySize = long.Parse(value);
                        break;
                    case TotalMemorySizeField:
                        TotalMemorySize = long.Parse(value);
                        break;
                    case EnvironmentField:
                        Environment = value;
                        break;
                    case DisplayField:
                        Display = value;
                        break;
                    case LogcatField:
                        Logcat = value;
                        break;
                    case StackTraceField:
                        StackTrace = value;
                        break;
                    case StackTraceHashField:
                        StackTraceHash = value;
                        break;
                    case AppStartDateField:
                        AppStartDate = DateTime.Parse(value);
                        DateTime.SpecifyKind(AppStartDate, DateTimeKind.Utc);
                        break;
                    case CrashDateField:
                        CrashDate = DateTime.Parse(value);
                        DateTime.SpecifyKind(CrashDate, DateTimeKind.Utc);
                        break;
                    case PackageNameField:
                        PackageName = value;
                        break;
                    case VersionNameField:
                        VersionName = value;
                        break;
                    case VersionCodeField:
                        VersionCode = int.Parse(value);
                        break;
                    case IsSilentField:
                        IsSilent = bool.Parse(value);
                        break;
                    case CustomDataField:
                        CustomData = value;
                        break;
                }
            }
        }

        public FatalReport ToFatal()
        {
            return new FatalReport
            {
                Product = Product, Brand = Brand, Model = Model, Display = Display, AndroidVersion = AndroidVersion, Environment = Environment,
                Logcat = Logcat, StackTrace = StackTrace, StackTraceHash = StackTraceHash, AppStartDate = AppStartDate, CrashDate = CrashDate,
                PackageName = PackageName, VersionName = VersionName, VersionCode = VersionCode, AvailableMemorySizeGb = AvailableMemorySize / 1e+9, 
                TotalMemorySizeGb = TotalMemorySize / 1e+9
            };
        }
    }
}
