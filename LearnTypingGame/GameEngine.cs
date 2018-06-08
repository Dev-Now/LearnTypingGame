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
            if (args.Any()) { cDatRdr = new DataReader(args[0]); } else { Console.WriteLine("GameEngine => Constructor: ERROR! No game data"); return; }

            // Game init page
            Console.WriteLine(" *** THUNDERTYPER! *** ");
            Console.WriteLine("     *************     ");
            Console.WriteLine();
            GameSession cSession = new GameSession(CalculateMD5(args[0]));

            // Render game session
            Renderer cRndr = new Renderer(); cRndr.RenderGameSession(cDatRdr.GetGameData(), cSession);
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
         * Class SavedProgress:
         * A class used to represent and save player progress in the game session
         * 
         * */
        protected class SavedProgress
        {
            public uint CurrLvl { get; set; } // Current level (by index)
            public uint CurrPart { get; set; } // Current part (by index)
            public uint CurrEx { get; set; } // Current exercice (by index)

            public double CurrScore { get; set; } // Current score
        }

        protected string           szDatFileHash;   // used data file hash (game session data should use same data file as ref. for score comparison and storage)

        protected string           szPlayer;        // Player name
        protected SavedProgress    tProgress;       // Player progress
        protected uint[]           uBestResults;    // player best results set

        /**
         * Returns sessions file path
         * */
        private string GetSessionsFilePath()
        {
            return string.Format(@"Sessions/{0}.csv", szDatFileHash);
        }

        /**
         * Initializes the session
         * */
        private void InitSession()
        {
            tProgress       = new SavedProgress();                   
            uBestResults    = new uint[(int)BEST_SCORES.eXthBEST];
        }

        /**
         * Load the current player (given by her name) session and return TRUE if loaded successfully.
         * */
        private bool LoadSession(string szName, string szSessionsFilePath)
        {
            bool bLoadOK = false;
            Console.WriteLine("Loading player {0} data...", szName);
            // Todo... open the file and load all associated session data
            // [Todo... add special logic to prevent player from cheating by tempering sessions info.]
            // Todo... ask the player based on loaded data if he wants to continue or start a new game
            // Todo... reset tProgress if new game
            return bLoadOK;
        }

        /**
         * GameSession constructor
         * */
        public GameSession(string szDatHash)
        {
            this.EndReq = false;
            // Save data file hash
            szDatFileHash = szDatHash;
            // Get the player name
            Console.WriteLine("Please enter a player name:");
            szPlayer = Console.ReadLine();
            // Check the sessions file exists; to load/init the session
            string szSessionsFilePath = GetSessionsFilePath(); // determine the sessions file path
            if(File.Exists(szSessionsFilePath)) { LoadSession(szPlayer, szSessionsFilePath); }
            else                                { InitSession(); }
        }

        /**
         * Get the current session LEVEL / PART / EXERCICE.
         * */
        public uint GetCurr(string szComp)
        {
            switch(szComp)
            {
                case "Level":
                    return tProgress.CurrLvl;
                case "Part":
                    return tProgress.CurrPart;
                case "Exercice":
                    return tProgress.CurrEx;
                default:
                    // shouldn't fall here!
                    return 0;
            }
        }

        /**
         * Increment the current session LEVEL / PART / EXERCICE.
         * */
        public void Next(string szComp)
        {
            switch (szComp)
            {
                case "Level":
                    tProgress.CurrLvl++;
                    break;
                case "Part":
                    tProgress.CurrPart++;
                    break;
                case "Exercice":
                    tProgress.CurrEx++;
                    break;
                default:
                    // shouldn't fall here!
                    break;
            }
        }

        /**
         * Reset the current session LEVEL / PART / EXERCICE.
         * */
        public void Reset(string szComp)
        {
            switch (szComp)
            {
                case "Level":
                    tProgress.CurrLvl = 0;
                    break;
                case "Part":
                    tProgress.CurrPart = 0;
                    break;
                case "Exercice":
                    tProgress.CurrEx = 0;
                    break;
                default:
                    // shouldn't fall here!
                    break;
            }
        }

        public bool EndReq { get; set; } // End Session request
        
        /**
         * Save the current session and return TRUE if saved successfully.
         * */
        public bool Save()
        {
            Console.WriteLine("Saving player {0} session...", szPlayer);
            // Todo...
            Console.ReadKey(true);
            return false;
        }

        /**
         * Save the current session and return TRUE if saved successfully.
         * */
        public void UpdateScore(double dTimeInSecs, int nExSize)
        {
            double CHAR_GAIN = 10.0;
            tProgress.CurrScore += (nExSize * CHAR_GAIN) / dTimeInSecs;
            Console.WriteLine("\nYOUR SCORE ***** {0:0} *****", tProgress.CurrScore); // FOR TESTING
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
