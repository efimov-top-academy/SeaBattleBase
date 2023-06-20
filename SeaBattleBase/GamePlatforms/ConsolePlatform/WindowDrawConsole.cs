using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeaBattleBase.GamePlatforms.ConsolePlatform
{
    public enum BorderCorner
    {
        TopLeft,
        TopMiddle,
        TopRight,
        CenterLeft,
        CenterMiddle,
        CenterRight,
        BottomLeft,
        BottomMiddle,
        BottomRight,
        Horizontal,
        Vertical,
    }
    public static class WindowDrawConsole
    {
        public static int SizeField { set; get; }
        public static int SizeCell { set; get; }
        public static int RowTop { set; get; }
        public static int ColumnLeft { set; get; }
        public static ConsoleColor ColorFill { get; set; }
        public static ConsoleColor ColorBorder { get; set; }
        public static ConsoleColor ColorText { set; get; }
        public static ConsoleColor ColorWater { get; set; }
        public static ConsoleColor ColorDesk { get; set; }
        public static ConsoleColor ColorWaterShot { get; set; }
        public static ConsoleColor ColorDeskShot { get; set; }
        public static ConsoleColor ColorDeskEmpty { get; set; }
        public static ConsoleColor ColorDeskSelect { get; set; }

public static char[] BorderSingle { get; } = new char[] { '\u250C', '\u252C', '\u2510',
                                                                  '\u251C', '\u253C', '\u2524',
                                                                  '\u2514', '\u2534', '\u2518',
                                                                  '\u2500', '\u2502' };
        public static char[] BorderDouble { get; } = new char[] { '\u2554', '\u2566', '\u2557',
                                                                  '\u2560', '\u256C', '\u2563',
                                                                  '\u255A', '\u2569', '\u255D',
                                                                  '\u2550', '\u2551' };
        public const char Box100 = '\u2588';
        public const char Box75 = '\u2593';
        public const char Box50 = '\u2592';
        public const char Box25 = '\u2591';

        static WindowDrawConsole()
        {
            SizeField = Resource.SizeField;
            SizeCell = Resource.SizeCell;
            RowTop = Resource.RowTop;
            ColumnLeft = Resource.ColumnLeft;

            ColorFill = Resource.ColorFill;
            ColorBorder = Resource.ColorBorder;
            ColorText = Resource.ColorText;
            ColorWater = Resource.ColorWater;
            ColorDesk = Resource.ColorDesk;
            ColorWaterShot = Resource.ColorWaterShot;
            ColorDeskShot = Resource.ColorDeskShot;
            ColorDeskEmpty = Resource.ColorDeskEmpty;
            ColorDeskSelect = Resource.ColorDeskSelect;
    }

        public static void Window(string title, int width, int height)
        {
            Console.ForegroundColor = ColorText;
            Console.BackgroundColor = ColorFill;

            for(int row = 0; row < height; row++)
                for(int column = 0; column < width; column++)
                {
                    Console.SetCursorPosition(column + ColumnLeft, row + RowTop);
                    Console.Write(" ");
                }
            int titleStart = RowTop + (width - title.Length) / 2;
            Console.SetCursorPosition(titleStart, RowTop);
            Console.WriteLine(title);
        }
        
        public static void Cell(int row, int column, char symbol, ConsoleColor color)
        {
            for(int r = 0; r < SizeCell; r++)
                for(int c = 0; c < SizeCell; c += SizeCell)
                {
                    Console.ForegroundColor = color;
                    Console.SetCursorPosition(c + column, r + row);
                    Console.Write(new String(symbol, SizeCell * 2));
                }
        }

        public static void Field(int row, int column)
        {
            ConsoleColor color = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Black;
            char alpha = 'A';

            for (int i = 0; i < SizeField; i++)
            {
                Console.SetCursorPosition(column + i * SizeCell * 2, row - 1);
                Console.Write(alpha++);
                Console.SetCursorPosition(column - 2, row + i * SizeCell);
                Console.Write($"{i + 1:d2}");
            }

            Console.ForegroundColor = color;

            for (int i = 0; i < Resource.SizeField; i++)
                for (int j = 0; j < Resource.SizeField; j++)
                    Cell(row + i * SizeCell, column + j * SizeCell * 2, WindowDrawConsole.Box75, ColorWater);
        }

        public static void ViewSetShips(int row, int column, int[] countShips)
        {
            for(int i = countShips.Length - 1; i > 0; i--)
            {
                Console.SetCursorPosition(column, row + (countShips.Length - i - 1) * SizeCell * 2);
                Console.ForegroundColor = ConsoleColor.Black;
                Console.Write(countShips[i]);

                for(int j = 0; j < i; j++)
                {
                    ConsoleColor color = (countShips[i] == 0) ? ColorDeskEmpty : ColorDesk;
                    Cell(row + (countShips.Length - i - 1) * SizeCell * 2, column + SizeCell * 2 * (j + 1), WindowDrawConsole.Box100, color);
                }
            }
        }

        public static int ChoiseShip(int row, int column, int[] countShips, List<Ship> flotilla)
        {
            int rowShip = 4;
            bool isSelect = false;

            foreach (Ship ship in flotilla)
            {
                int r = ship.Row;
                int c = ship.Column;

                for (int s = 0; s < ship.Size; s++)
                {
                    Cell(row + s * SizeCell, column + s * SizeCell * 2, WindowDrawConsole.Box100, ColorDesk);
                    if (ship.Direction == Direction.Horizontal)
                        c++;
                    else
                        r++;
                }
            }

            while (true)
            {
                for (int j = 0; j < rowShip; j++)
                    Cell(row + (countShips.Length - rowShip - 1) * SizeCell * 2, column + SizeCell * 2 * (j + 1), WindowDrawConsole.Box100, ColorDeskSelect);

                int rowPrev = rowShip;

                ConsoleKeyInfo key = Console.ReadKey();

                switch (key.Key)
                {
                    case ConsoleKey.UpArrow:
                        if(rowShip < 4)
                            rowShip++;
                        break;
                    case ConsoleKey.DownArrow:
                        if (rowShip > 1)
                            rowShip--;
                        break;
                    case ConsoleKey.Enter:
                        if (countShips[rowShip] > 0)
                            isSelect = true;
                        break;
                    case ConsoleKey.Escape:
                        rowShip = -1;
                        break;
                }

                if (isSelect) break;

                for (int j = 0; j < rowPrev; j++)
                    Cell(row + (countShips.Length - rowPrev - 1) * SizeCell * 2, column + SizeCell * 2 * (j + 1), WindowDrawConsole.Box100, ColorDesk);

                for (int j = 0; j < rowShip; j++)
                    Cell(row + (countShips.Length - rowShip - 1) * SizeCell * 2, column + SizeCell * 2 * (j + 1), WindowDrawConsole.Box100, ColorDeskSelect);
            }

            return rowShip;
        }

        public static Ship? InsertShip(int row, int column, List<Ship> flotilla, int currentSize)
        {
            Direction direction = Direction.Horizontal;
            int rowShip = 0;
            int columnShip = 0;

            while(true)
            {
                ConsoleKeyInfo key = Console.ReadKey();
                bool isCan = false;
                bool isEsc = false;

                switch (key.Key)
                {
                    case ConsoleKey.DownArrow:
                        rowShip++;
                        isCan = rowShip >= 0 && rowShip < SizeField && IsView();
                        if (!isCan) rowShip--;
                        break;
                    case ConsoleKey.UpArrow:
                        rowShip--;
                        isCan = rowShip >= 0 && rowShip < SizeField && IsView();
                        if (!isCan) rowShip++;
                        break;
                    case ConsoleKey.LeftArrow:
                        columnShip--;
                        isCan = columnShip >= 0 && columnShip < SizeField && IsView();
                        if (!isCan) columnShip++;
                        break;
                    case ConsoleKey.RightArrow:
                        columnShip++;
                        isCan = columnShip >= 0 && columnShip < SizeField && IsView();
                        if (!isCan) columnShip--;
                        break;
                    case ConsoleKey.Spacebar:
                        direction = (direction == Direction.Horizontal) ? Direction.Vertical : Direction.Horizontal;
                        isCan = rowShip >= 0 && rowShip < SizeField && 
                                columnShip >= 0 && columnShip < SizeField && 
                                IsView();
                        if(!isCan)
                            direction = (direction == Direction.Horizontal) ? Direction.Vertical : Direction.Horizontal;
                        break;
                    case ConsoleKey.Enter:
                        if (IsView())
                            return new Ship(new Point(rowShip, columnShip), currentSize, direction);
                        break;
                    case ConsoleKey.Escape:
                        isEsc = true;
                        break;
                }

                if (isEsc) break;

                if(isCan)
                {
                    ViewWater();
                    ViewFlotilla();
                    ViewShip();
                }
            }

            return null;

            bool IsView()
            {
                bool isView = false;
                if (direction == Direction.Horizontal)
                    isView = columnShip + currentSize <= SizeField;
                else
                    isView = rowShip + currentSize <= SizeField;
                if(!isView) return isView;

                foreach(Ship ship in flotilla)
                {
                    int r = rowShip;
                    int c = columnShip;

                    for(int i = 0; i < ship.Size; i++)
                    {
                        for (int rdx = -1; rdx < 2; rdx++)
                            for (int cdx = -1; cdx < 2; cdx++)
                            {
                                isView = !ship.IsPoint(new Point(r + rdx, c + cdx));
                                if (!isView)
                                    return isView;
                            }
                        if (ship.Direction == Direction.Horizontal)
                            c++;
                        else
                            r++;
                    }
                    
                }

                return isView;
            }

            void ViewShip()
            {
                int r = rowShip;
                int c = columnShip;

                for(int i = 0; i < currentSize; i++)
                {
                    Cell(row + r * SizeCell, column + c * SizeCell * 2, WindowDrawConsole.Box100, ColorDesk);
                    if (direction == Direction.Horizontal)
                        c++;
                    else
                        r++;
                }
            }

            void ViewWater()
            {
                for (int i = 0; i < Resource.SizeField; i++)
                    for (int j = 0; j < Resource.SizeField; j++)
                        Cell(row + i * SizeCell, column + j * SizeCell * 2, WindowDrawConsole.Box75, ColorWater);
            }

            void ViewFlotilla()
            {
                foreach(Ship ship in flotilla)
                {
                    int r = ship.Row;
                    int c = ship.Column;

                    for(int s = 0; s < ship.Size; s++)
                    {
                        Cell(row + s * SizeCell, column + s * SizeCell * 2, WindowDrawConsole.Box100, ColorDesk);
                        if(ship.Direction == Direction.Horizontal) 
                            c++;
                        else
                            r++;
                    }
                }
            }
        }



        public static string TextBoxName(int width)
        {
            ConsoleColor colorBack = Console.BackgroundColor;
            ConsoleColor colorText = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Black;

            for (int i = 0; i < width - 2; i++)
            {
                Console.SetCursorPosition(ColumnLeft + 1 + i, RowTop + 2);
                Console.Write(" ");
            }
            string? name;
            Console.SetCursorPosition(ColumnLeft + 1, RowTop + 2);
            name = Console.ReadLine();

            Console.ForegroundColor = colorText;
            Console.BackgroundColor = colorBack;
            return name!;
        }

        public static void Clear()
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Black;
            Console.Clear();
        }

        
    }
}
