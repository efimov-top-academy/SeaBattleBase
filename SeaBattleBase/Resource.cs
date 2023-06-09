﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeaBattleBase
{
    public static class Resource
    {
        public static int SizeField { get; set; } = 10;
        public static int SizeCell { set; get; } = 2;
        public static int RowTop { get; set; } = 3;
        public static int ColumnLeft { get; set; } = 5;

        public static ConsoleColor ColorFill { get; set; } = ConsoleColor.Gray;
        public static ConsoleColor ColorBorder { get; set; } = ConsoleColor.DarkBlue;
        public static ConsoleColor ColorText { get; set; } = ConsoleColor.Black;
        public static ConsoleColor ColorWater { get; set; } = ConsoleColor.DarkBlue;
        public static ConsoleColor ColorDesk { get; set; } = ConsoleColor.DarkMagenta;
        public static ConsoleColor ColorDeskEmpty { get; set; } = ConsoleColor.White;
        public static ConsoleColor ColorDeskSelect { get; set; } = ConsoleColor.DarkGreen;

        public static ConsoleColor ColorWaterShot { get; set; } = ConsoleColor.Red;
        public static ConsoleColor ColorDeskShot { get; set; } = ConsoleColor.DarkRed;
    }
}
