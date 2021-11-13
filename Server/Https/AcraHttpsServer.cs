using System;
using System.Net;
using System.Net.Sockets;
using AcraBackend.Server.Utils;
using NetCoreServer;

namespace AcraBackend.Server.Https
{
    /*
     "C:\Program Files\Git\usr\bin\openssl" req -nodes -x509 -newkey rsa:4096 -keyout key.pem -out cert.cer -days 365 -subj '/CN=IP-ADDRESS' -addext "subjectAltName = IP.1:IP-ADDRESS"
    "C:\Program Files\Git\usr\bin\openssl" pkcs12 -export -out cert.pfx -inkey key.pem -in cert.cer
    */

    class AcraHttpsServer : HttpsServer
    {
        public SerialQueue TaskQueue { get; } = new();

        public AcraHttpsServer(SslContext context, IPAddress address, int port) : base(context, address, port) { }

        protected override SslSession CreateSession() { return new AcraHttpsSession(this); }

        protected override void OnError(SocketError error)
        {
            Console.WriteLine($"HTTPS server caught an error: {error}");
        }
    }
}