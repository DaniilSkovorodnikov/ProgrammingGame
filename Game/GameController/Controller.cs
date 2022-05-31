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
        private static Form game;
        private static List<string> questions;
        private static List<string> correctAnswers;
        private static List<List<string>> possibleAnswers;
        private static string currentAnswer;
        public static int currentStep { get; set; }
        private static Stack<Point> prevPositions;
        public static int score { get; set; }
        public static Panel questionPanel { get; set; }

        public Controller(Form game)
        {
            Controller.game = game;
            questions = new List<string>() {
                "ln(x) = 1;  x - ?",
                "x=sin(pi/2);  x - ?",
                "new int[]{1,2,3} == new int[]{1,2,3}",
                "Какова сложность алгоритма бинарного поиска?",
                "Вычислите интеграл от 1/tg(x)",
                "Динамический метод можно вызывать только в контексте экземпляра класса",
                "var a = 3;\nvar b = a/2;\nЧему равно b?",
                "С помощью какой универсальной подстановки рационализируется тригонометрическая функция",
                "Что обозначает ключевое слово var ?",
                "Выберите наибольшее число для которого истинно (X > 5) И НЕ(X<15)",
                "Выберите метод LINQ удаляющий все дубликаты из коллекции",
                "Можно ли объявлять делегат неявным образом? Например:\nvar increment = (x) => x++;",
                "На клавиатуре телефона 10 цифр, от 0  до 9. Какова вероятность того, что случайно нажатая цифра будет чётной?",
                "Какой класс в Windows Forms отвечает за рисование?",
                "Как на один делегат подписать несколько методов?",
                "Какое ключевое слово используется при определении делегата?",
                "Как по умолчанию работает оператор == ?",
                "Являются ли методы ToList и ToArray ленивыми?",
                "Что делает метод стека Push?",
                "Как переопределить виртуальный метод?",
                "Возможно ли сложить разные лямбда выражения в лист?",
                "Как объявить какой-либо класс с универсальным типом?"
            };
            correctAnswers = new List<string>() { "e", "1", "false","O(log(n))","ln(sin(x))", "Верно","1","t = tg(x/2)",
            "Обозначает неявный тип данных","15","Distinct","Нельзя", "0,5", "Graphics", "Через команду +=", "delegate",
            "Сравнивает по ссылке", "Не являются", "Вносит элемент в стек", "Использовать override", "Возможно", "class Name<T>"};
            possibleAnswers = new List<List<string>>()
            {
                new List<string> { "0", "1", "e"},
                new List<string> { "0", "sqrt(3)/2", "1"},
                new List<string> { "true", "false"},
                new List<string> { "O(n)", "O(log(n))", "O(n^2)"},
                new List<string> { "-1/cos(x)^2", "ln(sin(x))", "cos(x)^2"},
                new List<string> { "Верно", "Неверно"},
                new List<string> { "1", "1.5", "2"},
                new List<string> { "t = tg(x/2)", "t = sin(2x)", "tg(x)"},
                new List<string> { "Объявляет войну программ", "Обозначает неявный тип данных", "Обозночает явный тип данных"},
                new List<string> { "14", "5", "15"},
                new List<string> { "Distinct", "Intersect", "Skip"},
                new List<string> { "Можно", "Нельзя"},
                new List<string> { "0,5", "4/9", "0,4"},
                new List<string> { "Drawing", "Graphics", "Paint"},
                new List<string> { "Через команду +=", "Нельзя", "delegate.Add()"},
                new List<string> { "delegate", "function", "Такого слова нет"},
                new List<string> { "Сравнивает по ссылке", "Сравнивает по полям"},
                new List<string> { "Являются", "Не являются"},
                new List<string> { "Вносит элемент в стек", "Удаляет элемент из стека"},
                new List<string> { "Использовать override", "Написать как простой метод", "Нельзя переопределить"},
                new List<string> { "Возможно", "Невозможно"},
                new List<string> { "class<T> Name", "class Name<T>", "class T Name"}
            };
            currentStep = 0;
            prevPositions = new Stack<Point>();
        }

        public static void MakeStep()
        {
            if (score == Level1.ChestsCount)
            {
                CreateControls.CreateExitPanel(true);
                return;
            }
  
            if (currentStep == questions.Count && score != Level1.ChestsCount)
            {
                CreateControls.CreateExitPanel(false);
                return;
            }
            questionPanel = CreateControls.CreateQuestionPanel(questions[currentStep], possibleAnswers);
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
            return Map.cells[playerPos.Y, playerPos.X] == 2;
        }

        public static void OpenChest()
        {
            var playerPos = Player.GetCurrentCellPos();
            Map.cells[playerPos.Y, playerPos.X] = 0;
            Map.map[playerPos.X, playerPos.Y].CellType = Cell.OpenedChest;
            score++;
        }
    }
}
