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
            DataReader cDatRdr;
            if (args.Any()) { cDatRdr = new DataReader(args[0]); } else { return; }
            Console.WriteLine("THUNDERTYPER!");
            Renderer cRndr = new Renderer(); cRndr.RenderGameSession(cDatRdr.GetGameData());
        }
    }
}