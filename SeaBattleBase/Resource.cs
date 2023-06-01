using System;
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
        public static int SizeCell { set; get; } = 1;
        public static int RowStart { get; set; } = 3;
        public static int ColumnStart { get; set; } = 5;

        public static ConsoleColor ColorFill { get; set; } = ConsoleColor.Blue;
        public static ConsoleColor ColorBorder { get; set; } = ConsoleColor.DarkBlue;
    }
}
