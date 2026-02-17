using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

using Raylib_cs;
using static Raylib_cs.Raylib;

namespace LADDERS
{
    public class Layer
    {
        public string name { get; set; } = string.Empty;
        public string type { get; set; } = string.Empty;
        public int id { get; set; }
        public List<int> data { get; set; } = new List<int>();
    }

    public class TileSet
    {
        public string image { get; set; } = string.Empty;
        public int imagewidth { get; set; }
        public int imageheight { get; set; }
        public int tilewidth { get; set; }
        public int tileheight { get; set; }
        public int tilecount { get; set; }
        public int columns { get; set; }
        public int firstgid { get; set; }
    }

    public class MapJsonDatas
    {
        public int width { get; set; }
        public int height { get; set; }
        public int tilewidth { get; set; }
        public List<Layer> layers { get; set; } = new List<Layer>();
        public List<TileSet> tilesets { get; set; } = new List<TileSet>();
    }
    public class MapRead
    {
        public static int Width { get; set; }
        public static int Height { get; set; }
        public static int TileWidth { get; set; }
        public static List<Layer> Layers { get; set; } = new List<Layer> { };
        public static List<TileSet> TileSets { get; set; } = new List<TileSet> { };
        public static List<Rectangle> TileRec { get; set; } = new List<Rectangle>();
        public static Rectangle tilerec { get; set; }
        MapJsonDatas? DatasMap { get; set; }

        public void LoadDatas(string JsonName)
        {
            try
            {
                string JsonDatas = File.ReadAllText(JsonName);
                DatasMap = JsonSerializer.Deserialize<MapJsonDatas>(JsonDatas);

                if (DatasMap == null)
                {
                    throw new Exception("Contenu du Json illisible : " + JsonName);
                }

                Width = DatasMap.width;
                Height = DatasMap.height;
                TileWidth = DatasMap.tilewidth;
                Layers.AddRange(DatasMap.layers);
                TileSets.AddRange(DatasMap.tilesets);

            }

            catch (Exception e)
            {
                Console.WriteLine("erreur lors du chargement de la carte" + e.Message);
            }
        }

        public void UnloadDatas()
        {
            DatasMap = null;        // Coupe la référence au contenu JSON
            Layers.Clear();         // Vide la liste des calques
            TileSets.Clear();       // Vide la liste des tilesets
            TileRec.Clear();        // Vide la liste des rectangles
        }

        public void Close()
        {
            UnloadDatas();
        }
    }
}
