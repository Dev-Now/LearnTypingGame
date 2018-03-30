using System;
using System.Xml.Linq;

namespace LearnTypingGame
{
    class Program
    {
        static void Main(string[] args)
        {
            XDocument xDoc = XDocument.Load("DATA/all_exs.xml");
            Console.WriteLine("Hello World!");

            Console.WriteLine(xDoc);

            Console.ReadLine();
        }
    }
}