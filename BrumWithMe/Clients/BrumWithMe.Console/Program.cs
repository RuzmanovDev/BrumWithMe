using System;

namespace BrumWithMe.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            DateTime? date = DateTime.Now;
            DateTime second = DateTime.Now;

            System.Console.WriteLine(date>second);
        }
    }
}
