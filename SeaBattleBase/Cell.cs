using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeaBattleBase
{
    public enum CellType
    {
        Empty,
        Water,
        Deck
    }
    public struct Point
    {
        public int Row { get; set; }
        public int Column { get; set; }
        public Point(int row, int column)
        {
            this.Row = row;
            this.Column = column;
        }
        public Point() : this(0, 0) { }
    }
    public class Cell
    {
        public Point Point { set; get; }
        public CellType Type { set; get; }
        public bool IsShot { set; get; }

        public Cell(Point point)
        {
            this.Point = point;
            this.Type = CellType.Water;
            this.IsShot = false;
        }
        public Cell(int row, int column) : this(new Point(row, column)) { }
        public Cell() : this(0, 0) { }
    }
}
