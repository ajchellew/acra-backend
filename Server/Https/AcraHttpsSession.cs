using System;
using System.Net.Sockets;
using AcraBackend.Common.Database.Dao;
using AcraBackend.Server.Utils;
using NetCoreServer;

namespace AcraBackend.Server.Https
{
    class AcraHttpsSession : HttpsSession
    {
        private readonly ReportsDao _dao = new();
        private readonly SerialQueue _taskQueue;

        public AcraHttpsSession(AcraHttpsServer server) : base(server)
        {
            // use the task queue from the server to ensure reports are added to the 
            // database one at a time, otherwise repeated of the same report are added as multiple entries
            _taskQueue = server.TaskQueue;
        }

        protected override void OnReceivedRequest(HttpRequest request)
        {
            // Only support POST type
            if (request.Method == "POST")
            {
                bool userAgentValid = false;
                bool contentTypeValid = false;
                bool isGzipped = false;

                // Check the connection is from ACRA and is JSON.
                for (int i = 0; i < request.Headers; i++)
                {
                    var (name, value) = request.Header(i);
                    
                    switch (name)
                    {
                        case "User-Agent" when value.StartsWith("Android ACRA"):
                            userAgentValid = true;
                            break;
                        case "Content-Type" when value == "application/json":
                            contentTypeValid = true;
                            break;
                        case "Content-Encoding" when value == "gzip":
                            isGzipped = true;
                            break;
                    }
                }

                if (userAgentValid && contentTypeValid) {
                    // Lets ACRA know the report was successful
                    SendResponseAsync(Response.MakeOkResponse());
                    // Decompress if needed
                    string json = isGzipped ? GzipHelper.DecompressGzip(request.BodyBytes) : request.Body;
                    // Add Task to queue to put report in database
                    _taskQueue.Enqueue(() => AddToDatabase(json));
                }
                else
                {
                    Console.WriteLine("POST received from wrong user agent or content-type is not JSON");
                    SendResponseAsync(Response.MakeErrorResponse());
                }
            }
            else
                SendResponseAsync(Response.MakeErrorResponse("Unsupported HTTP method: " + request.Method));
        }

        private void AddToDatabase(string json)
        {
            var report = CrashReport.FromJson(json);

            if (!report.IsSilent)
            {
                var isNewReport = _dao.OnFatal(report.ToFatal());
                Console.WriteLine((isNewReport ? "New" : "Repeated") + " Report Logged");
            }
            /*else
            {
                // todo  Silent custom reports   
            }*/
        }

        protected override void OnReceivedRequestError(HttpRequest request, string error)
        {
            Console.WriteLine($"Request error: {error}");
        }

        protected override void OnError(SocketError error)
        {
            Console.WriteLine($"HTTPS session caught an error: {error}");
        }
    }
}