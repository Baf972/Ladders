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
        private float HurtTimerSmall { get; set; }
        private float HurtTimerMedium { get; set; }
        private float HurtTimerVerySmall1 { get; set; }
        private float HurtTimerVerySmall2 { get; set; }
        private float HurtTimerVerySmall3{ get; set; }
        private float HurtTimerVerySmall4{ get; set; }
        private float HurtTimerVerySmall5{ get; set; }
        private float RockColorTimer { get; set; }


        public AssetsUpdate()
        {
            MyAssetsManager = AssetsManager.Instance;
            MyMapRead = new MapRead();
            MyBob = Bob.Instance;
            RockFallTimer = 0;
            //RockColorTimer = 2;
            HurtTimerSmall = 1f;
            HurtTimerMedium = 1f;
            HurtTimerVerySmall1 = 1f;
            HurtTimerVerySmall2 = 1f;
            HurtTimerVerySmall3 = 1f;
            HurtTimerVerySmall4 = 1f;
        }


        public void Update()
        {
            float DeltaTime = GetFrameTime();

            // BobRec -> bobStates
            BobStates MyBobState = MyBob.GetCurrentState();

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


            foreach (Assets gift in MyAssetsManager.Gifts.ToList())
            {
                Rectangle giftrec = new Rectangle(gift.AssetX - 15, gift.AssetY - gift.AssetFrameHeight / 2 + 10, gift.AssetFrameWidth / 2, gift.AssetFrameHeight / 2);

                if ( gift.AssetName == "Gift" && CheckCollisionRecs(BobRec, giftrec))
                {
                    MyAssetsManager.Gifts.Remove(gift);
                    Assets giftExplo = new Assets("GiftExplo", MyAssetsManager.MyTexturesManager.GetTexture("assets/GiftExplo.png"), (int)gift.AssetX, (int)gift.AssetY-10, (int)gift.AssetR, 0 , 17, 5, false, false, false);
                    MyAssetsManager.Gifts.Add(giftExplo);
                }

            }

            foreach (Assets fruit in MyAssetsManager.Fruits.ToList())
            {
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
                    SpawnRocks();
            }


            RockColorTimer -= DeltaTime;

            // Update des rochers
            foreach (Assets rock in MyAssetsManager.Rocks.ToList())
            {

                // Deplacement et Rotation des rochers
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
                    
                else if (rock.AssetSpeedR < 0)
                {
                    rock.AssetSpeedR -= 100 * DeltaTime;
                    if (rock.AssetSpeedR < 5)
                    {
                        rock.AssetSpeedR = - 5;
                        rock.AssetR += rock.AssetSpeedR;
                    }
                }

                
                // Collision avec Bob    


                if (rock.AssetName == "RockVerySmall1")
                {
                    Rectangle rockrec = new Rectangle(rock.AssetX + rock.AssetFrameWidth * 2, rock.AssetY - rock.AssetFrameHeight, rock.AssetFrameWidth, rock.AssetFrameHeight);
                    if (CheckCollisionRecs(BobRec, rockrec) && !rock.AssetActiv)
                    {
                        rock.AssetActiv = true;
                        MyBob.IsHurt = true;
                    }
                    if (rock.AssetActiv)
                    {
                        HurtTimerVerySmall1 -= DeltaTime;
                        rock.AssetSpeedX -= rock.AssetVelocity * DeltaTime;
                        if (rock.AssetSpeedX <= 0)
                            rock.AssetSpeedX = 0;
                        rock.AssetX -= (int)rock.AssetSpeedX * DeltaTime;

                        if (HurtTimerVerySmall1 <= 0)
                        {
                            rock.AssetActiv = false;
                            HurtTimerVerySmall1 = 1f;
                            MyBob.IsHurt = false;
                        }
                    }
                    
                }
                if (rock.AssetName == "RockVerySmall2")
                {
                    Rectangle rockrec = new Rectangle(rock.AssetX + rock.AssetFrameWidth * 2, rock.AssetY - rock.AssetFrameHeight, rock.AssetFrameWidth, rock.AssetFrameHeight);
                    if (CheckCollisionRecs(BobRec, rockrec) && !rock.AssetActiv)
                    {
                        rock.AssetActiv = true;
                        MyBob.IsHurt = true;
                    }
                    if (rock.AssetActiv)
                    {
                        HurtTimerVerySmall1 -= DeltaTime;
                        rock.AssetSpeedX -= rock.AssetVelocity * DeltaTime;
                        if (rock.AssetSpeedX <= 0)
                            rock.AssetSpeedX = 0;
                        rock.AssetX += (int)rock.AssetSpeedX * DeltaTime;

                        if (HurtTimerVerySmall1 <= 0)
                        {
                            rock.AssetActiv = false;
                            HurtTimerVerySmall1 = 1f;
                            MyBob.IsHurt = false;
                        }
                    }

                }
                if (rock.AssetName == "RockVerySmall3")
                {
                    Rectangle rockrec = new Rectangle(rock.AssetX + rock.AssetFrameWidth * 2, rock.AssetY - rock.AssetFrameHeight, rock.AssetFrameWidth, rock.AssetFrameHeight);
                    if (CheckCollisionRecs(BobRec, rockrec) && !rock.AssetActiv)
                    {
                        rock.AssetActiv = true;
                        MyBob.IsHurt = true;
                    }
                    if (rock.AssetActiv)
                    {
                        HurtTimerVerySmall1 -= DeltaTime;
                        rock.AssetSpeedX -= rock.AssetVelocity * DeltaTime;
                        if (rock.AssetSpeedX <= 0)
                            rock.AssetSpeedX = 0;
                        rock.AssetX -= (int)rock.AssetSpeedX * DeltaTime;

                        if (HurtTimerVerySmall1 <= 0)
                        {
                            rock.AssetActiv = false;
                            HurtTimerVerySmall1 = 1f;
                            MyBob.IsHurt = false;
                        }
                    }

                }
                if (rock.AssetName == "RockVerySmall4")
                {
                    Rectangle rockrec = new Rectangle(rock.AssetX + rock.AssetFrameWidth * 2, rock.AssetY - rock.AssetFrameHeight, rock.AssetFrameWidth, rock.AssetFrameHeight);
                    if (CheckCollisionRecs(BobRec, rockrec)  && !rock.AssetActiv)
                    {
                        rock.AssetActiv = true;
                        MyBob.IsHurt = true;
                    }
                    if (rock.AssetActiv)
                    {
                        HurtTimerVerySmall1 -= DeltaTime;
                        rock.AssetSpeedX -= rock.AssetVelocity * DeltaTime;
                        if (rock.AssetSpeedX <= 0)
                            rock.AssetSpeedX = 0;
                        rock.AssetX += (int)rock.AssetSpeedX * DeltaTime;

                        if (HurtTimerVerySmall1 <= 0)
                        {
                            rock.AssetActiv = false;
                            HurtTimerVerySmall1 = 1f;
                            MyBob.IsHurt = false;
                        }
                    }

                }
                if (rock.AssetName == "RockSmall")
                {
                    Rectangle rockrec = new Rectangle(rock.AssetX + rock.AssetFrameWidth / 2, rock.AssetY - rock.AssetFrameHeight / 2, rock.AssetFrameWidth, rock.AssetFrameHeight);
                    if (CheckCollisionRecs(BobRec, rockrec) && !rock.AssetActiv)
                    {
                        rock.AssetActiv = true;
                        MyBob.IsHurt = true;
                    }
                    if (rock.AssetActiv)
                    {
                        HurtTimerVerySmall1 -= DeltaTime;
                        rock.AssetSpeedX -= rock.AssetVelocity * DeltaTime;
                        if (rock.AssetSpeedX <= 0)
                            rock.AssetSpeedX = 0;
                        rock.AssetX -= (int)rock.AssetSpeedX * DeltaTime;

                        if (HurtTimerVerySmall1 <= 0)
                        {
                            rock.AssetActiv = false;
                            HurtTimerVerySmall1 = 1f;
                            MyBob.IsHurt = false;
                        }
                    }

                }
                if (rock.AssetName == "RockMedium")
                {
                    Rectangle rockrec = new Rectangle(rock.AssetX, rock.AssetY - rock.AssetFrameHeight / 2, rock.AssetFrameWidth, rock.AssetFrameHeight);
                    if (CheckCollisionRecs(BobRec, rockrec) && !rock.AssetActiv)
                    {
                        rock.AssetActiv = true;
                        MyBob.IsHurt = true;
                    }
                    if (rock.AssetActiv)
                    {
                        HurtTimerVerySmall1 -= DeltaTime;
                        rock.AssetSpeedX -= rock.AssetVelocity * DeltaTime;
                        if (rock.AssetSpeedX <= 0)
                            rock.AssetSpeedX = 0;
                        rock.AssetX += (int)rock.AssetSpeedX * DeltaTime;

                        if (HurtTimerVerySmall1 <= 0)
                        {
                            rock.AssetActiv = false;
                            HurtTimerVerySmall1 = 1f;
                            MyBob.IsHurt = false;
                        }
                    }

                }


                
                /*// Le rocher clignote rouge (Danger)
                if ((rock.AssetName == "RockMedium" || rock.AssetName == "RockSmall") && RockColorTimer >= 1)
                    rock.AssetActiv = false;

                if ((rock.AssetName == "RockMedium" || rock.AssetName == "RockSmall") && RockColorTimer < 1)
                    rock.AssetActiv = true;

                if (RockColorTimer <= 0)
                    RockColorTimer = 2;*/


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


             /*foreach (Assets rock in MyAssetsManager.Rocks.ToList())
             {
                 if (rock.AssetName == "RockVerySmall2" )
                 {
                     Rectangle rockrec = new Rectangle(rock.AssetX + rock.AssetFrameWidth * 2, rock.AssetY - rock.AssetFrameHeight , rock.AssetFrameWidth, rock.AssetFrameHeight);
                     //DrawRectangleLinesEx(BobRec, 2, Color.White);
                     DrawRectangleLinesEx(rockrec, 2, Color.White);
                 }
                 if (rock.AssetName == "RockVerySmall2")
                 {
                     //BobRec = new Rectangle(MyBob.X - MyBob.FrameWidth, MyBob.Y - MyBob.FrameHeight, MyBob.FrameWidth, MyBob.FrameHeight);
                      Rectangle rockrec = new Rectangle(rock.AssetX + rock.AssetFrameWidth, rock.AssetY - rock.AssetFrameHeight / 2, rock.AssetFrameWidth, rock.AssetFrameHeight);
                     //DrawRectangleLinesEx(BobRec, 2, Color.White);
                     DrawRectangleLinesEx(rockrec, 2, Color.White);
                 }

            }*/
             DrawText("MyBobHurt  " + MyBob.IsHurt.ToString(), 40, 80, 30, Color.White) ;
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

                        int RockSize = Assets.MyRandom.Next(1, 4); // Taille aléatoire des rochers
                        float RockRotation = Assets.MyRandom.Next(-80, 80); // Rotation aléatoire des rochers                                          
                        float RockSpeed = 100; // Vitesse de chute aléatoire des rochers
                        
                        int Column = Assets.MyRandom.Next(2, 40) * 32; // Colonne aléatoire pour l'apparition des rochers
                        int Column2 = Assets.MyRandom.Next(2, 40) * 32; // Colonne aléatoire pour l'apparition des rochers
                        int RockVerySmallY = Assets.MyRandom.Next(40, 70); // Position Y aléatoire pour les petits rochers
                        int RockVerySmallY2 = Assets.MyRandom.Next(40, 70); // Position Y aléatoire pour les petits rochers
                        int RockVerySmallX = Assets.MyRandom.Next(-20, 20); // Position X aléatoire pour les petits rochers
                        int RockVerySmallX2 = Assets.MyRandom.Next(-20, 20); // Position X aléatoire pour les petits rochers

                        if (RockSize == 1)
                        {
                            Assets RockSmall = new Assets("RockSmall", MyAssetsManager.MyTexturesManager.GetTexture("assets/RockSmall.png"), Column, (int)y, (int)RockRotation, (int)RockSpeed, 1, 0, false, false, false);
                            RockSmall.AssetSpeedX = 300;
                            MyAssetsManager.Rocks.Add(RockSmall);

                            Assets RockVerySmall1 = new Assets("RockVerySmall1", MyAssetsManager.MyTexturesManager.GetTexture("assets/RockVerySmall2.png"), Column - RockVerySmallX, (int)y - RockVerySmallY, (int)RockRotation, (int)RockSpeed, 1, 0, false, false, false);
                            RockVerySmall1.AssetSpeedX = 300;
                            MyAssetsManager.Rocks.Add(RockVerySmall1);   
                        }

                        if (RockSize == 2)
                        {
                            Assets Rock = new Assets("RockMedium", MyAssetsManager.MyTexturesManager.GetTexture("assets/RockMedium.png"), Column, (int)y, (int)RockRotation, (int)RockSpeed, 1, 0, false, false, false);
                            Rock.AssetSpeedX = 300;
                            MyAssetsManager.Rocks.Add(Rock);

                            Assets RockVerySmall2 = new Assets("RockVerySmall2", MyAssetsManager.MyTexturesManager.GetTexture("assets/RockVerySmall1.png"), Column - RockVerySmallX, (int)y - RockVerySmallY2, (int)RockRotation, (int)RockSpeed, 1, 0, false, false, false);
                            RockVerySmall2.AssetSpeedX = 300;
                            MyAssetsManager.Rocks.Add(RockVerySmall2);
                            
                        }
                        
                        Assets RockVerySmall3 = new Assets("RockVerySmall3", MyAssetsManager.MyTexturesManager.GetTexture("assets/RockVerySmall1.png"), Column, (int)y, (int)RockRotation, (int)RockSpeed, 1, 0, false, false, false);
                        RockVerySmall3.AssetSpeedX = 300;
                        MyAssetsManager.Rocks.Add(RockVerySmall3);

                        Assets RockVerySmall4 = new Assets("RockVerySmall4", MyAssetsManager.MyTexturesManager.GetTexture("assets/RockVerySmall1.png"), Column2, (int)y, (int)RockRotation, (int)RockSpeed, 1, 0, false, false, false);
                        RockVerySmall4.AssetSpeedX = 300;
                        MyAssetsManager.Rocks.Add(RockVerySmall4);

                        RockFallTimer = Assets.MyRandom.Next(1, 3);
                    }

                    
                }
            }

        }

    }
    
}
