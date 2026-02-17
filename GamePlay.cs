using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Raylib_cs;
using static Raylib_cs.Raylib;

namespace LADDERS
{
    public enum GameLevels
    {
        Level1,
        Level2,
        Level3
    }

    public class GamePlay : GameScenes
    {

        public GameLevels GameLevel;
        public Level1 MyLevel1;
        public GamePlay()
        {
            GameLevel = GameLevels.Level1;
            MyLevel1 = new Level1();

        }

        public override void Update()
        {
            MyLevel1.Update();
            switch (GameLevel)
            {
                case GameLevels.Level1:
                    //MyLevel1.Update();
                    break;
                case GameLevels.Level2:
                    // Update Level 2
                    break;
                case GameLevels.Level3:
                    // Update Level 3
                    break;

            }
            base.Update();
        }

        public override void Draw()
        {
            MyLevel1.Draw();
            switch (GameLevel)
            {
                case GameLevels.Level1:
                    //MyLevel1.Draw();

                    break;
                case GameLevels.Level2:
                    // Update Level 2
                    break;
                case GameLevels.Level3:
                    // Update Level 3
                    break;

            }
            base.Draw();
        }

        public override void Close()
        {
            switch (GameLevel)
            {
                case GameLevels.Level1:
                    //MyLevel1.Close();
                    break;
                case GameLevels.Level2:
                    // Update Level 2
                    break;
                case GameLevels.Level3:
                    // Update Level 3
                    break;
            }
            base.Close();

        }
    }
}
