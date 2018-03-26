using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebHeroes.Code
{
    public class Position
    {
        public int x { get; set; }
        public int y { get; set; }

        public static int Distance(Position p1, Position p2)
        {
            return (Math.Abs(p1.x - p2.x) + Math.Abs(p1.y - p2.y)) / 2;
        }

        public static Position operator -(Position p1, Position p2)
        {
            return new Position(p1.x - p2.x, p1.y - p2.y);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as Position);
        }
        public bool Equals(Position obj)
        {
            return obj != null && obj.x == this.x && obj.y == this.y;
        }

        /// <summary>
        /// Поличить объект Position для ячейки
        /// </summary>
        /// <param name="x">позиция ячейки в строке</param>
        /// <param name="y">номер строка</param>
        /// <returns></returns>
        public Position CellToPosition(int x, int y)
        {
            return new Position(x * 2 + y % 2, y);
        }

        public Position(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
        public Position(Position p)
        {
            this.x = p.x;
            this.y = p.y;
        }
    }
}