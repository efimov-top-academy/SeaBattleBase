using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeaBattleBase
{
    public enum Direction
    {
        Horizontal, 
        Vertical
    }
    public class Ship
    {
        public Point Point { get; }
        public int Size { get; }
        public Direction Direction { set; get; }
        public int Damage { get; set; }
        public int Row
        {
            get => Point.Row;
        }
        public int Column
        {
            get => Point.Column;
        }

        public bool IsDead
        {
            get => Size == Damage;
        }

        public Ship(Point point, int size, Direction direction)
        {
            Point = point;
            Size = size;
            Direction = direction;
            Damage = 0;
        }
        public Ship(int row, int column, int size, Direction direction)
            : this(new Point(row, column), size, direction) { }

        public bool IsPoint(Point point)
        {
            bool isPoint = false;
            int row = this.Point.Row;
            int column = this.Point.Column;
            for (int i = 0; i < Size; i++)
            {
                if (point.Row == row && point.Column == column)
                    isPoint = true;
                if (Direction == Direction.Horizontal)
                    column++;
                else
                    row++;
            }
            return isPoint;
        }

    }
}
