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
        public Rectangle BobRec { get; set; }
        private Assets CloudFront { get; set; }
        private Assets CloudFrontUp { get; set; }

        public AssetsUpdate()
        {
            MyAssetsManager = AssetsManager.Instance;
            MyBob = Bob.Instance;
        }


        public void Update()
        {
            float DeltaTime = GetFrameTime();

            BobStates MyBobState = MyBob.GetCurrentState();

            foreach (Assets gift in MyAssetsManager.Gifts.ToList())
            {
                
                Rectangle bobrec = new Rectangle(MyBob.X - MyBob.FrameWidth, MyBob.Y - MyBob.FrameHeight, MyBob.FrameWidth, MyBob.FrameHeight);
                Rectangle giftrec = new Rectangle(gift.AssetX - 15, gift.AssetY - gift.AssetFrameHeight + 60, gift.AssetFrameWidth / 2, gift.AssetFrameHeight / 2);

                if (CheckCollisionRecs(bobrec, giftrec))
                {
                    MyAssetsManager.Gifts.Remove(gift);
                    Assets giftExplo = new Assets("GiftExplo", MyAssetsManager.MyTexturesManager.GetTexture("assets/GiftExplo.png"), (int)gift.AssetX, (int)gift.AssetY-20, (int)gift.AssetR, 0 , 17, 5, false, false, false);
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


                Rectangle fruittrec = new Rectangle(fruit.AssetX - 15, fruit.AssetY - fruit.AssetFrameHeight / 2 - 20, fruit.AssetFrameWidth / 2, fruit.AssetFrameHeight / 2 + 40);

                if (CheckCollisionRecs(BobRec, fruittrec))
                {
                    MyAssetsManager.Fruits.Remove(fruit);
                    //Assets fruitExplo = new Assets("GiftExplo", MyAssetsManager.MyTexturesManager.GetTexture("assets/GiftExplo.png"), (int)fruit.AssetX, (int)fruit.AssetY - 20, (int)fruit.AssetR, 0, 17, 5, false, false, false);
                    //MyAssetsManager.Fruits.Add(fruitExplo);
                }

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
           /* foreach (Assets fruit in MyAssetsManager.Fruits.ToList())
            {
                //Rectangle bobrec = new Rectangle(MyBob.X - MyBob.FrameWidth, MyBob.Y - MyBob.FrameHeight, MyBob.FrameWidth, MyBob.FrameHeight);
                Rectangle fruittrec = new Rectangle(fruit.AssetX - 15, fruit.AssetY - fruit.AssetFrameHeight /2 -20 , fruit.AssetFrameWidth /2 , fruit.AssetFrameHeight /2 + 40);
                //DrawRectangleLinesEx(bobrec, 2, Color.White);
                DrawRectangleLinesEx(fruittrec, 2, Color.White);
            }
            DrawRectangleLinesEx(BobRec, 2, Color.White);
            /*foreach (Assets gift in MyAssetsManager.Gifts.ToList())
            {
                Rectangle bobrec = new Rectangle(MyBob.X - MyBob.FrameWidth, MyBob.Y - MyBob.FrameHeight, MyBob.FrameWidth, MyBob.FrameHeight);
                Rectangle giftrec = new Rectangle(gift.AssetX - 15, gift.AssetY - gift.AssetFrameHeight + 60, gift.AssetFrameWidth / 2, gift.AssetFrameHeight / 2);
                DrawRectangleLinesEx(bobrec, 2, Color.White);
                DrawRectangleLinesEx(giftrec, 2, Color.White);
            }*/
        }
    }
    
}
