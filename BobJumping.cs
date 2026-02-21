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
    public class BobJumping : BobStatesManager
    {

        public BobJumping(Bob MyBob) : base(MyBob)
        {
        }
        public override void HandleInput(Bob MyBob)
        {
            if (CollidLadder)
            {
                MyBob.StatesTransition(BobStates.Landing);
                MyBob.SpeedFallDown = 0;
                MyBob.SpeedJumpUp = 0;
                MyBob.SpeedJumpLenght = 0;
            }

            base.HandleInput(MyBob);
        }
        public override void Update(Bob MyBob)
        {
            OldX = MyBob.X;
            OldY = MyBob.Y;
            //CollidLadder = false;
            BreakLoop = true;

            
            
                       

            if (MyBob.CurrentFrame < MyBob.FrameCount - 1)
            {
                //MyBob.Y -= MyBob.SpeedJumpUp * DeltaTime;
                MapDraw.CameraY += MyBob.SpeedJumpUp * DeltaTime;
                MapDraw.BackGroundPos.Y -= (MyBob.SpeedJumpUp / 3) * DeltaTime;
            }

            else if (MyBob.CurrentFrame >= MyBob.FrameCount - 1)
            {
                
                MyBob.SpeedFallDown += MyBob.FallVelocity * DeltaTime;
                MapDraw.CameraY -= MyBob.SpeedFallDown * DeltaTime;
                MapDraw.BackGroundPos.Y -= (MyBob.SpeedFallDown / 3) * DeltaTime;

            }

            // Velocyti & Bob.X 
            MyBob.SpeedJumpLenght -= MyBob.LenghtVelocity * DeltaTime;

            if (MyBob.IsFlipped)
                MyBob.X += MyBob.SpeedJumpLenght * DeltaTime;
            else
                MyBob.X -= MyBob.SpeedJumpLenght * DeltaTime;
           


            // Id TileMap & Collisions
            int NbCol = MapRead.Width;
            int NbLig = MapRead.Height;

            for (int Lig = 0; Lig < NbLig; Lig++)
            {
                for (int Col = 0; Col < NbCol; Col++)
                {
                    TileId = MyMapRead.GetTileId(Col, Lig, "Ladders");
                    TileRecLadder = new Rectangle((Col * MapRead.TileWidth) + 16, Lig * MapRead.TileWidth, 5, MapRead.TileWidth);
                    TileRecLadder.Y += MapDraw.CameraY;

                    if (MyBob.IsFlipped)
                    {
                        MyBob.BobRec = new Rectangle(MyBob.X +2, MyBob.Y - MyBob.FrameHeight +10, 5, 20);
                        if (CheckCollisionRecs(TileRecLadder, MyBob.BobRec) && MyBob.IsFlipped)
                        {
                            if (TileId != 0)
                            {
                                MyBob.X = (Col + 1) * MapRead.TileWidth;
                                CollidLadder = true;
                                break;
                            }
                        }
                    }
                    
                    if (!MyBob.IsFlipped)
                    {
                        MyBob.BobRec = new Rectangle(MyBob.X - (MyBob.FrameWidth -40), MyBob.Y - MyBob.FrameHeight +10, 5, 20);
                        if (CheckCollisionRecs(TileRecLadder, MyBob.BobRec) && !MyBob.IsFlipped)
                        {
                            if (TileId != 0)
                            {
                                MyBob.X = (Col + 1) * MapRead.TileWidth;
                                CollidLadder = true;
                                break;

                              
                            }
                        }
                    }                   


                }
            }
           

            base.Update(MyBob);
        }
        public  override void Draw(Bob MyBob)
        {
            if (!MyBob.IsFlipped)
                DrawTexturePro(MyBob.TileSet, MyBob.BobSourceRec, new Rectangle(MyBob.X, MyBob.Y, MyBob.FrameWidth, MyBob.FrameHeight), new Vector2(MyBob.FrameWidth, MyBob.FrameHeight), MyBob.R, Color.White);

            else
                DrawTexturePro(MyBob.TileSet, MyBob.BobSourceRec, new Rectangle(MyBob.X + MyBob.FrameWidth / 2, MyBob.Y, MyBob.FrameWidth, MyBob.FrameHeight), new Vector2(MyBob.FrameWidth, MyBob.FrameHeight), MyBob.R, Color.White);

            base.Draw(MyBob);
        }
    }
}
