using Game.MapController;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.GameController
{
    public class PathFinder
    {
        public static List<Point> FindPath()
        {
            var queue = new Queue<SinglyLinkedList<Point>>();
            var visitedPoints = new HashSet<Point>();
            queue.Enqueue(new SinglyLinkedList<Point>(Player.GetCurrentCellPos(), null));
            var lastPoint = new SinglyLinkedList<Point>(new Point(), null);
            while (queue.Count != 0)
            {
                var currentPoint = queue.Dequeue();
                if (!InBounds(currentPoint.Value)) continue;
                if (Map.cells[currentPoint.Value.Y, currentPoint.Value.X] == 1) continue;
                if(Map.cells[currentPoint.Value.Y, currentPoint.Value.X] == 2)
                {
                    lastPoint = currentPoint;
                    break;
                }
                foreach (var direction in GetDirections())
                {
                    var nextPoint = currentPoint.Value + direction;
                    if(visitedPoints.Contains(nextPoint)) continue;
                    visitedPoints.Add(nextPoint);
                    queue.Enqueue(new SinglyLinkedList<Point>(nextPoint, currentPoint));
                }
            }
            return FindTarget(lastPoint);
        }

        public static List<Point> FindTarget(SinglyLinkedList<Point> point)
        {
            var path = new List<Point>();
            var currentPoint = point;
            while(currentPoint.Previous != null)
            {
                path.Add(currentPoint.Value);
                currentPoint = currentPoint.Previous;
            }
            path.Reverse();
            //if (path.Count >= 4) return path[3];
            //else return path[path.Count - 1];
            if (path.Count >= 4) return path.Take(4).ToList();
            else return path;
        }

        private static bool InBounds(Point point)
        {
            var bounds = new Rectangle(0,0,Map.cells.GetLength(1), Map.cells.GetLength(0));
            return bounds.Contains(point);
        }

        private static List<Size> GetDirections()
        {
            var directions = new List<Size>();
            directions.Add(new Size(-1,0));
            directions.Add(new Size(1,0));
            directions.Add(new Size(0, -1));
            directions.Add(new Size(0, 1));
            return directions;
        }
    }

    public class SinglyLinkedList<T>
    {
        public Point Value { get; set; }
        public SinglyLinkedList<T> Previous { get; set; }

        public SinglyLinkedList(Point value, SinglyLinkedList<T> previous)
        {
            Value = value;
            Previous = previous;
        }
    }

    public enum Direction
    {
        Left,
        Right,
        Up,
        Down
    }
}
