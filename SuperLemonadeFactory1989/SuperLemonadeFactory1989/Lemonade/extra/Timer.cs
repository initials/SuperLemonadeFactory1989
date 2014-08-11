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
    class Timer : FlxText
    {
        public float time = 0.0f;

        public Timer(float X, float Y, float Width)
            : base(X, Y, Width)
        {
            color = Lemonade_Globals.GAMEBOY_COLOR_1;

            alignment = FlxJustification.Left;
            scale = 2;
            
        }

        override public void update()
        {

            text = String.Format("{0:#,###.#}", time);


            base.update();

            time -= FlxG.elapsed;

        }


    }
}
