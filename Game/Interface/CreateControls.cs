using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Game.GameController;

namespace Game.Interface
{
    public static class CreateControls
    {
        public static Form game;
        public static Button CreateButton(int width, int height, Bitmap image, Point location)
        {
            var button = new Button();
            button.Width = width;
            button.Height = height;
            button.BackgroundImage = image;
            button.FlatStyle = FlatStyle.Flat;
            button.ForeColor = Color.Transparent;
            button.BackgroundImageLayout = ImageLayout.Stretch;
            button.BackColor = Color.Transparent;
            button.FlatAppearance.BorderSize = 0;
            button.FlatAppearance.MouseDownBackColor = Color.Transparent;
            button.FlatAppearance.MouseOverBackColor = Color.Transparent;
            button.Location = location;
            return button;
        }
        
        public static Panel CreateQuestionPanel(string questionText, List<string> possibleAnswers)
        {
                var panel = new Panel();
                var question = new Label();
                var label = new Label();
                var answers = new ComboBox();
                var checkAnswer = new Button();

                panel.Location = new Point(800, 420);
                panel.Size = new Size(300, 200);
                panel.BorderStyle = BorderStyle.Fixed3D;

                label.Location = new Point(0, 0);
                label.Text = "Решите задачу";
                label.TextAlign = ContentAlignment.MiddleCenter;
                label.Size = new Size(panel.Width, 32);
                label.Font = new Font("Rockwell", 16);

                question.Location = new Point(0, 40);
                question.Text = questionText;
                question.TextAlign = ContentAlignment.MiddleCenter;
                question.Size = new Size(panel.Width, 74);
                question.Font = new Font("Rockwell", 16);

                foreach (var answer in possibleAnswers[Controller.currentStep])
                    answers.Items.Add(answer);
                answers.Location = new Point(75, 120);
                answers.Size = new Size(150, 70);
                answers.Text = "Выберите ответ";

                checkAnswer.Location = new Point(75, 150);
                checkAnswer.Text = "Ответить";
                checkAnswer.Size = new Size(150, 40);


                panel.Controls.Add(question);
                panel.Controls.Add(label);
                panel.Controls.Add(answers);
                panel.Controls.Add(checkAnswer);
                return panel;
        }
        

        public static Label CreateScoreLabel(int score)
        {
            var scoreLabel = new Label();
            scoreLabel.Location = new Point(850, 20);
            scoreLabel.Size = new Size(200, 64);
            scoreLabel.Font = new Font("Rockwell", 40);
            scoreLabel.BackColor = Color.Transparent;
            scoreLabel.Text = String.Format("Счёт: {0}", score);
            return scoreLabel;
        }
    }
}
