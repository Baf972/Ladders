using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Raylib_cs;
using static Raylib_cs.Raylib;

namespace LADDERS
{
    public class GameStates
    {
        public GameScenes? CurrentScene;
        public Dictionary<string, GameScenes> Scenes;
        public bool QuitMyGame;

        private static GameStates? instance;
        public static GameStates Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new GameStates();
                }
                return instance;
            }
        }

        public GameStates()
        {
            Scenes = new Dictionary<string, GameScenes>();
            QuitMyGame = false;
        }

        public void AddScene(string name, GameScenes myScene)
        {
            Scenes[name] = myScene;
            myScene.Name = name;

        }

        public void ChangeScene(string name)
        {
            if (Scenes.ContainsKey(name))
            {
                if (CurrentScene != null)
                {
                    CurrentScene.Hide();
                }
                CurrentScene = Scenes[name];
                CurrentScene.Show();
            }
            else
            {
                Debug.WriteLine($"Scene '{name}' not found.");
                throw new Exception($"Scene '{name}' not found.");
            }
        }


        public void Update()
        {
            CurrentScene?.Update();
        }

        public void Draw()
        {
            CurrentScene?.Draw();
        }

        public void Show()
        {
            CurrentScene?.Show();
        }

        public void Hide()
        {
            CurrentScene?.Hide();
        }

        public void Close()
        {
            CurrentScene?.Close();
        }

        public void QuitGame()
        {
            foreach (var scene in Scenes.Values)
            {
                scene.Close();
            }
            QuitMyGame = true;
        }
    }
}
