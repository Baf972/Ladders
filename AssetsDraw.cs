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
    public class AssetsDraw
    {
        private Bob MyBob { get; set; }

        private AssetsManager MyAssetsManager {  get; set; }
        private static AssetsDraw? instance;
        public static AssetsDraw Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new AssetsDraw();
                }
                return instance;
            }
        }
        private Random PartRotation { get; set; } = new Random();
        private Random PartSpeed { get; set; } = new Random();
        private Texture2D BackGround { get; set; }
        private string TextGuide { get; set; }
        private string TextDLC { get; set; }
        private string TextMasterClass { get; set; }
        private string TextDevPerso{ get; set; }
        private bool TextGuideOk { get; set; }
        private bool TextDLCOk { get; set; }
        private bool TextMasterClassOk { get; set; }
        private bool TextDevPersoOk{ get; set; }
        private float TextGuideTimer { get; set; }
        private float TextDLCTimer { get; set; }
        private float TextMasterClassTimer { get; set; }
        private float TextDevPersoTimer { get; set; }

        private Font FontGifts = LoadFont("assets/fonts/COOPBL.ttf");

        public AssetsDraw()
        {
            string chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789éèàùçâêîôûëïüöÉÈÀÙÇÂÊÎÔÛËÏÜÖ' .:-!?";
            int[] codepoints = chars.Select(c => (int)c).ToArray();
            FontGifts = LoadFontEx("assets/fonts/COOPBL.ttf", 70, codepoints, codepoints.Length);

            MyAssetsManager = AssetsManager.Instance;
            MyBob = Bob.Instance;
            BackGround = MyAssetsManager.MyTexturesManager.GetTexture("assets/BackGround2.png");
            TextGuide = "Guide de programmation !";
            TextDLC = "Pack DLC !";
            TextMasterClass = "MasterClass !";
            TextDevPerso = "Coaching !";
            TextGuideTimer = 1;
            TextDLCTimer = 1;
            TextMasterClassTimer = 1;
            TextDevPersoTimer = 1;
        }

        public void Update()
        {
            float DeltaTime = GetFrameTime();
            
            
            switch (Level1.Level1State)
            {
                case Level1States.root:
                    foreach (Assets part in AssetsManager.Instance.LadderParts.ToList())
                    {
                        part.AssetCurrentFrame = 1;
                        part.AssetY += part.AssetSpeed * DeltaTime;
                        part.AssetR += part.AssetR * DeltaTime;

                        if (part.AssetY > GetScreenHeight() + part.AssetFrameHeight)
                            MyAssetsManager.LadderParts.Remove(part);


                        int frame = part.AssetFrameWidth * part.AssetCurrentFrame;
                        part.AssetSourceRec = new Rectangle(frame, 0, part.AssetFrameWidth, part.AssetFrameHeight);
                    }

                    foreach (Assets gift in MyAssetsManager.Gifts.ToList())
                    {
                        gift.AssetFrameTimer -= 100 * DeltaTime;
                        if (gift.AssetFrameTimer <= 0)
                        {
                            int random = Assets.MyRandom.Next(0, 5);
                            gift.AssetCurrentFrame++;
                            if (gift.AssetCurrentFrame >= gift.AssetFrameCount)
                            {

                                if (gift.AssetName == "GiftExplo")
                                {
                                    MyAssetsManager.Gifts.Remove(gift);

                                    if (random == 0)
                                        TextDevPersoOk = true;
                                    else if (random == 1)
                                        TextDLCOk = true;
                                    else if (random == 2)
                                        TextGuideOk = true;
                                    else if (random == 3)
                                        TextMasterClassOk = true;
                                }
                                else
                                    gift.AssetCurrentFrame = 0;

                            }
                            gift.AssetFrameTimer = gift.AssetNewFrameTimer;
                        }
                        int frame = gift.AssetFrameWidth * gift.AssetCurrentFrame;
                        gift.AssetSourceRec = new Rectangle(frame, 0, gift.AssetFrameWidth, gift.AssetFrameHeight);
                    }

                    foreach (Assets fruit in MyAssetsManager.Fruits.ToList())
                    {
                        fruit.AssetFrameTimer -= 100 * DeltaTime;
                        if (fruit.AssetFrameTimer <= 0)
                        {
                            fruit.AssetCurrentFrame++;
                            if (fruit.AssetCurrentFrame >= fruit.AssetFrameCount)
                            {
                                if (fruit.AssetName == "FruitExplo")
                                    MyAssetsManager.Fruits.Remove(fruit);
                                else
                                    fruit.AssetCurrentFrame = 0;

                            }
                            fruit.AssetFrameTimer = fruit.AssetNewFrameTimer;
                        }
                        int frame = fruit.AssetFrameWidth * fruit.AssetCurrentFrame;
                        fruit.AssetSourceRec = new Rectangle(frame, 0, fruit.AssetFrameWidth, fruit.AssetFrameHeight);
                    }

                    foreach (Assets endurance in MyAssetsManager.Endurance.ToList())
                    {
                        endurance.AssetFrameTimer -= 100 * DeltaTime;
                        if (endurance.AssetFrameTimer <= 0)
                        {
                            endurance.AssetCurrentFrame++;
                            if (endurance.AssetCurrentFrame >= endurance.AssetFrameCount)
                                endurance.AssetCurrentFrame = 0;

                            endurance.AssetFrameTimer = endurance.AssetNewFrameTimer;
                        }
                        int frame = endurance.AssetFrameWidth * endurance.AssetCurrentFrame;
                        endurance.AssetSourceRec = new Rectangle(frame, 0, endurance.AssetFrameWidth, endurance.AssetFrameHeight);
                    }

                    foreach (Assets rock in MyAssetsManager.Rocks)
                    {
                        rock.AssetCurrentFrame = 1;
                        int frame = rock.AssetFrameWidth * rock.AssetCurrentFrame;
                        rock.AssetSourceRec = new Rectangle(frame, 0, rock.AssetFrameWidth, rock.AssetFrameHeight);
                    }

                    foreach (Assets cloud in MyAssetsManager.Clouds)
                    {
                        cloud.AssetCurrentFrame = 1;
                        int frame = cloud.AssetFrameWidth * cloud.AssetCurrentFrame;
                        cloud.AssetSourceRec = new Rectangle(frame, 0, cloud.AssetFrameWidth, cloud.AssetFrameHeight);
                    }

                    break;

                case Level1States.final:

                    foreach (Assets cloud in MyAssetsManager.Clouds)
                    {
                        cloud.AssetCurrentFrame = 1;
                        int frame = cloud.AssetFrameWidth * cloud.AssetCurrentFrame;
                        cloud.AssetSourceRec = new Rectangle(frame, 0, cloud.AssetFrameWidth, cloud.AssetFrameHeight);
                    }

                    foreach (Assets David in MyAssetsManager.David)
                    {
                        David.AssetFrameTimer -= 100 * DeltaTime;
                        if (David.AssetFrameTimer <= 0)
                        {
                            David.AssetCurrentFrame++;
                            if (David.AssetCurrentFrame >= David.AssetFrameCount)
                                David.AssetCurrentFrame = 0;
                            David.AssetFrameTimer = David.AssetNewFrameTimer;
                        }
                        int frame = David.AssetFrameWidth * David.AssetCurrentFrame;
                        David.AssetSourceRec = new Rectangle(frame, 0, David.AssetFrameWidth, David.AssetFrameHeight);
                    }

                    foreach (Assets dialogue in MyAssetsManager.Dialogues)
                    {
                        dialogue.AssetCurrentFrame = 1;
                        int frame = dialogue.AssetFrameWidth * dialogue.AssetCurrentFrame;
                        dialogue.AssetSourceRec = new Rectangle(frame, 0, dialogue.AssetFrameWidth, dialogue.AssetFrameHeight);
                    }

                    break;
            }

            
                       
        }


        public void Draw()
        {
            switch (Level1.Level1State)
            {
                case Level1States.root:

                    foreach (Assets part in AssetsManager.Instance.LadderParts)
                        DrawTexturePro(part.AssetTileSet, part.AssetSourceRec, new Rectangle(part.AssetX, part.AssetY, part.AssetFrameWidth, part.AssetFrameHeight), new Vector2(part.AssetFrameWidth / 2, 0), part.AssetR, Color.White);

                    foreach (Assets gift in MyAssetsManager.Gifts)
                        DrawTexturePro(gift.AssetTileSet, gift.AssetSourceRec, new Rectangle(gift.AssetX, gift.AssetY - 64, gift.AssetFrameWidth, gift.AssetFrameHeight), new Vector2(gift.AssetFrameWidth / 2, 0), 0, Color.White);   

                    foreach (Assets fruit in MyAssetsManager.Fruits)
                        DrawTexturePro(fruit.AssetTileSet, fruit.AssetSourceRec, new Rectangle(fruit.AssetX, fruit.AssetY - 64, fruit.AssetFrameWidth, fruit.AssetFrameHeight), new Vector2(fruit.AssetFrameWidth / 2, 0), 0, Color.White);

                    foreach (Assets endurance in MyAssetsManager.Endurance)
                        DrawTexturePro(endurance.AssetTileSet, endurance.AssetSourceRec, new Rectangle(endurance.AssetX, endurance.AssetY - 64, endurance.AssetFrameWidth, endurance.AssetFrameHeight), new Vector2(endurance.AssetFrameWidth / 2, 0), 0, Color.White);
                    

                    if (TextMasterClassOk)
                    {
                        TextMasterClassTimer -= GetFrameTime();                       
                        float textWidth = MeasureTextEx(FontGifts, TextMasterClass, 20, 0).X;
                        float textHeight = MeasureTextEx(FontGifts, TextMasterClass, 20, 0).Y;
                        DrawTextPro(FontGifts, TextMasterClass, new Vector2 ((int)MyBob.X - MyBob.FrameWidth / 2, (int)MyBob.Y - MyBob.FrameHeight - 10),new Vector2(textWidth / 2, textHeight), 0, 20, 0, Color.White);

                        if (TextMasterClassTimer <= 0)
                        {
                            TextMasterClassOk = false;
                            TextMasterClassTimer = 1;
                        }
                    }
                    if (TextDLCOk)
                    {
                        TextDLCTimer -= GetFrameTime();
                        float textWidth = MeasureTextEx(FontGifts, TextDLC, 20, 0).X;
                        float textHeight = MeasureTextEx(FontGifts, TextDLC, 20, 0).Y;
                        DrawTextPro(FontGifts, TextDLC, new Vector2((int)MyBob.X - MyBob.FrameWidth / 2, (int)MyBob.Y - MyBob.FrameHeight -10), new Vector2(textWidth / 2, textHeight), 0, 20, 0, Color.White);

                        if (TextDLCTimer <= 0)
                        {
                            TextDLCOk = false;
                            TextDLCTimer = 1;
                        }
                    }
                    if (TextGuideOk)
                    {
                        TextGuideTimer -= GetFrameTime();

                        float textWidth = MeasureTextEx(FontGifts, TextGuide, 20, 0).X;
                        float textHeight = MeasureTextEx(FontGifts, TextGuide, 20, 0).Y;
                        DrawTextPro(FontGifts, TextGuide, new Vector2((int)MyBob.X - MyBob.FrameWidth / 2, (int)MyBob.Y - MyBob.FrameHeight - 10), new Vector2(textWidth / 2, textHeight), 0, 20, 0, Color.White);

                        if (TextGuideTimer <= 0)
                        {
                            TextGuideOk = false;
                            TextGuideTimer = 1;
                        }
                    }
                    if (TextDevPersoOk)
                    {
                        TextDevPersoTimer -= GetFrameTime();

                        float textWidth = MeasureTextEx(FontGifts, TextDevPerso, 20, 0).X;
                        float textHeight = MeasureTextEx(FontGifts, TextDevPerso, 20, 0).Y;
                        DrawTextPro(FontGifts, TextDevPerso, new Vector2((int)MyBob.X - MyBob.FrameWidth / 2, (int)MyBob.Y - MyBob.FrameHeight - 10), new Vector2(textWidth / 2, textHeight), 0, 20, 0, Color.White);

                        if (TextDevPersoTimer <= 0)
                        {
                            TextDevPersoOk = false;
                            TextDevPersoTimer = 1;
                        }
                    }
                    

                    break;

                case Level1States.final:

                    DrawTexture(BackGround, 0, 0, Color.White)  ;

                    // Clouds écran final
                    foreach (Assets cloud in MyAssetsManager.Clouds)
                        if (cloud.AssetName == "CloudFrontUp")
                            DrawTexturePro(cloud.AssetTileSet, cloud.AssetSourceRec, new Rectangle(cloud.AssetX, cloud.AssetY, cloud.AssetFrameWidth * 2, cloud.AssetFrameHeight * 2), new Vector2(cloud.AssetFrameWidth / 2, 0), 0, Color.White);
                        else
                            DrawTexturePro(cloud.AssetTileSet, cloud.AssetSourceRec, new Rectangle(cloud.AssetX, cloud.AssetY, cloud.AssetFrameWidth, cloud.AssetFrameHeight), new Vector2(cloud.AssetFrameWidth / 2, 0), 0, Color.White);

                    foreach (Assets David in MyAssetsManager.David)
                        DrawTexturePro(David.AssetTileSet, David.AssetSourceRec, new Rectangle(David.AssetX, David.AssetY - 64, David.AssetFrameWidth, David.AssetFrameHeight), new Vector2(David.AssetFrameWidth / 2, 0), 0, Color.White);

                    
                    break;
            }
                        
           



            //DrawText(MapDraw.CameraY.ToString(), 10, 10, 20, Color.White);
            //DrawText("List Clouds    " + MyAssetsManager.Clouds.Count.ToString(), 10, 30, 20, Color.White);
            //DrawText("List Rocks    " + MyAssetsManager.Rocks.Count.ToString(), 10, 60, 20, Color.White);
        }
        public void Close()
        {
            TextGuideOk = false;
            TextDLCOk = false;
            TextMasterClassOk = false;
            TextDevPersoOk = false;
        }
    }
}
