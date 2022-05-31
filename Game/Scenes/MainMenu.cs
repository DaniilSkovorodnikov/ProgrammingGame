using Game.Interface;
using Game.MapController;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Game.Scenes
{
    public class MainMenu : Scene
    {
        private Form game;
        public MainMenu(Form game)
        {
            ID = 0;
            SceneButtons = new List<Button>();
            SceneLabels = new List<Label>();
            this.game = game;
            BackgroundImage = Properties.Resources.BackgroundMenu;
            SceneButtons.Add(CreateControls.CreateButton(724, 208, Properties.Resources.StartGame, new Point(598, 378)));
            SceneButtons.Add(CreateControls.CreateButton(724, 208, Properties.Resources.ExitGame, new Point(598, 614)));
        }

        public override void HideScene()
        {
            game.Controls.Clear();
            SceneManager.CurrentGraphics.Clear(Color.White);
            game.BackgroundImage = null;
        }

        public override void InitScene()
        {
            foreach (var button in SceneButtons)
                game.Controls.Add(button);
            foreach (var label in SceneLabels)
                game.Controls.Add(label);
            game.BackgroundImage = BackgroundImage;
            SceneButtons[0].Click += (sender, e) =>
            {
                HideScene();
                SceneManager.CurrentScene = 1;
                SceneManager.Scenes[1].InitScene();
            };
            SceneButtons[1].Click += new EventHandler((sender, e) => Application.Exit());
        }
    }
}
