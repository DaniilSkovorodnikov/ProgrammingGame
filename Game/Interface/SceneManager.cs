using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Game.Interface
{
    public class SceneManager
    {
        public static List<Scene> Scenes { get; set; } = new List<Scene>();
        public static int CurrentScene { get; set; }
        public static Graphics CurrentGraphics { get; set; }
        public static Timer Timer { get; set; }
    }
}
