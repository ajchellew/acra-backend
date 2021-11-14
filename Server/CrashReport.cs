using System;
using AcraBackend.Common.Database.Model;
using AcraBackend.Server.Utils;
using Newtonsoft.Json;

namespace AcraBackend.Server
{
    /// <summary>
    /// Reads a JSON crash report into an object
    /// </summary>
    public class CrashReport
    {
        [JsonProperty(PropertyName = "ID")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "PRODUCT")]
        public string Product { get; set; }

        [JsonProperty(PropertyName = "BRAND")]
        public string Brand { get; set; }

        [JsonProperty(PropertyName = "PHONE_MODEL")]
        public string Model { get; set; }

        [JsonProperty(PropertyName = "ANDROID_VERSION")]
        public int AndroidVersion { get; set; }

        [JsonProperty(PropertyName = "AVAILABLE_MEM_SIZE")]
        public long AvailableMemorySize { get; set; }

        [JsonProperty(PropertyName = "TOTAL_MEM_SIZE")]
        public long TotalMemorySize { get; set; }

        [JsonConverter(typeof(RawJsonConverter))]
        [JsonProperty(PropertyName = "ENVIRONMENT")]
        public string Environment { get; set; }

        [JsonConverter(typeof(RawJsonConverter))]
        [JsonProperty(PropertyName = "DISPLAY")]
        public string Display { get; set; }

        [JsonProperty(PropertyName = "LOGCAT")]
        public string Logcat { get; set; }

        [JsonProperty(PropertyName = "STACK_TRACE")]
        public string StackTrace { get; set; }

        [JsonProperty(PropertyName = "STACK_TRACE_HASH")]
        public string StackTraceHash { get; set; }

        [JsonProperty(PropertyName = "USER_APP_START_DATE")]
        public DateTime AppStartDate { get; set; }

        [JsonProperty(PropertyName = "USER_CRASH_DATE")]
        public DateTime CrashDate { get; set; }

        [JsonProperty(PropertyName = "PACKAGE_NAME")]
        public string PackageName{ get; set; }

        [JsonProperty(PropertyName = "APP_VERSION_NAME")]
        public string VersionName { get; set; }

        [JsonProperty(PropertyName = "APP_VERSION_CODE")]
        public int VersionCode { get; set; }

        [JsonConverter(typeof(RawJsonConverter))]
        [JsonProperty(PropertyName = "CUSTOM_DATA")]
        public string CustomData { get; set; }

        [JsonProperty(PropertyName = "IS_SILENT")]
        public bool IsSilent { get; set; }

        public static CrashReport FromJson(string json)
        {
            return JsonConvert.DeserializeObject<CrashReport>(json, new JsonSerializerSettings
            {
                DateTimeZoneHandling = DateTimeZoneHandling.Utc
            });
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
