using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Raylib_cs;
using static Raylib_cs.Raylib;

namespace LADDERS
{
    public class AssetsUpdate
    {
        private static AssetsUpdate? instance;
        public static AssetsUpdate Instance
        {
            get 
            {
                if (instance == null)
                {
                    instance = new AssetsUpdate();
                }
                return instance;
            }
        }

        private Bob MyBob {  get; set; } 
        private AssetsManager MyAssetsManager { get; set; }
        private MapRead MyMapRead { get; set; }
        public Rectangle BobRec { get; set; }
        private Assets CloudFront { get; set; }
        private Assets CloudFrontUp { get; set; }
        private float RockFallTimer { get; set; }


        public AssetsUpdate()
        {
            MyAssetsManager = AssetsManager.Instance;
            MyMapRead = new MapRead();
            MyBob = Bob.Instance;
            RockFallTimer = 0;
        }


        public void Update()
        {
            float DeltaTime = GetFrameTime();

            BobStates MyBobState = MyBob.GetCurrentState();

            foreach (Assets gift in MyAssetsManager.Gifts.ToList())
            {
                
                Rectangle bobrec = new Rectangle(MyBob.X - MyBob.FrameWidth, MyBob.Y - MyBob.FrameHeight, MyBob.FrameWidth, MyBob.FrameHeight);
                Rectangle giftrec = new Rectangle(gift.AssetX - 15, gift.AssetY - gift.AssetFrameHeight / 2 + 10, gift.AssetFrameWidth / 2, gift.AssetFrameHeight / 2);

                if ( gift.AssetName == "Gift" && CheckCollisionRecs(bobrec, giftrec))
                {
                    MyAssetsManager.Gifts.Remove(gift);
                    Assets giftExplo = new Assets("GiftExplo", MyAssetsManager.MyTexturesManager.GetTexture("assets/GiftExplo.png"), (int)gift.AssetX, (int)gift.AssetY-10, (int)gift.AssetR, 0 , 17, 5, false, false, false);
                    MyAssetsManager.Gifts.Add(giftExplo);
                }

            }

            foreach (Assets fruit in MyAssetsManager.Fruits.ToList())
            {
                if (MyBobState == BobStates.Jumping && MyBob.IsFlipped)
                    BobRec = new Rectangle(MyBob.X - MyBob.FrameWidth / 2, MyBob.Y - MyBob.FrameHeight, MyBob.FrameWidth, MyBob.FrameHeight);

                else if (MyBobState == BobStates.Jumping && !MyBob.IsFlipped)
                    BobRec = new Rectangle(MyBob.X - MyBob.FrameWidth, MyBob.Y - MyBob.FrameHeight, MyBob.FrameWidth, MyBob.FrameHeight);

                else if (MyBobState == BobStates.RunningUp && !MyBob.IsFlipped)
                    BobRec = new Rectangle(MyBob.X - MyBob.FrameWidth / 2, MyBob.Y - MyBob.FrameHeight, MyBob.FrameWidth, MyBob.FrameHeight);

                else if (MyBobState == BobStates.RunningUp && !MyBob.IsFlipped)
                    BobRec = new Rectangle(MyBob.X + MyBob.FrameWidth / 2, MyBob.Y - MyBob.FrameHeight, MyBob.FrameWidth, MyBob.FrameHeight);

                else if (MyBobState == BobStates.Landing && MyBob.IsFlipped)
                    BobRec = new Rectangle(MyBob.X - MyBob.FrameWidth / 2, MyBob.Y - MyBob.FrameHeight, MyBob.FrameWidth, MyBob.FrameHeight);

                else
                    BobRec = new Rectangle(MyBob.X - MyBob.FrameWidth, MyBob.Y - MyBob.FrameHeight, MyBob.FrameWidth, MyBob.FrameHeight);


                Rectangle fruittrec = new Rectangle(fruit.AssetX - 15, fruit.AssetY - fruit.AssetFrameHeight / 2 - 10, fruit.AssetFrameWidth / 2, fruit.AssetFrameHeight / 2 + 20);

                if (CheckCollisionRecs(BobRec, fruittrec))
                {
                    MyAssetsManager.Fruits.Remove(fruit);
                    //Assets fruitExplo = new Assets("GiftExplo", MyAssetsManager.MyTexturesManager.GetTexture("assets/GiftExplo.png"), (int)fruit.AssetX, (int)fruit.AssetY - 20, (int)fruit.AssetR, 0, 17, 5, false, false, false);
                    //MyAssetsManager.Fruits.Add(fruitExplo);
                }

            }

            // Gestion des Rochers

            // Génère des rochers
            if (MapDraw.CameraY > -2000)
            {               
                RockFallTimer -= DeltaTime;
                if (RockFallTimer <= 0) // Apparition des rochers
                {
                    SpawnRocks();
                }
            }

            // Update des rochers
            foreach (Assets rock in MyAssetsManager.Rocks.ToList())
            {
                

                rock.AssetY += rock.AssetSpeed * DeltaTime;
                if (rock.AssetSpeedR >= 0)
                {
                    rock.AssetSpeedR += 100 * DeltaTime;

                    if (rock.AssetSpeedR > 5)
                    {
                        rock.AssetSpeedR = 5;
                        rock.AssetR += rock.AssetSpeedR;
                    }

                }
                    
                if (rock.AssetSpeedR < 0)
                {
                    rock.AssetSpeedR -= 100 * DeltaTime;
                    if (rock.AssetSpeedR < 5)
                    {
                        rock.AssetSpeedR = - 5;
                        rock.AssetR += rock.AssetSpeedR;
                    }
                }
                    
                    

                if (rock.AssetY > GetScreenHeight() + rock.AssetFrameHeight)
                    MyAssetsManager.Rocks.Remove(rock);
            }


            // Gestion des Nuages

            if (MapDraw.CloudsAppear)
            {
                CloudFront = new Assets("CloudFront", MyAssetsManager.MyTexturesManager.GetTexture("assets/CloudsFront.png"), 0, -700, 0, 10, 1, 1, false, false, false);
                MyAssetsManager.Clouds.Add(CloudFront);

                CloudFrontUp = new Assets("CloudFrontUp", MyAssetsManager.MyTexturesManager.GetTexture("assets/CloudsFrontUp.png"), -300, -1400, 0, 5, 1, 1, false, false, false);
                MyAssetsManager.Clouds.Add(CloudFrontUp);

                CloudFrontUp = new Assets("CloudFrontUp", MyAssetsManager.MyTexturesManager.GetTexture("assets/CloudsFrontUp.png"), 0, -1100, 0, 15, 1, 1, false, false, false);
                MyAssetsManager.Clouds.Add(CloudFrontUp);

                CloudFrontUp = new Assets("CloudFrontUp", MyAssetsManager.MyTexturesManager.GetTexture("assets/CloudsFrontUp.png"), 300, -1100, 0, 5, 1, 1, false, false, false);
                MyAssetsManager.Clouds.Add(CloudFrontUp);


                MapDraw.CloudsAppear = false;
            }
            foreach (Assets cloud in MyAssetsManager.Clouds)
            {
                cloud.AssetX += cloud.AssetSpeed * DeltaTime;
                if (cloud.AssetX > GetScreenWidth() + cloud.AssetFrameWidth / 4)
                {
                    if (cloud.AssetName == "CloudFront")
                        cloud.AssetX = 0 - cloud.AssetFrameWidth;
                    else if (cloud.AssetName == "CloudFrontUp")
                        cloud.AssetX = 0 - cloud.AssetFrameWidth * 1.5f;
                }   
            }
        }

        public void Draw()
        {
          /*  foreach (Assets fruit in MyAssetsManager.Fruits.ToList())
            {
                //Rectangle bobrec = new Rectangle(MyBob.X - MyBob.FrameWidth, MyBob.Y - MyBob.FrameHeight, MyBob.FrameWidth, MyBob.FrameHeight);
                Rectangle fruittrec = new Rectangle(fruit.AssetX - 15, fruit.AssetY - fruit.AssetFrameHeight /2 -10 , fruit.AssetFrameWidth /2 , fruit.AssetFrameHeight /2 +20);
                //DrawRectangleLinesEx(bobrec, 2, Color.White);
                DrawRectangleLinesEx(fruittrec, 2, Color.White);
            }
            DrawRectangleLinesEx(BobRec, 2, Color.White);
            foreach (Assets gift in MyAssetsManager.Gifts.ToList())
            {
                Rectangle bobrec = new Rectangle(MyBob.X - MyBob.FrameWidth, MyBob.Y - MyBob.FrameHeight, MyBob.FrameWidth, MyBob.FrameHeight);
                Rectangle giftrec = new Rectangle(gift.AssetX - 15, gift.AssetY - gift.AssetFrameHeight /2 + 10, gift.AssetFrameWidth / 2, gift.AssetFrameHeight / 2);
                DrawRectangleLinesEx(bobrec, 2, Color.White);
                DrawRectangleLinesEx(giftrec, 2, Color.White);
            }*/
        }

        public void SpawnRocks()
        {
            int NbCol = MapRead.Width;
            int NbLig = MapRead.Height;

            int TileWidth = 32;
            int TileHeight = 32;

            for (int Col = 0; Col < NbCol; Col++)
            {
                for (int Lig = 0; Lig < NbLig; Lig++)
                {
                    int TileId = MyMapRead.GetTileId(Col, Lig, "SpawnRocks");
                    if (TileId == 320)
                    {
                        float x = (Col * TileWidth) + 16;
                        float y = (Lig * TileHeight) + MapDraw.CameraY;

                        int RockSize = Assets.MyRandom.Next(1, 3); // Taille aléatoire des rochers
                        float RockRotation = Assets.MyRandom.Next(-80, 80); // Rotation aléatoire des rochers                                          
                        float RockSpeed = 100; // Vitesse de chute aléatoire des rochers
                        int Column = Assets.MyRandom.Next(2, 40) * 32; // Colonne aléatoire pour l'apparition des rochers

                        if (RockSize == 1)
                        {
                            Assets Rock = new Assets("RockSmall", MyAssetsManager.MyTexturesManager.GetTexture("assets/RockSmall.png"), Column, (int)y, (int)RockRotation, (int)RockSpeed, 1, 0, false, false, false);
                            MyAssetsManager.Rocks.Add(Rock);
                        }
                        if (RockSize == 2)
                        {
                            Assets Rock = new Assets("RockMedium", MyAssetsManager.MyTexturesManager.GetTexture("assets/RockMedium.png"), Column, (int)y, (int)RockRotation, (int)RockSpeed, 1, 0, false, false, false);
                            MyAssetsManager.Rocks.Add(Rock);
                        }



                        RockFallTimer = Assets.MyRandom.Next(2, 4);
                    }

                    
                }
            }

        }

    }
    
}
