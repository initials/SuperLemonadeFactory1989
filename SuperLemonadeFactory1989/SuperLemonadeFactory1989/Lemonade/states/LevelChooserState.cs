using System;
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
    public class LevelChooserState : FlxState
    {
        private FlxGroup icons;
        private FlxTileblock block;
        private List<Vector3Tweener> tweeners;

        private int selected;

        private float timer;

        private FlxText t1;
        private FlxText t2;


        override public void create()
        {
            base.create();

            FlxG.playMp3("Lemonade/music/Coffee", 1.0f);

            block = new FlxTileblock(0, 0, FlxG.width + 20, FlxG.height + 20);
            block.auto = FlxTileblock.FRAMENUMBER;
            block.frameNumber = 6;
            block.setScrollFactors(0, 0);
            block.loadTiles(FlxG.Content.Load<Texture2D>("Lemonade/fade"), 20, 20, 0);

            add(block);

            icons = new FlxGroup();
            tweeners = new List<Vector3Tweener>();

            for (int i = 0; i < 6; i++)
            {
                FlxSprite p1 = new FlxSprite(0 + (i * 36), 12);
                p1.loadGraphic("Lemonade/illustration/people", true, false, 302, 640);
                p1.frame = i;
                icons.add(p1);

                tweeners.Add(new Vector3Tweener(new Vector3(-100 + (i * 36), -290, 0.1f), new Vector3(12, 100 , 1), 0.45f, Bounce.EaseOut));

                

            }

            foreach (var item in tweeners)
            {
                item.Pause();
            }

            add(icons);

            selected = 0;

            tweeners[selected].Play();

            timer = 5.0f;


            t1 = new FlxText(0, FlxG.height - 72, FlxG.width);
            t1.setFormat(null, 2, Lemonade_Globals.GAMEBOY_COLOR_1, FlxJustification.Center, Lemonade_Globals.GAMEBOY_COLOR_4);
            add(t1);

            t2 = new FlxText(0, 3, FlxG.width);
            t2.setFormat(null, 2, Lemonade_Globals.GAMEBOY_COLOR_1, FlxJustification.Center, Lemonade_Globals.GAMEBOY_COLOR_4);
            add(t2);
        }

        override public void update()
        {
            timer -= FlxG.elapsed;

            block.frameNumber = (int)FlxG.elapsedTotal;
            block.loadTiles(FlxG.Content.Load<Texture2D>("Lemonade/fade"), 20, 20, 0);

            if (FlxControl.LEFTJUSTPRESSED)
            {
                selected--;

                tweeners[selected + 1].Reset();
                tweeners[selected + 1].Pause();

                if (selected < 0) selected = 5;

                tweeners[selected].Play();

            }
            if (FlxControl.RIGHTJUSTPRESSED)
            {
                selected++;

                tweeners[selected - 1].Reset();
                tweeners[selected - 1].Pause();

                if (selected >= 6) selected = 0;

                tweeners[selected].Play();

            }

            int count = 0;
            foreach (FlxSprite item in icons.members)
            {
                item.x = tweeners[count].Position.X;
                item.y = tweeners[count].Position.Y;
                item.scale = tweeners[count].Position.Z;

                tweeners[count].Update(FlxG.elapsedAsGameTime);


                count++;
            }

            switch (selected)
            {
                case 0:
                    Lemonade_Globals.location = "warehouse";
                    break;
                case 1:
                    Lemonade_Globals.location = "military";
                    break;
                case 2:
                    Lemonade_Globals.location = "newyork";
                    break;
                case 3:
                    Lemonade_Globals.location = "sydney";
                    break;
                case 4:
                    Lemonade_Globals.location = "management";
                    break;
                case 5:
                    Lemonade_Globals.location = "factory";
                    break;

                default:
                    break;
            }

            t1.text = Lemonade_Globals.niceLocationNames[Lemonade_Globals.location].ToString();
            //t2.text = String.Format("{0:#,###.#}", timer);

            if (FlxControl.ACTIONJUSTPRESSED)
            {
                FlxG.state = new PlayState();
                return;
            }

            base.update();
        }


    }
}
