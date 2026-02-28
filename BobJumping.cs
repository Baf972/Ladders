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

            MyBob.Energy -= 1f * DeltaTime;
            JumpingTimer -= DeltaTime;
                       

            if (MyBob.CurrentFrame < MyBob.FrameCount - 1)
            {
                //MyBob.Y -= MyBob.SpeedJumpUp * DeltaTime;
                MapDraw.CameraY += MyBob.SpeedJumpUp * DeltaTime;

                foreach(Assets gift in MyAssetsManager.Gifts)
                    gift.AssetY += MyBob.SpeedJumpUp * DeltaTime;

                foreach (Assets fruit in MyAssetsManager.Fruits)
                    fruit.AssetY += MyBob.SpeedJumpUp * DeltaTime;

                foreach (Assets rock in MyAssetsManager.Rocks)                
                    rock.AssetY += MyBob.SpeedJumpUp * DeltaTime;

                foreach (Assets endurance in MyAssetsManager.Endurance)
                    endurance.AssetY += MyBob.SpeedJumpUp * DeltaTime;

            }

            else if (MyBob.CurrentFrame >= MyBob.FrameCount - 1)
            {
                
                MyBob.SpeedFallDown += MyBob.FallVelocity * DeltaTime;
                MapDraw.CameraY -= MyBob.SpeedFallDown * DeltaTime;

                foreach (Assets gift in MyAssetsManager.Gifts)
                    gift.AssetY -= MyBob.SpeedFallDown * DeltaTime;

                foreach (Assets fruit in MyAssetsManager.Fruits)
                    fruit.AssetY -= MyBob.SpeedFallDown * DeltaTime;

                foreach (Assets rock in MyAssetsManager.Rocks)
                    rock.AssetY -= MyBob.SpeedFallDown * DeltaTime;

                foreach (Assets endurance in MyAssetsManager.Endurance)
                    endurance.AssetY -= MyBob.SpeedFallDown * DeltaTime;

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
            if (CollidLadder)
            {


                float AbsolutBobY = MyBob.Y + Math.Abs(MapDraw.CameraY);
                int tileCol = (int)(MyBob.X / MapRead.TileWidth);
                int tileLig = (int)(AbsolutBobY / MapRead.TileWidth);

                int woodSoundId = MyMapRead.GetTileId(tileCol - 1, tileLig - 1, "Ladders");
                if (woodSoundId == 1312 && !LandingWoodTimer)
                    LandingWoodTimer = true;

                if (LandingWoodTimer)
                {
                    PlaySound(LandingWoodSound);
                    LandingWoodTimer = false;

                }
                int metalSoundId = MyMapRead.GetTileId(tileCol - 1, tileLig - 1, "Ladders");
                if (metalSoundId == 1311 && !LandingMetalTimer)
                    LandingMetalTimer = true;

                if (LandingMetalTimer)
                {
                    PlaySound(LandingMetalSound);
                    LandingMetalTimer = false;

                }
            }
            if (MapDraw.CameraY <= -2120  || JumpingTimer <= 0)
            {
                MyBob.StatesTransition(BobStates.Falling);
                JumpingTimer = 2f;
            }


            base.Update(MyBob);
        }
        public  override void Draw(Bob MyBob)
        {
            if (!MyBob.IsFlipped)
            {
                if (MyBob.IsHurt)
                    DrawTexturePro(MyBob.TileSet, MyBob.BobSourceRec, new Rectangle(MyBob.X, MyBob.Y, MyBob.FrameWidth, MyBob.FrameHeight), new Vector2(MyBob.FrameWidth, MyBob.FrameHeight), MyBob.R, Color.Red);
                else
                    DrawTexturePro(MyBob.TileSet, MyBob.BobSourceRec, new Rectangle(MyBob.X, MyBob.Y, MyBob.FrameWidth, MyBob.FrameHeight), new Vector2(MyBob.FrameWidth, MyBob.FrameHeight), MyBob.R, Color.White);
            }    
            else
            {
                if (MyBob.IsHurt)
                    DrawTexturePro(MyBob.TileSet, MyBob.BobSourceRec, new Rectangle(MyBob.X + MyBob.FrameWidth / 2, MyBob.Y, MyBob.FrameWidth, MyBob.FrameHeight), new Vector2(MyBob.FrameWidth, MyBob.FrameHeight), MyBob.R, Color.Red);
                else
                    DrawTexturePro(MyBob.TileSet, MyBob.BobSourceRec, new Rectangle(MyBob.X + MyBob.FrameWidth / 2, MyBob.Y, MyBob.FrameWidth, MyBob.FrameHeight), new Vector2(MyBob.FrameWidth, MyBob.FrameHeight), MyBob.R, Color.White);
            }
               


            
            //DrawText("JTimer  " + JumpingTimer.ToString(), 40, 40, 30, Color.White);
            base.Draw(MyBob);
        }
    }
}
