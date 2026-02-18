using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Raylib_cs;
using static Raylib_cs.Raylib;

namespace LADDERS
{
    
    public abstract class BobStatesManager
    {
        public bool Isflipped;
        public int CurrentFrame;
        public int FrameCount;
        public bool StopClimbing;
        protected bool BreakLoop;
        protected float DeltaTime;

        public BobStatesManager(Bob MyBob) 
        {
             DeltaTime = GetFrameTime();
             StopClimbing = false;
             BreakLoop = false;
        }

        public virtual void HandleInput(Bob MyBob)
        {
        }
        public virtual void Update(Bob MyBob)
        {

            if (BreakLoop)
            {
                MyBob.FrameTimer -= 118 * GetFrameTime();
                if (MyBob.FrameTimer <= 0)
                {
                    MyBob.CurrentFrame++;
                    if (MyBob.CurrentFrame >= MyBob.FrameCount - 1)
                    {
                        MyBob.CurrentFrame = MyBob.FrameCount - 1;
                    }
                    MyBob.FrameTimer = MyBob.NewFrameTimer;
                }
            }
            else
            {
                MyBob.FrameTimer -= 118 * GetFrameTime();
                if (MyBob.FrameTimer <= 0)
                {
                    MyBob.CurrentFrame++;
                    if (MyBob.CurrentFrame >= MyBob.FrameCount)
                    {
                        MyBob.CurrentFrame = 0;
                    }
                    MyBob.FrameTimer = MyBob.NewFrameTimer;
                }
                
            }
            if (MyBob.IsFlipped)
            {
                MyBob.BobSourceRec = new Rectangle(MyBob.CurrentFrame * MyBob.FrameWidth, 0, -MyBob.FrameWidth, MyBob.FrameHeight);
            }
            else
            {
                MyBob.BobSourceRec = new Rectangle(MyBob.CurrentFrame * MyBob.FrameWidth, 0, MyBob.FrameWidth, MyBob.FrameHeight);
            }

        }

        public virtual void Draw(Bob MyBob)
        {
            MyBob.BobState = MyBob.GetCurrentState();

            switch (MyBob.BobState)
            {
                case BobStates.Idle:

                    DrawTexturePro(MyBob.TileSet, MyBob.BobSourceRec, new Rectangle(MyBob.X, MyBob.Y, MyBob.FrameWidth, MyBob.FrameHeight), new Vector2(MyBob.FrameWidth, MyBob.FrameHeight), MyBob.R, Color.White);
                    break;

                case BobStates.Climbing:

                    DrawTexturePro(MyBob.TileSet, MyBob.BobSourceRec, new Rectangle(MyBob.X, MyBob.Y, MyBob.FrameWidth, MyBob.FrameHeight), new Vector2(MyBob.FrameWidth, MyBob.FrameHeight), MyBob.R, Color.White);
                    break;

                case BobStates.Jumping:

                    if (!MyBob.IsFlipped)
                        DrawTexturePro(MyBob.TileSet, MyBob.BobSourceRec, new Rectangle(MyBob.X, MyBob.Y, MyBob.FrameWidth, MyBob.FrameHeight), new Vector2(MyBob.FrameWidth, MyBob.FrameHeight), MyBob.R, Color.White);

                    else
                        DrawTexturePro(MyBob.TileSet, MyBob.BobSourceRec, new Rectangle(MyBob.X + MyBob.FrameWidth / 2, MyBob.Y, MyBob.FrameWidth, MyBob.FrameHeight), new Vector2(MyBob.FrameWidth, MyBob.FrameHeight), MyBob.R, Color.White);

                    break;

                case BobStates.Falling:

                    DrawTexturePro(MyBob.TileSet, MyBob.BobSourceRec, new Rectangle(MyBob.X, MyBob.Y, MyBob.FrameWidth, MyBob.FrameHeight), new Vector2(MyBob.FrameWidth, MyBob.FrameHeight), MyBob.R, Color.White);
                    break;

                case BobStates.RunningUp:
                    if (!MyBob.IsFlipped)
                        DrawTexturePro(MyBob.TileSet, MyBob.BobSourceRec, new Rectangle(MyBob.X + MyBob.FrameWidth / 2, MyBob.Y, MyBob.FrameWidth, MyBob.FrameHeight), new Vector2(MyBob.FrameWidth, MyBob.FrameHeight), MyBob.R, Color.White);
                    else
                        DrawTexturePro(MyBob.TileSet, MyBob.BobSourceRec, new Rectangle(MyBob.X, MyBob.Y, MyBob.FrameWidth, MyBob.FrameHeight), new Vector2(MyBob.FrameWidth, MyBob.FrameHeight), MyBob.R, Color.White);
                    break;

                case BobStates.Landing:
                    DrawTexturePro(MyBob.TileSet, MyBob.BobSourceRec, new Rectangle(MyBob.X, MyBob.Y, MyBob.FrameWidth, MyBob.FrameHeight), new Vector2(MyBob.FrameWidth, MyBob.FrameHeight), MyBob.R, Color.White);
                    break;
            }
           

            DrawText(MyBob.BobState.ToString(), 10, 10, 20, Color.White);
        }
    };


   

}
