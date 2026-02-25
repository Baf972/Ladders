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
    public class BobIdle : BobStatesManager
    {

        public BobIdle (Bob MyBob) : base(MyBob)
        {
        }
        public override void HandleInput(Bob MyBob)
        {

            if (IsKeyDown(KeyboardKey.Up))
            {
                MyBob.StatesTransition(BobStates.Climbing);
                IsIdle = false;
            }

            if (IsKeyDown(KeyboardKey.Down))
            {
                MyBob.StatesTransition(BobStates.ClimbingDown);
                IsIdle = false;

            }

            if (IsKeyDown(KeyboardKey.Left))
            {
                MyBob.StatesTransition(BobStates.RunningUp);
                MyBob.IsFlipped = false;
                IsIdle = false;

            }
            else if (IsKeyDown(KeyboardKey.Right))
            {
                MyBob.StatesTransition(BobStates.RunningUp);
                MyBob.IsFlipped = true;
                IsIdle = false;
            }

            base.HandleInput(MyBob);
        }
        public override void Update(Bob MyBob)
        {
            IsIdle = true;


            float AbsolutBobY = MyBob.Y + Math.Abs(MapDraw.CameraY);

            int tileCol = (int)(MyBob.X / MapRead.TileWidth);
            int tileLig = (int)(AbsolutBobY / MapRead.TileWidth);
            
            // Supprime Tile echelle en bois
            int ModifiableTileId = MyMapRead.GetTileId(tileCol - 1, tileLig - 1, "Ladders");
            if (ModifiableTileId != 1312 && ModifiableTileId != 0)
            {
                LadderTimer -= DeltaTime;
                if (LadderTimer <= 0)
                {
                    MyMapRead.ModifyTile(tileCol - 1, tileLig - 1, "Ladders", 0);
                    LadderTimer = 2;
                }
                
            }
             
            // Si plus d'échelle, Bob tombe
            int DeleteTileId = MyMapRead.GetTileId(tileCol - 1, tileLig - 1, "Ladders");
            if (DeleteTileId == 0)
                MyBob.StatesTransition(BobStates.Falling);

           
            
               
            base.Update(MyBob);
        }
        public override void Draw(Bob MyBob)
        {
            if (MyBob.IsHurt)
                DrawTexturePro(MyBob.TileSet, MyBob.BobSourceRec, new Rectangle(MyBob.X, MyBob.Y, MyBob.FrameWidth, MyBob.FrameHeight), new Vector2(MyBob.FrameWidth, MyBob.FrameHeight), MyBob.R, Color.Red);
            else
                DrawTexturePro(MyBob.TileSet, MyBob.BobSourceRec, new Rectangle(MyBob.X, MyBob.Y, MyBob.FrameWidth, MyBob.FrameHeight), new Vector2(MyBob.FrameWidth, MyBob.FrameHeight), MyBob.R, Color.White);

            base.Draw(MyBob);
        }
    }
}
