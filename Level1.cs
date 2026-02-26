using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
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
        public static bool Respawn { get; set; }
        public static bool EndGame { get; set; }
        public Level1()
        {
            Init();
        }

        public void Init()
        {
            MyMapRead = new MapRead();
            MyMapRead.LoadDatas("assets/LadderMapLV1.json");
            MyMapDraw = new MapDraw(MyMapRead, "assets/");
            MyAssetsManager = AssetsManager.Instance;
            MyAssetsDraw = AssetsDraw.Instance;
            MyBob = Bob.Instance;
            MyAssetsUpdate = AssetsUpdate.Instance;
            Respawn = false;
            Assets Endurance = new Assets("Endurance", MyAssetsManager.MyTexturesManager.GetTexture("assets/Endurance.png"), 310, GetScreenHeight() - 32, 0, 0, 12, 15, false, false, false);
            MyAssetsManager.IconesMenu.Add(Endurance);
            Assets Energy = new Assets("Energy", MyAssetsManager.MyTexturesManager.GetTexture("assets/Energy.png"), 610, GetScreenHeight() - 32, 0, 0, 14, 15, false, false, false);
            MyAssetsManager.IconesMenu.Add(Energy);

        }
        public void Update()
        {float DeltaTime = GetFrameTime();

            if (Respawn)
            {
                TryAgain();
            }
            if (EndGame)
            {
                Close();
            }
            MyMapDraw.Update();
            MyBob.HandleInput();
            MyBob.Update();
            MyAssetsDraw.Update();
            MyAssetsUpdate.Update();
            if (IsKeyDown(KeyboardKey.G))
                MapDraw.CameraY += 30;

            if (IsKeyPressed(KeyboardKey.R))
                Retry();

            foreach (Assets icone in MyAssetsManager.IconesMenu)
            {
                icone.AssetFrameTimer -= 100 * DeltaTime;
                if (icone.AssetFrameTimer <= 0)
                {
                    icone.AssetCurrentFrame++;
                    if (icone.AssetCurrentFrame >= icone.AssetFrameCount)
                        icone.AssetCurrentFrame = 0;
                    icone.AssetFrameTimer = icone.AssetNewFrameTimer;
                }
                int frame = icone.AssetFrameWidth * icone.AssetCurrentFrame;
                icone.AssetSourceRec = new Rectangle(frame, 0, icone.AssetFrameWidth, icone.AssetFrameHeight);
            }


        }

        public void Draw()
        {
            MyMapDraw.Draw();
            MyAssetsDraw.Draw();
            MyAssetsUpdate.Draw();
            MyBob.Draw();

            //InGameMenu
            Rectangle menuRec = new Rectangle(0, GetScreenHeight() -32, GetScreenWidth(), 32);

            
            DrawRectanglePro(menuRec, new Vector2(0, 0), 0, new Color (2, 6, 10));
           
            DrawText("ENDURANCE ...  " + MyBob.Endurance.ToString("F0") + " % ", 350, GetScreenHeight() - 25, 20, new Color(150, 161, 180));
            DrawText("ENERGY ...  " + MyBob.Energy.ToString("F0") + " % ", 650, GetScreenHeight() - 25, 20, new Color(150, 161, 180));
            DrawText("R ... Recommencer ", 1000, GetScreenHeight() - 25, 20, new Color(150, 161, 180));

            foreach (Assets icone in MyAssetsManager.IconesMenu)
                DrawTexturePro(icone.AssetTileSet, icone.AssetSourceRec, new Rectangle(icone.AssetX, icone.AssetY, icone.AssetFrameWidth, icone.AssetFrameHeight), new Vector2(0, 0), 0, Color.White);

            // Clouds devant Bob
            foreach (Assets cloud in MyAssetsManager.Clouds)
                if (cloud.AssetName == "CloudFrontUp")
                    DrawTexturePro(cloud.AssetTileSet, cloud.AssetSourceRec, new Rectangle(cloud.AssetX, cloud.AssetY, cloud.AssetFrameWidth * 2, cloud.AssetFrameHeight * 2), new Vector2(cloud.AssetFrameWidth / 2, 0), 0, Color.White);
                else
                    DrawTexturePro(cloud.AssetTileSet, cloud.AssetSourceRec, new Rectangle(cloud.AssetX, cloud.AssetY, cloud.AssetFrameWidth, cloud.AssetFrameHeight), new Vector2(cloud.AssetFrameWidth / 2, 0), 0, Color.White);
        }
        public void Retry()
        {

            MyMapDraw.Close();
            MyMapRead.Close();
            MyAssetsManager.Close();
            Init();            
            Assets Endurance = new Assets("Endurance", MyAssetsManager.MyTexturesManager.GetTexture("assets/Endurance.png"), 310, GetScreenHeight() - 32, 0, 0, 12, 15, false, false, false);
            MyAssetsManager.IconesMenu.Add(Endurance);
            Assets Energy = new Assets("Energy", MyAssetsManager.MyTexturesManager.GetTexture("assets/Energy.png"), 610, GetScreenHeight() - 32, 0, 0, 14, 15, false, false, false);
            MyAssetsManager.IconesMenu.Add(Energy);
            MyBob.Init();
        }
        public void TryAgain()
        {
            MyMapDraw.Close();
            MyMapRead.Close();
            MyAssetsManager.Close();
            MyAssetsManager = AssetsManager.Instance;
            MyMapRead = new MapRead();
            MyMapRead.LoadDatas("assets/LadderMapLV1.json");
            MyMapDraw = new MapDraw(MyMapRead, "assets/");
            MyAssetsDraw = AssetsDraw.Instance;
            MyAssetsUpdate = AssetsUpdate.Instance;
            MyBob.Respawn();
            Respawn = false;
        }
        public void Close()
        {
            MyMapDraw.Close();
            MyMapRead.Close();
            MyAssetsManager.Close();
            MyBob.Close();

            MyBob.Init();
            MyAssetsManager.Init();
        }

        
    }
}
