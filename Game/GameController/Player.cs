using Game.Interface;
using Game.MapController;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Game.GameController
{
    public class Player
    {   
        public static int posX { get; set; }
        public static int posY { get; set; }
        public static Image sprite { get; set; }
        private Point coordsOnSprite;
        private Size size;
        private static List<Point> path;
        private static int currentStep;
        private static bool isFirstStep = false;
        private static Form form;

        public Player(Point startPos, Bitmap spriteSheet)
        {
            posX = startPos.X;
            posY = startPos.Y;
            sprite = spriteSheet;
            size = new Size(88, 100);
            currentStep = -1;
            coordsOnSprite = new Point(0, 0);
        }

        public static void Move(List<Point> target)
        {
            path = target;
            SceneManager.Timer.Tick += MoveTo;
        }

        public static void MoveTo(object sender, EventArgs e)
        {
            currentStep++;
            if (!isFirstStep)
            {
                form = Form.ActiveForm;
                form.Controls.Remove(Controller.questionPanel);
                isFirstStep = true;
            }
            if (currentStep == path.Count)
            {
                currentStep = -1;
                SceneManager.Timer.Tick -= MoveTo;
                form = Form.ActiveForm;
                isFirstStep = false;
                Controller.MakeStep();
                return;
            }
            OneStep(path[currentStep]);
        }

        private static void OneStep(Point target)
        {
            posX = target.X * 120;
            posY = target.Y * 120;
            if (Controller.IsSteppedOnChest())
                Controller.OpenChest();
        }

        public static Point GetCurrentCellPos()
        {
            var x = Math.Ceiling((double)posX / 120);
            var y = Math.Ceiling((double)posY / 120);
            return new Point((int)x,(int)y);
        }

        public void DrawPlayer(object sender, PaintEventArgs g)
        {
            g.Graphics.DrawImage(sprite, new Rectangle(new Point(posX, posY), MapCell.Size), coordsOnSprite.X, coordsOnSprite.Y, 
                size.Width, size.Height, GraphicsUnit.Pixel);
        }

    }
}
