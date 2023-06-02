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
        
        public static void Cell(int row, int column, ConsoleColor color)
        {
            for(int r = 0; r < SizeCell; r++)
                for(int c = 0; c < SizeCell; c += SizeCell)
                {
                    Console.ForegroundColor = color;
                    Console.SetCursorPosition(c + column, r + row);
                    Console.Write(new String(WindowDrawConsole.Box100, SizeCell * 2));
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
                    Cell(row + i * SizeCell, column + j * SizeCell * 2, ColorWater);
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

        public static 
    }
}
