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
        public TextureManager MyTexturesManager;

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
                {"tileSet", MyTexturesManager.GetTexture("assets/bobIdle.png") },
            };
        }


    }
}
