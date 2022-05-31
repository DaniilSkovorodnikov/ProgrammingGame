using Game.GameController;
using Game.Scenes;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Game.MapController
{
    public class Map1 : Map
    {
        public static int[,] cells = new int[,] {
            { 1, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
            { 1, 0, 1, 0, 1, 1, 2, 0, 0, 0, 0, 2, 1, 0, 2, 1 },
            { 1, 0, 0, 0, 0, 1, 1, 0, 1, 1, 1, 1, 1, 0, 1, 1 },
            { 1, 1, 1, 1, 0, 1, 0, 0, 0, 1, 2, 0, 1, 0, 0, 1 },
            { 1, 0, 0, 0, 0, 1, 0, 1, 0, 1, 1, 0, 1, 0, 0, 1 },
            { 1, 0, 2, 1, 1, 1, 0, 1, 0, 0, 0, 0, 0, 0, 1, 1 },
            { 1, 0, 1, 1, 0, 0, 0, 1, 0, 1, 1, 1, 0, 1, 1, 1 },
            { 1, 0, 0, 0, 0, 1, 0, 1, 0, 1, 2, 0, 0, 0, 0, 1 },
            { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 }
        };
        public Point Start { get; set; }
        public static MapCell[,] map;

        public Map1()
        {
            map = new MapCell[16, 9];
            Start = new Point(120, 0);
            for (int i = 0; i < 16; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    map[i, j] = new MapCell(new Point(0, 0), new Point(0,0), Cell.Empty);
                }
            }
            for (int i = 0; i < 16; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if(map[i,j].CellType == Cell.Empty)
                        map[i,j] = new MapCell(new Point(110, 20), new Point(120 * i, 120 * j), Cell.Trail);
                    if(cells[j,i] == 1)
                        map[i,j] = new MapCell(new Point(0, 0), new Point(120 * i, 120 * j), Cell.Wall);
                    if (cells[j,i] == 2)
                        map[i, j] = new MapCell(new Point(0, 0), new Point(120 * i, 120 * j), Cell.Chest);
                }
            }
        }

        public void DrawMap(object sender, PaintEventArgs g)
        {
            foreach (var cell in map)
            {
                g.Graphics.DrawImage(Properties.Resources.Tileset, new Rectangle(cell.Location, MapCell.Size), cell.CoordsOnTileset.X, cell.CoordsOnTileset.Y, 32, 32, GraphicsUnit.Pixel);
                if (cell.CellType == Cell.Wall)
                    g.Graphics.DrawImage(Properties.Resources.Wall, new Rectangle(cell.Location, MapCell.Size), cell.CoordsOnTileset.X, cell.CoordsOnTileset.Y, 667, 667, GraphicsUnit.Pixel);
                if (cell.CellType == Cell.Chest)
                    g.Graphics.DrawImage(Properties.Resources.Chest, new Rectangle(cell.Location, MapCell.Size), cell.CoordsOnTileset.X, cell.CoordsOnTileset.Y, 201, 180, GraphicsUnit.Pixel);
                if(cell.CellType == Cell.OpenedChest)
                    g.Graphics.DrawImage(Properties.Resources.OpenedChest, new Rectangle(cell.Location, MapCell.Size), 0, 0, 223, 180, GraphicsUnit.Pixel);
            }
            Level1.scoreLabel.Text = String.Format("Счёт: {0}", Controller.score);
        }
    }
}
