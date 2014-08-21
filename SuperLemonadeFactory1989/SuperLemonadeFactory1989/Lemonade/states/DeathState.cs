using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using org.flixel;

using System.Linq;
using System.Xml.Linq;

using XNATweener;

namespace Lemonade
{
    public class DeathState : FlxState
    {
        FlxSprite follower;
        FlxText credits;
        FlxText instruction;
        Tweener tween;

        override public void create()
        {
            base.create();

#if __ANDROID__
			FlxG.BUILD_TYPE = FlxG.BUILD_TYPE_OUYA;
#endif

            FlxTilemap bgMap = new FlxTilemap();
            bgMap.auto = FlxTilemap.STRING;
            bgMap.indexOffset = -1;
            bgMap.loadTMXMap("Lemonade/levels/slf2/newyork/newyork_intro.tmx", "map", "bg", FlxXMLReader.TILES, FlxG.Content.Load<Texture2D>("Lemonade/bgtiles_newyork"), 20, 20);
            bgMap.boundingBoxOverride = false;
            bgMap.setScrollFactors(1, 1);
            add(bgMap);

            bgMap = new FlxTilemap();
            bgMap.auto = FlxTilemap.STRING;
            bgMap.indexOffset = -1;
            bgMap.loadTMXMap("Lemonade/levels/slf2/newyork/newyork_intro.tmx", "map", "bg2", FlxXMLReader.TILES, FlxG.Content.Load<Texture2D>("Lemonade/bgtiles_newyork"), 20, 20);
            bgMap.boundingBoxOverride = false;
            bgMap.setScrollFactors(1, 1);
            add(bgMap);

            FlxTilemap bgMap3 = new FlxTilemap();
            bgMap3.auto = FlxTilemap.STRING;
            bgMap3.indexOffset = -1;
            bgMap3.loadTMXMap("Lemonade/levels/slf2/newyork/newyork_intro.tmx", "map", "stars", FlxXMLReader.TILES, FlxG.Content.Load<Texture2D>("Lemonade/bgtiles_newyork"), 20, 20);
            bgMap3.boundingBoxOverride = false;
            bgMap3.setScrollFactors(0.5f, 0.5f);
            add(bgMap3);

            FlxTilemap bgMap4 = new FlxTilemap();
            bgMap4.auto = FlxTilemap.STRING;
            bgMap4.indexOffset = -1;
            bgMap4.loadTMXMap("Lemonade/levels/slf2/newyork/newyork_intro.tmx", "map", "city", FlxXMLReader.TILES, FlxG.Content.Load<Texture2D>("Lemonade/bgtiles_newyork"), 20, 20);
            bgMap4.boundingBoxOverride = false;
            bgMap4.setScrollFactors(1, 1);
            add(bgMap4);

            follower = new FlxSprite(0, -100);
            follower.visible = false;
            add(follower);
            follower.velocity.Y = 1450;
            follower.drag.Y = 10;

            FlxG.follow(follower, 20.0f);
            FlxG.followBounds(0, 0, int.MaxValue, 2000);

            FlxText text1 = new FlxText(0, FlxG.height / 2 - 50, FlxG.width, "GAME OVER");
            text1.setFormat(FlxG.Content.Load<SpriteFont>("Lemonade/SMALL_PIXEL"), 2, Lemonade_Globals.GAMEBOY_COLOR_4, FlxJustification.Center, Lemonade_Globals.GAMEBOY_COLOR_1);
            text1.setScrollFactors(1.5f, 1.5f);

            add(text1);


            string howWellDidYouGo = "Collected " + Lemonade_Globals.coins.ToString() + " from " + Lemonade_Globals.totalCoins.ToString() + " Coins ";
            credits = new FlxText(0, FlxG.height / 2 - 100, FlxG.width, "");
            credits.setFormat(FlxG.Content.Load<SpriteFont>("Lemonade/SMALL_PIXEL"), 2, Lemonade_Globals.GAMEBOY_COLOR_4, FlxJustification.Center, Lemonade_Globals.GAMEBOY_COLOR_1);
            credits.setScrollFactors(0, 0);
            credits.visible = true;
            add(credits);

            instruction = new FlxText(0, FlxG.height / 1.3f, FlxG.width, "Press X to Continue");
            instruction.setFormat(FlxG.Content.Load<SpriteFont>("Lemonade/SMALL_PIXEL"), 2, Lemonade_Globals.GAMEBOY_COLOR_4, FlxJustification.Center, Lemonade_Globals.GAMEBOY_COLOR_1);
            instruction.setScrollFactors(0, 0);
            instruction.visible = true;
            add(instruction);


            //rgb(237, 0, 142)

            tween = new Tweener(FlxG.height / 1.3f, FlxG.height / 1.2f, TimeSpan.FromSeconds(1.0f), XNATweener.Cubic.EaseInOut);
            tween.PingPong = true;
            tween.Start();

            // play some music
            FlxG.playMp3("Lemonade/music/Lemonade1989Theme", 0.75f);

            FlxG.play("Lemonade/sfx/cw_sound15", 0.5f, false);


        }

        override public void update()
        {

            base.update();

            if (instruction.visible)
            {
                if (tween.hasEnded)
                {
                    FlxG.play("Lemonade/sfx/cw_sound09", 0.75f, false);
                }
            }

            tween.Update(FlxG.elapsedAsGameTime);
            instruction.y = tween.Position;


            if (((follower.y > int.MaxValue && follower.x == 0) ||
                (FlxG.keys.justPressed(Keys.X) && follower.y > 100) ||
                (FlxG.gamepads.isNewButtonPress(Buttons.A) && follower.y > 100) || (FlxControl.ACTIONJUSTPRESSED && follower.y > 100))
                && (FlxG.transition.members[0] as FlxSprite).scale < 0.001f)
            {
                FlxG.transition.startFadeOut(0.15f, -90, 150);
                follower.x = 1;
            }
            if (FlxG.transition.complete)
            {
#if __ANDROID__
				FlxG.state = new OuyaEasyMenuState();
#endif
#if !__ANDROID__

                FlxG.state = new IntroState();
#endif
                return;
            }


        }


    }
}
