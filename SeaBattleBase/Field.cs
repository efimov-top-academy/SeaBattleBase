using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeaBattleBase
{
    public enum HitType
    {
        Beside,
	    Reach,
	    Wound,
	    Destroy
    };
    public class Field
    {
        public int Size { get; }

        List<List<Cell>> cells = new List<List<Cell>>();
        public Field(int size)
        {
            Size = size;
            for(int i = 0; i < Size; i++)
            {
                List<Cell> line = new List<Cell>();
                for(int j = 0; j < Size; j++)
                    line.Add(new Cell(i, j));
                cells.Add(line);
            }
        }
        public void SetCellType(Point point, CellType type)
        {
            cells[point.Row][point.Column].Type = type;
        }

        public void SetCellShot(Point point)
        {
            cells[point.Row][point.Column].IsShot = true;
        }
        public void SetFlotilla(List<Ship> flotilla)
        {
            foreach (Ship ship in flotilla)
            {
                int row = ship.Row;
                int column = ship.Column;
                for (int i = 0; i < ship.Size; i++)
                {
                    SetCellType(new Point(row, column), CellType.Deck);
                    if (ship.Direction == Direction.Horizontal)
                        column++;
                    else
                        row++;
                }
            }
        }

        public HitType CheckShot(Point point)
        {
            if (cells[point.Row][point.Column].IsShot == true)
                return HitType.Beside;
            else
            {
                if (cells[point.Row][point.Column].Type == CellType.Water)
                    return HitType.Beside;
                else
                    return HitType.Reach;
            }
        }

        public Cell GetCell(int row, int column)
        {
            return cells[row][column];
        }
    }
}
