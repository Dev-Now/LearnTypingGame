using System;
using System.Linq;
using System.Xml.Linq;

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
            // Load the game data
            try
            {
                XDocument xDoc = XDocument.Load(szDatXmlFilePath);
            }
            catch(Exception e)
            {
                Console.WriteLine("An error occured during Game data loading: '{0}'", e);
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
    }
}
