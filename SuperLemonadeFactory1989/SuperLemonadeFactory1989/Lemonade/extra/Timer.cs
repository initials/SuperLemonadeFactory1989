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
    class Timer : FlxText
    {
        public float time = 0.0f;
        public Tweener tween;

        public Timer(float X, float Y, float Width)
            : base(X, Y, Width)
        {
            color = Lemonade_Globals.GAMEBOY_COLOR_1;

            alignment = FlxJustification.Left;
            scale = 2;

            tween = new Tweener(0, 2, 0.4f, Bounce.EaseIn);
        }

        override public void update()
        {
            //text = String.Format("{0:#,###.#}", time);

            scale = tween.Position;

            //Console.WriteLine("Tween {0} {1}", scale, tween.Position);

            if ((int)time % 3 == 0)
            {
                //Console.WriteLine("Starting Tween");

                tween = new Tweener(4, 2, 0.5f, Bounce.EaseOut);
                tween.Start();
            }

            tween.Update(FlxG.elapsedAsGameTime);

            base.update();

            time -= FlxG.elapsed;

        }


    }
}
