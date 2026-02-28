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
    public enum BobStates
    {
        Idle,
        Climbing,
        ClimbingDown,
        Jumping,
        Falling,
        RunningUp,
        Landing
    }

    public class Bob
    {
        public AssetsManager MyAssetsManager { get; set; }
        public BobStatesManager MyState { get; set; }
        public Texture2D TileSet { get; set; }
        public Rectangle BobRec { get; set; }
        public Rectangle BobSourceRec { get; set; }
        public Dictionary<string, Texture2D> BobStatesTextures { get; set; }
        public BobStates BobState { get; set; }
        public int FrameCount { get; set; }
        public int FrameWidth { get; set; }
        public int FrameHeight { get; set; }
        public int CurrentFrame { get; set; }
        public float FrameTimer { get; set; }
        public float NewFrameTimer { get; set; }
        public float X { get; set; }
        public float Y { get; set; }
        public float R { get; set; }
        public float SpeedJumpUp { get; set; }
        public float SpeedFallDown { get; set; }
        public float SpeedOutScreen { get; set; }
        public float SpeedJumpLenght { get; set; }
        public float SpeedClimb { get; set; }
        public float FallVelocity { get; set; }
        public float LenghtVelocity { get; set; }
        public bool IsFlipped { get; set; }
        public bool IsHurt { get; set; }
        public float Life { get; set; }
        public float Endurance { get; set; }
        public float Energy { get; set; }
        public int CollisionRange { get; set; }       

        private static Bob? instance;
        public static Bob Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Bob();
                }
                return instance;
            }
        }
        public void Init()
        {
            BobStatesTextures = new Dictionary<string, Texture2D>();
            MyAssetsManager = AssetsManager.Instance;
            LoadStatesTextures();

            X = 768;
            //X = 480;
            Y = 500;
            SpeedClimb = 50f;
            SpeedJumpUp = 40f;
            SpeedFallDown = 200f;
            SpeedJumpLenght = 200f;
            SpeedOutScreen = 300f;
            LenghtVelocity = 100f;
            FallVelocity = 200f;
            BobSourceRec = new Rectangle(0, 0, 32, 32);
            FrameCount = 0;
            FrameTimer = 30f;
            FrameWidth = 32;
            FrameHeight = 32;            
            CurrentFrame = 0;
            Life = 3f;
            Energy = 100f;
            Endurance = 100f;
            MyState = new BobIdle(this);
            CollisionRange = 100;
            StatesTransition(BobStates.Idle);
            

        }
        public Bob()
        {

            Init();

        }

        public void HandleInput()
        {
            MyState.HandleInput(this);
        }
        public void Update()
        {
            float DeltaTime = GetFrameTime();

            if (Y >= GetScreenHeight() + FrameHeight && Life > 0)
                Level1.Respawn = true;

            else if ( Life < 1)
            {
                TakeScreenshot("BackGround.png");
                Level1.ShotBackGround = LoadTexture("BackGround.png");
                File.Delete("ShotBackGround.png");
                Level1.Level1State = Level1States.end;
            }
                

            if (Energy <= 50)
                Endurance -= 1 * DeltaTime;            

            if (Energy >= 100)
                Energy = 100;

            if (Energy <= 0)
                Energy = 0;
  

            if (Endurance >= 100)
                Endurance = 100;

            if (Endurance <= 0)
            {
                Endurance = 0;
                Level1.Respawn = true;
            }





            MyState.Update(this);         



        }
        public void Draw()
        {
            MyState.Draw(this);
        }

        public void Respawn()
        {
            BobStatesTextures = new Dictionary<string, Texture2D>();
            LoadStatesTextures();
            Life -= 1f;
            X = 768;
            Y = 500;
            Energy = 100f;
            Endurance = 100f;
            SpeedJumpUp = 40f;
            SpeedFallDown = 200f;
            SpeedJumpLenght = 200f;
            IsFlipped = false;
            MapDraw.CameraY = -2160;
            MyState = new BobIdle(this);
            CollisionRange = 100;
            StatesTransition(BobStates.Idle);
        }
        public void Close()
        {
            UnloadBobAnimations();
        }

        public void UnloadBobAnimations()
        {
            foreach (var Texture in BobStatesTextures.Values)
            {
                UnloadTexture(Texture);
            }
            BobStatesTextures.Clear();
        }
        public void LoadStatesTextures()
        {
            BobStatesTextures["Idle"] = MyAssetsManager.MyTexturesManager.GetTexture("assets/bobIdle.png");
            BobStatesTextures["Climbing"] = MyAssetsManager.MyTexturesManager.GetTexture("assets/bobClimbing.png");
            BobStatesTextures["Jumping"] = MyAssetsManager.MyTexturesManager.GetTexture("assets/bobJumping.png");
            BobStatesTextures["Falling"] = MyAssetsManager.MyTexturesManager.GetTexture("assets/bobFalling.png");
            BobStatesTextures["RunningUp"] = MyAssetsManager.MyTexturesManager.GetTexture("assets/bobRunningUp.png");
            BobStatesTextures["Landing"] = MyAssetsManager.MyTexturesManager.GetTexture("assets/bobLanding.png");
        }

        public void StatesTransition(BobStates BobState)
        {

            switch (BobState)
            {
                case BobStates.Idle:

                    TileSet = BobStatesTextures["Idle"];
                    FrameWidth = TileSet.Width / 8;
                    FrameHeight = TileSet.Height;
                    NewFrameTimer = 15f;
                    FrameCount = 8;
                    

                    break;

                case BobStates.Climbing:

                    TileSet = BobStatesTextures["Climbing"];
                    FrameWidth = TileSet.Width / 8;
                    FrameHeight = TileSet.Height;
                    NewFrameTimer = 15f;
                    FrameCount = 8;

                    break;

                case BobStates.ClimbingDown:

                    TileSet = BobStatesTextures["Climbing"];
                    FrameWidth = TileSet.Width / 8;
                    FrameHeight = TileSet.Height;
                    NewFrameTimer = 15f;
                    FrameCount = 8;

                    break;

                case BobStates.RunningUp:

                    TileSet = BobStatesTextures["RunningUp"];
                    FrameWidth = TileSet.Width / 10;
                    FrameHeight = TileSet.Height;
                    NewFrameTimer = 15f;
                    FrameCount = 10;
                    CurrentFrame = 0;


                    break;

                case BobStates.Jumping:

                    TileSet = BobStatesTextures["Jumping"];
                    FrameWidth = TileSet.Width / 12;
                    FrameHeight = TileSet.Height;
                    NewFrameTimer = 1f;
                    FrameCount = 12;
                    CurrentFrame = 0;


                    break;

                case BobStates.Landing:

                    TileSet = BobStatesTextures["Landing"];
                    FrameWidth = TileSet.Width / 10;
                    FrameHeight = TileSet.Height;
                    NewFrameTimer = 10f;
                    FrameCount = 10;
                    NewFrameTimer = 10;
                    CurrentFrame = 0;



                    break;
                case BobStates.Falling:
                    TileSet = BobStatesTextures["Falling"];
                    FrameWidth = TileSet.Width / 4;
                    FrameHeight = TileSet.Height;
                    NewFrameTimer = 10f;
                    FrameCount = 4;
                    NewFrameTimer = 10;
                    CurrentFrame = 0;
                    break;
            }

            MyState = BobState
                switch
            {
                BobStates.Idle => new BobIdle(this),
                BobStates.Climbing => new BobClimbing(this),
                BobStates.ClimbingDown => new BobClimbingDown(this),
                BobStates.Jumping => new BobJumping(this),
                BobStates.Falling => new BobFalling(this),
                BobStates.RunningUp => new BobRunningUp(this),
                BobStates.Landing => new BobLanding(this),
                _ => MyState
            };
        }

        public BobStates GetCurrentState()
        {
            return MyState switch
            { 
                BobIdle _=> BobStates.Idle,
                BobClimbing _ => BobStates.Climbing,
                BobClimbingDown _ => BobStates.ClimbingDown,
                BobJumping _ => BobStates.Jumping,
                BobFalling _ => BobStates.Falling,
                BobRunningUp _ => BobStates.RunningUp,
                BobLanding _ => BobStates.Landing,
                _ => BobStates.Idle
            };

        }
    }
}
