using System;
using System.IO;

namespace AcraBackend.Common.Database
{
    public class DatabaseUtil
    {
        // for dev/testing
        private const bool DeleteExisting = false;

        public static void EnsureDatabase()
        {
            string filePath = DatabaseContext.DatabaseFile;
            if (File.Exists(filePath) && DeleteExisting)
                File.Delete(filePath);

            try
            {
                using var context = new DatabaseContext();
                //DatabaseContext.Database.Migrate();
                context.Database.EnsureCreated();
                context.Dispose();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Failed to load database", ex);
            }
        }
    }
}