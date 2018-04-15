using System;
using System.Collections.Generic;
using System.Text;

namespace LearnTypingGame
{
    /**
     * Class Renderer :
     * Used to render game session components (according to predefined rules)
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

        /**
         * Render a complete game session based on given components array
         * */
        public void RenderGameSession(GenericComponent[] cComps /*, Game rules */)
        {
            // Rendering logic
            foreach (GenericComponent cOneComp in cComps)
            {
                RenderSingleComponent(cOneComp);
            }
        }

        /**
         * Render a single game session component
         * */
        public void RenderSingleComponent(GenericComponent cComp)
        {
            switch(cComp.GetType().Name)
            {
                case "Level":
                    Console.Clear();
                    Console.WriteLine(cComp.GetTitle());
                    Console.WriteLine("--------------");
                    RenderGameSession(cComp.GetSubComps());
                    break;
                case "Part":
                    Console.WriteLine("----");
                    Console.WriteLine(cComp.GetTitle());
                    Console.WriteLine("----");
                    RenderGameSession(cComp.GetSubComps());
                    break;
                case "Exercice":
                    Console.WriteLine("Ready? {0}", cComp.GetHint());
                    Console.WriteLine(  " Type this: {0}", cComp.GetText());
                    Console.Write(      " =========> "); Console.ReadLine();
                    break;
                default:
                    // shouldn't fall here!
                    break;
            }
        }
    }
}
