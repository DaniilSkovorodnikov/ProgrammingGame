using Game.GameController;
using Game.Interface;
using Game.MapController;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Game.Scenes
{
    public class Level1 : Scene
    {
        private Form game;
        public Map level1 { get; set; }
        private Player player;
        public static Label scoreLabel { get; set; }
        public static int ChestsCount { get; set; }

        public Level1(Form game)
        {
            ID = 1;
            SceneButtons = new List<Button>();
            SceneLabels = new List<Label>();
            this.game = game;
            level1 = new Map();
            player = new Player(level1.Start, Properties.Resources.Player);
            ChestsCount = 6;
        }

        private void OnKeyDownEsc(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Escape)
            {
                HideScene();
                SceneManager.Scenes[0].InitScene();
            }
        }

        public override void InitScene()
        {
            game.KeyDown += OnKeyDownEsc;
            game.Paint += new PaintEventHandler(level1.DrawMap);
            game.Paint += new PaintEventHandler(player.DrawPlayer);
            scoreLabel = CreateControls.CreateScoreLabel(Controller.score);
            SceneLabels.Add(scoreLabel);
            foreach (var button in SceneButtons)
                game.Controls.Add(button);
            foreach (var label in SceneLabels)
                game.Controls.Add(label);
            Controller.MakeStep();
        }

        public override void HideScene()
        {
            game.Controls.Clear();
            SceneManager.CurrentGraphics.Clear(Color.White);
            SceneManager.CurrentScene = 0;
            SceneButtons.Clear();
            SceneLabels.Clear();
            game.Paint -= new PaintEventHandler(level1.DrawMap);
            game.Paint -= new PaintEventHandler(player.DrawPlayer);
            game.KeyDown -= OnKeyDownEsc;
        }
    }
}
