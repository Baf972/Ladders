using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LADDERS
{
    
    public abstract class BobStatesManager
    {
        public bool Isflipped;
        public int CurrentFrame;
        public int FrameCount;
        public bool StopClimbing;

        public BobStatesManager(Bob MyBob) 
        {
           
        }

        public virtual void HandleInput(Bob MyBob)
        {
        }
        public virtual void Update(Bob MyBob)
        {
        }

        public virtual void Draw(Bob MyBob)
        {

        }
    };


   

}
