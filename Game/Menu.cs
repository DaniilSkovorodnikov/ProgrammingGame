using System;
using System.Windows.Forms;
using Game.GameController;
using Game.Interface;
using Game.Scenes;

namespace Game
{
    public partial class Game : Form
    {
        public static Scenes.MainMenu mainMenu { get; set; }
        public static Level1 level1 { get; set; }
        public Game()
        {
            InitializeComponent();
        }
        private void Menu_Load(object sender, EventArgs e)
        {
            mainMenu = new Scenes.MainMenu(this);
            level1 = new Level1(this);
            var controller = new Controller(this);
            CreateControls.game = this;
            SceneManager.Scenes.Add(mainMenu);
            SceneManager.Scenes.Add(level1);
            SceneManager.CurrentGraphics = CreateGraphics();
            SceneManager.Timer = new Timer();
            SceneManager.Timer.Interval = 10;
            SceneManager.Timer.Tick += Update;
            SceneManager.Timer.Start();
            mainMenu.InitScene();
        }

        public void Update(object sender, EventArgs e)
        {
            Invalidate();
        }

        private void Menu_Paint(object sender, PaintEventArgs e)
        {
        }

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == 0x128) return;
            base.WndProc(ref m);
        }
    }
}
