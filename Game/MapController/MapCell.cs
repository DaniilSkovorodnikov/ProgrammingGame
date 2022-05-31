using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.MapController
{
    public class MapCell
    {
        public Point Location { get; set; }
        public static Size Size { get; } = new Size(120, 120);
        public Point CoordsOnTileset { get; set; }
        public Cell CellType { get; set; }

        public MapCell(Point coordsOnTileset, Point location, Cell cellType)
        {
            CoordsOnTileset = coordsOnTileset;
            Location = location;
            CellType = cellType;
        }
    }

    public enum Cell 
    { 
        Empty,
        Trail,
        Wall,
        Chest,
        OpenedChest
    }
}
