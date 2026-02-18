using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LADDERS
{
   
    public class BobLanding : BobStatesManager
    {
        public BobLanding(Bob MyBob) : base(MyBob)
        {
        }
        public override void HandleInput(Bob MyBob)
        {
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
