using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

using Raylib_cs;
using static Raylib_cs.Raylib;

namespace LADDERS
{
    public class Level1
    {
        public Bob MyBob { get; set; }
        public MapRead MyMapRead { get; set; }
        public MapDraw MyMapDraw { get; set; }
        public AssetsDraw MyAssetsDraw { get; set; }
        public AssetsManager MyAssetsManager { get; set; }
        public AssetsUpdate MyAssetsUpdate { get; set; }
        public Level1()
        {
            MyMapRead = new MapRead();
            MyMapRead.LoadDatas("assets/LadderMapLV1.json");
            MyMapDraw = new MapDraw(MyMapRead, "assets/");
            MyAssetsManager = AssetsManager.Instance;
            MyAssetsDraw = AssetsDraw.Instance;
            MyBob = Bob.Instance;
            MyAssetsUpdate = AssetsUpdate.Instance;


        }

        public void Update()
        {
            MyMapDraw.Update();
            MyBob.HandleInput();
            MyBob.Update();
            MyAssetsDraw.Update();
            MyAssetsUpdate.Update();
            if (IsKeyDown(KeyboardKey.G))
                MapDraw.CameraY += 30;




        }

        public void Draw()
        {
            MyMapDraw.Draw();
            MyAssetsDraw.Draw();
            MyAssetsUpdate.Draw();
            MyBob.Draw();

            // Clouds devant Bob
            foreach (Assets cloud in MyAssetsManager.Clouds)
                if (cloud.AssetName == "CloudFrontUp")
                    DrawTexturePro(cloud.AssetTileSet, cloud.AssetSourceRec, new Rectangle(cloud.AssetX, cloud.AssetY, cloud.AssetFrameWidth * 2, cloud.AssetFrameHeight * 2), new Vector2(cloud.AssetFrameWidth / 2, 0), 0, Color.White);
                else
                    DrawTexturePro(cloud.AssetTileSet, cloud.AssetSourceRec, new Rectangle(cloud.AssetX, cloud.AssetY, cloud.AssetFrameWidth, cloud.AssetFrameHeight), new Vector2(cloud.AssetFrameWidth / 2, 0), 0, Color.White);
        }

        public void Close()
        {
            MyMapRead.Close();
        }

        
    }
}
