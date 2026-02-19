using System;
using System.Collections.Generic;
using System.Linq;
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
            if (MyBob.CurrentFrame >= MyBob.FrameCount - 1)
            {
                MyBob.StatesTransition(BobStates.Idle);
                MyBob.SpeedFallDown = 200f;
                MyBob.SpeedJumpUp = 40f;
                MyBob.SpeedJumpLenght = 90;
            }
            
            base.HandleInput(MyBob);
        }
        public override void Update(Bob MyBob)
        {
            base.Update(MyBob);
        }
        public override void Draw(Bob MyBob)
        {
            base.Draw(MyBob);
        }
    }
    
}
