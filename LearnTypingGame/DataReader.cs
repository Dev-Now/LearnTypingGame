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
     * - reads all game data from the data file
     * - organize it for use into level objects array
     * 
     * */
    class DataReader
    {
        private Level[] cLvls; // game levels - array

        /**
         * Returns the game data (the levels array contains all the game data)
         * */
        public Level[] GetGameData() { return cLvls; }

        /**
         * DataReader constructor
         * */
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
        private string szTitle; // level title
        private Part[] cParts;  // level parts - array

        /**
         * Level constructor
         * */
        public Level(string szTtl)
        {
            szTitle = szTtl;
        }

        /**
         * sets the level parts
         * */
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
        private string szTitle;     // part title
        private Exercice[] cExs;    // part challenges - array

        /**
         * Part constructor
         * */
        public Part(string szTtl)
        {
            szTitle = szTtl;
        }

        /**
         * sets the part challenges
         * */
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
        private string szHint; // challenge hint
        private string szText; // challenge text

        /**
         * Exercice constructor
         * */
        public Exercice(string szHnt, string szTxt)
        {
            szHint = szHnt;
            szText = szTxt;
        }
    }
}
