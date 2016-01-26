#region File Description
//-----------------------------------------------------------------------------
// Flixel for XNA.
// Original repo : https://github.com/StAidan/X-flixel
// Extended and edited repo : https://github.com/initials/XNAMode
//-----------------------------------------------------------------------------
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using org.flixel;
using Microsoft.Xna.Framework;

namespace Loader_SuperLemonadeFactory
{
    /// <summary>
    /// Flixel enters here.
    /// <code>FlxFactory</code> refers to it as the "masterclass".
    /// </summary>
    public class FlixelEntryPoint2 : FlxGame
    {
        public FlixelEntryPoint2(Game game)
            : base(game)
        {

            Console.WriteLine("Flixel Entry Point");

            int w = FlxG.resolutionWidth / FlxG.zoom;
            int h = FlxG.resolutionHeight / FlxG.zoom;

            initGame(w, h, new Lemonade.IntroState(), new Color(15, 15, 15), true, new Color(5, 5, 5));

            FlxG.debug = false;
            FlxG.level = 1;

            // Customize splash screen.
            FlxG.splashBGColor = new Color(77, 81, 60);
            FlxG.splashLogo = "flixel/initials/initialsLogoGameboy";
            FlxG.buildDescription = "GAMEBOY";
            FlxG.splashAudioWave = "Lemonade/sfx/cw_sound44";
            FlxG.splashAudioWaveFlixel = "Lemonade/sfx/cw_sound19";

            //Lemonade.Lemonade_Globals.PAID_VERSION = Lemonade.Lemonade_Globals.PIRATE_MODE;
            Lemonade.Lemonade_Globals.PAID_VERSION = Lemonade.Lemonade_Globals.FULL_MODE;
            string buildType = "FULL";
#if DEBUG
            FlxG.debug = true;
#endif

#if DEMO
            Lemonade.Lemonade_Globals.PAID_VERSION = Lemonade.Lemonade_Globals.DEMO_MODE;
#endif
#if FULL
            Lemonade.Lemonade_Globals.PAID_VERSION = Lemonade.Lemonade_Globals.FULL_MODE;
#endif
#if PIRATE
            Lemonade.Lemonade_Globals.PAID_VERSION = Lemonade.Lemonade_Globals.PIRATE_MODE;
#endif


            FlxG.BUILD_TYPE = FlxG.BUILD_TYPE_PC;


            using (System.IO.StreamWriter file =
            new System.IO.StreamWriter(@"version.txt"))
            {
                file.WriteLine(typeof(Lemonade.Actor).Assembly.GetName().Version);
            }

            using (System.IO.StreamWriter file =
            new System.IO.StreamWriter(@"buildType.txt"))
            {
                file.WriteLine(buildType);
            }

        }
    }
}
