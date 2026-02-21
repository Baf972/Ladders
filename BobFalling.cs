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
    public class BobFalling : BobStatesManager
    {
        public BobFalling(Bob MyBob) : base(MyBob)
        {
        }
        public override void HandleInput(Bob MyBob)
        {
            base.HandleInput(MyBob);
        }
        public override void Update(Bob MyBob)
        {
            // Bob Tombe, camera suit
            MapDraw.CameraY -= MyBob.SpeedFallDown * DeltaTime;
            MapDraw.BackGroundPos.Y -= (MyBob.SpeedFallDown / 3) * DeltaTime ;

            float AbsolutBobY = MyBob.Y + Math.Abs(MapDraw.CameraY);
            int tileCol = (int)(MyBob.X / MapRead.TileWidth);
            int tileLig = (int)(AbsolutBobY / MapRead.TileWidth);

            // Si Bob tombe sur une échelle en métal, il s'arrête
            int MetalTileId = MyMapRead.GetTileId(tileCol - 1, tileLig -1, "Ladders");           
            if (MetalTileId == 1312)
            {
                MyBob.StatesTransition(BobStates.Landing);
                MyBob.Y += 32;
            }

            // Si Bob rencontre une échelle en bois, elle se casse
            if (MetalTileId != 1312 )
                MyMapRead.ModifyTile(tileCol - 1, tileLig -1, "Ladders", 0);
            

            base.Update(MyBob);
        }
        public override void Draw(Bob MyBob)
        {
            DrawTexturePro(MyBob.TileSet, MyBob.BobSourceRec, new Rectangle(MyBob.X, MyBob.Y, MyBob.FrameWidth, MyBob.FrameHeight), new Vector2(MyBob.FrameWidth, MyBob.FrameHeight), MyBob.R, Color.White);

            base.Draw(MyBob);
        }
    }
}
