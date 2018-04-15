using System;
using System.Collections.Generic;
using System.Text;

namespace LearnTypingGame
{
    /**
     * Class GameEngine :
     * To manage game sessions
     * 
     * */
    class GameEngine
    {
        
    }

    /**
     * Class GameSession :
     * To modelize and manage a single game session
     * 
     * */
    class GameSession
    {
        struct SavedProgress
        {
            uint uCurrLvl;
            uint uCurrPart;
            uint uCurrEx;

            uint uScore;
        }

        string           szPlayer;
        SavedProgress    tProgress;
        uint[]           uBestResults = new uint[(int)BEST_SCORES.eXthBEST];
    }

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
