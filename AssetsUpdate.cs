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
        }

        public void Draw()
        {
          /*  foreach (Assets gift in MyAssetsManager.Gifts.ToList())
            {
                Rectangle bobrec = new Rectangle(MyBob.X - MyBob.FrameWidth, MyBob.Y - MyBob.FrameHeight, MyBob.FrameWidth, MyBob.FrameHeight);
                Rectangle giftrec = new Rectangle(gift.AssetX - 15, gift.AssetY - gift.AssetFrameHeight +60 , gift.AssetFrameWidth /2 , gift.AssetFrameHeight / 2);
                DrawRectangleLinesEx(bobrec, 2, Color.White);
                DrawRectangleLinesEx(giftrec, 2, Color.White);
            }*/
        }
    }
    
}
