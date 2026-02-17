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
        public void Update(Bob MyBob)
        {
            base.Update(MyBob);
        }
        public void Draw(Bob MyBob)
        {
            base.Draw(MyBob);
        }
    }
}
