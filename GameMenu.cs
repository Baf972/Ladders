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
        private string[] ItemsMenu = { "jouer", "options", "quitter" };

        private int SelectedItem = 0;
        private bool ItemValidated = false;

        private int LeftPadding = 700;
        private int VerticalSpacing = 10;
        private int FontSize = 40;

        private Color ItemColor;
        private Texture2D TileSet1;

        private Font FontMenu = LoadFont("assets/fonts/PixelOperator.ttf");
        public GameMenu()
        {
            string chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789éèàùçâêîôûëïüöÉÈÀÙÇÂÊÎÔÛËÏÜÖ' :-!?";
            int[] codepoints = chars.Select(c => (int)c).ToArray();
            FontMenu = LoadFontEx("bin/Debug/net8.0/assets/fonts/PixelOperatorMonoHB.ttf", 70, codepoints, codepoints.Length);
            SelectedItem = 0;          

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
            if (IsKeyPressed(KeyboardKey.Enter))
                ItemValidated = true;

            switch (SelectedItem)
            {
                case 0:
                    if (ItemValidated)
                    {
                        GameStates.Instance.ChangeScene("gamePlay");
                        ItemValidated = false;
                    }
                    break;

                case 1:
                    if (ItemValidated)
                    {
                        GameStates.Instance.ChangeScene("options");
                        ItemValidated = false;
                    }
                    break;

                case 2:
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
            for (int i = 0; i < ItemsMenu.Length; i++)
            {
                if (i == SelectedItem)
                {
                    ItemColor = Color.White;
                }
                else
                {
                    ItemColor = Color.DarkBrown;
                }
                DrawTextEx(FontMenu, ItemsMenu[i], new Vector2(LeftPadding, 200 + i * (FontSize + VerticalSpacing)), FontSize, 3, ItemColor);
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
            base.Close();
        }
    }
}
