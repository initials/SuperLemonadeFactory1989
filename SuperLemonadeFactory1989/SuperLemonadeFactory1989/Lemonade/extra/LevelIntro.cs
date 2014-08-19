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
    class LevelIntro : FlxGroup
    {

        Tweener textTween1;
        Tweener textTween2;

        // --

        FlxSprite b1;
        FlxSprite b2;
        FlxSprite b3;
        FlxSprite b4;

        public FlxText bT1;
        public FlxText bT2;

        // --

        float timer;



        public LevelIntro()
        {
            b1 = new FlxSprite(0, 0);
            b1.createGraphic(FlxG.width, FlxG.height , Lemonade_Globals.GAMEBOY_COLOR_4);
            b1.setScrollFactors(0, 0);
            add(b1);

            b2 = new FlxSprite(0, 0);
            b2.createGraphic(FlxG.width, FlxG.height, Lemonade_Globals.GAMEBOY_COLOR_3);
            b2.setScrollFactors(0, 0);
            add(b2);

            b3 = new FlxSprite(0, 0);
            b3.createGraphic(FlxG.width , FlxG.height, Lemonade_Globals.GAMEBOY_COLOR_2);
            b3.setScrollFactors(0, 0);
            add(b3);

            b4 = new FlxSprite(0, 0);
            b4.createGraphic(FlxG.width , FlxG.height, Lemonade_Globals.GAMEBOY_COLOR_1);
            b4.setScrollFactors(0, 0);
            add(b4);


            bT1 = new FlxText(0, FlxG.height / 4, FlxG.width);
            bT1.setFormat(null, 3, Lemonade_Globals.GAMEBOY_COLOR_1, FlxJustification.Center, Lemonade_Globals.GAMEBOY_COLOR_3);

            bT1.text = "SYDNEY";
            add(bT1);

            bT2 = new FlxText(0, (FlxG.height / 4 )* 3, FlxG.width);
            bT2.setFormat(null, 2, Lemonade_Globals.GAMEBOY_COLOR_1, FlxJustification.Center, Lemonade_Globals.GAMEBOY_COLOR_3);

            bT2.text = "MR. AMSTERDAAM";
            add(bT2);


            textTween1 = new Tweener(FlxG.height / 4, -1000, 4.0f, XNATweener.Bounce.EaseOut);
            textTween2 = new Tweener((FlxG.height / 4) * 3, 1000, 4.5f, XNATweener.Bounce.EaseOut);

            timer = 0.0f;

        }

        override public void update()
        {

            if (timer > 0.3f)
            {
                b4.visible = false;
            }
            if (timer > 0.9f)
            {
                b3.visible = false;
            }
            if (timer > 1.4f)
            {
                b2.visible = false;
                b1.velocity.Y = -500;
            }
            if (timer > 1.9f)
            {
                //b1.visible = false;
            }

            if (timer > 2.0f)
            {
                textTween1.Update(FlxG.elapsedAsGameTime);
                textTween2.Update(FlxG.elapsedAsGameTime);
                bT1.y = textTween1.Position;
                bT2.y = textTween2.Position;

            }





            timer += FlxG.elapsed;

            base.update();

        }


    }
}
