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
            if (IsKeyReleased(KeyboardKey.Space))
                MyBob.StatesTransition(BobStates.Idle);

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
            base.Update(MyBob);
        }
        public override void Draw(Bob MyBob)
        {
            base.Draw(MyBob);
        }
    }
}
