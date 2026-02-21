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
            // Initialize any necessary resources here
        }

        public void Update()
        {
            float DeltaTime = GetFrameTime();

            foreach (Assets part in AssetsManager.Instance.LadderParts)
            {
                part.AssetCurrentFrame = 1;

                part.AssetY += part.AssetSpeed * DeltaTime;
                part.AssetR += part.AssetR * DeltaTime;

               


                int frame = part.AssetFrameWidth * part.AssetCurrentFrame;
                part.AssetSourceRec = new Rectangle(frame, 0, part.AssetFrameWidth, part.AssetFrameHeight);
            }
            

        }

        public void Draw()
        {
            foreach (Assets part in AssetsManager.Instance.LadderParts)
            {
                DrawTexturePro(part.AssetTileSet, part.AssetSourceRec, new Rectangle(part.AssetX, part.AssetY, part.AssetFrameWidth, part.AssetFrameHeight), new Vector2(part.AssetFrameWidth / 2, 0), part.AssetR, Color.White);
            }
        }   
        public void Close()
        {
            // Clean up any resources if necessary
        }
    }
}
