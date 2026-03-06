using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Raylib_cs;
using static Raylib_cs.Raylib;

namespace LADDERS
{
    
    public abstract class BobStatesManager
    {
        protected MapRead MyMapRead;
        protected AssetsManager MyAssetsManager;
        protected Sound JumpSound;
        protected Sound ClimbWoodSound;
        protected Sound ClimbMetalSound;
        protected Sound LandingMetalSound;
        protected Sound LandingWoodSound;
        protected Sound FallingSound;
        protected Sound OufSound;
        protected Sound BreakingWoodSound;
        protected Sound BreakingWoodSound2;
        public bool Isflipped;
        public int CurrentFrame;
        public int FrameCount;
        protected int TileId;
        protected Rectangle TileRecLadder;
        protected Rectangle TileRecEmpty;
        public bool StopClimbing;
        protected bool BreakLoop;
        protected bool CollidLadder;
        protected bool NoTile;
        protected bool BreakLadder;
        protected bool AnimReverse;
        protected bool IsIdle;
        protected float DeltaTime;        
        protected float OldX;
        protected float OldY;
        protected int ScreenWidth = GetScreenWidth();
        protected int ScreenHeight = GetScreenHeight();
        protected float LadderTimer;
        protected float FallingTimer;
        protected float JumpingTimer; // Durée du Saut
        protected float LadderPartsTimer;

        protected float ClimbWoodTimer;
        protected float ClimbMetalTimer;
        protected bool LandingWoodTimer;
        protected bool LandingMetalTimer;
        protected bool JumpTimer;
        protected bool FallingSoundTimer;
        protected bool OufTimer;
        protected float BreakingWoodSoundTimer;



        public BobStatesManager(Bob MyBob) 
        {
            MyAssetsManager = AssetsManager.Instance;
            MyMapRead = new MapRead();
            DeltaTime = GetFrameTime();
            StopClimbing = false;
            BreakLoop = false;
            LadderTimer = 2f;
            FallingTimer = 2f;
            LadderPartsTimer = 0.1f;
            JumpingTimer = 1.5f;
            JumpSound = LoadSound("assets/sounds/JumpSound.wav");
            FallingSound = LoadSound("assets/sounds/FallingSound.wav");
            ClimbWoodSound = LoadSound("assets/sounds/ClimbWood.wav");
            ClimbWoodTimer = 0.5f;
            ClimbMetalSound = LoadSound("assets/sounds/ClimbMetal.wav");
            ClimbMetalTimer = 0.5f;
            LandingMetalSound = LoadSound("assets/sounds/LandingWood.wav");
            LandingWoodSound = LoadSound("assets/sounds/LandingMetal.wav");
            OufSound = LoadSound("assets/sounds/OufSound.wav");
            BreakingWoodSound = LoadSound("assets/sounds/BreakingWoodSound.wav");
            BreakingWoodSound2 = LoadSound("assets/sounds/BreakingWoodSound2.wav");
            JumpTimer = true;
            FallingSoundTimer = true;
            OufTimer = true;
            BreakingWoodSoundTimer = 1f;

        }

        public virtual void HandleInput(Bob MyBob)
        {
        }
        public virtual void Update(Bob MyBob)
        {


            /* // Id TileMap & Collisions
             int NbCol = MapRead.Width;
             int NbLig = MapRead.Height;

             for (int Lig = 0; Lig < NbLig; Lig++)
             {
                 for (int Col = 0; Col < NbCol; Col++)
                 {
                     TileId = MyMapRead.GetTileId(Lig, Col, "Ladders");
                     MyBob.BobRec = new Rectangle(MyBob.X + 2, MyBob.Y - MyBob.FrameHeight, 5, MyBob.FrameHeight);
                     TileRec.Y += MapDraw.CameraY;

                     if (TileId != 0)
                     {
                         TileRec = new Rectangle((Col * MapRead.TileWidth) + 16, Lig * MapRead.TileWidth, 5, MapRead.TileWidth);
                         DrawRectangleLinesEx(TileRec, 2, Color.White);

                     }
                     if (TileId == 0)
                     {

                         DrawRectangleLinesEx(TileRec, 2, Color.White);

                     }

                 }
             }*/
           

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
            else if (AnimReverse)
            {
                MyBob.FrameTimer -= 118 * GetFrameTime();
                if (MyBob.FrameTimer <= 0)
                {
                    MyBob.CurrentFrame--;
                    if (MyBob.CurrentFrame <= 0)
                    {
                        MyBob.CurrentFrame = MyBob.FrameCount;
                    }
                    MyBob.FrameTimer = MyBob.NewFrameTimer;
                }
            }
            else if (IsIdle)
            {
                MyBob.FrameTimer -= 118 * GetFrameTime();
                if (MyBob.FrameTimer <= 0)
                {
                    MyBob.CurrentFrame++;
                    if (MyBob.CurrentFrame >= MyBob.FrameCount)
                    {
                        MyBob.CurrentFrame = 2;
                    }
                    MyBob.FrameTimer = MyBob.NewFrameTimer;
                }
            }
            else
            {
                MyBob.FrameTimer -= 145 * GetFrameTime();
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

            //DrawText("ListGifts  " + MyAssetsManager.Gifts.Count.ToString(), 40, 40, 30, Color.White);
            //MyBob.BobRec = new Rectangle(MyBob.X + 2, MyBob.Y - MyBob.FrameHeight +10, 5, 20);

            //DrawRectangleLinesEx(MyBob.BobRec, 2, Color.White);

            //DrawText("X : " + MyBob.X.ToString(), 10, 10, 20, Color.White);
            //DrawText("Camera  "  + MapDraw.CameraY.ToString(), 10, 30, 20, Color.White);
            //DrawText("Respawn 1  "  + MapDraw.CameraYRespawn1.ToString(), 10, 50, 20, Color.White);
            //DrawText("Respawn 2  " + MapDraw.CameraYRespawn2.ToString(), 10, 70, 20, Color.White);

        }
    };


   

}
