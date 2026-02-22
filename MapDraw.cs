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
    public class MapDraw
    {

        private MapRead MyMapRead;
        private Dictionary<int, List<Rectangle>> RectanglesTilesets;
        private Dictionary<int, TileSet> TileTilesets;
        private Dictionary<int, Texture2D> TexturesTileset;
        private AssetsManager MyAssetsManager;
        private Bob MyBob { get; set; }

        private Texture2D BackGround;
        public static Vector2 BackGroundPos;
        private Texture2D BackGround2;
        public static Vector2 BackGround2Pos;

        public static  float CameraY;
        private float CameraSpeed = 5f;


        public MapDraw(MapRead map, string assetsPath)
        {

            MyMapRead = map;
            MyAssetsManager = AssetsManager.Instance;
            RectanglesTilesets = new Dictionary<int, List<Rectangle>>();
            TexturesTileset = new Dictionary<int, Texture2D>();
            TileTilesets = new Dictionary<int, TileSet>();
            
            MyBob = Bob.Instance;
            BackGround = MyAssetsManager.MyTexturesManager.GetTexture("assets/BackGround.png");
            BackGround2 = MyAssetsManager.MyTexturesManager.GetTexture("assets/BackGround2.png");
            BackGroundPos =  new Vector2(0, 400);
            BackGround2Pos =  new Vector2(0, -420); 

            CameraY = -2110;
            CameraSpeed = MyBob.SpeedClimb;


            InitRectTilesets();
            LoadTextTilesets(assetsPath);
            AssoTilesTilesets();
        }

        private void InitRectTilesets()
        {
            foreach (TileSet tileset in MapRead.TileSets)
            {
                List<Rectangle> rectangles = new List<Rectangle>();
                int tileSize = tileset.tilewidth;
                int nbColumns = tileset.columns;

                for (int i = 0; i < tileset.tilecount; i++)
                {
                    int col = i % nbColumns;
                    int line = i / nbColumns;

                    Rectangle rect = new Rectangle(col * tileSize, line * tileSize, tileSize, tileSize);
                    rectangles.Add(rect);
                }
                RectanglesTilesets[tileset.firstgid] = rectangles;
            }
        }

        private void LoadTextTilesets(string assetsPath)
        {
            foreach (TileSet tileset in MapRead.TileSets)
            {
                string path = System.IO.Path.Combine(assetsPath, tileset.image);
                Texture2D texture = LoadTexture(path);
                TexturesTileset[tileset.firstgid] = texture;
            }
        }

        private void AssoTilesTilesets()
        {
            foreach (TileSet tileset in MapRead.TileSets)
            {
                for (int i = 0; i < tileset.tilecount; i++)
                {
                    int idTile = tileset.firstgid + i;
                    TileTilesets[idTile] = tileset;
                }
            }
        }

        public void Update()
        {
                       
            

        }

        public void DrawLayer(Layer layer, int tileSize)
        {            
            tileSize = MapRead.TileWidth;
            for (int y = 0; y < MapRead.Height; y++)
            {
                for (int x = 0; x < MapRead.Width; x++)
                {
                    
                    if (layer.type != "tilelayer")
                    {
                        continue;
                    }

                    int index = y * MapRead.Width + x;
                    int idTile = layer.data[index];
                    if (idTile == 0)
                    {
                        continue;
                    }

                    TileSet tileset = TileTilesets[idTile];
                    int firstgid = tileset.firstgid;
                    Texture2D texture = TexturesTileset[firstgid];
                    int realId = idTile - firstgid;
                    Rectangle rect = RectanglesTilesets[firstgid][realId];
                    Vector2 pos = new Vector2(x * tileSize, (y * tileSize) + CameraY);
                    
                    if (pos.X > -32 && pos.X < GetScreenWidth() && pos.Y > -32 && pos.Y < GetScreenHeight())
                        DrawTextureRec(texture, rect, pos, Color.White);
                                        
                }
            }
        }
        public void Draw()
        {
            int tileSize = MapRead.TileWidth;

            DrawTexture(BackGround,(int) BackGroundPos.X, (int)(BackGroundPos.Y + (CameraY /2)), Color.White);
            DrawTexture(BackGround2,(int) BackGround2Pos.X, (int)(BackGround2Pos.Y + (CameraY)), Color.White);
                
            foreach (Layer layer in MapRead.Layers)
            {
                if (layer.name == "Fond2")
                {
                    DrawLayer(layer, tileSize);
                }
            }
            foreach (Layer layer in MapRead.Layers)
            {
                if (layer.name == "Fond3")
                {
                    DrawLayer(layer, tileSize);
                }
            }
            foreach (Layer layer in MapRead.Layers)
            {
                if (layer.name == "Fond4")
                {
                    DrawLayer(layer, tileSize);
                }
            }
            foreach (Layer layer in MapRead.Layers)
            {
                if (layer.name == "Fond5")
                {
                    DrawLayer(layer, tileSize);
                }
            }
            foreach (Layer layer in MapRead.Layers)
            {
                if (layer.name == "Montain")
                {
                    DrawLayer(layer, tileSize);
                }
            }
            foreach (Layer layer in MapRead.Layers)
            {
                if (layer.name == "Ground")
                {
                    DrawLayer(layer, tileSize);
                }
            }
            foreach (Layer layer in MapRead.Layers)
            {
                if (layer.name == "Ladders")
                {
                    DrawLayer(layer, tileSize);
                }
            }
        }

        public void UnloadDatas()
        {
            // Décharge explicitement toutes les textures Raylib
            foreach (var texture in TexturesTileset.Values)
            {
                UnloadTexture(texture);
            }
            TexturesTileset.Clear();

            // Vider les autres dictionnaires (pas besoin de faire autre chose)
            RectanglesTilesets.Clear();
            TileTilesets.Clear();

        }

        public void Close()
        {
            UnloadDatas();

        }


    }
}
