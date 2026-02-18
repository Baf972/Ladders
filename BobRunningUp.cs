using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            base.Draw(MyBob);
        }
    }
    
}
