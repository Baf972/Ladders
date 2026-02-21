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
    

    public class BobRunningUp : BobStatesManager
    {
        public BobRunningUp(Bob MyBob) : base(MyBob)
        {
        }
        public override void HandleInput(Bob MyBob)
        {
            if (MyBob.CurrentFrame  >= MyBob.FrameCount - 1)
            {
                MyBob.StatesTransition(BobStates.Jumping);
            }
            base.HandleInput(MyBob);
        }
        public override void Update(Bob MyBob)
        {
            MyBob.SpeedUp = 0f;
            base.Update(MyBob);
        }
        public override void Draw(Bob MyBob)
        {
            if (!MyBob.IsFlipped)
                DrawTexturePro(MyBob.TileSet, MyBob.BobSourceRec, new Rectangle(MyBob.X, MyBob.Y, MyBob.FrameWidth, MyBob.FrameHeight), new Vector2(MyBob.FrameWidth / 2, MyBob.FrameHeight), MyBob.R, Color.White);
            else
                DrawTexturePro(MyBob.TileSet, MyBob.BobSourceRec, new Rectangle(MyBob.X, MyBob.Y, MyBob.FrameWidth, MyBob.FrameHeight), new Vector2(MyBob.FrameWidth, MyBob.FrameHeight), MyBob.R, Color.White);

            base.Draw(MyBob);
        }
    }
    
}
