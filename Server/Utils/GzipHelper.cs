using System.IO;
using System.IO.Compression;
using System.Text;

namespace AcraBackend.Server.Utils
{
    public class GzipHelper
    {
        public static string DecompressGzip(byte[] compressed, Encoding encoding = null)
        {
            using var from = new MemoryStream(compressed);
            using var to = new MemoryStream();
            using var gZipStream = new GZipStream(from, CompressionMode.Decompress);
            gZipStream.CopyTo(to);
            return (encoding ?? Encoding.UTF8).GetString(to.ToArray());
        }
    }
}
