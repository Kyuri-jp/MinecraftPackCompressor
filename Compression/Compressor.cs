using System.IO.Compression;

namespace MinecraftPackCompressor.Compression
{
    internal class Compressor
    {
        internal static void Compress(string path, string outPut)
        {
            DirectoryInfo directoryInfo = new(path);

            outPut = Path.Combine(outPut, directoryInfo.Name);
            int count = 1;
            if (File.Exists($"{outPut}.zip"))
            {
                while (File.Exists($"{outPut}({count}).zip"))
                {
                    outPut = $"{outPut}({count})";
                    count++;
                }

                outPut = $"{outPut}({count})";
            }

            try
            {
                ZipFile.CreateFromDirectory(path, $"{outPut}.zip");
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
                Console.ForegroundColor = ConsoleColor.White;
            }
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"{outPut}.zip");
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}