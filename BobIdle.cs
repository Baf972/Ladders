using System;
using System.Collections.Generic;
using System.Linq;
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

            if (IsKeyDown(KeyboardKey.Space))
            {
                MyBob.StatesTransition(BobStates.Climbing);
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
            
            

            float AbsolutBobY = MyBob.Y + Math.Abs(MapDraw.CameraY);

            int tileCol = (int)(MyBob.X / MapRead.TileWidth);
            int tileLig = (int)(AbsolutBobY / MapRead.TileWidth);

            // Supprime Tile echelle en bois
            int ModifiableTileId = MyMapRead.GetTileId(tileCol - 1, tileLig - 1, "Ladders");
            if (ModifiableTileId != 9 && ModifiableTileId != 6 && ModifiableTileId != 3 && ModifiableTileId != 0)
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

            //DrawText("ID  " + ModifiableTileId.ToString(), 30, 50, 30, Color.White);
            base.Update(MyBob);
        }
        public override void Draw(Bob MyBob)
        {
            base.Draw(MyBob);
        }
    }
}
