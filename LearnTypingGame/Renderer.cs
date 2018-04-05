using System;
using System.Collections.Generic;
using System.Text;

namespace LearnTypingGame
{
    /**
     * Class DataReader :
     * Used to read the game data and keep ready it for use by the current game session
     * 
     * Inputs :
     * - Graphical configuration info
     * - Rendering game rules
     * 
     * Features :
     * - orchestrate game rendering
     *  >> displays current game level / part / hint / challenge text - according to graphical config.
     *  >> increments levels / parts / hints / challenges - according to given rules
     * 
     * */
    class Renderer
    {
        /**
         * Renderer constructor
         * */
        public Renderer()
        {
            // the constructor should set the initial rendering config. 
            // - text colors / fonts 
            // - background colors
            // - spacing
            // etc.
        }

        public void RenderGameSession(Level[] cLvls /*, Game rules */)
        {
            // Rendering logic
        }
    }
}
