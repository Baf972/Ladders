using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Raylib_cs;
using static Raylib_cs.Raylib;

namespace LADDERS
{
    public class Level1
    {
        Bob MyBob { get; set; }
        MapRead MyMapRead { get; set; }
        MapDraw MyMapDraw { get; set; }
        
        AssetsManager MyAssetsManager { get; set; }
        public Level1()
        {
            MyMapRead = new MapRead();
            MyMapRead.LoadDatas("assets/LadderMapLV1.json");
            MyMapDraw = new MapDraw(MyMapRead, "assets/");
            MyAssetsManager = AssetsManager.Instance;
            MyBob = Bob.Instance;
        }

        public void Update()
        {
            MyMapDraw.Update();
            MyBob.HandleInput();
            MyBob.Update();
            MyMapDraw.Update();
            

        }

        public void Draw()
        {
            MyMapDraw.Draw();
            MyBob.Draw();
        }

        public void Close()
        {
            MyMapRead.Close();
        }


    }
}
