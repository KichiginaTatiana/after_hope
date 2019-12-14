using System.IO;

namespace AfterHope.Exporting.Extensions
{
    public static class StreamExtensions
    {
        public static byte[] ReadToEnd(this Stream stream)
        {
            const int bufferSize = 1024;
            var buffer = new byte[bufferSize];
            var result = new MemoryStream();
            int size;
            do
            {
                size = stream.Read(buffer, 0, bufferSize);
                result.Write(buffer, 0, size);
            } while (size > 0);
            return result.ToArray();
        }
    }
}