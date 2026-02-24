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
    public class BobClimbing : BobStatesManager
    {

        public BobClimbing(Bob MyBob) : base(MyBob)
        {
        }
        public override void HandleInput(Bob MyBob)
        {

            if (IsKeyDown(KeyboardKey.Up))
            {
                if (MyBob.Y > 480)
                    MyBob.Y -= 50 * GetFrameTime();

                if (MyBob.Y < 480)
                    {
                        MyBob.SpeedClimb = 50 * GetFrameTime();
                        MapDraw.CameraY += MyBob.SpeedClimb;

                    foreach (Assets gift in MyAssetsManager.Gifts)
                        gift.AssetY += MyBob.SpeedClimb; 

                    foreach (Assets fruit in MyAssetsManager.Fruits)
                        fruit.AssetY += MyBob.SpeedClimb;
                }

                if (MapDraw.CameraY > - 1000)
                {
                    foreach (Assets cloud in MyAssetsManager.Clouds)
                        cloud.AssetY += MyBob.SpeedClimb;
                }

            }
            if (IsKeyDown(KeyboardKey.Down))
            {
                MyBob.StatesTransition(BobStates.ClimbingDown);
                MyBob.SpeedClimb = 0f;
                MyBob.CurrentFrame = 0;
            }

            if (IsKeyReleased(KeyboardKey.Up))
            {
                MyBob.StatesTransition(BobStates.Idle);
                MyBob.SpeedClimb = 0f;
                MyBob.CurrentFrame = 0;
            }
            if (IsKeyReleased(KeyboardKey.Down))
            {
               
                MyBob.StatesTransition(BobStates.Idle);
                MyBob.SpeedClimb = 0f;
                MyBob.CurrentFrame = 0;
            }

            if (IsKeyDown(KeyboardKey.Left) )
            {
                MyBob.StatesTransition(BobStates.RunningUp);
                MyBob.IsFlipped = false;                
            }
            else if (IsKeyDown(KeyboardKey.Right))
            {
                MyBob.StatesTransition(BobStates.RunningUp);
                MyBob.IsFlipped = true;
            }



                base.HandleInput(MyBob);
        }
        public override void Update(Bob MyBob)
        {
                     

            float AbsolutBobY = MyBob.Y + Math.Abs(MapDraw.CameraY);
            int tileCol = (int)(MyBob.X / MapRead.TileWidth);
            int tileLig = (int)(AbsolutBobY / MapRead.TileWidth);
            
            // Si Pas d'échelle au dessus, Bob s'arrête
            int NoTileId = MyMapRead.GetTileId(tileCol - 1, tileLig - 2, "Ladders");  
            if (NoTileId == 0)                
                MyBob.StatesTransition(BobStates.Idle);

            //Si Bob est sur une échelle en bois, elle se casse
            int ModifiableLadderId = MyMapRead.GetTileId(tileCol - 1, tileLig + 1, "Ladders");
            if (ModifiableLadderId != 1312  && ModifiableLadderId != 0)
            {
                MyMapRead.ModifyTile(tileCol - 1, tileLig + 1, "Ladders", 0);
                BreakLadder = true;   
            }

            if (BreakLadder)
            {
                Assets LadderPart1 = new Assets("Rung1", MyAssetsManager.MyTexturesManager.GetTexture("assets/Rung1.png"), (int)MyBob.X + 20, (int)MyBob.Y, 0, 0, 1, 1, false, false, false);
                LadderPart1.AssetR = Assets.MyRandom.Next(-50, +50);
                LadderPart1.AssetSpeed = Assets.MyRandom.Next(120, 180);

                Assets LadderPart2 = new Assets("Rung2", MyAssetsManager.MyTexturesManager.GetTexture("assets/Rung2.png"), (int)MyBob.X - 30, (int)MyBob.Y, 0, 0, 1, 1, false, false, false);
                LadderPart2.AssetR = Assets.MyRandom.Next(-50, +50);
                LadderPart2.AssetSpeed = Assets.MyRandom.Next(120, 180);

                Assets LadderPart3 = new Assets("Rung3", MyAssetsManager.MyTexturesManager.GetTexture("assets/Rung3.png"), (int)MyBob.X + 15, (int)MyBob.Y, 0, 0, 1, 1, false, false, false);
                LadderPart3.AssetR = Assets.MyRandom.Next(-50, +50);
                LadderPart3.AssetSpeed = Assets.MyRandom.Next(120, 180);

                Assets LadderPart4 = new Assets("Rung4", MyAssetsManager.MyTexturesManager.GetTexture("assets/Rung4.png"), (int)MyBob.X - 25, (int)MyBob.Y, 0, 0, 1, 1, false, false, false);
                LadderPart4.AssetR = Assets.MyRandom.Next(-50, +50);
                LadderPart4.AssetSpeed = Assets.MyRandom.Next(120, 180);

                Assets LadderPart5 = new Assets("Stack1", MyAssetsManager.MyTexturesManager.GetTexture("assets/Stack1.png"), (int)MyBob.X + 20, (int)MyBob.Y, 0, 0, 1, 1, false, false, false);
                LadderPart5.AssetR = Assets.MyRandom.Next(-50, +50);
                LadderPart5.AssetSpeed = Assets.MyRandom.Next(120, 180);

                Assets LadderPart6 = new Assets("Stack2", MyAssetsManager.MyTexturesManager.GetTexture("assets/Stack2.png"), (int)MyBob.X - 35, (int)MyBob.Y, 0, 0, 1, 1, false, false, false);
                LadderPart6.AssetR = Assets.MyRandom.Next(-50, +50);
                LadderPart6.AssetSpeed = Assets.MyRandom.Next(120, 180);

                MyAssetsManager.LadderParts.Add(LadderPart1);
                MyAssetsManager.LadderParts.Add(LadderPart2);
                MyAssetsManager.LadderParts.Add(LadderPart3);
                MyAssetsManager.LadderParts.Add(LadderPart4);
                MyAssetsManager.LadderParts.Add(LadderPart5);
                MyAssetsManager.LadderParts.Add(LadderPart6);

                BreakLadder = false;
            }


            base.Update(MyBob);
        }
        public override void Draw(Bob MyBob)
        {
            DrawTexturePro(MyBob.TileSet, MyBob.BobSourceRec, new Rectangle(MyBob.X, MyBob.Y, MyBob.FrameWidth, MyBob.FrameHeight), new Vector2(MyBob.FrameWidth, MyBob.FrameHeight), MyBob.R, Color.White);           
            base.Draw(MyBob);
        }
    }
}
