using System;
using System.Linq;
using System.Xml.Linq;
using System.Collections.Generic;

namespace LearnTypingGame
{
    /**
     * Class DataReader :
     * Used to read the game data and keep ready it for use by the current game session
     * 
     * Inputs :
     * A valid game data xml file path
     * 
     * Features :
     * - return current game level / part / hint / challenge text
     * - increments levels / parts / hints / challenges
     * 
     * */
    class DataReader
    {
        private Level[] cLvls;

        public DataReader(string szDatXmlFilePath)
        {
            // Load the game data file
            XDocument xDoc;
            try
            {
                xDoc = XDocument.Load(szDatXmlFilePath);
            }
            catch (Exception e)
            {
                Console.WriteLine("An error occured during Game data loading: '{0}'", e);
                return;
            }

            // --- Extract levels
            var qLvls = from c in xDoc.Elements().Elements("level") select c;
            cLvls = new Level[qLvls.Count()]; uint nI = 0;
            foreach (var itLvl in qLvls)
            {
                cLvls[nI] = new Level(itLvl.Element("title").Value); // create the level

                // -- Extract parts
                var qParts = from c in itLvl.Elements("part") select c;
                cLvls[nI].SetParts(qParts);

                nI++; // next level
            }
        }
    }

    /**
     * Class Level :
     * Used to modelize a game level
     * 
     * */
    class Level
    {
        private string szTitle;
        private Part[] cParts;

        public Level(string szTtl)
        {
            szTitle = szTtl;
        }

        public void SetParts(IEnumerable<XElement> qParts)
        {
            cParts = new Part[qParts.Count()]; uint nI = 0;
            foreach (var itPart in qParts)
            {
                // Create part
                if (itPart.Elements("title").Any()) { cParts[nI] = new Part(itPart.Element("title").Value); }
                else                                { cParts[nI] = new Part(""); }

                // - Extract challenges
                var qExs = from c in itPart.Element("exercices").Elements("ex") select c;
                cParts[nI].SetExercices(qExs);

                nI++; // next part
            }
        }
    }

    /**
     * Class Part :
     * Used to modelize a part of a game level
     * 
     * */
    class Part
    {
        private string szTitle;
        private Exercice[] cExs;

        public Part(string szTtl)
        {
            szTitle = szTtl;
        }

        public void SetExercices(IEnumerable<XElement> qExs)
        {
            cExs = new Exercice[qExs.Count()]; uint nI = 0;
            foreach (var itEx in qExs)
            {
                // create challenge
                string szHint = ""; string szText = "";
                if (itEx.Elements("hint").Any()) { szHint = itEx.Element("hint").Value; }
                if (itEx.Elements("text").Any()) { szText = itEx.Element("text").Value; }
                cExs[nI] = new Exercice(szHint, szText);

                nI++; // next challenge
            }
        }
    }

    /**
     * Class Level :
     * Used to modelize a single game typing challenge or exercice
     * 
     * */
    class Exercice
    {
        private string szHint;
        private string szText;

        public Exercice(string szHnt, string szTxt)
        {
            szHint = szHnt;
            szText = szTxt;
        }
    }
}
