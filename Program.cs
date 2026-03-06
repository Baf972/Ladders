using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
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
        static Music GameTheme;
        public static int Main()
        {
            int gameWidth = 1280;
            int gameHeight = 720;

            InitWindow(gameWidth, gameHeight, "LADDERS");
            InitAudioDevice();
            SetTargetFPS(60);
            SetExitKey(KeyboardKey.Null);            

            MyGameState = GameStates.Instance;
            MyGameState.AddScene("menu", MyGameMenu);
            MyGameState.AddScene("options", MyGameOptions);
            MyGameState.AddScene("gamePlay", MyGamePlay);

            MyGameState.ChangeScene("menu");


            GameTheme = LoadMusicStream("assets/sounds/LaddersTheme.mp3");
            SetMusicVolume(GameTheme, 0.1f);
            GameTheme.Looping = true;
            PlayMusicStream(GameTheme);

            //ToggleFullscreen();

            while (!WindowShouldClose() && !MyGameState.QuitMyGame)
            {

                if (IsKeyPressed(KeyboardKey.F11))
                    ToggleFullscreen();

                UpdateMusicStream(GameTheme);

                MyGameState.Update();   

                BeginDrawing();

                ClearBackground(Color.Black);
                MyGameState.Draw();
                                              
                EndDrawing();
            }
           
            MyGameState.Close();
            CloseAudioDevice();
            CloseWindow();
            return 0;
        }
    }
}
