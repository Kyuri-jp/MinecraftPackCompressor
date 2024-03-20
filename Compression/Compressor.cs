using System.IO.Compression;

namespace MinecraftPackCompressor.Compression
{
    internal class Compressor
    {
        internal static void Compress(string path, string outPut)
        {
            DirectoryInfo directoryInfo = new(path);

            //set path
            outPut = Path.Combine(outPut, directoryInfo.Name);

            //exists
            int count = 1;
            if (File.Exists($"{outPut}.zip"))
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"{outPut}.zip is already exists.");
                Console.ForegroundColor = ConsoleColor.White;
                while (File.Exists($"{outPut}({count}).zip"))
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine($"{outPut}({count}).zip already exists.");
                    Console.ForegroundColor = ConsoleColor.White;
                    count++;
                }

                outPut = $"{outPut}({count})";
            }

            try
            {
                //compress
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