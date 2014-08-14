﻿using System;
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

            credits = new FlxText(0, FlxG.height / 2 - 100, FlxG.width, "Time Makes\nFools Of\nUs All");
            credits.setFormat(FlxG.Content.Load<SpriteFont>("Lemonade/SMALL_PIXEL"), 2, Lemonade_Globals.GAMEBOY_COLOR_4, FlxJustification.Center, Lemonade_Globals.GAMEBOY_COLOR_1);
            credits.setScrollFactors(0, 0);
            credits.visible = false;
            add(credits);

            instruction = new FlxText(0, FlxG.height / 1.3f, FlxG.width, "Press X to Continue");
            instruction.setFormat(FlxG.Content.Load<SpriteFont>("Lemonade/SMALL_PIXEL"), 2, Lemonade_Globals.GAMEBOY_COLOR_4, FlxJustification.Center, Lemonade_Globals.GAMEBOY_COLOR_1);
            instruction.setScrollFactors(0, 0);
            instruction.visible = false;
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



            float mult = 3.3f;

            if (follower.y > 2100 * mult)
            {
                credits.visible = true;
            }
            if (follower.y > 2300 * mult)
            {
                credits.text = "You Do";
            }
            if (follower.y > 2500 * mult)
            {
                credits.text = "Not Own";
            }
            if (follower.y > 2700 * mult)
            {
                credits.text = "Super";
            }
            if (follower.y > 2900 * mult)
            {
                credits.text = "Lemonade";
            }
            if (follower.y > 3000 * mult)
            {
                credits.text = "Factory";
            }
            if (follower.y > 3100 * mult)
            {
                credits.scale = 5;
                credits.text = "1989";
            }
            if (follower.y > 3200 * mult)
            {
                credits.scale = 2;
                credits.text = "Great Shame";
            }
            if (follower.y > 3300 * mult)
            {
                instruction.visible = true;
            }

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

                //int loc = (int)FlxU.random(0, 6);
                //if (loc == 0) Lemonade_Globals.location = "sydney";
                //else if (loc == 1) Lemonade_Globals.location = "newyork";
                //else if (loc == 2) Lemonade_Globals.location = "military";
                //else if (loc == 3) Lemonade_Globals.location = "warehouse";
                //else if (loc == 4) Lemonade_Globals.location = "factory";
                //else Lemonade_Globals.location = "management";
                //Console.WriteLine("Location: {0} {1}", Lemonade_Globals.location, loc);

                Lemonade_Globals.location = "factory";
                FlxG.level = 1;


                Lemonade_Globals.coins = 0;
                Lemonade_Globals.timeLeft = Lemonade_Globals.totalTimeAvailable = 30.1f;
                FlxG.state = new IntroState();
#endif
                return;
            }


        }


    }
}