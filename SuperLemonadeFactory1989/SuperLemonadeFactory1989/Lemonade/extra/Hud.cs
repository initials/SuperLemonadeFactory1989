using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using org.flixel;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using XNATweener;

namespace Lemonade
{
    class Hud : FlxSprite
    {

        public Coin coin;
        public FlxText coinCounter;
        private XNATweener.Tweener tween;

        public FlxGroup powerBar;
        public float time = 0.0f;
        public bool canStart = false;


        private float coinsPerMinute;
        private float totalTime;

        public FlxSprite toastySprite;


        public Hud(int xPos, int yPos)
            : base(xPos, yPos)
        {
            loadGraphic(FlxG.Content.Load<Texture2D>("Lemonade/currentChar"), true, false, 14, 28);

            setScrollFactors(0, 0);

            addAnimation("andre", new int[] { 2 }, 0, true);
            //addAnimation("liselot", new int[] { 1 }, 0, true);

            play("andre");


            coin = new Coin(FlxG.width - 24, 2, true);
            coin.setScrollFactors(0, 0);
            //add(coin);

            coinCounter = new FlxText(FlxG.width - 36, 10, 100);
            coinCounter.setFormat(null, 1, Lemonade_Globals.GAMEBOY_COLOR_4, FlxJustification.Left, Lemonade_Globals.GAMEBOY_COLOR_1);
            coinCounter.alignment = FlxJustification.Left;
            coinCounter.setScrollFactors(0, 0);

            toastySprite = new FlxSprite(0, FlxG.height);

            string person = "Lemonade/illustration/andre_drawing";
            int ran = (int)FlxU.random(0, 4);
            switch (ran)
            {
                case 0:
                    person = "Lemonade/illustration/andre_drawing";
                    break;
                case 1:
                    person = "Lemonade/illustration/liselot_drawing";
                    break;
                case 2:
                    person = "Lemonade/illustration/army_drawing";
                    break;
                case 3:
                    person = "Lemonade/illustration/worker_drawing";
                    break;
                default:
                    person = "Lemonade/illustration/andre_drawing";
                    break;
            }
            
            toastySprite.loadGraphic(person, false, false, 302, 640);
            toastySprite.setScrollFactors(0, 0);

            tween = new Tweener(4, 12, 1, Quadratic.EaseInOut);
            tween.Start();
            tween.PingPong = true;

            powerBar = new FlxGroup();

            for (int i = 0; i < 30; i++)
            {
                FlxSprite bar = new FlxSprite(5 + (i * 10), FlxG.height - 10);
                bar.createGraphic(8, 8, Lemonade_Globals.GAMEBOY_COLOR_4);
                bar.setScrollFactors(0, 0);
                powerBar.add(bar);
            }

        }

        override public void update()
        {
            coinCounter.text = Lemonade_Globals.coins.ToString();

            tween.Update(FlxG.elapsedAsGameTime);

            coin.update();
            coinCounter.update();
            toastySprite.update();

            coin.y = tween.Position;
            //coinCounter.y = tween.Position;

            int count = 1;
            foreach (FlxSprite item in powerBar.members)
            {
                if (count > Lemonade_Globals.totalTimeAvailable)
                {
                    item.visible = false;
                }
                if (count > time)
                {
                    if (item.color == Lemonade_Globals.GAMEBOY_COLOR_1 && count < 4)
                    {
                        FlxG.play("Lemonade/sfx/cw_sound09");
                    }
                    else if (item.color == Lemonade_Globals.GAMEBOY_COLOR_1 )
                    {
                        //Console.WriteLine("Coins per minute {0}", coinsPerMinute);

                        if (coinsPerMinute > 2.5f)
                        {
                            //toastySprite.y = FlxG.height - 180;

                            toastySprite.velocity.Y = -200;

                            Lemonade_Globals.coinsThisLevel = 0;

                            FlxG.play("Lemonade/sfx/cw_sound12", 0.25f, false);
                            FlxG.play("Lemonade/sfx/cw_sound15", 0.44f, false);

                            FlxG.quake.start(0.005f, 0.5f);



                        }

                    }
                    item.color = Lemonade_Globals.GAMEBOY_COLOR_4;
                }

                else
                {

                    item.color = Lemonade_Globals.GAMEBOY_COLOR_1;
                }

                item.update();
                count++;
            }

            if (time <= 0.5f)
            {
                if (FlxU.random() < 0.1f)
                    FlxG.play("Lemonade/sfx/cw_sound09");
            }

            if (toastySprite.y < FlxG.height - 180)
            {
                toastySprite.velocity.Y *= -1;
            }

            base.update();

            if (canStart)
            {
                int mult = 1;

                if (FlxG.keys.F5) mult = 10;

                time -= FlxG.elapsed * mult;

                totalTime += FlxG.elapsed;

            }

            coinsPerMinute = Lemonade_Globals.coinsThisLevel / totalTime;


        }
        public override void render(SpriteBatch spriteBatch)
        {

            coin.render(spriteBatch);
            coinCounter.render(spriteBatch);
            powerBar.render(spriteBatch);
            toastySprite.render(spriteBatch);

            //foreach (FlxSprite item in powerBar.members)
            //{
            //    item.render(spriteBatch);
            //}

            base.render(spriteBatch);



        }

    }
}
