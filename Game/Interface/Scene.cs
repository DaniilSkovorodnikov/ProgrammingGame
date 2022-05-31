using Game.MapController;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Game.Interface
{
    public abstract class Scene
    {
        public int ID { get; set; }
        public List<Button> SceneButtons;
        public List<Label> SceneLabels { get; set; }
        public Image BackgroundImage { get; set; }

        public abstract void InitScene();

        public abstract void HideScene();
    }
}
