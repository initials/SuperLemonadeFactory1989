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

        }

        override public void update()
        {
            base.update();
        }

        public override void overlapped(FlxObject obj)
        {
            base.overlapped(obj);
        }
    }
}
