using SeaBattleBase.Players;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeaBattleBase.GamePlatforms.ConsolePlatform
{
    public class GameViewConsole : IGameView
    {
        public void Setup(Player playerHuman)
        {
            WindowDrawConsole.Clear();
            Console.WriteLine("Setup game");

            WindowDrawConsole.Window("Name", 20, 5);
            string name = WindowDrawConsole.TextBoxName(20);


            WindowDrawConsole.Clear();

            int width = Resource.SizeCell * Resource.SizeField * 2 * 2 + 8 * Resource.SizeCell;
            int height = Resource.SizeField * Resource.SizeCell + Resource.SizeCell * 3;

            WindowDrawConsole.Window($"Setup Flotilla | Player {name}", width, height);

            int rowStart = Resource.RowTop + Resource.SizeCell + 1;
            int columnStart = Resource.ColumnLeft + Resource.SizeCell * 2;
            WindowDrawConsole.Field(rowStart, columnStart);

            columnStart += Resource.SizeCell * 2 * Resource.SizeField + Resource.SizeCell * 2 * 2;
        }

        public void ViewGame(List<Player> players)
        {
            WindowDrawConsole.Clear();

            Console.WriteLine("View game");
            int width = Resource.SizeCell * Resource.SizeField * 2 * 2 + 8 * Resource.SizeCell;
            int height = Resource.SizeField * Resource.SizeCell + Resource.SizeCell * 2;

            WindowDrawConsole.Window("Setup Flotilla", width, height);

            int rowStart = Resource.RowTop + Resource.SizeCell;
            int columnStart = Resource.ColumnLeft + Resource.SizeCell * 2;
            WindowDrawConsole.Field(rowStart, columnStart);

            columnStart += Resource.SizeCell * 2 * Resource.SizeField + Resource.SizeCell * 2 * 2;
            WindowDrawConsole.Field(rowStart, columnStart);
        }

        
    }
}
