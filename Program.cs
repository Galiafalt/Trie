using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Threading;
using System.Collections;

namespace TextAutocomplite
{
    class Program
    {
        private static Trie TrieDictionary = new Trie();
        
        static void Main(string[] args)
        {
            Stopwatch WorkTime = new Stopwatch();
            String InputFile = String.Empty;

            int LinesToRead = 0;
            while (!File.Exists(InputFile)) InputFile = Console.ReadLine();
            WorkTime.Start();
            using (StreamReader ReadFile = new StreamReader(InputFile))
            {
                LinesToRead = int.Parse(ReadFile.ReadLine());
                for (int i = 0; i < LinesToRead; ++i) TrieDictionary.Add(ReadFile.ReadLine());
                LinesToRead = int.Parse(ReadFile.ReadLine());
                for (int i = 0; i < LinesToRead; ++i)
                {
                    var seg = TrieDictionary.GetSeg(ReadFile.ReadLine());
                    foreach (var str in seg)
                    {
                        Console.WriteLine(str);
                    }
                    Console.WriteLine();
                }
            }
            WorkTime.Stop();
            Console.WriteLine("Время выполнения {0} мин {1} сек {2} милсек", WorkTime.Elapsed.Minutes, WorkTime.Elapsed.Seconds, WorkTime.Elapsed.Milliseconds);
            Console.ReadKey();
        }
    }
}
