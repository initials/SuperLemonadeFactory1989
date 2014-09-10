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
    public class BaseInformationState : FlxState
    {
        FlxSprite follower;
        
        public FlxText credits;
        public FlxText instruction;
        public FlxText heading;

        Tweener tween;
        FlxGroup rain;
        FlxEmitter splashes;

        public int speed = 450;

        override public void create()
        {
            base.create();

            Lemonade_Globals.totalCoins = Lemonade_Globals.calculateTotalCoins();

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

            follower = new FlxSprite(400, 1500);
            follower.visible = false;
            add(follower);
            follower.velocity.Y = this.speed;

            FlxG.follow(follower, 20.0f);
            FlxG.followBounds(0, 0, int.MaxValue, 2000);

			int textSize = 2;

			#if __ANDROID__
			textSize = 4;
			#endif

            
            heading = new FlxText(0, 50, FlxG.width, "Collection Incomplete");
			heading.setFormat(FlxG.Content.Load<SpriteFont>("Lemonade/SMALL_PIXEL"), textSize, Lemonade_Globals.GAMEBOY_COLOR_4, FlxJustification.Center, Lemonade_Globals.GAMEBOY_COLOR_1);
            heading.setScrollFactors(0, 0);
            add(heading);



            string howWellDidYouGo = "Collected " + Lemonade_Globals.coins.ToString() + "\nfrom " + Lemonade_Globals.totalCoins.ToString() + " Coins ";
            credits = new FlxText(0, FlxG.height / 1.75f , FlxG.width, howWellDidYouGo);
			credits.setFormat(FlxG.Content.Load<SpriteFont>("Lemonade/SMALL_PIXEL"), textSize, Lemonade_Globals.GAMEBOY_COLOR_4, FlxJustification.Center, Lemonade_Globals.GAMEBOY_COLOR_1);
            credits.setScrollFactors(0,0);
            credits.visible = false;
            add(credits);

            string ins = "Press X to Continue";
#if __ANDROID__
            ins = "Press O to Continue";
#endif

            instruction = new FlxText(0, FlxG.height / 1.3f, FlxG.width, ins);
			instruction.setFormat(FlxG.Content.Load<SpriteFont>("Lemonade/SMALL_PIXEL"), textSize, Lemonade_Globals.GAMEBOY_COLOR_4, FlxJustification.Center, Lemonade_Globals.GAMEBOY_COLOR_1);
            instruction.setScrollFactors(0, 0);
            instruction.visible = false;
            add(instruction);

            if (Lemonade_Globals.coins == Lemonade_Globals.totalCoins)
            {
				heading.setFormat(FlxG.Content.Load<SpriteFont>("Lemonade/SMALL_PIXEL"), textSize, Lemonade_Globals.GAMEBOY_COLOR_4, FlxJustification.Center, Lemonade_Globals.GAMEBOY_COLOR_1);
				heading.text = "Complete Collection!!\nThe Lemonade Factory is saved.";
				instruction.setFormat(FlxG.Content.Load<SpriteFont>("Lemonade/SMALL_PIXEL"), textSize, Lemonade_Globals.GAMEBOY_COLOR_4, FlxJustification.Center, Lemonade_Globals.GAMEBOY_COLOR_1);


				string ins2 = "Press X to Continue";
#if __ANDROID__
				ins2 = "Press O to Continue";
#endif

				instruction.text = ins2;

                //FlxU.openURL("http://initials.itch.io/slf2/download/Y9wdBOHe7a92Qpo9t5UJdz05HhZR5p10F0L6wfdP");

            }
            
            tween = new Tweener(FlxG.height / 1.3f, FlxG.height / 1.2f, TimeSpan.FromSeconds(0.67f), XNATweener.Cubic.EaseInOut);
            tween.PingPong = true;
            tween.Start();

            // play some music
            FlxG.playMp3("Lemonade/music/March", 0.75f);

            rain = new FlxGroup();
            for (int i = 0; i < 150; i++)
            {
                FlxSprite rainDrop = new FlxSprite((FlxU.random()* FlxG.width*2), -200 + (FlxU.random() * 1000));
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
                if (item.y > 2000)
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
                instruction.visible = true;
            }

            if (((follower.y > int.MaxValue && follower.x == 0) || 
                (FlxG.keys.justPressed(Keys.X) && follower.y > 100) || 
                (FlxG.gamepads.isNewButtonPress(Buttons.A) && follower.y > 100) ||  (FlxControl.ACTIONJUSTPRESSED && follower.y > 100)) 
                && (FlxG.transition.members[0] as FlxSprite).scale < 0.001f )
            {
                if (Lemonade_Globals.coins == Lemonade_Globals.totalCoins)
                {
					#if !__ANDROID__
                    FlxU.openURL("http://initials.itch.io/slf2/download/Y9wdBOHe7a92Qpo9t5UJdz05HhZR5p10F0L6wfdP");
					#endif

				}

                follower.velocity.X = -250;
            }
            if (follower.x < 0)
            {
                FlxG.transition.startFadeOut(0.15f, -90, 150);
                follower.x = 1;
                follower.velocity.X = 1;
            }
            if (FlxG.transition.complete)
            {
                FlxG.state = new IntroState();

                FlxG.transition.resetAndStop();

                return;
            }


        }


    }
}

