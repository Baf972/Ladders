using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Raylib_cs;
using static Raylib_cs.Raylib;

namespace LADDERS
{
    public class Program
    {
        static GameStates? MyGameState;

        static GameMenu? MyGameMenu = new GameMenu();
        static GameOptions? MyGameOptions = new GameOptions();
        static GamePlay? MyGamePlay = new GamePlay();
        public static int Main()
        {
            

            InitWindow(1280, 720, "LADDERS");
            InitAudioDevice();
            SetTargetFPS(60);
            SetExitKey(KeyboardKey.Null);
            Texture2D texture = LoadTexture("assets/Tileset1.png");

            MyGameState = GameStates.Instance;
            MyGameState.AddScene("menu", MyGameMenu);
            MyGameState.AddScene("options", MyGameOptions);
            MyGameState.AddScene("gamePlay", MyGamePlay);

            MyGameState.ChangeScene("menu");

            while (!WindowShouldClose() && !MyGameState.QuitMyGame)
            {
                MyGameState.Update();
                BeginDrawing();
                ClearBackground(Color.Black);

                MyGameState.Draw();
                DrawTexture(texture, 0, 0, Color.White);
                EndDrawing();
            }
           
            MyGameState.Close();
            CloseAudioDevice();
            CloseWindow();
            return 0;
        }
    }
}
