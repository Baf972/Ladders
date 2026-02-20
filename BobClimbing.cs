using System;
using System.Collections.Generic;
using System.Linq;
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
                MyBob.SpeedUp = 30 * GetFrameTime();
                MapDraw.CameraY += MyBob.SpeedUp;
            }
            if (IsKeyDown(KeyboardKey.Down))
            {
                MyBob.SpeedUp = 40 * GetFrameTime();
                MapDraw.CameraY += MyBob.SpeedUp;
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
            if (ModifiableLadderId != 9 && ModifiableLadderId != 6 && ModifiableLadderId != 3 && ModifiableLadderId != 0)
                MyMapRead.ModifyTile(tileCol - 1, tileLig +1, "Ladders", 0);

            base.Update(MyBob);
        }
        public override void Draw(Bob MyBob)
        {
            
            base.Draw(MyBob);
        }
    }
}
