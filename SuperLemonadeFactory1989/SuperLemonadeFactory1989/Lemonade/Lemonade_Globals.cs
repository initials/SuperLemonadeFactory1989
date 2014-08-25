using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using org.flixel;

using Microsoft.Xna.Framework;

namespace Lemonade
{
    /// <summary>
    /// A Global Holder
    /// </summary>
    public class Lemonade_Globals
    {

        public static string location = "";
        
        public static int game_version = 2;

        public const float GRAVITY = 2040.0f;

        public static string[] characterSelected;
        
        public static bool[] isPlayerControlled;

        public static Dictionary<string, GameProgress> gameProgress;
        
        public static Dictionary<string, Dictionary<string, Vector2>> stateSaver;

        public const int DEMO_MODE = 0;
        
        public const int FULL_MODE = 1;
        
        public const int PIRATE_MODE = 2;

        public static float timeLeft = 30.0f;
        
        public static float totalTimeAvailable = 30.0f;

        public static int PAID_VERSION;

        public static int LAST_LOCATION;
        
        public static int LAST_SELECTED_ON_MENU;

        public static bool restartMusic = true;

        /// <summary>
        /// Darkest color
        /// </summary>
        public static Color GAMEBOY_COLOR_1 = new Color(77, 81, 60);

        /// <summary>
        /// Second Darkest Color
        /// </summary>
        public static Color GAMEBOY_COLOR_2 = new Color(107, 116, 84);

        /// <summary>
        /// Second Lightest Color
        /// </summary>
        public static Color GAMEBOY_COLOR_3 = new Color(176, 186, 142);

        /// <summary>
        /// Lightest Color
        /// </summary>
        public static Color GAMEBOY_COLOR_4 = new Color(199, 207, 162);

        /// <summary>
        /// Coins that have been collected.
        /// </summary>
        public static int coins = 0;

        public static int coinsThisLevel = 0;
        public static int levelChanges = 0;

        public static string[] locationOrder = { "management", "military", "sydney", "newyork", "warehouse", "factory", "management", "military", "sydney", "newyork", "warehouse", "factory", "management", "military", "sydney", "newyork", "warehouse", "factory", "management", "military", "sydney", "newyork", "warehouse", "factory", "management", "military", "sydney", "newyork", "warehouse", "factory", "management", "military", "sydney", "newyork", "warehouse", "factory", "management", "military", "sydney", "newyork", "warehouse", "factory" };

        public static Dictionary<string, string> niceLocationNames = new Dictionary<string, string> { 
        { "sydney", "Sydney" }, 
        { "newyork", "New York" }, 
        { "warehouse", "Warehouse" },
        { "factory", "Factory" },
        { "management", "Management\nOffice" },
        { "military", "Military\nComplex" },
        };

        public static Dictionary<string, string> niceActorNames = new Dictionary<string, string> { 
        { "sydney", "Doust, The Inspector" }, 
        { "newyork", "Reeder The Dirty Chef" }, 
        { "warehouse", "Andre" },
        { "factory", "Working Man Hero" },
        { "management", "Liselot" },
        { "military", "Mr Amsterdaam" },
        };

        public static Dictionary<string, int> thisTurnProgress = new Dictionary<string, int> { 
        { "sydney", 0 }, 
        { "newyork", 0 }, 
        { "warehouse", 0 },
        { "factory", 0 },
        { "management", 0},
        { "military", 0},
        };

        public static Dictionary<string, int> framesForIllustration = new Dictionary<string, int> { 
        { "sydney", 3 }, 
        { "newyork", 2 }, 
        { "warehouse", 0 },
        { "factory", 5 },
        { "management", 4},
        { "military", 1},
        };

        /// <summary>
        /// The total number of coins available.
        /// </summary>
        public static int totalCoins = 0;

        public Lemonade_Globals()
        {
            

        }

        public static int calculateTotalCoins()
        {
            string[] locs = { "management", "military", "sydney", "newyork", "warehouse", "factory" };
            int coinCount = 0;

            foreach (var loc in locs)
            {
                List<Dictionary<string,string>> actorsString = FlxXMLReader.readNodesFromTmxFile("Lemonade/levels/slf2/" + loc + "/" + loc + "_level1"  + ".tmx", "map", "bg", FlxXMLReader.ACTORS);
                string[] actorsSpl = actorsString[0]["csvData"].Split(',');
                foreach (string item in actorsSpl)
                {
                    if (item == "390")
                    {
                        coinCount++;
                    }
                }
            }

            return coinCount;

        }

        public  static  void writeGameProgressToFile()
        {
            string progress = "";
            foreach (var item in gameProgress)
            {
                progress += item.Key.ToString() + ","
                    + item.Value.KilledArmy.ToString().ToLower() + ","
                    + item.Value.KilledChef.ToString().ToLower() + ","
                    + item.Value.KilledInspector.ToString().ToLower() + ","
                    + item.Value.KilledWorker.ToString().ToLower() + ","
                    + item.Value.LevelComplete.ToString().ToLower() + "\n";
            }
            FlxU.saveToDevice(progress, "gameProgress.slf");

            
        }

    }
}
