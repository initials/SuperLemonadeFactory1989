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


        public Hud(int xPos, int yPos)
            : base(xPos, yPos)
        {
            loadGraphic(FlxG.Content.Load<Texture2D>("Lemonade/currentChar"), true, false, 14, 28);

            setScrollFactors(0, 0);

            addAnimation("andre", new int[] { 0 }, 0, true);
            addAnimation("liselot", new int[] { 1 }, 0, true);

            play("andre");


            coin = new Coin(FlxG.width - 24, 2, true);
            coin.setScrollFactors(0, 0);
            //add(coin);

            coinCounter = new FlxText(FlxG.width - 36, 10, 100);
            coinCounter.setFormat(null, 1, Lemonade_Globals.GAMEBOY_COLOR_4, FlxJustification.Left, Lemonade_Globals.GAMEBOY_COLOR_1);
            coinCounter.alignment = FlxJustification.Left;
            coinCounter.setScrollFactors(0, 0);
            coinCounter.text = Lemonade_Globals.coins.ToString();

            tween = new Tweener(4, 12, 1, Quadratic.EaseInOut);
            tween.Start();
            tween.PingPong = true;

        }

        override public void update()
        {
            tween.Update(FlxG.elapsedAsGameTime);

            coin.update();
            coinCounter.update();

            coin.y = tween.Position;
            //coinCounter.y = tween.Position;


            base.update();

        }
        public override void render(SpriteBatch spriteBatch)
        {

            coin.render(spriteBatch);
            coinCounter.render(spriteBatch);

            base.render(spriteBatch);
        }

    }
}
