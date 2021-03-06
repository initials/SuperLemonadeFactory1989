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
    public class IntroState : FlxState
    {
        FlxSprite follower;
        FlxText credits;
        FlxText instruction;
        Tweener tween;
        FlxGroup rain;
        FlxEmitter splashes;

        override public void create()
        {
            base.create();

            Lemonade_Globals.totalCoins = Lemonade_Globals.calculateTotalCoins();

            Console.WriteLine("Total coins = {0}", Lemonade_Globals.totalCoins);

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
            follower.velocity.Y = 450;

            FlxG.follow(follower, 20.0f);
            FlxG.followBounds(0, 0, int.MaxValue, 2000);

            FlxText text1 = new FlxText(0, FlxG.height / 2 - 50, FlxG.width, "Initials\nVideo\nGames\nPresents");
            text1.setFormat(FlxG.Content.Load<SpriteFont>("Lemonade/SMALL_PIXEL"), 2, Lemonade_Globals.GAMEBOY_COLOR_4, FlxJustification.Center, Lemonade_Globals.GAMEBOY_COLOR_1);
            text1.setScrollFactors(1.5f, 1.5f);
           
            add(text1);

            credits = new FlxText(0, FlxG.height / 2 - 100, FlxG.width, "A Game by\nShane Brouwer");
            credits.setFormat(FlxG.Content.Load<SpriteFont>("Lemonade/SMALL_PIXEL"), 2, Lemonade_Globals.GAMEBOY_COLOR_4, FlxJustification.Center, Lemonade_Globals.GAMEBOY_COLOR_1);
            credits.setScrollFactors(0,0);
            credits.visible = false;
            add(credits);

			string ins = "Press X to Continue";
			#if __ANDROID__
			ins = "Press O to Continue";
			#endif

			instruction = new FlxText(0, FlxG.height / 1.3f, FlxG.width, ins);
            instruction.setFormat(FlxG.Content.Load<SpriteFont>("Lemonade/SMALL_PIXEL"), 2, Lemonade_Globals.GAMEBOY_COLOR_4, FlxJustification.Center, Lemonade_Globals.GAMEBOY_COLOR_1);
            instruction.setScrollFactors(0, 0);
            instruction.visible = false;
            add(instruction);

            tween = new Tweener(FlxG.height / 1.3f, FlxG.height / 1.2f, TimeSpan.FromSeconds(0.67f), XNATweener.Cubic.EaseInOut);
            tween.PingPong = true;
            tween.Start();

            // play some music
            FlxG.playMp3("Lemonade/music/Beyond", 0.75f);

            rain = new FlxGroup();
            for (int i = 0; i < 150; i++)
            {
                FlxSprite rainDrop = new FlxSprite((FlxU.random()* FlxG.width), -200 + (FlxU.random() * 1000));
                rainDrop.loadGraphic("Lemonade/rain", true, false, 2, 2);
                rainDrop.frame = (int)FlxU.random(0, 6);
                rainDrop.velocity.Y = FlxU.random(350, 400);
                rain.add(rainDrop);
            }
            add(rain);

            splashes = new FlxEmitter();
            splashes.createSprites("Lemonade/rain", 150, true, 0.0f, 0.0f);
            splashes.setXSpeed(-25, 25);
            splashes.setYSpeed(-30, 0);
            splashes.gravity = 1.0f;
            add(splashes);



        }

        override public void update()
        {
            if (FlxG.keys.justPressed(Keys.Q))
            {
                FlxG.bloom.Visible = !FlxG.bloom.Visible;
                FlxG.bloom.usePresets = true;

                follower.velocity.Y += 1450;
            }
            if (FlxG.keys.ONE)
            {
                FlxG.bloom.Settings = BloomPostprocess.BloomSettings.PresetSettings[1];
            }
            if (FlxG.keys.TWO)
            {
                FlxG.bloom.Settings = BloomPostprocess.BloomSettings.PresetSettings[2];
            }
            if (FlxG.keys.THREE)
            {
                FlxG.bloom.Settings = BloomPostprocess.BloomSettings.PresetSettings[3];
            }
            if (FlxG.keys.FOUR)
            {
                FlxG.bloom.Settings = BloomPostprocess.BloomSettings.PresetSettings[4];
            }
            if (FlxG.keys.FIVE)
            {
                FlxG.bloom.Settings = BloomPostprocess.BloomSettings.PresetSettings[5];
            }
            if (FlxG.keys.SIX)
            {
                FlxG.bloom.Settings = BloomPostprocess.BloomSettings.PresetSettings[6];
            }
            if (FlxG.keys.SEVEN)
            {
                FlxG.bloom.Settings = BloomPostprocess.BloomSettings.PresetSettings[7];
            }

            foreach (FlxSprite item in rain.members)
            {
				int rainOffset = 0;

				#if __ANDROID__
				rainOffset = 50;
				#endif
				if (item.y > 2000 - rainOffset)
                {
                    splashes.at(item);
                    splashes.start(true, 0.0f, 10);

                    item.y = 1200;
                }
            }

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

            if (follower.y > 2100 )
            {
                credits.visible = true;
            }
            if (follower.y > 2300)
            {
                credits.text = "Pixel Art by\nMiguelito";
            }
            if (follower.y > 2500)
            {
                credits.text = "Dithering Expert\nAndrio";
            }
            if (follower.y > 2700)
            {
                credits.text = "Super";
            }
            if (follower.y > 2900)
            {
                credits.text = "Lemonade";
            }
            if (follower.y > 3000)
            {
                credits.text = "Factory";
            }
            if (follower.y > 3100)
            {
                credits.scale = 5;
                credits.text = "1989";
            }
            if (follower.y > 3200)
            {
                credits.scale = 2;

				#if __ANDROID__
				credits.scale = 4;
				#endif


                credits.text = "Super Lemonade\nFactory 1989";
            }
            if (follower.y > 3300)
            {
                instruction.visible = true;
            }

            if (((follower.y > int.MaxValue && follower.x == 0) || 
                (FlxG.keys.justPressed(Keys.X) && follower.y > 100) || 
                (FlxG.gamepads.isNewButtonPress(Buttons.A) && follower.y > 100) ||  (FlxControl.ACTIONJUSTPRESSED && follower.y > 100)) 
                && (FlxG.transition.members[0] as FlxSprite).scale < 0.001f )
            {
                FlxG.transition.startFadeOut(0.15f, -90, 150);
                follower.x = 1;
            }
            if (FlxG.transition.complete)
            {

                FlxG.level = 1 ;

                Lemonade_Globals.stateSaver = new Dictionary<string, Dictionary<string, Vector2>> 
                {  
                    {"newyork", new Dictionary<string, Vector2>()},
                    {"sydney", new Dictionary<string, Vector2>()},
                    {"military", new Dictionary<string, Vector2>()},
                    {"warehouse", new Dictionary<string, Vector2>()},
                    {"factory", new Dictionary<string, Vector2>()},
                    {"management", new Dictionary<string, Vector2>()},

                };

                foreach (var item in Lemonade_Globals.stateSaver)
                {
                    Lemonade_Globals.thisTurnProgress[item.Key] = 0;
                }


                Lemonade_Globals.levelChanges = 0;
                Lemonade_Globals.coins = 0;
                Lemonade_Globals.timeLeft  = Lemonade_Globals.totalTimeAvailable = 30.1f;

                FlxG.state = new LevelChooserState();

                return;
            }


        }


    }
}
