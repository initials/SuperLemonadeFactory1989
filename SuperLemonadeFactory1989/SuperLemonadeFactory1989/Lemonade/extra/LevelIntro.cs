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
        Tweener textTween3;
        Tweener textTween4;
        // --

        public FlxSprite b1;
        public FlxSprite b2;
        public FlxSprite b3;
        public FlxSprite b4;

        public FlxTileblock block;

        public FlxText bT1;
        public FlxText bT2;

        // --

        float timer;
        int frames = 0;


        public LevelIntro()
        {
            //b1 = new FlxSprite(0, 0);
            //b1.createGraphic(FlxG.width, FlxG.height , Lemonade_Globals.GAMEBOY_COLOR_4);
            //b1.setScrollFactors(0, 0);
            //add(b1);

            //b2 = new FlxSprite(0, 0);
            //b2.createGraphic(FlxG.width, FlxG.height, Lemonade_Globals.GAMEBOY_COLOR_3);
            //b2.setScrollFactors(0, 0);
            //add(b2);

            //b3 = new FlxSprite(0, 0);
            //b3.createGraphic(FlxG.width , FlxG.height, Lemonade_Globals.GAMEBOY_COLOR_2);
            //b3.setScrollFactors(0, 0);
            //add(b3);

            //b4 = new FlxSprite(0, 0);
            //b4.createGraphic(FlxG.width , FlxG.height, Lemonade_Globals.GAMEBOY_COLOR_1);
            //b4.setScrollFactors(0, 0);
            //add(b4);

            block = new FlxTileblock(0, 0, FlxG.width + 20, FlxG.height + 20);
            block.auto = FlxTileblock.FRAMENUMBER;
            block.frameNumber = 0;
            block.setScrollFactors(0, 0);
            block.loadTiles(FlxG.Content.Load<Texture2D>("Lemonade/fade"), 20, 20, 0);
            
            add(block);


            bT1 = new FlxText(0, FlxG.height / 4, FlxG.width);
            bT1.setFormat(null, 3, Lemonade_Globals.GAMEBOY_COLOR_1, FlxJustification.Center, Lemonade_Globals.GAMEBOY_COLOR_3);

            bT1.text = Lemonade_Globals.niceLocationNames[Lemonade_Globals.location];
            add(bT1);

            bT2 = new FlxText(0, (FlxG.height / 4 )* 3, FlxG.width);
            bT2.setFormat(null, 2, Lemonade_Globals.GAMEBOY_COLOR_1, FlxJustification.Center, Lemonade_Globals.GAMEBOY_COLOR_3);

            bT2.text = Lemonade_Globals.niceActorNames[Lemonade_Globals.location];
            add(bT2);


            textTween1 = new Tweener(FlxG.height / 4, -95, 1.20f, XNATweener.Elastic.EaseInOut);
            textTween2 = new Tweener((FlxG.height / 4) * 3, FlxG.height + 95, 1.5f, XNATweener.Elastic.EaseInOut);
            textTween3 = new Tweener(-20, FlxG.height / 4, 0.7f, XNATweener.Quintic.EaseOut);
            textTween4 = new Tweener(800, (FlxG.height / 4) * 3, 1.2f, XNATweener.Quintic.EaseOut);

            timer = 0.0f;

        }

        override public void update()
        {
            if (frames % 4 == 1)
            {
                block.frameNumber ++;
                block.loadTiles(FlxG.Content.Load<Texture2D>("Lemonade/fade"), 20, 20, 0);

                //Console.WriteLine("Frames == {0}", frames);

                if (block.frameNumber > 20)
                {
                    block.visible = false;
                }
            }
            if (timer > 0.1f)
            {
                //block.frameNumber = 1;
                //block.loadTiles(FlxG.Content.Load<Texture2D>("Lemonade/fade"), 20, 20, 0);
            }
            if (timer > 0.2f)
            {
                //block.frameNumber = 2;
                //block.loadTiles(FlxG.Content.Load<Texture2D>("Lemonade/fade"), 20, 20, 0);
            }
            if (timer > 0.7f)
            {
                //b2.visible = false;
                //b1.velocity.Y = -500;
            }
            if (timer > 1.9f)
            {
                //b1.visible = false;
            }

            if (timer > 0.85f)
            {
                textTween1.Update(FlxG.elapsedAsGameTime);
                textTween2.Update(FlxG.elapsedAsGameTime);
                bT1.y = textTween1.Position;
                bT2.y = textTween2.Position;

            }
            else 
            {
                textTween3.Update(FlxG.elapsedAsGameTime);
                textTween4.Update(FlxG.elapsedAsGameTime);
                bT1.y = textTween3.Position;
                bT2.y = textTween4.Position;

            }




            timer += FlxG.elapsed;
            frames++;
            base.update();

        }


    }
}
