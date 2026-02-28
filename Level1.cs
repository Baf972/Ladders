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
    public enum  Level1States
    {
        root,
        final,
        end
    }
    public class Level1
    {

        public Bob MyBob { get; set; }
        public MapRead MyMapRead { get; set; }
        public MapDraw MyMapDraw { get; set; }
        public AssetsDraw MyAssetsDraw { get; set; }
        public AssetsManager MyAssetsManager { get; set; }
        public AssetsUpdate MyAssetsUpdate { get; set; }        
        public static Texture2D ShotBackGround { get; set; }
        public static Level1States Level1State { get; set; }
        public static bool Respawn { get; set; }
        public static bool EndGame { get; set; }
        public static bool CloudsFinal { get; set; }
        private int FadeRectOut {  get; set; }
        private int FadeRectOutMontain {  get; set; }
        private int FadeRectEnd {  get; set; }
        private int FadeOutTypo {  get; set; }
        private float GameFinishTimer { get; set; }

        private Font FontDialogues = LoadFont("assets/fonts/COOPBL.ttf");
        public Level1()
        {
            Init();
        }

        public void Init()
        {

            string chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789éèàùçâêîôûëïüöÉÈÀÙÇÂÊÎÔÛËÏÜÖ' .:-!?";
            int[] codepoints = chars.Select(c => (int)c).ToArray();
            FontDialogues = LoadFontEx("assets/fonts/COOPBL.ttf", 70, codepoints, codepoints.Length);

            MyMapRead = new MapRead();
            MyMapRead.LoadDatas("assets/LadderMapLV1.json");
            MyMapDraw = new MapDraw(MyMapRead, "assets/");
            MyAssetsManager = AssetsManager.Instance;
            MyAssetsDraw = AssetsDraw.Instance;
            MyBob = Bob.Instance;
            MyAssetsUpdate = AssetsUpdate.Instance;
            Respawn = false;
            Assets Endurance = new Assets("Endurance", MyAssetsManager.MyTexturesManager.GetTexture("assets/Endurance.png"), 360, GetScreenHeight() - 32, 0, 0, 12, 15, false, false, false);
            MyAssetsManager.IconesMenu.Add(Endurance);
            Assets Energy = new Assets("Energy", MyAssetsManager.MyTexturesManager.GetTexture("assets/Energy.png"), 660, GetScreenHeight() - 32, 0, 0, 14, 15, false, false, false);
            MyAssetsManager.IconesMenu.Add(Energy);
            Assets bobIcone2 = new Assets("BobIcone1", MyAssetsManager.MyTexturesManager.GetTexture("assets/bobIcone.png"), 105, GetScreenHeight() - 32, 0, 0, 1, 0, false, false, false);
            MyAssetsManager.IconesMenu.Add(bobIcone2);
            GameFinishTimer = 3;
            FadeOutTypo = 255;
            FadeRectEnd = 0;
            FadeRectOutMontain = 0;
            FadeRectOut = 0;
            CloudsFinal = true;
            Level1State = Level1States.root;
            

        }
        public void Update()
        {
            float DeltaTime = GetFrameTime();

            if (Respawn)
            {
                ReSpawnBob();
            }            

            if (IsKeyPressed(KeyboardKey.R))
                Retry();

            switch (Level1State)
            {
                case Level1States.root:
                    if (IsKeyDown(KeyboardKey.G))
                        MapDraw.CameraY += 10;
                    MyMapDraw.Update();
                    MyBob.HandleInput();
                    MyBob.Update();
                    MyAssetsDraw.Update();
                    MyAssetsUpdate.Update();

                    if (MapDraw.CameraY >= -10)
                    {
                        FadeRectOutMontain += 3;
                        if (FadeRectOutMontain >= 255)
                        {
                            FadeRectOutMontain = 255;
                            Level1State = Level1States.final;
                        }
                            

                    }

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

                    break;

                case Level1States.final:

                    FadeRectOutMontain -= 1;
                    if (FadeRectOutMontain <= 0)
                        FadeRectOutMontain = 0;

                    if (MyAssetsUpdate.GameFinish)
                    {
                        FadeRectOut += 3;

                        if (FadeRectOut >= 255)
                            FadeRectOut = 255;
                    }

                    MyAssetsUpdate.Update();
                    MyAssetsDraw.Update();
                    
                    break;

                case Level1States.end:

                    if (IsKeyPressed(KeyboardKey.R))
                        ReSpawnBob();

                    if (IsKeyPressed(KeyboardKey.A))
                        EndGame = true;

                    if (EndGame)
                    {

                        FadeRectEnd += 3;
                        if (FadeRectEnd >= 255)
                        {
                            FadeRectEnd = 255;
                            GameStates.Instance.ChangeScene("menu");

                            Close();
                        }
                    }

                    break;
            }   
        }

        public void Draw()
        {
            switch (Level1State)
            {
                case Level1States.root:
                    MyMapDraw.Draw();
                    MyAssetsDraw.Draw();
                    MyAssetsUpdate.Draw();
                    MyBob.Draw();

                    //InGameMenu
                    Rectangle menuRec = new Rectangle(0, GetScreenHeight() - 32, GetScreenWidth(), 32);


                    DrawRectanglePro(menuRec, new Vector2(0, 0), 0, new Color(2, 6, 10));

                    DrawText("LIFE ...  " + MyBob.Life.ToString(), 150, GetScreenHeight() - 25, 20, new Color(150, 161, 180));
                    DrawText("ENDURANCE ...  " + MyBob.Endurance.ToString("F0") + " % ", 400, GetScreenHeight() - 25, 20, new Color(150, 161, 180));
                    DrawText("ENERGY ...  " + MyBob.Energy.ToString("F0") + " % ", 700, GetScreenHeight() - 25, 20, new Color(150, 161, 180));
                    DrawText("R ... Recommencer ", 1000, GetScreenHeight() - 25, 20, new Color(150, 161, 180));

                    foreach (Assets icone in MyAssetsManager.IconesMenu)
                        DrawTexturePro(icone.AssetTileSet, icone.AssetSourceRec, new Rectangle(icone.AssetX, icone.AssetY, icone.AssetFrameWidth, icone.AssetFrameHeight), new Vector2(0, 0), 0, Color.White);

                    // Clouds devant Bob
                    foreach (Assets cloud in MyAssetsManager.Clouds)
                        if (cloud.AssetName == "CloudFrontUp")
                            DrawTexturePro(cloud.AssetTileSet, cloud.AssetSourceRec, new Rectangle(cloud.AssetX, cloud.AssetY, cloud.AssetFrameWidth * 2, cloud.AssetFrameHeight * 2), new Vector2(cloud.AssetFrameWidth / 2, 0), 0, Color.White);
                        else
                            DrawTexturePro(cloud.AssetTileSet, cloud.AssetSourceRec, new Rectangle(cloud.AssetX, cloud.AssetY, cloud.AssetFrameWidth, cloud.AssetFrameHeight), new Vector2(cloud.AssetFrameWidth / 2, 0), 0, Color.White);
                    //DrawText("CamearY " + MapDraw.CameraY.ToString(), 50, 50, 30, Color.White);
                    DrawRectangle(0, 0, GetScreenWidth(), GetScreenHeight(), new Color(255, 255, 255, FadeRectOutMontain));
                    break;

                case Level1States.final:

                    MyAssetsDraw.Draw();
                    // Clouds devant Bob
                    foreach (Assets cloud in MyAssetsManager.Clouds)
                        if (cloud.AssetName == "CloudFront")
                            DrawTexturePro(cloud.AssetTileSet, cloud.AssetSourceRec, new Rectangle(cloud.AssetX, cloud.AssetY, cloud.AssetFrameWidth * 2, cloud.AssetFrameHeight * 2), new Vector2(cloud.AssetFrameWidth / 2, 0), 0, Color.White);

                    MyAssetsUpdate.Draw();
                    foreach (Assets dialogue in MyAssetsManager.Dialogues)
                        DrawTexturePro(dialogue.AssetTileSet, dialogue.AssetSourceRec, new Rectangle(dialogue.AssetX, dialogue.AssetY, dialogue.AssetFrameWidth, dialogue.AssetFrameHeight), new Vector2(300 , 0), 0, Color.White);

                    if (MyAssetsUpdate.GameFinish)
                    {
                        GameFinishTimer -= GetFrameTime();
                        DrawRectangle(0, 0, GetScreenWidth(), GetScreenHeight(), new Color(255, 255, 255, FadeRectOut));
                        DrawTextEx(FontDialogues, "Merci David !", new Vector2(515, GetScreenHeight() / 2 - 50), 40, 0, new Color(67, 41, 24, FadeOutTypo));
                        if (GameFinishTimer <= 0)
                            EndGame = true;

                        if (EndGame)
                        {
                            FadeOutTypo -= 5;
                            if (FadeOutTypo <= 0)
                            {
                                GameStates.Instance.ChangeScene("menu");
                                Level1State = Level1States.root;
                                EndGame = false;
                                Close();
                            }                            
                        }
                    }

                    DrawRectangle(0, 0, GetScreenWidth(), GetScreenHeight(), new Color(255, 255, 255, FadeRectOutMontain));

                    break;

                case Level1States.end:

                    DrawTexture(ShotBackGround, 0, 0, new Color (255, 255, 255, 150));
                    DrawTextEx(FontDialogues, "Press R to retry or Q to Quit", new Vector2(430, GetScreenHeight() / 2 - 50), 30, 0, new Color(255, 255, 255, FadeOutTypo));
                    DrawRectangle(0, 0, GetScreenWidth(), GetScreenHeight(), new Color(255, 255, 255, FadeRectEnd));

                    break;
            }





        }
        public void Retry()
        {

            MyMapDraw.Close();
            MyMapRead.Close();
            MyAssetsManager.Close();
            MyAssetsDraw.Close();
            Init();
            Assets Endurance = new Assets("Endurance", MyAssetsManager.MyTexturesManager.GetTexture("assets/Endurance.png"), 360, GetScreenHeight() - 32, 0, 0, 12, 15, false, false, false);
            MyAssetsManager.IconesMenu.Add(Endurance);
            Assets Energy = new Assets("Energy", MyAssetsManager.MyTexturesManager.GetTexture("assets/Energy.png"), 660, GetScreenHeight() - 32, 0, 0, 14, 15, false, false, false);
            MyAssetsManager.IconesMenu.Add(Energy);
            Assets bobIcone2 = new Assets("BobIcone1", MyAssetsManager.MyTexturesManager.GetTexture("assets/bobIcone.png"), 105, GetScreenHeight() - 32, 0, 0, 1, 0, false, false, false);
            MyAssetsManager.IconesMenu.Add(bobIcone2);
            MyBob.Init();
        }
        public void ReSpawnBob()
        {
            MyMapDraw.Close();
            MyMapRead.Close();
            MyAssetsManager.Close();
            MyAssetsDraw.Close();
            MyAssetsManager = AssetsManager.Instance;
            MyMapRead = new MapRead();
            MyMapRead.LoadDatas("assets/LadderMapLV1.json");
            MyMapDraw = new MapDraw(MyMapRead, "assets/");
            MyAssetsDraw = AssetsDraw.Instance;
            MyAssetsUpdate = AssetsUpdate.Instance;
            Assets Endurance = new Assets("Endurance", MyAssetsManager.MyTexturesManager.GetTexture("assets/Endurance.png"), 360, GetScreenHeight() - 32, 0, 0, 12, 15, false, false, false);
            MyAssetsManager.IconesMenu.Add(Endurance);
            Assets Energy = new Assets("Energy", MyAssetsManager.MyTexturesManager.GetTexture("assets/Energy.png"), 660, GetScreenHeight() - 32, 0, 0, 14, 15, false, false, false);
            MyAssetsManager.IconesMenu.Add(Energy);
            Assets bobIcone2 = new Assets("BobIcone1", MyAssetsManager.MyTexturesManager.GetTexture("assets/bobIcone.png"), 105, GetScreenHeight() - 32, 0, 0, 1, 0, false, false, false);
            MyAssetsManager.IconesMenu.Add(bobIcone2);
            MyBob.Respawn();
            Respawn = false;
        }
        public void Close()
        {
            MyMapDraw.Close();
            MyMapRead.Close();
            MyAssetsManager.Close();
            MyAssetsDraw.Close();
            MyBob.Close();    
            UnloadTexture(ShotBackGround);
            MyBob.Init();
            MyAssetsManager.Init();
            Init();
            EndGame = false;
        }

        
    }
}
