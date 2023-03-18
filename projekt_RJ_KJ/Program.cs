using System;
using System.Linq;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Exporters;
using BenchmarkDotNet.Running;

public class SortingBenchmark
{
    private int[] data;

    [Params(10, 100, 1000)] //Atrybut [Params] określa różne wartości dla parametru N, czyli rozmiaru tablicy do posortowania
    public int N;

    [GlobalSetup]
    public void Setup() //W metodzie Setup, tworzymy tablicę danych z użyciem metody Range i Reverse, tak aby była już posortowana odwrotnie
    {
        data = Enumerable.Range(1, N).Reverse().ToArray();
    }

    [Benchmark]
    public void BubbleSort()
    {
        int temp;
        for (int i = 0; i < data.Length; i++)
        {
            for (int j = 0; j < data.Length - 1; j++)
            {
                if (data[j] > data[j + 1])
                {
                    temp = data[j + 1];
                    data[j + 1] = data[j];
                    data[j] = temp;
                }
            }
        }
    }

    [Benchmark] //dekorator informuje BenchmarkDotNet, że ta metoda jest metodą testową, którą należy testować pod kątem wydajności
    public void QuickSort()
    {
        Array.Sort(data);
    }


}

class Program
{
    [Obsolete] //Oznacza przestarzały atrybut ("DefalutConfig"), który niebawem wyjdzie z użytku
    static void Main(string[] args)
    {
        var config = DefaultConfig.Instance.With(new HtmlExporter()); //C:\Users\Andrzej\source\repos\Benchmarks1\Benchmarks1\bin\Release\BenchmarkDotNet.Artifacts\results
        var summary = BenchmarkRunner.Run<SortingBenchmark>(config);
        Console.WriteLine(summary);
        Console.ReadKey();
    }
}