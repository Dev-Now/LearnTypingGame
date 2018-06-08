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
     *  >> Save game session progress when requested
     * 
     * */
    class Renderer
    {
        private bool bSessionEnded; // Current session ended

        /**
         * Renderer constructor
         * */
        public Renderer()
        {
            bSessionEnded = false;

            // Prevent example from ending if CTL+C is pressed.
            Console.TreatControlCAsInput = true;

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
            int nCompNb = cComps?.Length ?? 0;
            if (nCompNb > 0)
            {
                string szCompsType = cComps[0].GetType().Name;
                uint nCurrComp = cSession.GetCurr(szCompsType);
                for (uint nCompNdx = nCurrComp; nCompNdx < cComps.Length; nCompNdx++)
                {
                    RenderSingleComponent(cComps[nCompNdx], cSession);
                    if (cSession.EndReq) { break; }
                    cSession.Next(szCompsType);
                }
                if (cSession.EndReq) { RenderSessionEndDialog(ref bSessionEnded, cSession); return; }
                cSession.Reset(szCompsType);
            }
        }

        /**
         * Render a single game session component
         * */
        private void RenderSingleComponent(GenericComponent cComp, GameSession cSession)
        {
            switch(cComp.GetType().Name)
            {
                case "Level":
                    Console.Clear(); Console.WriteLine("Press the Escape (Esc) key to quit. \n");
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
                    string szEx = cComp.GetText();
                    Console.WriteLine("Ready? {0}", cComp.GetHint());
                    Console.WriteLine(  " Type this: {0}", szEx);
                    Console.Write(      " =========> "); ReadUserInput(szEx, cSession);
                    break;
                default:
                    // shouldn't fall here!
                    break;
            }
        }

        /**
         * Render a single game session component
         * */
        private void ReadUserInput(string szEx, GameSession cSession)
        {
            bool bQuit = false; double dTimeInSecs = 1; bool bFirstKey = true; double dStartTime = 0;
            ConsoleKeyInfo cki; int nCharNdx = 0;
            do
            {
                // get typed key
                cki = Console.ReadKey(true);
                // set timing start 
                if (bFirstKey)  { dStartTime = DateTime.Now.TimeOfDay.TotalSeconds; bFirstKey = false; }
                // process typed key
                if (cki.KeyChar == szEx[nCharNdx])
                {
                    Console.Write(cki.KeyChar);
                    nCharNdx++;
                }
                else
                {
                    Console.Beep();
                }
                bQuit = (cki.Key == ConsoleKey.Escape);
            } while (!bQuit && (nCharNdx < szEx.Length));
            // Update score
            if (!bQuit) {
                dTimeInSecs = DateTime.Now.TimeOfDay.TotalSeconds - dStartTime;
                cSession.UpdateScore(dTimeInSecs, szEx.Length);
            }
            // End Challenge
            Console.WriteLine();
            cSession.EndReq = bQuit;
        }

        /**
         * Render the game session end dialog
         * */
        private void RenderSessionEndDialog(ref bool bSessionEnded, GameSession cSession)
        {
            if (!bSessionEnded)
            {
                Console.Clear(); Console.Write("You are about to quit.\nDo you want to save your session? Y (yes), N (No): ");
                ConsoleKeyInfo cki = Console.ReadKey(true);
                while ((cki.Key != ConsoleKey.Y) && (cki.Key != ConsoleKey.N)) { Console.Beep(); cki = Console.ReadKey(true); }
                Console.WriteLine(cki.Key.ToString()); bSessionEnded = true;
                if (cki.Key == ConsoleKey.Y) { cSession.Save(); }
            }
        }
    }
}
