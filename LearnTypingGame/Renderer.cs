using System;

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
        public void RenderGameSession(GenericComponent[] cComps, GameSession cSession)
        {
            // Rendering logic
            uint nLvl   = cSession.GetCurr("Level");
            uint nPart  = cSession.GetCurr("Part");
            uint nEx    = cSession.GetCurr("Exercice");
            switch (cComps[0].GetType().Name)
            {
                case "Level":
                    for (uint nLvlNdx = nLvl; nLvlNdx < cComps.Length; nLvlNdx++) { RenderSingleComponent(cComps[nLvlNdx], cSession); cSession.Next("Level"); }
                    cSession.Reset("Level");
                    break;
                case "Part":
                    for (uint nPartNdx = nPart; nPartNdx < cComps.Length; nPartNdx++) { RenderSingleComponent(cComps[nPartNdx], cSession); cSession.Next("Part"); }
                    cSession.Reset("Part");
                    break;
                case "Exercice":
                    for (uint nExNdx = nEx; nExNdx < cComps.Length; nExNdx++) { RenderSingleComponent(cComps[nExNdx], cSession); cSession.Next("Exercice"); }
                    cSession.Reset("Exercice");
                    break;
                default:
                    // shouldn't fall here!
                    break;
            }
        }

        /**
         * Render a single game session component
         * */
        public void RenderSingleComponent(GenericComponent cComp, GameSession cSession)
        {
            switch(cComp.GetType().Name)
            {
                case "Level":
                    Console.Clear();
                    Console.WriteLine(cComp.GetTitle());
                    Console.WriteLine("--------------");
                    RenderGameSession(cComp.GetSubComps(), cSession);
                    break;
                case "Part":
                    Console.WriteLine("----");
                    Console.WriteLine(cComp.GetTitle());
                    Console.WriteLine("----");
                    RenderGameSession(cComp.GetSubComps(), cSession);
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
