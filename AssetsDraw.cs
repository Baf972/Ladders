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
        public AssetsDraw()
        {
            MyAssetsManager = AssetsManager.Instance;
            MyBob = Bob.Instance;
            // Initialize any necessary resources here
        }

        public void Update()
        {
            float DeltaTime = GetFrameTime();
            


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
                
                if (gift.AssetName == "GiftExplo")
                {

                }
                gift.AssetFrameTimer -= 100* DeltaTime;
                if (gift.AssetFrameTimer <= 0)
                {
                    gift.AssetCurrentFrame++;
                    if (gift.AssetCurrentFrame >= gift.AssetFrameCount)
                    {
                        if (gift.AssetName == "GiftExplo")
                            MyAssetsManager.Gifts.Remove(gift);
                        else                        
                            gift.AssetCurrentFrame = 0;
                        
                    }
                    gift.AssetFrameTimer = gift.AssetNewFrameTimer;
                }
                int frame = gift.AssetFrameWidth * gift.AssetCurrentFrame;
                gift.AssetSourceRec = new Rectangle(frame, 0, gift.AssetFrameWidth, gift.AssetFrameHeight);
            }          
        }

        public void Draw()
        {
            foreach (Assets part in AssetsManager.Instance.LadderParts)
                DrawTexturePro(part.AssetTileSet, part.AssetSourceRec, new Rectangle(part.AssetX, part.AssetY, part.AssetFrameWidth, part.AssetFrameHeight), new Vector2(part.AssetFrameWidth / 2, 0), part.AssetR, Color.White);

            foreach (Assets gift in MyAssetsManager.Gifts)
                DrawTexturePro(gift.AssetTileSet, gift.AssetSourceRec, new Rectangle(gift.AssetX, gift.AssetY - 64, gift.AssetFrameWidth, gift.AssetFrameHeight), new Vector2(gift.AssetFrameWidth / 2, 0), 0, Color.White);

            //DrawText("Parts  " + MyAssetsManager.LadderParts.Count.ToString(), 40, 40, 30, Color.White);
        }
        public void Close()
        {
            // Clean up any resources if necessary
        }
    }
}
