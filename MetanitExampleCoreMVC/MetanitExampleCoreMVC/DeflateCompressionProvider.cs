using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.ResponseCompression;
using System.IO;
using System.IO.Compression;

namespace MetanitExampleCoreMVC
{
    public class DeflateCompressionProvider : ICompressionProvider
    {
        public string EncodingName => "deflate";
        public bool SupportsFlush => true;

        public Stream CreateStream(Stream outputStream)
        {
            return new DeflateStream(outputStream, CompressionLevel.Optimal);
        }
    }
}
