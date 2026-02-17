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
        MapRead MyMapRead { get; set; }
        MapDraw MyMapDraw { get; set; }
        public Texture2D text;

        public Level1()
        {
            MyMapRead = new MapRead();
            MyMapRead.LoadDatas("assets/LadderMap001.json");
            MyMapDraw = new MapDraw(MyMapRead, "assets/");
            text = LoadTexture("assets/Tileset.png");
        }

        public void Update()
        {
            MyMapDraw.Update();
        }

        public void Draw()
        {
            MyMapDraw.Draw();
            DrawTexture(text, 100, 100, Color.White);
        }

        public void Close()
        {
            MyMapRead.Close();
        }


    }
}
