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
            

            InitWindow(1280, 768, "LADDERS");
            InitAudioDevice();
            SetTargetFPS(60);
            SetExitKey(KeyboardKey.Null);            

            MyGameState = GameStates.Instance;
            MyGameState.AddScene("menu", MyGameMenu);
            MyGameState.AddScene("options", MyGameOptions);
            MyGameState.AddScene("gamePlay", MyGamePlay);

            MyGameState.ChangeScene("menu");

            while (!WindowShouldClose() && !MyGameState.QuitMyGame)
            {
                MyGameState.Update();
                
                BeginDrawing();

                ClearBackground(Color.Brown);
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
