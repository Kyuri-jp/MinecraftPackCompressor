namespace MinecraftPackCompressor.Common
{
    internal class PackSearch
    {
        private static List<String> packFolders = [];

        internal static List<String> PackSearcher(string dir)
        {

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Searching...");
            Console.ForegroundColor = ConsoleColor.Green;

            PackMetaSearcher(dir);

            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine();

            if (packFolders.Count <= 0)
                throw new FileNotFoundException("pack.mcmeta is not found.");

            return packFolders;
        }

        private static void PackMetaSearcher(string dir)
        {
            //Console.WriteLine(dir);
            try
            {
                string[] files = Directory.GetFiles(dir, "pack.mcmeta");
                foreach (var file in files)
                {
                    FileInfo fileInfo = new(file);
                    packFolders.Add(fileInfo.DirectoryName);
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
        }
    }
}