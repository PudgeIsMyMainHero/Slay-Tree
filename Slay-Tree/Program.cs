using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;

namespace Slay_Tree
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var rng = new Random(42);
        var array = Enumerable.Range(1, 10000).OrderBy(x => rng.Next()).ToArray();

        var tree = new SplayTree();
        var sw = new Stopwatch();

        var insertTimes = new List<double>();
        var insertOps = new List<int>();
        var searchTimes = new List<double>();
        var searchOps = new List<int>();
        var deleteTimes = new List<double>();
        var deleteOps = new List<int>();

        // 3. Вставка
        Console.WriteLine("🔹 Вставка 10000 элементов...");
        foreach (var val in array)
        {
            sw.Restart();
            tree.Insert(val);
            sw.Stop();
            insertTimes.Add(sw.Elapsed.TotalMilliseconds);
            insertOps.Add(tree.Operations);
        }

        // 4. Поиск 100 случайных
        Console.WriteLine("🔍 Поиск 100 случайных элементов...");
        var searchIndices = Enumerable.Range(0, 100).Select(_ => rng.Next(10000)).ToArray();
        foreach (var idx in searchIndices)
        {
            sw.Restart();
            tree.Search(array[idx]);
            sw.Stop();
            searchTimes.Add(sw.Elapsed.TotalMilliseconds);
            searchOps.Add(tree.Operations);
        }

        // 5. Удаление 1000 случайных
        Console.WriteLine("🗑️ Удаление 1000 случайных элементов...");
        var deleteIndices = Enumerable.Range(0, 1000).Select(_ => rng.Next(10000)).Distinct().Take(1000).ToArray();
        foreach (var idx in deleteIndices)
        {
            sw.Restart();
            tree.Delete(array[idx]);
            sw.Stop();
            deleteTimes.Add(sw.Elapsed.TotalMilliseconds);
            deleteOps.Add(tree.Operations);
        }

        // 6. Средние значения
        var avgInsertTime = insertTimes.Average();
        var avgInsertOps = insertOps.Average();
        var avgSearchTime = searchTimes.Average();
        var avgSearchOps = searchOps.Average();
        var avgDeleteTime = deleteTimes.Average();
        var avgDeleteOps = deleteOps.Average();

        Console.WriteLine("\n📊 СРЕДНИЕ РЕЗУЛЬТАТЫ:");
        Console.WriteLine($"Вставка:  {avgInsertTime:F4} мс | {avgInsertOps:F0} операций");
        Console.WriteLine($"Поиск:    {avgSearchTime:F4} мс | {avgSearchOps:F0} операций");
        Console.WriteLine($"Удаление: {avgDeleteTime:F4} мс | {avgDeleteOps:F0} операций");

        using var writer = new StreamWriter("results.csv");
        writer.WriteLine("Тип;Время_мс;Операции");

        CultureInfo culture = new CultureInfo("ru-RU");
        for (int i = 0; i < insertTimes.Count; i++) 
            writer.WriteLine($"Insert;{insertTimes[i].ToString("F6", culture)};{insertOps[i]}");
        for (int i = 0; i < searchTimes.Count; i++) 
            writer.WriteLine($"Search;{searchTimes[i].ToString("F6", culture)};{searchOps[i]}");
        for (int i = 0; i < deleteTimes.Count; i++) 
            writer.WriteLine($"Delete;{deleteTimes[i].ToString("F6", culture)};{deleteOps[i]}");
        }
    }
}