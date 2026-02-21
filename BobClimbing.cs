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

            if (IsKeyDown(KeyboardKey.Space))
            {
                MyBob.SpeedUp = 50 * GetFrameTime();
                MapDraw.CameraY += MyBob.SpeedUp;
            }
            if (IsKeyDown(KeyboardKey.Down))
            {
                MyBob.SpeedUp = 40 * GetFrameTime();
                MapDraw.CameraY += MyBob.SpeedUp;
                MapDraw.BackGroundPos.Y += MyBob.SpeedUp / 3;
            }
            if (IsKeyReleased(KeyboardKey.Space))
            {
                MyBob.SpeedUp = 0f;
                MyBob.StatesTransition(BobStates.Idle);
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
                LadderPart1.PartRotation = new Random();
                LadderPart1.AssetR = LadderPart1.PartRotation.Next(-50, +50);
                LadderPart1.PartSpeed = new Random();
                LadderPart1.AssetSpeed = LadderPart1.PartSpeed.Next(120, 180);

                Assets LadderPart2 = new Assets("Rung2", MyAssetsManager.MyTexturesManager.GetTexture("assets/Rung2.png"), (int)MyBob.X - 30, (int)MyBob.Y, 0, 0, 1, 1, false, false, false);
                LadderPart2.PartRotation = new Random();
                LadderPart2.AssetR = LadderPart1.PartRotation.Next(-50, +50);
                LadderPart2.PartSpeed = new Random();
                LadderPart2.AssetSpeed = LadderPart1.PartSpeed.Next(120, 180);

                Assets LadderPart3 = new Assets("Rung3", MyAssetsManager.MyTexturesManager.GetTexture("assets/Rung3.png"), (int)MyBob.X + 15, (int)MyBob.Y, 0, 0, 1, 1, false, false, false);
                LadderPart3.PartRotation = new Random();
                LadderPart3.AssetR = LadderPart1.PartRotation.Next(-50, +50);
                LadderPart3.PartSpeed = new Random();
                LadderPart3.AssetSpeed = LadderPart1.PartSpeed.Next(120, 180);

                Assets LadderPart4 = new Assets("Rung4", MyAssetsManager.MyTexturesManager.GetTexture("assets/Rung4.png"), (int)MyBob.X - 25, (int)MyBob.Y, 0, 0, 1, 1, false, false, false);
                LadderPart4.PartRotation = new Random();
                LadderPart4.AssetR = LadderPart1.PartRotation.Next(-50, +50);
                LadderPart4.PartSpeed = new Random();
                LadderPart4.AssetSpeed = LadderPart1.PartSpeed.Next(120, 180);

                Assets LadderPart5 = new Assets("Stack1", MyAssetsManager.MyTexturesManager.GetTexture("assets/Stack1.png"), (int)MyBob.X + 20, (int)MyBob.Y, 0, 0, 1, 1, false, false, false);
                LadderPart5.PartRotation = new Random();
                LadderPart5.AssetR = LadderPart1.PartRotation.Next(-50, +50);
                LadderPart5.PartSpeed = new Random();
                LadderPart5.AssetSpeed = LadderPart1.PartSpeed.Next(120, 180);

                Assets LadderPart6 = new Assets("Stack2", MyAssetsManager.MyTexturesManager.GetTexture("assets/Stack2.png"), (int)MyBob.X - 35, (int)MyBob.Y, 0, 0, 1, 1, false, false, false);
                LadderPart6.PartRotation = new Random();
                LadderPart6.AssetR = LadderPart1.PartRotation.Next(-50, +50);
                LadderPart6.PartSpeed = new Random();
                LadderPart6.AssetSpeed = LadderPart1.PartSpeed.Next(120, 180);

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
