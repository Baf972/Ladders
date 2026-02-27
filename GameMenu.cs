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
    public class GameMenu : GameScenes
    {
        private string[] ItemsMenu = { "Play", "Settings", "Crédits", "Quit"};

        private int SelectedItem = 0;
        private bool ItemValidated = false;

        private int LeftPadding = 870;
        private int VerticalSpacing = 0;
        private int FontSize = 35;

        private Color ItemColor;
        private Texture2D BackGroundMenu { get; set; }
        private Texture2D MenuSetting { get; set; }
        private Texture2D MenuCredits { get; set; }
        private Texture2D MenuPlay { get; set; }
        private int AlphaTextureSettings { get; set; }
        private int AlphaTextureCredits { get; set; }
        private int AlphaTexturePlay { get; set; }


        private Font FontMenu = LoadFont("assets/fonts/COOPBL.ttf");
        public GameMenu()
        {
            string chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789éèàùçâêîôûëïüöÉÈÀÙÇÂÊÎÔÛËÏÜÖ' :-!?";
            int[] codepoints = chars.Select(c => (int)c).ToArray();
            FontMenu = LoadFontEx("assets/fonts/COOPBL.ttf", 70, codepoints, codepoints.Length);
            SelectedItem = 0;          
            BackGroundMenu = LoadTexture("assets/Menu.png");
            MenuCredits = LoadTexture("assets/Credits.png");
            MenuSetting = LoadTexture("assets/Settings.png");
            MenuPlay = LoadTexture("assets/Play.png");
            AlphaTextureSettings = 0;
            AlphaTextureCredits = 0;
            AlphaTexturePlay = 0;
        }

        public override void Update()
        {
            

            // Sélection Items
            if (IsKeyPressed(KeyboardKey.Up))
            {
                SelectedItem--;
                if (SelectedItem < 0)
                    SelectedItem = ItemsMenu.Length - 1;
            }
            if (IsKeyPressed(KeyboardKey.Down))
            {
                SelectedItem++;
                if (SelectedItem >= ItemsMenu.Length)
                    SelectedItem = 0;
            }
            if (IsKeyPressed(KeyboardKey.Enter) || IsKeyPressed(KeyboardKey.KpEnter))
                ItemValidated = true;

            switch (SelectedItem)
            {
                case 0:
                    
                    AlphaTextureSettings = 0;

                    if (AlphaTextureSettings <= 0)
                    {
                        AlphaTexturePlay += 5;
                        if (AlphaTexturePlay > 255)
                            AlphaTexturePlay = 255;
                    }

                    if (ItemValidated)
                    {
                        GameStates.Instance.ChangeScene("gamePlay");
                        ItemValidated = false;
                    }
                    
                    break;

                case 1:  

                    AlphaTexturePlay = 0;
                    AlphaTextureCredits = 0;

                    if (AlphaTextureCredits <= 0)
                    {
                        AlphaTextureSettings += 5;
                        if (AlphaTextureSettings > 255)
                            AlphaTextureSettings = 255;
                    }   

                    break;

                case 2:  

                    AlphaTextureSettings = 0;

                    if (AlphaTextureSettings <= 0)
                    {
                        AlphaTextureCredits += 5;
                        if (AlphaTextureCredits >= 255)
                            AlphaTextureCredits = 255;
                    }   

                    break;

                case 3:

                    AlphaTextureCredits = 0;

                    if (ItemValidated)
                    {
                        GameStates.Instance.QuitGame();
                        ItemValidated = false;
                    }

                    break;
            }

            base.Update();
        }
        public override void Draw()
        {

            DrawTexture(BackGroundMenu, 0, 0, Color.White);

            for (int i = 0; i < ItemsMenu.Length; i++)
            {
                if (i == SelectedItem)
                {
                    ItemColor = Color.White;
                }
                else
                {
                    ItemColor = new Color(67, 41, 24);
                }
                DrawTextEx(FontMenu, ItemsMenu[i], new Vector2(LeftPadding, 250 + i * (FontSize + VerticalSpacing)), FontSize, 0, ItemColor);
            }

            switch (SelectedItem)
            {
                case 0:
                    DrawTexture(MenuPlay, 1060, 55, new Color(255, 255, 255, AlphaTexturePlay));
                    break;
                case 1:
                    DrawTexture(MenuSetting, 1060, 55, new Color(255, 255, 255, AlphaTextureSettings));
                    break;
                case 2:
                    DrawTexture(MenuCredits, 1060, 55, new Color(255, 255, 255, AlphaTextureCredits));
                    break;
            }

        }
        public override void Show()
        {
            base.Show();
        }
        public override void Hide()
        {
            base.Hide();
        }
        public override void Close()
        {
            UnloadTexture(BackGroundMenu);
            UnloadTexture(MenuCredits);
            UnloadTexture(MenuSetting);
            UnloadFont(FontMenu);
            base.Close();
        }
    }
}
