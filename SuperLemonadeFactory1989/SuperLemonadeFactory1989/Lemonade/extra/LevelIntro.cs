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


        Tweener t1;
        Tweener t2;
        Tweener t3;
        Tweener t4;

        Tweener textTween1;
        Tweener textTween2;

        // --

        FlxSprite b1;
        FlxSprite b2;
        FlxSprite b3;
        FlxSprite b4;

        FlxText bT1;
        FlxText bT2;

        // --

        float timer;



        public LevelIntro()
        {
            b1 = new FlxSprite(0, 0);
            b1.createGraphic(FlxG.width, FlxG.height/2 , Lemonade_Globals.GAMEBOY_COLOR_2);
            b1.setScrollFactors(0, 0);
            //add(b1);

            b2 = new FlxSprite(0, FlxG.height / 2);
            b2.createGraphic(FlxG.width, FlxG.height/2 , Lemonade_Globals.GAMEBOY_COLOR_2);
            b2.setScrollFactors(0, 0);
            //add(b2);

            bT1 = new FlxText(0, FlxG.height / 1.5f, FlxG.width);
            bT1.setFormat(null, 3, Lemonade_Globals.GAMEBOY_COLOR_1, FlxJustification.Center, Lemonade_Globals.GAMEBOY_COLOR_3);

            bT1.text = "SYDNEY";
            add(bT1);

            bT2 = new FlxText(0, FlxG.height / 2.5f, FlxG.width);
            bT2.setFormat(null, 3, Lemonade_Globals.GAMEBOY_COLOR_1, FlxJustification.Center, Lemonade_Globals.GAMEBOY_COLOR_3);

            bT2.text = "MR.\nAMSTERDAAM";
            add(bT2);

            b3 = new FlxSprite(0, 0);
            b3.createGraphic(FlxG.width/2, FlxG.height, Lemonade_Globals.GAMEBOY_COLOR_3);
            b3.setScrollFactors(0, 0);
            //add(b3);

            b4 = new FlxSprite(FlxG.width / 2, 0);
            b4.createGraphic(FlxG.width/2, FlxG.height, Lemonade_Globals.GAMEBOY_COLOR_3);
            b4.setScrollFactors(0, 0);
            //add(b4);

            t1 = new Tweener(0, 1000, 3.0f, XNATweener.Exponential.EaseInOut);
            t2 = new Tweener(0, 1000, 3.2f, XNATweener.Exponential.EaseInOut);
            t3 = new Tweener(0, 1000, 3.4f, XNATweener.Exponential.EaseInOut);
            t4 = new Tweener(0, 1000, 3.6f, XNATweener.Exponential.EaseInOut);




            textTween1 = new Tweener(FlxG.height / 1.5f, -1000, 4.0f, XNATweener.Bounce.EaseOut);
            textTween2 = new Tweener(FlxG.height / 2.5f, 1000, 4.5f, XNATweener.Bounce.EaseOut);

            timer = 0.0f;

        }

        override public void update()
        {

            if (timer > 0.25f)
            {
                t1.Update(FlxG.elapsedAsGameTime);
                t2.Update(FlxG.elapsedAsGameTime);
            }
            if (timer > 0.15f)
            {
                t3.Update(FlxG.elapsedAsGameTime);
                t4.Update(FlxG.elapsedAsGameTime);
            }
            if (timer > 1.25f)
            {
                textTween1.Update(FlxG.elapsedAsGameTime);
                textTween2.Update(FlxG.elapsedAsGameTime);
            }

            b1.x = t1.Position;
            b2.x = t2.Position * -1;
            b3.y = t3.Position;
            b4.y = t4.Position * -1;

            bT1.y = textTween1.Position;
            bT2.y = textTween2.Position;

            timer += FlxG.elapsed;

            base.update();

        }


    }
}
