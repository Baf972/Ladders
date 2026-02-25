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

            foreach (Assets fruit in MyAssetsManager.Fruits.ToList())
            {
                fruit.AssetFrameTimer -= 100 * DeltaTime;
                if (fruit.AssetFrameTimer <= 0)
                {
                    fruit.AssetCurrentFrame++;
                    if (fruit.AssetCurrentFrame >= fruit.AssetFrameCount)
                    {
                       /* if (fruit.AssetName == "GiftExplo")
                            MyAssetsManager.Gifts.Remove(fruit);
                        else*/
                            fruit.AssetCurrentFrame = 0;

                    }
                    fruit.AssetFrameTimer = fruit.AssetNewFrameTimer;
                }
                int frame = fruit.AssetFrameWidth * fruit.AssetCurrentFrame;
                fruit.AssetSourceRec = new Rectangle(frame, 0, fruit.AssetFrameWidth, fruit.AssetFrameHeight);
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
        }


        public void Draw()
        {
            foreach (Assets part in AssetsManager.Instance.LadderParts)
                DrawTexturePro(part.AssetTileSet, part.AssetSourceRec, new Rectangle(part.AssetX, part.AssetY, part.AssetFrameWidth, part.AssetFrameHeight), new Vector2(part.AssetFrameWidth / 2, 0), part.AssetR, Color.White);

            foreach (Assets gift in MyAssetsManager.Gifts)
                DrawTexturePro(gift.AssetTileSet, gift.AssetSourceRec, new Rectangle(gift.AssetX, gift.AssetY - 64, gift.AssetFrameWidth, gift.AssetFrameHeight), new Vector2(gift.AssetFrameWidth / 2, 0), 0, Color.White);

            foreach (Assets fruit in MyAssetsManager.Fruits)
                DrawTexturePro(fruit.AssetTileSet, fruit.AssetSourceRec, new Rectangle(fruit.AssetX, fruit.AssetY - 64, fruit.AssetFrameWidth, fruit.AssetFrameHeight), new Vector2(fruit.AssetFrameWidth / 2, 0), 0, Color.White);

            


            //DrawText(MapDraw.CameraY.ToString(), 10, 10, 20, Color.White);
            //DrawText("List Clouds    " + MyAssetsManager.Clouds.Count.ToString(), 10, 30, 20, Color.White);
            //DrawText("List Rocks    " + MyAssetsManager.Rocks.Count.ToString(), 10, 60, 20, Color.White);
        }
        public void Close()
        {
            
        }
    }
}
