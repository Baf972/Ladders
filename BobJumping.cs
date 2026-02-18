using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LADDERS
{
    public class BobJumping : BobStatesManager
    {

        public BobJumping(Bob MyBob) : base(MyBob)
        {
        }
        public override void HandleInput(Bob MyBob)
        {
            base.HandleInput(MyBob);
        }
        public override void Update(Bob MyBob)
        {

            BreakLoop = true;

            // Velocity & Bob.Y
            MyBob.SpeedJumpUp += (MyBob.Velocity * 1.2f) * DeltaTime;
            MyBob.SpeedFallDown += MyBob.Velocity * DeltaTime;            

            if (MyBob.CurrentFrame < MyBob.FrameCount - 1)
                MyBob.Y -= MyBob.SpeedJumpUp * DeltaTime;

            if (MyBob.CurrentFrame >= MyBob.FrameCount - 1)
                MyBob.Y += MyBob.SpeedFallDown * DeltaTime;

            // Velocyti & Bob.X 
            MyBob.SpeedJumpLenght += MyBob.Velocity * DeltaTime;

            if (MyBob.IsFlipped)
                MyBob.X += MyBob.SpeedJumpLenght * DeltaTime;
            else
                MyBob.X -= MyBob.SpeedJumpLenght * DeltaTime;



            base.Update(MyBob);
        }
        public  override void Draw(Bob MyBob)
        {
            base.Draw(MyBob);
        }
    }
}
