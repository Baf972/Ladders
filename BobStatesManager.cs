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
        protected float DeltaTime;        
        protected float OldX;
        protected float OldY;
        protected int ScreenWidth = GetScreenWidth();
        protected int ScreenHeight = GetScreenHeight();
        protected float LadderTimer;
        protected float FallingTimer;


        public BobStatesManager(Bob MyBob) 
        {
            MyAssetsManager = AssetsManager.Instance;
            MyMapRead = new MapRead();
            DeltaTime = GetFrameTime();
            StopClimbing = false;
            BreakLoop = false;
            LadderTimer = 2;
            FallingTimer = 2;
            
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
           

            //MyBob.BobRec = new Rectangle(MyBob.X + 2, MyBob.Y - MyBob.FrameHeight +10, 5, 20);

            //DrawRectangleLinesEx(MyBob.BobRec, 2, Color.White);

            //DrawText("CollidLadder: " + CollidLadder.ToString(), 10, 40, 20, Color.White);
            //DrawText(MyBob.IsFlipped.ToString(), 10, 10, 20, Color.White);
        }
    };


   

}
