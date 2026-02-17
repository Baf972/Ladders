using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Raylib_cs;
using static Raylib_cs.Raylib;

namespace LADDERS
{
    public abstract class GameScenes
    {
        public string Name = "";
        public virtual void Update()
        {

        }
        public virtual void Draw()
        {

        }
        public virtual void Show()
        {

        }
        public virtual void Hide()
        {

        }
        public virtual void Close()
        {

        }
    }
}
