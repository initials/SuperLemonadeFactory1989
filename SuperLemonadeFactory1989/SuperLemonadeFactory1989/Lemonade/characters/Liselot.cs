﻿
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
    public class Liselot : Actor
    {
        public FlxObject parent;

        public Liselot(int xPos, int yPos)
            : base(xPos, yPos)
        {
            loadGraphic(FlxG.Content.Load<Texture2D>("Lemonade/chars_50x80"), true, false, 50, 80);

            addAnimation("piggyback_run", new int [] {72,73,74,75,76,77} ,12, true);
            addAnimation("piggyback_idle", new int [] {78} , 0 );
            addAnimation("piggyback_jump", new int [] {76,77,76} ,4, true);
            addAnimation("piggyback_dash", new int [] {80} ,0);
        
            addAnimation("run", new int [] {12,13,14,15,16,17} ,16);
            addAnimation("run_push_crate", new int [] {69,70,71,81,82,83} ,16, true);
            addAnimation("idle", new int [] {2} ,0);
            addAnimation("talk", new int [] {2,55} ,12);
            addAnimation("jump", new int [] {2, 15,16,17} ,4 , true);
            addAnimation("death", new int [] {64,64,65,65,66,66,67,67} ,12 , false);

            play("idle");
            runSpeed = 50;

            width = 10;
            height = 41;
            setOffset(20, 39);
            setDrags(1251, 0);

            maxVelocity.X = 310;
            maxVelocity.Y = 2830;

			#if __ANDROID__
			maxVelocity.X/=1.75f;
			maxVelocity.Y/=1.75f;
			#endif

            setJumpValues(-340.0f, -410.0f, 0.35f, 0.075f);
            numberOfJumps = 2;

            _runningMax = maxVelocity.X;

            parent = null;

        }

        override public void update()
        {
            //if (control == Controls.none) color = Lemonade_Globals.GAMEBOY_COLOR_4;
            //else color = Color.White;

            if (piggyBacking)
            {
                visible = false;
            }
            else
            {
                visible = true;
            }

            if (parent != null)
            {
                x = parent.x;
                y = parent.y;

                
                if (parent.dead == true)
                {
                    piggyBacking = false;
                    parent = null;
                }

                if (FlxG.keys.justPressed(Keys.B) || FlxG.gamepads.isNewButtonPress(Buttons.Y))
                {
                    if (((FlxSprite)(parent)).facing == Flx2DFacing.Right)
                    {
                        facing = Flx2DFacing.Right;
                        y -= 20;
                        x -= 20;
                        velocity.Y = -300;
                        velocity.X = -150;
                    }
                    else
                    {
                        facing = Flx2DFacing.Left;
                        y -= 20;
                        x += 20;
                        velocity.Y = -300;
                        velocity.X = 150;
                    }

                    piggyBacking = false;
                    parent = null;


                }

            }


            base.update();
        }

       

        override public void overlapped(FlxObject obj)
        {
            base.overlapped(obj);

            string overlappedWith = obj.GetType().ToString();

            if ((overlappedWith == "Lemonade.Army" ||
                overlappedWith == "Lemonade.Inspector" ||
                overlappedWith == "Lemonade.Chef" ||
                overlappedWith == "Lemonade.Worker") && !flickering() )
            {
                if (obj.dead == false && control == Controls.player)
                {
                    //if (dead == false) FlxG.play("Lemonade/sfx/deathSFX", 0.8f, false);
                    //flicker(2);
                    //kill();
                }
            }
            else if (overlappedWith == "Lemonade.Trampoline")
            {
                velocity.Y = -1000;
                trampolineTimer = 0.0f;
            }
            else if (overlappedWith == "Lemonade.FilingCabinet")
            {
                originalPosition.X = obj.x;
                originalPosition.Y = obj.y;

            }

        }

        public override void hurt(float Damage)
        {
            if (piggyBacking)
            {

            }
            else
            {
                base.hurt(Damage);
            }
        }

        public override void kill()
        {
            control = Controls.none;
            dead = true;
            //base.kill();
        }

    }
}
