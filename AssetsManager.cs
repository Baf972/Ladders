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

        public TextureManager MyTexturesManager;

        public List<Assets> LadderParts;

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
                {"BackGround", MyTexturesManager.GetTexture("assets/BackGround.png") },
                {"LadderPart1", MyTexturesManager.GetTexture("assets/LadderParts.png") },


            };

            LadderParts = new List<Assets>();
        }


    }
}
