using System;
using System.Linq;
using System.Security.Cryptography;
using System.IO;

namespace LearnTypingGame
{
    /**
     * Class GameEngine :
     * To manage game sessions
     * 
     * */
    class GameEngine
    {
        /**
         * Returns the MD5 hash of the input file
         * */
        static string CalculateMD5(string filename)
        {
            using (var md5 = MD5.Create())
            {
                using (var stream = File.OpenRead(filename))
                {
                    var hash = md5.ComputeHash(stream);
                    return BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
                }
            }
        }

        /**
         * GameEngine constructor
         * */
        public GameEngine(string[] args)
        {
            // Read data
            DataReader cDatRdr;
            if (args.Any()) { cDatRdr = new DataReader(args[0]); } else { return; }

            // Game init page
            Console.WriteLine(" *** THUNDERTYPER! *** ");
            Console.WriteLine("     *************     ");
            Console.WriteLine();
            GameSession cSession = new GameSession(CalculateMD5(args[0]));

            // Render game session
            Renderer cRndr = new Renderer(); cRndr.RenderGameSession(cDatRdr.GetGameData() /*, cSession [TODO...]*/);
        }
    }

    /**
     * Class GameSession :
     * To modelize and manage a single game session
     * 
     * */
    class GameSession
    {
        /**
         * struct SavedProgress:
         * A structure used to represent and save player progress in the game session
         * 
         * */
        protected struct SavedProgress
        {
            uint uCurrLvl;  // Current level (by index)
            uint uCurrPart; // Current part (by index)
            uint uCurrEx;   // Current exercice (by index)

            uint uScore;    // Current score
        }

        protected string           szDatFileHash;                                      // used data file hash (game session data should use same data file as ref. for score comparison and keeping)

        protected string           szPlayer;                                           // Player name
        protected SavedProgress    tProgress = new SavedProgress();                    // Player progress
        protected uint[]           uBestResults = new uint[(int)BEST_SCORES.eXthBEST]; // player best results set

        /**
         * GameSession constructor
         * */
        public GameSession(string szDatHash /*, path to .csv file saving sessions information [TODO...]*/)
        {
            szDatFileHash = szDatHash;
            Console.WriteLine("Please enter a player name:");
            szPlayer = Console.ReadLine();
            // Todo... logic to check data file hash
            // [Todo... add special logic to prevent player from cheating by tempering sessions info.]
            // Todo... extract csv file information related to player if hash matches; otherwise add another player for the new hash.
        }
    }

    /**
     * enum BEST_SCORES :
     * To define player best results set indexes
     * 
     * */
    enum BEST_SCORES : byte
    {
        e1stBEST,
        e2ndBEST,
        e3rdBEST,
        e4thBEST,
        e5thBEST,
        e6thBEST,
        e7thBEST,
        e8thBEST,
        e9thBEST,
        e10thBEST,
        eXthBEST
    }
}
