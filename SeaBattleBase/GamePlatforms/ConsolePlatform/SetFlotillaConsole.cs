using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SeaBattleBase.GamePlatforms.ConsolePlatform
{
    public class SetFlotillaConsole : ISetFlotilla
    {
        List<Ship> flotilla = new List<Ship>();
        public List<Ship> SetShips(string name)
        {
            WindowDrawConsole.Clear();

            int width = Resource.SizeCell * Resource.SizeField * 2 * 2 + 8 * Resource.SizeCell;
            int height = Resource.SizeField * Resource.SizeCell + Resource.SizeCell * 3;
            WindowDrawConsole.Window($"Setup Flotilla | Player {name}", width, height);

            bool isSetFlotilla = false;
            int[] countShips = new[] { 0, 4, 3, 2, 1 };
            Console.CursorVisible = false;
            while (!isSetFlotilla)
            {
                int rowStart = Resource.RowTop + Resource.SizeCell + 1;
                int columnStart = Resource.ColumnLeft + Resource.SizeCell * 2;
                WindowDrawConsole.Field(rowStart, columnStart);

                columnStart += Resource.SizeCell * 2 * Resource.SizeField + Resource.SizeCell * 4 * 2;
                WindowDrawConsole.ViewSetShips(rowStart, columnStart, countShips);

                int currentSize = WindowDrawConsole.ChoiseShip(rowStart, columnStart, countShips, flotilla);
                if (currentSize == -1)
                    break;

                Ship? ship = WindowDrawConsole.InsertShip(rowStart, Resource.ColumnLeft + Resource.SizeCell * 2, flotilla, currentSize);
                if(ship is not null)
                    flotilla.Add(ship);
                if (flotilla.Count == 10)
                    isSetFlotilla = true;

                //Console.SetCursorPosition(1, 1);
                //Console.Write($"current = {current}");

                //
            }

            return new List<Ship>();
        }
    }
}
