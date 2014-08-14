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
    public class PlayState : FlxState
    {

        Dictionary<string, string> levelAttrs;
        List<Dictionary<string, string>> levelString;

        List<Dictionary<string, string>> actorsString;

        private FlxTilemap collidableTilemap;
        private FlxTilemap bgElementsTilemap;

        private FlxGroup actors;
        private FlxGroup trampolines;
        private FlxGroup levelItems;
        private FlxGroup hazards;
        private FlxGroup ramps;
        private FlxGroup smallCrates;
        private FlxGroup movingPlatforms;
        private Timer timer;

        /// <summary>
        /// Use for SLF1 style Ogmo1 levels; Collide only on version 1 style.
        /// </summary>
        private FlxGroup collidableTileblocks;

        private Andre andre;
        private Liselot liselot;
        private Army army;
        private Worker worker;
        private Inspector inspector;
        private Chef chef;
        private Trampoline trampoline;
        private LargeCrate largeCrate;
        private SmallCrate smallCrate;
        private Exit exit;
        private bool levelComplete = false;
        private Spike spike;
        private Ramp ramp;

        private Follower follow;

        private FlxEmitter bubbleParticle;
        private FlxEmitter crateParticle;

        private const float LERP = 6.0f;

        private Hud currentCharHud;


        FlxSprite badge1;
        FlxSprite badge2;
        FlxSprite badge3;
        FlxSprite badge4;

        Color notDone;
        Color done;

        private float transitionPause;

        public void buildTileset() //string LevelFile, string Tiles
        {
            transitionPause = 0.0f;

            List<Dictionary<string, string>> bgString = FlxXMLReader.readNodesFromTmxFile("Lemonade/levels/slf2/" + Lemonade_Globals.location + "/bg" + Lemonade_Globals.location + ".tmx", "map", "bg", FlxXMLReader.TILES);

            FlxTilemap bgMap = new FlxTilemap();
            bgMap.auto = FlxTilemap.STRING;
            bgMap.indexOffset = -1;
            bgMap.loadMap(bgString[0]["csvData"], FlxG.Content.Load<Texture2D>("Lemonade/bgtiles_" + Lemonade_Globals.location), 20, 20);
            bgMap.boundingBoxOverride = false;
            bgMap.setScrollFactors(0, 0);
            add(bgMap);
            
            levelAttrs = new Dictionary<string, string>();
            levelAttrs = FlxXMLReader.readAttributesFromTmxFile("Lemonade/levels/slf2/" + Lemonade_Globals.location + "/" + Lemonade_Globals.location + "_level" + FlxG.level.ToString() + ".tmx", "map");
            
            foreach (KeyValuePair<string, string> kvp in levelAttrs)
            {
                //Console.WriteLine("Key = {0}, Value = {1}", kvp.Key, kvp.Value);
            }

            FlxG.levelWidth = Convert.ToInt32(levelAttrs["width"]) * Convert.ToInt32(levelAttrs["tilewidth"]);
            FlxG.levelHeight = Convert.ToInt32(levelAttrs["height"]) * Convert.ToInt32(levelAttrs["tileheight"]);


            levelString = FlxXMLReader.readNodesFromTmxFile("Lemonade/levels/slf2/" + Lemonade_Globals.location + "/" + Lemonade_Globals.location + "_level" + FlxG.level.ToString() + ".tmx", "map", "bg", FlxXMLReader.TILES);
            foreach (Dictionary<string, string> nodes in levelString)
            {
                foreach (KeyValuePair<string, string> kvp in nodes)
                {
                    //Console.Write("Key = {0}, Value = {1}, ", kvp.Key, kvp.Value);
                }
                //Console.Write("\r\n");
            }

            bgElementsTilemap = new FlxTilemap();
            bgElementsTilemap.auto = FlxTilemap.STRING;
            bgElementsTilemap.indexOffset = -1;
            bgElementsTilemap.stringTileMin = 200;
            bgElementsTilemap.stringTileMax = 354;
            bgElementsTilemap.loadMap(levelString[0]["csvData"], FlxG.Content.Load<Texture2D>("Lemonade/tiles_" + Lemonade_Globals.location), 20, 20);
            bgElementsTilemap.boundingBoxOverride = false;
            add(bgElementsTilemap);


            collidableTilemap = new FlxTilemap();
            collidableTilemap.auto = FlxTilemap.STRING;

            // TMX maps have indexOffset of -1;
            collidableTilemap.indexOffset = -1;
            collidableTilemap.stringTileMax = 200;
            collidableTilemap.loadMap(levelString[0]["csvData"], FlxG.Content.Load<Texture2D>("Lemonade/tiles_" + Lemonade_Globals.location), 20, 20);
            collidableTilemap.boundingBoxOverride = false;
            add(collidableTilemap);
        }

        public void buildActors()
        {

            actorsString = FlxXMLReader.readNodesFromTmxFile("Lemonade/levels/slf2/" + Lemonade_Globals.location + "/" + Lemonade_Globals.location + "_level" + FlxG.level.ToString() + ".tmx", "map", "bg", FlxXMLReader.ACTORS);
            foreach (Dictionary<string, string> nodes in actorsString)
            {
                foreach (KeyValuePair<string, string> kvp in nodes)
                {
                    //Console.Write("Actors Key = {0}, Value = {1}, ", kvp.Key, kvp.Value);
                }
                //Console.Write("\r\n");
            }

            //string actorsStr = actorsString[0]["csvData"].Replace(",\n", ",");

            string[] actorsSpl = actorsString[0]["csvData"].Split(',');
            int count = 0;
            foreach (string item in actorsSpl)
            {
                int xPos = ((count) % ((Convert.ToInt32(levelAttrs["width"]))));
                int yPos = ((count) / ((Convert.ToInt32(levelAttrs["width"]))));

                xPos *= Convert.ToInt32(levelAttrs["tilewidth"]);
                yPos *= Convert.ToInt32(levelAttrs["tilewidth"]);

                if (item == "357")
                {
                    buildActor("largeCrate", xPos, yPos);
                }
                if (item == "381")
                {
                    buildActor("andre", xPos, yPos);
                }
                if (item == "382")
                {
                    buildActor("liselot", xPos, yPos);
                }
                if (item == "383")
                {
                    buildActor("army", xPos, yPos);
                }
                if (item == "384")
                {
                    buildActor("worker", xPos, yPos);
                }
                if (item == "385")
                {
                    buildActor("inspector", xPos, yPos);
                }
                if (item == "386")
                {
                    buildActor("chef", xPos, yPos);
                }
                if (item == "387")
                {
                    buildActor("exit", xPos, yPos);
                }
                if (item == "388")
                {
                    buildActor("trampoline", xPos, yPos);
                }
                if (item == "389")
                {
                    buildActor("smallCrate", xPos, yPos);
                }
                if (item == "390")
                {
                    buildActor("coin", xPos, yPos);
                }
                if (item == "391")
                {
                    buildActor("spike_up", xPos, yPos+10);
                }
                if (item == "392")
                {
                    buildActor("spike_right", xPos, yPos);
                }
                if (item == "393")
                {
                    buildActor("spike_down", xPos, yPos);
                }
                if (item == "394")
                {
                    buildActor("spike_left", xPos+10, yPos);
                }
                if (item == "341")
                {
                    buildActor("rampLeft", xPos , yPos);
                }
                if (item == "342")
                {
                    buildActor("rampRight", xPos, yPos);
                }
                if (item == "363")
                {
                    buildActor("plant1", xPos, yPos);
                }
                if (item == "364")
                {
                    buildActor("plant2", xPos, yPos);
                }
                if (item == "365")
                {
                    buildActor("filing1", xPos, yPos);
                }
                if (item == "366")
                {
                    buildActor("filing1", xPos, yPos);
                }
                count++;
            }
        }

        public void buildActor(string actor, int xPos, int yPos)
        {
            if (actor == "andre")
            {
                andre = new Andre(xPos, yPos);
                andre.control = FlxPlatformActor.Controls.player;
                andre.ControllingPlayer = PlayerIndex.One;
                actors.add(andre);
            }
            else if (actor == "liselot")
            {
                liselot = new Liselot(xPos, yPos);
                liselot.control = FlxPlatformActor.Controls.player;
                liselot.ControllingPlayer = PlayerIndex.One;
                actors.add(liselot);
            }
            else if (actor == "army")
            {
                army = new Army(xPos, yPos);
                army.control = FlxPlatformActor.Controls.player;
                army.ControllingPlayer = PlayerIndex.One;
                actors.add(army);
                //army.startPlayingBack();
            }
            else if (actor == "chef")
            {
                chef = new Chef(xPos, yPos);
                chef.control = FlxPlatformActor.Controls.player;
                chef.ControllingPlayer = PlayerIndex.One;
                actors.add(chef);
                //chef.startPlayingBack();

            }
            else if (actor == "inspector")
            {
                inspector = new Inspector(xPos, yPos);
                inspector.control = FlxPlatformActor.Controls.player;
                inspector.ControllingPlayer = PlayerIndex.One;
                actors.add(inspector);
                //inspector.startPlayingBack();
            }
            else if (actor == "worker")
            {
                worker = new Worker(xPos, yPos);
                worker.control = FlxPlatformActor.Controls.player;
                worker.ControllingPlayer = PlayerIndex.One;
                actors.add(worker);
                //worker.startPlayingBack();

            }
            else if (actor == "trampoline")
            {
                xPos -= 2;
                yPos -= 2;

                trampoline = new Trampoline(xPos, yPos);
                trampolines.add(trampoline);
            }
            else if (actor == "coin")
            {
                Coin coin = new Coin(xPos, yPos, false);
                hazards.add(coin);
            }

            else if (actor == "largeCrate")
            {
                largeCrate = new LargeCrate(xPos, yPos);
                levelItems.add(largeCrate);
            }
            else if (actor == "smallCrate")
            {
                smallCrate = new SmallCrate(xPos, yPos);
                smallCrates.add(smallCrate);
            }
            else if (actor == "exit")
            {
                yPos -= 90 ;
                exit = new Exit(xPos, yPos);
                add(exit);
            }
            else if (actor == "spike_up")
            {
                spike = new Spike(xPos, yPos,0);
                hazards.add(spike);
            }
            else if (actor == "spike_right")
            {
                spike = new Spike(xPos, yPos, 1);
                hazards.add(spike);
            }
            else if (actor == "spike_down")
            {
                spike = new Spike(xPos, yPos, 2);
                hazards.add(spike);
            }
            else if (actor == "spike_left")
            {
                spike = new Spike(xPos, yPos, 3);
                hazards.add(spike);
            }
            else if (actor == "rampLeft")
            {
                ramp = new Ramp(xPos, yPos, FlxRamp.LOW_SIDE_LEFT);
                ramps.add(ramp);

            }
            else if (actor == "rampRight")
            {
                ramp = new Ramp(xPos, yPos, FlxRamp.LOW_SIDE_RIGHT);
                ramps.add(ramp);
            }
            else if (actor == "plant1")
            {
                Plant plant = new Plant(xPos, yPos-90, 1);
                hazards.add(plant);
            }
            else if (actor == "plant2")
            {
                Plant plant = new Plant(xPos, yPos - 90, 2);
                hazards.add(plant);
            }
            else if (actor == "filing1")
            {
                FilingCabinet filing = new FilingCabinet(xPos, yPos - 70, 1);
                hazards.add(filing);
            }
            else if (actor == "filing2")
            {
                FilingCabinet filing = new FilingCabinet(xPos, yPos - 40, 2);
                hazards.add(filing);
            }
        }

        public void buildBoxes()
        {
            List<Dictionary<string, string>> levelString = FlxXMLReader.readObjectsFromTmxFile("Lemonade/levels/slf2/" + Lemonade_Globals.location + "/" + Lemonade_Globals.location + "_level" + FlxG.level.ToString() + ".tmx", 
                "map", 
                "boxes", 
                FlxXMLReader.NONE);
            foreach (Dictionary<string, string> nodes in levelString)
            {
                foreach (KeyValuePair<string, string> kvp in nodes)
                {
                    //Console.Write("Level String -- Key = {0}, Value = {1}, ", kvp.Key, kvp.Value);
                }
                //Console.Write("\r\n");
            }

            foreach (var item in levelString)
            {
                if (item.ContainsKey("pointsX"))
                {
                    MovingPlatform movingPlatform = new MovingPlatform(Int32.Parse(item["x"]), Int32.Parse(item["y"]));
                    movingPlatform.solid = true;
                    movingPlatform.@fixed = true;

                    movingPlatforms.add(movingPlatform);

                    FlxPath xpath = new FlxPath(null);
                    //xpath.add(Int32.Parse(item["x"]), Int32.Parse(item["y"]));
                    xpath.addPointsUsingStrings(item["pointsX"], item["pointsY"]);
                    movingPlatform.followPath(xpath, 150, FlxSprite.PATH_YOYO, false);
                    movingPlatform.pathCornering = 0.0f;
                }


            }
        }

        public void buildTilesetForOgmo1() //string LevelFile, string Tiles
        {
            //push width / height to flxg.levelheight;

            Dictionary<string, string> w = FlxXMLReader.readAttributesFromOelFile("Lemonade/levels/slf/level1.oel", "level/width");
            FlxG.levelWidth = Convert.ToInt32( w["width"]);
            Dictionary<string, string> h = FlxXMLReader.readAttributesFromOelFile("Lemonade/levels/slf/level1.oel", "level/height");
            FlxG.levelHeight = Convert.ToInt32(h["height"]);

            Console.WriteLine("FlxG.lw = {0} {1}", FlxG.levelWidth, FlxG.levelHeight);


            // ------------------------------------------

            List<Dictionary<string, string>> bgString = FlxXMLReader.readNodesFromOel1File("Lemonade/levels/slf/level" + FlxG.level + ".oel", "level/solids");

            foreach (Dictionary<string, string> nodes in bgString)
            {
                FlxTileblock ta = new FlxTileblock(Convert.ToInt32(nodes["x"]), Convert.ToInt32(nodes["y"]), Convert.ToInt32(nodes["w"]), Convert.ToInt32(nodes["h"]));
                ta.loadTiles(FlxG.Content.Load<Texture2D>("Lemonade/slf1/level1/level1_tiles"), 10, 10, 0);
                ta.auto = FlxTileblock.AUTO;
                collidableTileblocks.add(ta);
            }
        }

        public void buildActorsForOgmo1()
        {
            List<Dictionary<string, string>> bgString = FlxXMLReader.readNodesFromOel1File("Lemonade/levels/slf/level" + FlxG.level + ".oel", "level/characters");

            foreach (Dictionary<string, string> nodes in bgString)
            {
                if (nodes["Name"] == "player")
                {
                    buildActor("andre", Convert.ToInt32(nodes["x"]), Convert.ToInt32(nodes["y"]));
                }
                if (nodes["Name"] == "liselot")
                {
                    buildActor("liselot", Convert.ToInt32(nodes["x"]), Convert.ToInt32(nodes["y"]));
                }
                if (nodes["Name"] == "worker")
                {
                    buildActor("worker", Convert.ToInt32(nodes["x"]), Convert.ToInt32(nodes["y"]));
                }
                if (nodes["Name"] == "army")
                {
                    buildActor("army", Convert.ToInt32(nodes["x"]), Convert.ToInt32(nodes["y"]));
                }
                if (nodes["Name"] == "inspector")
                {
                    buildActor("inspector", Convert.ToInt32(nodes["x"]), Convert.ToInt32(nodes["y"]));
                }
                if (nodes["Name"] == "chef")
                {
                    buildActor("chef", Convert.ToInt32(nodes["x"]), Convert.ToInt32(nodes["y"]));
                }
            }

            List<Dictionary<string, string>> objects = FlxXMLReader.readNodesFromOel1File("Lemonade/levels/slf/level" + FlxG.level + ".oel", "level/objects");

        }

        override public void create()
        {

            base.create();

            Console.WriteLine("Starting Level {0} : {1}", FlxG.level, Lemonade_Globals.location);
            FlxG.transition.resetAndStop();


            FlxG._game.hud.p1HudText.x = -1000;
            FlxG._game.hud.p2HudText.x = -1000;
            FlxG._game.hud.p3HudText.x = -1000;
            FlxG._game.hud.p4HudText.x = -1000;

            FlxG._game.hud.setHudGamepadButton( FlxHud.TYPE_KEYBOARD ,0, -1000, -1000);

            FlxG.mouse.hide();

            FlxG.autoHandlePause = true;

            actors = new FlxGroup();
            trampolines = new FlxGroup();
            levelItems = new FlxGroup();
            ramps = new FlxGroup();
            smallCrates = new FlxGroup();
            movingPlatforms = new FlxGroup();
            hazards = new FlxGroup();
            collidableTileblocks = new FlxGroup();

            //Level Adjust

            buildTileset();
            buildActors();
            buildBoxes();

            //if (Lemonade_Globals.location == "factory") FlxG.level += 12;
            //if (Lemonade_Globals.location == "management") FlxG.level += 24;

            //// Build for slf2 (Tiled Maps)
            //if (Lemonade_Globals.location == "military" ||
            //    Lemonade_Globals.location == "newyork" ||
            //    Lemonade_Globals.location == "sydney")
            //{
            //    buildTileset();
            //    buildActors();
            //    buildBoxes();
                
            //    Lemonade_Globals.game_version = 2;

            //}
            //else if (   Lemonade_Globals.location == "warehouse" ||
            //            Lemonade_Globals.location == "factory" ||
            //            Lemonade_Globals.location == "management")
            //{
            //    buildTilesetForOgmo1();
            //    buildActorsForOgmo1();

            //    Lemonade_Globals.game_version = 1;
            //}

            add(trampolines);
            add(levelItems);
            
            add(ramps);
            add(smallCrates);
            add(movingPlatforms);
            add(actors);
            add(collidableTileblocks);

            add(hazards);

            
            

            //set up a little bubble particle system.

            bubbleParticle = new FlxEmitter();
            bubbleParticle.delay = 3;
            bubbleParticle.setXSpeed(-150, 150);
            bubbleParticle.setYSpeed(-40, 100);
            bubbleParticle.setRotation(-720, 720);
            bubbleParticle.gravity = Lemonade_Globals.GRAVITY * -0.25f;
            bubbleParticle.createSprites(FlxG.Content.Load<Texture2D>("Lemonade/bubble"), 200, true, 1.0f, 0.65f);
            add(bubbleParticle);

            crateParticle = new FlxEmitter();
            crateParticle.delay = float.MaxValue;
            crateParticle.setSize(80, 60);
            crateParticle.setXSpeed(-350, 350);
            crateParticle.setYSpeed(-200, 200);
            crateParticle.setRotation(-720, 720);
            crateParticle.gravity = Lemonade_Globals.GRAVITY;
            crateParticle.createSprites(FlxG.Content.Load<Texture2D>("Lemonade/crateShards"), 200, true, 1.0f, 0.65f);
            add(crateParticle);


            // follow.
            FlxG.followBounds(0,0,FlxG.levelWidth, FlxG.levelHeight);

            if (Lemonade_Globals.location == "factory") FlxG.follow(worker, LERP);
            if (Lemonade_Globals.location == "management") FlxG.follow(liselot, LERP);
            if (Lemonade_Globals.location == "military") FlxG.follow(army, LERP);
            if (Lemonade_Globals.location == "newyork") FlxG.follow(chef, LERP);
            if (Lemonade_Globals.location == "warehouse") FlxG.follow(andre, LERP);
            if (Lemonade_Globals.location == "sydney") FlxG.follow(inspector, LERP);

            playSong();

            currentCharHud = new Hud(5, 5);
            add(currentCharHud);



            int YPOS = 125;

            notDone = new Color(0.1f, 0.1f, 0.1f);
            done = Color.White;
            badge1 = new FlxSprite((FlxG.width / 2) - 150, YPOS);
            badge1.loadGraphic(FlxG.Content.Load<Texture2D>("Lemonade/offscreenIcons"), true, false, 12, 12);
            badge1.frame = 4;
            badge1.color = notDone;
            add(badge1);
            badge1.visible = false;
            badge1.setScrollFactors(0, 0);

            badge2 = new FlxSprite((FlxG.width / 2) - 50, YPOS);
            badge2.loadGraphic(FlxG.Content.Load<Texture2D>("Lemonade/offscreenIcons"), true, false, 12, 12);
            badge2.frame = 5;
            badge2.color = notDone;
            add(badge2);
            badge2.setScrollFactors(0, 0);

            badge3 = new FlxSprite((FlxG.width / 2) + 50, YPOS);
            badge3.loadGraphic(FlxG.Content.Load<Texture2D>("Lemonade/offscreenIcons"), true, false, 12, 12);
            badge3.frame = 3;
            badge3.color = notDone;
            add(badge3);
            badge3.setScrollFactors(0, 0);

            badge4 = new FlxSprite((FlxG.width / 2) + 150, YPOS);
            badge4.loadGraphic(FlxG.Content.Load<Texture2D>("Lemonade/offscreenIcons"), true, false, 12, 12);
            badge4.frame = 2;
            badge4.color = notDone;
            add(badge4);
            badge4.setScrollFactors(0, 0);

            badge1.visible = false;
            badge2.visible = false;
            badge3.visible = false;
            badge4.visible = false;


            timer = new Timer(FlxG.width / 2 - 50, 10, 200);
            timer.time = Lemonade_Globals.timeLeft;
            add(timer);

            currentCharHud.time = Lemonade_Globals.timeLeft;


            foreach (FlxSprite item in actors.members)
            {
                //Lemonade_Globals.stateSaver[Lemonade_Globals.location][item.ToString()] = new Vector2(item.x, item.y);

                try
                {
                    item.x = Lemonade_Globals.stateSaver[Lemonade_Globals.location][item.ToString()].X;
                    item.y = Lemonade_Globals.stateSaver[Lemonade_Globals.location][item.ToString()].Y;
                }
                catch (Exception)
                {
                    
                }



            }
            int count = 0;
            foreach (FlxSprite item in hazards.members)
            {

                try
                {
                    //Lemonade_Globals.stateSaver[Lemonade_Globals.location][item.ToString() + count.ToString()] = new Vector2(item.x, item.y);
                    if (item.ToString().StartsWith("Lemonade. Coin"))
                    {
                        item.x = Lemonade_Globals.stateSaver[Lemonade_Globals.location][item.ToString() + count.ToString()].X;
                        item.y = Lemonade_Globals.stateSaver[Lemonade_Globals.location][item.ToString() + count.ToString()].Y;
                    }

                    count++;
                }
                catch (Exception)
                {
                    
                }

            }





        }

        /// <summary>
        /// Play a song based on the location.
        /// </summary>
        public void playSong()
        {
            if (Lemonade_Globals.restartMusic == false)
            {
                FlxG.resumeMp3();
            }
            else
            {
                if (Lemonade_Globals.location == "sydney")
                {
                    FlxG.playMp3("Lemonade/music/level3", 0.5f);
                }
                else if (Lemonade_Globals.location == "newyork")
                {
                    FlxG.playMp3("Lemonade/music/level1", 0.5f);
                }
                else if (Lemonade_Globals.location == "military")
                {
                    FlxG.playMp3("Lemonade/music/level2", 0.5f);
                }
                else
                {
                    FlxG.playMp3("Lemonade/music/level3", 0.5f);
                }
            }

        }

        override public void update()
        {
            #region cheats
            // Run cheats.
            if (FlxG.debug == true && elapsedInState>0.2f)
            {
                if (FlxGlobal.cheatString == "exits")
                {
                    andre.at(exit);
                    liselot.at(exit);
                }
                if (FlxGlobal.cheatString == "pirate")
                {
                    Lemonade_Globals.PAID_VERSION = Lemonade_Globals.PIRATE_MODE;
                }
                if (FlxG.keys.justPressed(Keys.F10) && FlxG.debug==true)
                {
                    //Lemonade_Globals.gameProgress[Lemonade_Globals.location + "_" + FlxG.level.ToString()].KilledArmy = true;
                    //Lemonade_Globals.gameProgress[Lemonade_Globals.location + "_" + FlxG.level.ToString()].KilledChef = true;
                    //Lemonade_Globals.gameProgress[Lemonade_Globals.location + "_" + FlxG.level.ToString()].KilledInspector = true;
                    //Lemonade_Globals.gameProgress[Lemonade_Globals.location + "_" + FlxG.level.ToString()].KilledWorker = true;
                    andre.x = exit.x;
                    andre.y = exit.y;
                    liselot.x = exit.x;
                    liselot.y = exit.y;

                }
				if (FlxG.keys.justPressed(Keys.F9) || (FlxG.gamepads.isButtonDown(Buttons.RightStick) && FlxG.debug==true ))
                {
                    Lemonade_Globals.restartMusic = false;
                    //Lemonade_Globals.gameProgress[Lemonade_Globals.location + "_" + FlxG.level.ToString()].LevelComplete = true;
                    //Lemonade_Globals.gameProgress[Lemonade_Globals.location + "_" + FlxG.level.ToString()].KilledArmy = true;
                    //Lemonade_Globals.gameProgress[Lemonade_Globals.location + "_" + FlxG.level.ToString()].KilledChef = true;
                    //Lemonade_Globals.gameProgress[Lemonade_Globals.location + "_" + FlxG.level.ToString()].KilledInspector = true;
                    //Lemonade_Globals.gameProgress[Lemonade_Globals.location + "_" + FlxG.level.ToString()].KilledWorker = true;



                    Lemonade_Globals.writeGameProgressToFile();

                    if (FlxG.level == 12 && Lemonade_Globals.game_version == 2)
                    {
                        FlxG.state = new VictoryState();
                        return;
                    }
                    else if (FlxG.level == 12 && Lemonade_Globals.game_version == 1)
                    {
						#if __ANDROID__
						FlxG.state = new OuyaEasyMenuState();
						#endif
						#if !__ANDROID__
						FlxG.state = new IntroState();
						#endif
                        return;
                    }
                    else
                    {
                        goToRandomLevel();

                        return;
                    }
                }
                else if (FlxG.keys.justPressed(Keys.F7) && FlxG.debug == true)
                {
                    goToRandomLevel();

                    return;
                }
                else if (FlxG.keys.justPressed(Keys.F8) && FlxG.debug == true)
                {
                    goToRandomLevel();
                    return;
                }
            }
            #endregion

            #region pirateVersion

            if (Lemonade_Globals.PAID_VERSION == Lemonade_Globals.PIRATE_MODE)
            {
                if (elapsedInState > 3.0 && FlxG.level > 2)
                {
                    foreach (var item in actors.members)
                    {
                        ((FlxPlatformActor)(item)).maxVelocity.X += 2.5f;
                    }

                    foreach (var mov in movingPlatforms.members)
                    {
                        mov.pathSpeed += 0.25f;
                    }
                }
            }

            #endregion

            //FlxU.collideRamp(actors, ramps);

            if (timer.time < 0.0f )
            {


                if (Lemonade_Globals.totalTimeAvailable < 0.9f)
                    FlxG.state = new DeathState();
                else
                    goToRandomLevel();
            }


            if (Lemonade_Globals.game_version == 2)
            {
                FlxU.collide(collidableTilemap, actors);
                FlxU.collide(crateParticle, collidableTilemap);
                FlxU.collide(levelItems, collidableTilemap);
                FlxU.collide(smallCrates, collidableTilemap);
            }
            else
            {
                FlxU.collide(collidableTileblocks, actors);
                FlxU.collide(crateParticle, collidableTileblocks);
                FlxU.collide(levelItems, collidableTileblocks);
                FlxU.collide(smallCrates, collidableTileblocks);
            }

            FlxU.overlap(actors, actors, genericOverlap);
            FlxU.overlap(actors, trampolines, trampolinesOverlap);
            FlxU.overlap(actors, levelItems, genericOverlap);
            FlxU.overlap(actors, hazards, genericOverlap);
            FlxU.overlap(actors, smallCrates, genericOverlap);
            FlxU.overlap(smallCrates, trampolines, trampolinesOverlap);

            FlxU.collide(actors, movingPlatforms);
            FlxU.collide(smallCrates, levelItems);

            FlxU.collideOnY(smallCrates, andre);
            FlxU.collideOnY(smallCrates, liselot);

            bool andreExit = FlxU.overlap(andre, exit, exitOverlap);
            bool liselotExit = FlxU.overlap(liselot, exit, exitOverlap);

            if (andreExit && liselotExit)
            {
                levelComplete = true;
            }

            FlxU.collide(actors, levelItems);

            foreach (FlxObject crate in levelItems.members)
            {
                if (crate.GetType().ToString() == "Lemonade.LargeCrate")
                {
                    if (((LargeCrate)(crate)).canExplode && !crate.dead)
                    {
                        crateParticle.at(crate);
                        crateParticle.start(true, 0.0f, 50);
                        crate.kill();

                        FlxG.play("Lemonade/sfx/cw_sound35", 0.5f, false);
                    }
                }
            }

            base.update();



            // Console.WriteLine("The current follow target is : {0}  SCROLL {1} {2}", FlxG.followTarget.GetType().ToString() ,FlxG.scroll.X, FlxG.scroll.Y);

            // Switch Controlling Character.
            if (FlxG.keys.justPressed(Keys.V) || FlxG.gamepads.isNewButtonPress(Buttons.B))
            {
                //Console.WriteLine("The current follow target is : {0} ", FlxG.followTarget.GetType().ToString());

                //Console.WriteLine("The current follow target is : {0} ", FlxG.followTarget.GetType().ToString());

                if (andre.piggyBacking || andre.dead || liselot.dead)
                {
                    // do nothing.

                }
                else
                {

                    FlxG.play("Lemonade/sfx/cw_sound36", 0.5f, false);

                    if (follow.currentFollow == 1)
                    {
                        follow.tweenX = new Tweener(andre.x, liselot.x, TimeSpan.FromSeconds(0.45f), Linear.EaseNone);
                        follow.tweenY = new Tweener(andre.y, liselot.y, TimeSpan.FromSeconds(0.45f), Linear.EaseNone);

                        follow.tweenX.Start();
                        follow.tweenY.Start();
                        
                        follow.currentFollow = 2;

                        bubbleParticle.at(liselot);
                        bubbleParticle.start(true, 0, 30);


                    }
                    else if (follow.currentFollow == 2)
                    {
                        follow.tweenX = new Tweener(liselot.x, andre.x, TimeSpan.FromSeconds(0.45f), Linear.EaseNone);
                        follow.tweenY = new Tweener(liselot.y, andre.y, TimeSpan.FromSeconds(0.45f), Linear.EaseNone);

                        follow.tweenX.Start();
                        follow.tweenY.Start();

                        follow.currentFollow = 1;

                        bubbleParticle.at(andre);
                        bubbleParticle.start(true, 0, 30);

                    } 
                }
            }

#if __ANDROID__
			if (FlxG.pauseAction == "Exit")
			{
#if __ANDROID__
				FlxG.state = new OuyaEasyMenuState();
#endif
#if !__ANDROID__
				FlxG.state = new EasyMenuState();
#endif
			}
#endif
            if (FlxG.keys.justPressed(Keys.Escape) || FlxG.gamepads.isButtonDown(Buttons.Back))
            {
				#if __ANDROID__
				FlxG.state = new OuyaEasyMenuState();
				#endif
				#if !__ANDROID__
				FlxG.state = new IntroState();
				#endif
            }
            if (levelComplete == true)
            {
                //andre.alpha -= 0.1f;
                //liselot.alpha -= 0.1f;
            }
            if (levelComplete == true && ! FlxG.transition.hasStarted)
            {
                if (transitionPause == 0.0f)
                {
                    FlxG.play("initials/initials_empire_tagtone4", 0.8f, false);
                }
                
                FlxG.pauseMp3();

                andre.control = FlxPlatformActor.Controls.none;
                liselot.control = FlxPlatformActor.Controls.none;

                andre.visible = false;
                liselot.visible = false;
                transitionPause += FlxG.elapsed;

                if (transitionPause > 0.85f)
                {

                    FlxG.transition.startFadeOut(0.05f, -90, 150);
                }
            }
            if (FlxG.transition.complete)
            {
                if (FlxG.level != 12)
                {
                    //FlxG.level++;

                    goToRandomLevel();
                    
                    Lemonade_Globals.restartMusic = false;

					if (Lemonade_Globals.PAID_VERSION == Lemonade_Globals.DEMO_MODE && FlxG.level >= 3) {
						#if __ANDROID__
						FlxG.state = new OuyaEasyMenuState();
						#endif
						#if !__ANDROID__
						FlxG.state = new EasyMenuState();
						#endif
						FlxG.transition.resetAndStop();
						return;
					} else {

                        Lemonade_Globals.timeLeft = timer.time;
						
						FlxG.transition.resetAndStop ();
                        
                        goToRandomLevel();

                        return;
					}
                }
                if (FlxG.level == 12 && Lemonade_Globals.game_version == 2)
                {
                    FlxG.state = new VictoryState();
                    FlxG.transition.resetAndStop();
                    return;
                }
                else if (FlxG.level == 12 && Lemonade_Globals.game_version == 1)
                {
					#if __ANDROID__
					FlxG.state = new OuyaEasyMenuState();
					#endif
					#if !__ANDROID__
					FlxG.state = new EasyMenuState();
					#endif
                    FlxG.transition.resetAndStop();
                    return;
                }
            }
        }

        public void goToRandomLevel()
        {
            Lemonade_Globals.restartMusic = false;

            Lemonade_Globals.totalTimeAvailable -= 2;

            Lemonade_Globals.timeLeft = Lemonade_Globals.totalTimeAvailable;

            foreach (FlxSprite item in actors.members)
            {
                Lemonade_Globals.stateSaver[Lemonade_Globals.location][item.ToString()] = new Vector2(item.x, item.y);

                
                //Lemonade_Globals.stateSaver[Lemonade_Globals.location].Add(item.ToString(), new Vector2(item.x, item.y));
            }
            int count = 0;
            foreach (FlxSprite item in hazards.members)
            {
                //Lemonade_Globals.stateSaver[Lemonade_Globals.location].Add(item.ToString() + count.ToString(), new Vector2(item.x, item.y));
                Lemonade_Globals.stateSaver[Lemonade_Globals.location][item.ToString() + count.ToString()] = new Vector2(item.x, item.y);


                count++;
            }

            foreach (var item in Lemonade_Globals.stateSaver[Lemonade_Globals.location])
            {
                Console.WriteLine(item.Key + " " + item.Value);

            }
            



            int loc = (int)FlxU.random(0, 6);
            if (loc == 0)
            {
                Lemonade_Globals.location = "sydney";
                //FlxG._game.switchState(Lemonade_Globals.STATE_SYDNEY_STATE);
            }
            else if (loc == 1)
            {
                Lemonade_Globals.location = "newyork";
                //FlxG.state = Lemonade_Globals.STATE_NEWYORK_STATE;
                //FlxG._game.switchState(Lemonade_Globals.STATE_NEWYORK_STATE);
            }
            else if (loc == 2)
            {
                Lemonade_Globals.location = "military";
                //FlxG.state = Lemonade_Globals.STATE_MILITARY_STATE;
                //FlxG._game.switchState(Lemonade_Globals.STATE_MILITARY_STATE);
            }
            else if (loc == 3)
            {
                Lemonade_Globals.location = "warehouse";
                //FlxG.state = Lemonade_Globals.STATE_WAREHOUSE_STATE;
                //FlxG._game.switchState(Lemonade_Globals.STATE_WAREHOUSE_STATE);
            }
            else if (loc == 4)
            {
                Lemonade_Globals.location = "factory";
                //FlxG.state = Lemonade_Globals.STATE_FACTORY_STATE;
                //FlxG._game.switchState(Lemonade_Globals.STATE_FACTORY_STATE);
            }
            else
            {
                Lemonade_Globals.location = "management";
                //FlxG.state = Lemonade_Globals.STATE_MANAGEMENT_STATE;
                //FlxG._game.switchState(Lemonade_Globals.STATE_MANAGEMENT_STATE);
            }
            Console.WriteLine("Location: {0} {1}", Lemonade_Globals.location, loc);



            FlxG.state = new PlayState();

            return;
        }

        protected bool exitOverlap(object Sender, FlxSpriteCollisionEvent e)
        {
            ((Exit)(e.Object2)).play("open", true);
	        return true;
        }

        protected bool rampOverlap(object Sender, FlxSpriteCollisionEvent e)
        {
            e.Object1.overlapped(e.Object2);
            //e.Object2.overlapped(e.Object1);
            return true;
        }

        protected bool genericOverlap(object Sender, FlxSpriteCollisionEvent e)
        {
            e.Object1.overlapped(e.Object2);
            e.Object2.overlapped(e.Object1);
            return true;
        }

        protected bool trampolinesOverlap(object Sender, FlxSpriteCollisionEvent e)
        {
            if (e.Object1.dead == false)
            {
                bubbleParticle.at(e.Object1);
                bubbleParticle.start(true, 0, 30);
            }
            e.Object1.overlapped(e.Object2);
            e.Object2.overlapped(e.Object1);
            return true;
        }


        

    }
}
