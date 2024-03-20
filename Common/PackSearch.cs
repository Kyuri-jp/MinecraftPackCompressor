namespace MinecraftPackCompressor.Common
{
    internal class PackSearch
    {
        private static readonly List<String> packFolders = [];

        internal static List<String> PackSearcher(string dir)
        {
            //notice
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Searching...");
            Console.ForegroundColor = ConsoleColor.Green;

            //search
            PackMetaSearcher(dir);

            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine();

            //not found
            if (packFolders.Count <= 0)
                throw new FileNotFoundException("pack.mcmeta is not found.");

            return packFolders;
        }

        private static void PackMetaSearcher(string dir)
        {
            //Console.WriteLine(dir);
            try
            {
                //file set
                string[] files = Directory.GetFiles(dir, "pack.mcmeta");
                foreach (var file in files)
                {
                    FileInfo fileInfo = new(file);
#pragma warning disable CS8604 // Null 参照引数の可能性があります。
                    packFolders.Add(fileInfo.DirectoryName);
#pragma warning restore CS8604 // Null 参照引数の可能性があります。
                }

                string[] subDirectories = Directory.GetDirectories(dir);
                foreach (var directory in subDirectories)
                {
                    PackMetaSearcher(directory);
                }
            }
            catch (UnauthorizedAccessException ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.White;
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.White;
            }
        }
    }
}