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
   
    public class BobLanding : BobStatesManager
    {
        public BobLanding(Bob MyBob) : base(MyBob)
        {
        }
        public override void HandleInput(Bob MyBob)
        {
            if (MyBob.CurrentFrame >= MyBob.FrameCount - 2)
            {
                MyBob.StatesTransition(BobStates.Idle);
                MyBob.SpeedFallDown = 200f;
                MyBob.SpeedJumpUp = 40f;
                MyBob.SpeedJumpLenght = 200f;
            }

            
            base.HandleInput(MyBob);
        }
        public override void Update(Bob MyBob)
        {
            base.Update(MyBob);
        }
        public override void Draw(Bob MyBob)
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
                    DrawTexturePro(MyBob.TileSet, MyBob.BobSourceRec, new Rectangle(MyBob.X, MyBob.Y, MyBob.FrameWidth, MyBob.FrameHeight), new Vector2(MyBob.FrameWidth - 40, MyBob.FrameHeight), MyBob.R, Color.Red);
                else
                    DrawTexturePro(MyBob.TileSet, MyBob.BobSourceRec, new Rectangle(MyBob.X, MyBob.Y, MyBob.FrameWidth, MyBob.FrameHeight), new Vector2(MyBob.FrameWidth - 40, MyBob.FrameHeight), MyBob.R, Color.White);
            }

            base.Draw(MyBob);
        }
    }
    
}
