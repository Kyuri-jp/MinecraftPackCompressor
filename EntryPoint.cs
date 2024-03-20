using MinecraftPackCompressor.Common;
using MinecraftPackCompressor.Compression;
using System.Diagnostics;

internal class EntryPoint
{
    private static void Main(string[] args)
    {
        //path
        string outPut = Path.Combine(Directory.GetCurrentDirectory(), "Output");

        //create output folder
        if (!Directory.Exists(outPut))
            Directory.CreateDirectory(outPut);

        //intro
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine
            ("==============================================\n" +
            "Hello!\n" +
            "This is Minecraft Pack Compressor.\n" +
            "This is distributed under the MIT License.\n" +
            "Copyright (c) 2024 Kyuri\n" +
            "==============================================\n");

    askPath:
        //ask
        Console.WriteLine("Enter the folder path to compress.");
        string? folderPath;

        while (true)
        {
            //read path
            folderPath = Console.ReadLine();

            // exist
            if (Directory.Exists(folderPath))
                break;

            // not found
            if (folderPath == "")
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Enter any folder path.\n");
                Console.ForegroundColor = ConsoleColor.White;
                continue;
            }

            if (!Directory.Exists(folderPath))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"{folderPath} is not found.\n");
                Console.ForegroundColor = ConsoleColor.White;
                continue;
            }
        }

        //search pack
        List<String> packFolders = [];

        try
        {
            //search
            packFolders = PackSearch.PackSearcher(folderPath);
        }
        catch (FileNotFoundException ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(ex.Message);
            Console.WriteLine(ex.StackTrace);
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.White;
            goto askPath;
        }

        //result
        Console.WriteLine("\nThe following folders were found.");
        Console.ForegroundColor = ConsoleColor.Blue;
        foreach (String pack in packFolders)
        {
            Console.WriteLine(pack);
        }
        Console.ForegroundColor = ConsoleColor.White;

        //all compress?
        Console.WriteLine("\nDo you want it all compressed? (y/n)");

        ConsoleKeyInfo key = Console.ReadKey(true);
        while (!key.Key.ToString().Equals("Y") && !key.Key.ToString().Equals("N"))
            key = Console.ReadKey(true);

        if (key.Key.ToString().Equals("Y"))
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Compressing...");
            foreach (String pack in packFolders)
            {
                Compressor.Compress(pack, outPut);
            }
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Done!");
            Console.ForegroundColor = ConsoleColor.White;
            Process.Start("Explorer.exe", outPut);
        }
        if (key.Key.ToString().Equals("N"))
        {
            foreach (String pack in packFolders)
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine($"\nDo you compress [{pack}]? (y/n)");
                key = Console.ReadKey(true);
                while (!key.Key.ToString().Equals("Y") && !key.Key.ToString().Equals("N"))
                    key = Console.ReadKey(true);
                if (key.Key.ToString().Equals("N"))
                    continue;
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Compressing...");
                Compressor.Compress(pack, outPut);
            }
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Done!");
            Console.ForegroundColor = ConsoleColor.White;
            Process.Start("Explorer.exe", outPut);
        }
    }
}