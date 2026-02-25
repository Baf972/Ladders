using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Raylib_cs;
using static Raylib_cs.Raylib;



namespace LADDERS
{
    public class TextureManager
    {
        public Dictionary<string, Texture2D> AssetsTextures = new Dictionary<string, Texture2D>();

        public Texture2D GetTexture(string filePath)
        {
            if (!AssetsTextures.ContainsKey(filePath))
            {
                AssetsTextures[filePath] = LoadTexture(filePath);
            }
            return AssetsTextures[filePath];
        }
    }
    public class AssetsManager
    {

        private static AssetsManager? instance;
        public static AssetsManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new AssetsManager();
                }
                return instance;
            }
        }

        public List<Assets> LadderParts;
        public List<Assets> Gifts;
        public List<Assets> Fruits;
        public List<Assets> Clouds;
        public List<Assets> Rocks;
        public TextureManager MyTexturesManager;


        public void Init()
        {
            MyTexturesManager = new TextureManager();
            InitializedTexture();
        }
        public AssetsManager()
        {
            Init();
        }

        private void InitializedTexture()
        {
            var MyTextures = new Dictionary<string, Texture2D>
            {

                {"Idle", MyTexturesManager.GetTexture("assets/bobIdle.png") },
                { "Climbing", MyTexturesManager.GetTexture("assets/bobClimbing.png")},
                {"Jumping", MyTexturesManager.GetTexture("assets/bobJumping.png")},
                { "Falling", MyTexturesManager.GetTexture("assets/bobFalling.png")},
                { "RunningUp", MyTexturesManager.GetTexture("assets/bobRunningUp.png")},
                {"Landing", MyTexturesManager.GetTexture("assets/bobLanding.png")},
                { "BackGround", MyTexturesManager.GetTexture("assets/BackGround.png") },
                {"BackGround2", MyTexturesManager.GetTexture("assets/BackGround2.png") },
                {"LadderPart1", MyTexturesManager.GetTexture("assets/LadderParts.png") },
                {"Gift", MyTexturesManager.GetTexture("assets/Gift.png")},
                {"GiftExplo", MyTexturesManager.GetTexture("assets/GiftExplo.png")},
                {"Fruit", MyTexturesManager.GetTexture("assets/Fruits.png")},
                {"CloudFrontUp", MyTexturesManager.GetTexture("assets/CloudsFrontUp.png")},
                {"CloudFront", MyTexturesManager.GetTexture("assets/CloudsFront.png")},
                {"RockBig", MyTexturesManager.GetTexture("assets/RockBig.png")},
                {"RockMedium", MyTexturesManager.GetTexture("assets/RockMedium.png")},
                {"RockSmall", MyTexturesManager.GetTexture("assets/RockSmall.png")},
                {"RockVerySmall1", MyTexturesManager.GetTexture("assets/RockVerySmall1.png")},
                {"RockVerySmall2", MyTexturesManager.GetTexture("assets/RockVerySmall2.png")},


            };

            LadderParts = new List<Assets>();
            Gifts = new List<Assets>();
            Fruits = new List<Assets>();
            Clouds = new List<Assets>();
            Rocks = new List<Assets>();

        }
        public void Unloadtextures()
        {
            foreach (var texture in MyTexturesManager.AssetsTextures.Values)
            {
                UnloadTexture(texture);
            }
            MyTexturesManager.AssetsTextures.Clear();
        }

        public void UnloadAssets()
        {
            Unloadtextures();
            LadderParts.Clear();
            Gifts.Clear();
            Fruits.Clear();
            Clouds.Clear();
            Rocks.Clear();
        }

        public void Close()
        {
            UnloadAssets();
        }
    }
}
