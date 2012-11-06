using System;

namespace LightningGame
{
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (LightningGameHost game = new LightningGameHost())
            //using (MercuryTest.Game1 game = new MercuryTest.Game1())
            {
                game.Run();
            }
        }
    }
#endif
}

