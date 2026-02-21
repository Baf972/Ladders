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
        AssetsDraw MyAssetsDraw { get; set; }
        AssetsManager MyAssetsManager { get; set; }
        public Level1()
        {
            MyMapRead = new MapRead();
            MyMapRead.LoadDatas("assets/LadderMapLV1.json");
            MyMapDraw = new MapDraw(MyMapRead, "assets/");
            MyAssetsManager = AssetsManager.Instance;
            MyAssetsDraw = AssetsDraw.Instance;
            MyBob = Bob.Instance;
        }

        public void Update()
        {
            MyMapDraw.Update();
            MyBob.HandleInput();
            MyBob.Update();
            MyAssetsDraw.Update();



        }

        public void Draw()
        {
            MyMapDraw.Draw();
            MyAssetsDraw.Draw();
            MyBob.Draw();
        }

        public void Close()
        {
            MyMapRead.Close();
        }


    }
}
