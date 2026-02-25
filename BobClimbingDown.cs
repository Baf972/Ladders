using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Raylib_cs;
using static Raylib_cs.Raylib;

namespace LADDERS
{
    public class BobClimbingDown : BobStatesManager
    {
        public BobClimbingDown (Bob MyBob) : base(MyBob)
        {
        }
        public override void HandleInput(Bob MyBob)
        {

            if (IsKeyDown(KeyboardKey.Up))
            {
                MyBob.StatesTransition(BobStates.Climbing);
            }
            if (IsKeyDown(KeyboardKey.Down))
            {
                if (MapDraw.CameraY < -2110)
                    MyBob.Y += 50 * GetFrameTime();
                    

                if (MyBob.Y < 480)
                {
                    MyBob.SpeedClimb = 50 * GetFrameTime();
                    MapDraw.CameraY -= MyBob.SpeedClimb;

                    foreach (Assets gift in MyAssetsManager.Gifts)
                        gift.AssetY -= MyBob.SpeedClimb;

                    foreach (Assets fruit in MyAssetsManager.Fruits)
                        fruit.AssetY -= MyBob.SpeedClimb;

                    foreach (Assets rock in MyAssetsManager.Rocks)
                        rock.AssetY -= MyBob.SpeedClimb;
                }

                if (MapDraw.CameraY > - 1000)
                {
                    foreach (Assets cloud in MyAssetsManager.Clouds)
                        cloud.AssetY -= MyBob.SpeedClimb;
                }
                    
            }
            if (IsKeyReleased(KeyboardKey.Down))
            {                
                MyBob.StatesTransition(BobStates.Idle);
                MyBob.SpeedClimb = 0f;
                MyBob.CurrentFrame = 0;
            }
            if (IsKeyReleased(KeyboardKey.Up))
            {              
                MyBob.StatesTransition(BobStates.Idle);
                MyBob.SpeedClimb = 0f;
                MyBob.CurrentFrame = 0;
            }

            if (IsKeyDown(KeyboardKey.Left))
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
            AnimReverse = true;

            float AbsolutBobY = MyBob.Y + Math.Abs(MapDraw.CameraY);
            int tileCol = (int)(MyBob.X / MapRead.TileWidth);
            int tileLig = (int)(AbsolutBobY / MapRead.TileWidth);

            // Si Pas d'échelle au dessous, Bob s'arrête
            int NoTileId = MyMapRead.GetTileId(tileCol - 1, tileLig, "Ladders");
            if (NoTileId == 0)
                MyBob.StatesTransition(BobStates.Idle);            


            base.Update(MyBob);
        }
        public override void Draw(Bob MyBob)
        {
            DrawTexturePro(MyBob.TileSet, MyBob.BobSourceRec, new Rectangle(MyBob.X, MyBob.Y, MyBob.FrameWidth, MyBob.FrameHeight), new Vector2(MyBob.FrameWidth, MyBob.FrameHeight), MyBob.R, Color.White);
            base.Draw(MyBob);
        }
    }
}
