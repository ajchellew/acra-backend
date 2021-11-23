# acra-backend
.NET C# / WPF backend server and client for capturing and viewing Android ACRA crash reports over HTTPS 

Whilst significantly better backend solutions exist, we have C# applications as backend servers and wanted to add capturing the crash reports directly to these servers, without running a docker container for example.

## Server 

A simple HTTPS server that listens for HTTP POSTs from ACRA, reports get added to an SQLite Database via Entity Framework.

The server is basic and only captures unique stacktrace hashes, further reports just add to the occurence counter.

### Configuring ACRA in Android App

For HTTPS you require a certificate to use for the connection. To use a self signed certificate ensure the subject alternative name is set, or the host will not be trusted when ACRA tries sending.

To generate certificates, in Windows `openssl` can likely be found at `C:\Program Files\Git\usr\bin\`

<code>openssl req -nodes -x509 -newkey rsa:4096 -keyout key.pem -out cert.cer -days 365 -subj '/CN=<b>SERVER-IP-ADDRESS</b>' -addext "subjectAltName = IP.1:<b>SERVER-IP-ADDRESS</b>"</code>

`openssl pkcs12 -export -out cert.pfx -inkey key.pem -in cert.cer`

The application expects the certificate password to be `password123` but this can be changed.

Copy the `cert.cer` to the app's `res/raw`

<pre>
fun configure(builder: CoreConfigurationBuilder) {
        builder.getPluginConfigurationBuilder(HttpSenderConfigurationBuilder::class.java)
            .withUri("https://<b>SERVER-IP-ADDRESS</b>:9999/report")
            .withResCertificate(R.raw.cert)
            .withCertificateType(ACRAConstants.DEFAULT_CERTIFICATE_TYPE)
            .withCompress(true)
            .withTlsProtocols(TLS.V1_2)
            .withEnabled(true)
    }
</pre>

Also overwrite the `cert.pfx` in the server Res folder

ACRA must be set to JSON ReportFormat and the HttpSender use Method POST (default)

## Client

For visualising the reports captured into the EF Database by the server

<img src='/Screenshots/Open.png' width='500'>

The UI is simple, allows reports to be closed, these are shown struckthrough in the list, closed reports can be reopened by a repeat of the same stacktrace. Closed reports can also be deleted.

<img src='/Screenshots/Closed.png' width='500'>
