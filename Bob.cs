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
        Falling
    }

    public class Bob
    {
        AssetsManager MyAssetsManager { get; set; }
        BobStatesManager MyState { get; set; }
        public Texture2D TileSet { get; set; }
        public Rectangle BobRec { get; set; }
        public Rectangle BobSourceRec { get; set; }
        public Dictionary<string, Texture2D> BobStatesTextures { get; set; }
        private BobStates BobState{ get; set; }
        public int FrameCounter { get; set; }
        public int FrameWidth { get; set; }
        public int FrameHeight { get; set; }
        public int CurrentFrame { get; set; }

        public int X { get; set; }
        public int Y { get; set; }
        public int R { get; set; }
        public int Speed { get; set; }
        public bool Isflipped { get; set; }
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


        }
        public Bob() 
        {
            MyState = new BobIdle(this);
            X = 100;
            Y = 100;
            TileSet = MyAssetsManager.MyTexturesManager.GetTexture("assets/bobIdle.png");
            BobSourceRec = new Rectangle(0, 0, 32, 32);
            FrameCounter = 0;
            FrameWidth = 32;
            FrameHeight = 32;
            BobRec = new Rectangle(X, Y, FrameWidth, FrameHeight);
            CurrentFrame = 0;
            Isflipped = false;
            Life = 100f;
    
            LoadStatesTextures();
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
            DrawTexturePro(TileSet, BobRec, new Rectangle (X, Y, FrameWidth, FrameHeight), new Vector2(FrameWidth / 2, FrameHeight), R, Color.White);
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
        }

        public void StatesTransition(BobStates newState)
        {
            
           switch(newState)
            {
                case BobStates.Idle:
                    // Transition to Idle state
                    break;
                case BobStates.Climbing:
                    // Transition to Climbing state
                    break;
                case BobStates.Jumping:
                    // Transition to Jumping state
                    break;
                case BobStates.Falling:
                    // Transition to Falling state
                    break;
            }

            MyState = newState 
                switch
                {
                    BobStates.Idle => new BobIdle(this),
                    BobStates.Climbing => new BobClimbing(this),
                    BobStates.Jumping => new BobJumping(this),
                    BobStates.Falling => new BobFalling(this),
                    _ => throw new ArgumentOutOfRangeException(nameof(newState), $"Not expected state value: {newState}")
                };
        }
    }
}
