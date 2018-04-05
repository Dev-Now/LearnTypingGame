using System;
using System.Linq;
using System.Xml.Linq;

namespace LearnTypingGame
{
    class Program
    {
        static void Main(string[] args)
        {
            // Read data
            if (args.Any()) { DataReader cDatRdr = new DataReader(args[0]); } else { return; }

            // !!!!! TEST CODE !!!!! to remove when renderer is in place.
            XDocument xDoc = XDocument.Load("DATA/all_exs.xml");
            Console.WriteLine("THUNDERTYPER!");

            var qLvls = from c in xDoc.Elements().Elements("level") select c;
            foreach (var itLvl in qLvls)
            {
                // Current level announcement
                Console.WriteLine(itLvl.Element("title").Value);

                var qParts = from c in itLvl.Elements("part") select c;
                foreach (var itPart in qParts)
                {
                    // Current Part announcement
                    if (itPart.Elements("title").Any()) { Console.WriteLine(itPart.Element("title").Value); }

                    var qExs = from c in itPart.Element("exercices").Elements("ex") select c;
                    foreach (var itEx in qExs)
                    {
                        // display current hint
                        if (itEx.Elements("hint").Any()) { Console.WriteLine(itEx.Element("hint").Value); }
                        // display challenge text
                        if (itEx.Elements("text").Any()) { Console.WriteLine(itEx.Element("text").Value); }

                        // Wait for input
                        Console.ReadLine();
                    }
                }

                Console.Clear();
            }

        }
    }
}