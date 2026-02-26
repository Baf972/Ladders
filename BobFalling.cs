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
    public class BobFalling : BobStatesManager
    {
        public BobFalling(Bob MyBob) : base(MyBob)
        {
        }
        public override void HandleInput(Bob MyBob)
        {
            base.HandleInput(MyBob);
        }
        public override void Update(Bob MyBob)
        {

            BreakLoop = true;
            if (MapDraw.CameraY > -1000)
            {
                foreach (Assets cloud in MyAssetsManager.Clouds)
                    cloud.AssetY -= MyBob.SpeedFallDown * DeltaTime;
            }
            // Bob Tombe, camera suit
            if (MapDraw.CameraY > -2160)
            {
                MapDraw.CameraY -= MyBob.SpeedFallDown * DeltaTime;

                foreach (Assets gift in MyAssetsManager.Gifts)
                    gift.AssetY -= MyBob.SpeedFallDown * DeltaTime;

                foreach (Assets fruit in MyAssetsManager.Fruits)
                    fruit.AssetY -= MyBob.SpeedFallDown * DeltaTime;

                foreach (Assets rock in MyAssetsManager.Rocks)
                    rock.AssetY -= MyBob.SpeedFallDown * DeltaTime;

                foreach (Assets endurance in MyAssetsManager.Endurance)
                    endurance.AssetY -= MyBob.SpeedFallDown * DeltaTime;

                float AbsolutBobY = MyBob.Y + Math.Abs(MapDraw.CameraY);
                int tileCol = (int)(MyBob.X / MapRead.TileWidth);
                int tileLig = (int)(AbsolutBobY / MapRead.TileWidth);

                // Si Bob tombe sur une échelle en métal, il s'arrête
                int MetalTileId = MyMapRead.GetTileId(tileCol - 1, tileLig - 1, "Ladders");
                if (MetalTileId == 1312)
                {
                    MyBob.StatesTransition(BobStates.Landing);
                    MyBob.Y += 32;
                }

                // Si Bob rencontre une échelle en bois, elle se casse
                LadderPartsTimer -= DeltaTime;
                if (LadderPartsTimer <= 0)
                {
                    if (MetalTileId != 1312 && MetalTileId != 0)
                    {
                        MyMapRead.ModifyTile(tileCol - 1, tileLig - 2, "Ladders", 0);
                        LadderPartsTimer = 0.1f;
                        BreakLadder = true;

                    }
                }


                if (BreakLadder)
                {                

                        Assets LadderPart1 = new Assets("Rung1", MyAssetsManager.MyTexturesManager.GetTexture("assets/Rung1.png"), (int)MyBob.X + 20, (int)MyBob.Y, 0, 0, 1, 1, false, false, false);
                        LadderPart1.AssetR = Assets.MyRandom.Next(-50, +50);
                        LadderPart1.AssetSpeed = Assets.MyRandom.Next(120, 180);

                        Assets LadderPart2 = new Assets("Rung2", MyAssetsManager.MyTexturesManager.GetTexture("assets/Rung2.png"), (int)MyBob.X - 30, (int)MyBob.Y, 0, 0, 1, 1, false, false, false);
                        LadderPart2.AssetR = Assets.MyRandom.Next(-50, +50);
                        LadderPart2.AssetSpeed = Assets.MyRandom.Next(120, 180);

                        Assets LadderPart3 = new Assets("Rung3", MyAssetsManager.MyTexturesManager.GetTexture("assets/Rung3.png"), (int)MyBob.X + 15, (int)MyBob.Y, 0, 0, 1, 1, false, false, false);
                        LadderPart3.AssetR = Assets.MyRandom.Next(-50, +50);
                        LadderPart3.AssetSpeed = Assets.MyRandom.Next(120, 180);

                        Assets LadderPart4 = new Assets("Rung4", MyAssetsManager.MyTexturesManager.GetTexture("assets/Rung4.png"), (int)MyBob.X - 25, (int)MyBob.Y, 0, 0, 1, 1, false, false, false);
                        LadderPart4.AssetR = Assets.MyRandom.Next(-50, +50);
                        LadderPart4.AssetSpeed = Assets.MyRandom.Next(120, 180);

                        Assets LadderPart5 = new Assets("Stack1", MyAssetsManager.MyTexturesManager.GetTexture("assets/Stack1.png"), (int)MyBob.X + 20, (int)MyBob.Y, 0, 0, 1, 1, false, false, false);
                        LadderPart5.AssetR = Assets.MyRandom.Next(-50, +50);
                        LadderPart5.AssetSpeed = Assets.MyRandom.Next(120, 180);

                        Assets LadderPart6 = new Assets("Stack2", MyAssetsManager.MyTexturesManager.GetTexture("assets/Stack2.png"), (int)MyBob.X - 35, (int)MyBob.Y, 0, 0, 1, 1, false, false, false);
                        LadderPart6.AssetR = Assets.MyRandom.Next(-50, +50);
                        LadderPart6.AssetSpeed = Assets.MyRandom.Next(120, 180);

                        MyAssetsManager.LadderParts.Add(LadderPart1);
                        MyAssetsManager.LadderParts.Add(LadderPart2);
                        MyAssetsManager.LadderParts.Add(LadderPart3);
                        MyAssetsManager.LadderParts.Add(LadderPart4);
                        MyAssetsManager.LadderParts.Add(LadderPart5);
                        MyAssetsManager.LadderParts.Add(LadderPart6);

                        BreakLadder = false;
                
                }
            }

            if (MapDraw.CameraY <= -2160)
            {
                MapDraw.CameraY = -2160;
                MapDraw.BackGround2Pos.Y = -420;
                MyBob.Y += MyBob.SpeedFallDown * DeltaTime;
            }


            base.Update(MyBob);
        }
        public override void Draw(Bob MyBob)
        {
            if (MyBob.IsHurt)
                DrawTexturePro(MyBob.TileSet, MyBob.BobSourceRec, new Rectangle(MyBob.X, MyBob.Y, MyBob.FrameWidth, MyBob.FrameHeight), new Vector2(MyBob.FrameWidth, MyBob.FrameHeight), MyBob.R, Color.Red);
            else 
                DrawTexturePro(MyBob.TileSet, MyBob.BobSourceRec, new Rectangle(MyBob.X, MyBob.Y, MyBob.FrameWidth, MyBob.FrameHeight), new Vector2(MyBob.FrameWidth, MyBob.FrameHeight), MyBob.R, Color.White);
            base.Draw(MyBob);
        }
    }
}
