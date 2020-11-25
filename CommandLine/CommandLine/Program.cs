using System;

namespace CommandLine
{
    class Program
    {
        static void Main(string[] args)
        {
            foreach (var argument in args)
                Console.WriteLine(argument);
        }
    }
}
