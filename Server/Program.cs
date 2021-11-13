using System;
using System.Net;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;
using AcraBackend.Common.Database;
using AcraBackend.Server.Https;
using NetCoreServer;

namespace AcraBackend.Server
{
    class Program
    {
        private const int Port = 9999;
        
        static void Main(string[] args)
        {
            Console.WriteLine($"HTTPS server port: {Port}");
            Console.WriteLine();
            
            DatabaseUtil.EnsureDatabase();;
            
            var context = new SslContext(SslProtocols.Tls12, new X509Certificate2("Res/cert.pfx", "password123"));
            var server = new AcraHttpsServer(context, IPAddress.Any, Port);

            server.Start();

            for (; ; )
            {
                string line = Console.ReadLine();
                if (string.IsNullOrEmpty(line))
                    break;
            }
            
            server.Stop();
            Console.WriteLine("Stopped");
        }
    }
}
