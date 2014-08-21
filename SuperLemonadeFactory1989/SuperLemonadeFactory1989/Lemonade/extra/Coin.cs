using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using org.flixel;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace Lemonade
{
    class Coin : FlxSprite
    {
        private FlxEmitter fanfare;
        private XNATweener.Tweener tween;

        public Coin(int xPos, int yPos, bool Outline)
            : base(xPos, yPos)
        {
            if (Outline)
                loadGraphic("Lemonade/GoldCoinGameboyWithOutline", true, false, 32, 32);
            else
                loadGraphic("Lemonade/GoldCoinGameboy", true, false, 32, 32);

            addAnimation("animation", new int[] { 0,1,2,3,4,5,6,7,8,4 }, (int)FlxU.random(4,8), true);
            play("animation");

            width = 16;
            height = 16;
            setOffset(8, 8);

            fanfare = new FlxEmitter();
            fanfare.createSprites("Lemonade/bubble",8,true,0.0f,0.0f);
            fanfare.setXSpeed(-100, 100);
            fanfare.setYSpeed(-100, 100);
            fanfare.gravity = 5;
            fanfare.delay = 3.0f;

            tween = new XNATweener.Tweener(-25, 25, FlxU.random(0.8f,1.2f), XNATweener.Quadratic.EaseInOut);
            tween.PingPong = true;
            tween.Start();


        }

        override public void update()
        {
            velocity.Y = tween.Position;

            tween.Update(FlxG.elapsedAsGameTime);

            fanfare.update();
            base.update();
        }

        public override void render(SpriteBatch spriteBatch)
        {
            fanfare.render(spriteBatch);
            base.render(spriteBatch);
        }

        public override void overlapped(FlxObject obj)
        {
            base.overlapped(obj);
        }

        public override void kill()
        {
            fanfare.at(this);
            fanfare.start(true,1.0f,20);

            x = -100;
            y = -100;

            this.dead = true;

            //base.kill();
        }
    }
}
