using Game.Interface;
using Game.MapController;
using Game.Scenes;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Game.GameController
{
    public class Controller
    {
        public static Form game;
        public static List<string> questions;
        public static List<string> correctAnswers;
        public static List<List<string>> possibleAnswers;
        private static string currentAnswer;
        public static int currentStep;
        private static Stack<Point> prevPositions;
        public static int score;
        public static Panel questionPanel;

        public Controller(Form game)
        {
            Controller.game = game;
            questions = new List<string>() {
                "ln(x) = 1;  x - ?",
                "x=sin(pi/2);  x - ?",
                "new int[]{1,2,3} == \nnew int[]{1,2,3}",
                "Какова сложность алгоритма\nбинарного поиска?",
                "Вычислите интеграл от\n1/tg(x)",
                "Динамический метод можно\nвызывать только в контексте\nэкземпляра класса",
                "var a = 3;\nvar b = a/2;\nЧему равно b?",
                "Сколько будет 2+2?"
            };
            correctAnswers = new List<string>() { "e", "1", "false","O(log(n))","ln(sin(x))", "Верно","1","5000"};
            possibleAnswers = new List<List<string>>()
            {
                new List<string> { "0", "1", "e"},
                new List<string> { "0", "sqrt(3)/2", "1"},
                new List<string> { "true", "false"},
                new List<string> { "O(n)", "O(log(n))", "O(n^2)"},
                new List<string> { "-1/cos(x)^2", "ln(sin(x))", "cos(x)^2"},
                new List<string> { "Верно", "Неверно"},
                new List<string> { "1", "1.5", "2"},
                new List<string> { "4", "5000"}
            };
            currentStep = 0;
            prevPositions = new Stack<Point>();
        }

        public static void MakeStep()
        {
            if (currentStep == questions.Count)
                return;
            questionPanel = CreateControls.CreateQuestionPanel();
            game.Controls.Add(questionPanel);
            questionPanel.Controls[3].Click += (sender, e) =>
            {
                
                currentAnswer = questionPanel.Controls[2].Text;
                game.Controls.Remove(questionPanel);
                if (IsCorrectAnswer())
                {
                    var target = PathFinder.FindPath();
                    prevPositions.Push(Player.GetCurrentCellPos());
                    foreach (var item in target)
                        prevPositions.Push(item);
                    prevPositions.Pop();
                    Player.Move(target);
                }
                else
                {
                    if(prevPositions.Count > 1)
                    {
                        var reversedTrack = new List<Point>();
                        reversedTrack.Add(prevPositions.Pop());
                        reversedTrack.Add(prevPositions.Pop());
                        Player.Move(reversedTrack);
                    }
                }
                currentStep++;
            };
        }

        public static bool IsCorrectAnswer()
        {
            if (Equals(currentAnswer, correctAnswers[currentStep])) return true;
            else return false;
        }

        public static bool IsSteppedOnChest()
        {
            var playerPos = Player.GetCurrentCellPos();
            return Map1.cells[playerPos.Y, playerPos.X] == 2;
        }

        public static void OpenChest()
        {
            var playerPos = Player.GetCurrentCellPos();
            Map1.cells[playerPos.Y, playerPos.X] = 0;
            Map1.map[playerPos.X, playerPos.Y].CellType = Cell.OpenedChest;
            score++;
        }
    }
}
