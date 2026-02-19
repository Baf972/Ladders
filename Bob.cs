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
        public float SpeedJumpLenght { get; set; }
        public float SpeedUp { get; set; }
        public float Velocity { get; set; }
        public bool IsFlipped { get; set; }
        public float Life { get; set; }

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
            X = 608;
            Y = 480;
            SpeedUp = 0f;
            SpeedJumpUp = 40f;
            SpeedFallDown = 100f;
            SpeedJumpLenght = 90f;
            Velocity = 150f;
            BobSourceRec = new Rectangle(0, 0, 32, 32);
            FrameCount = 0;
            FrameTimer = 30f;
            FrameWidth = 32;
            FrameHeight = 32;            
            CurrentFrame = 0;
            Life = 100f;
            MyState = new BobIdle(this);
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
            MyState.Update(this);

            



        }
        public void Draw()
        {
            MyState.Draw(this);
        }
        public void Close()
        {
            // Reset Bob's position, state, and other properties to their initial values
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
                    FrameWidth = TileSet.Width / 4;
                    FrameHeight = TileSet.Height;
                    NewFrameTimer = 20f;
                    FrameCount = 4;
                    

                    break;

                case BobStates.Climbing:

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
                    // Transition to Falling state
                    break;
            }

            MyState = BobState
                switch
            {
                BobStates.Idle => new BobIdle(this),
                BobStates.Climbing => new BobClimbing(this),
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
                BobJumping _ => BobStates.Jumping,
                BobFalling _ => BobStates.Falling,
                BobRunningUp _ => BobStates.RunningUp,
                BobLanding _ => BobStates.Landing,
                _ => BobStates.Idle
            };

        }
    }
}
