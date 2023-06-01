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
            Console.WriteLine("Setup game");
            BoxView();
        }

        public void ViewGame(List<Player> players)
        {
            Console.WriteLine("View game");
        }

        void BoxView()
        {
            int width = Resource.SizeCell * 2 * 4 + Resource.SizeCell * 2 * Resource.SizeField;
            int height = Resource.SizeCell * 2 * 2 + Resource.SizeField * Resource.SizeCell;
            
            // border
            for(int row = 0; row < height; row++)
            {
                Console.SetCursorPosition(0, row);
                Console.BackgroundColor = Resource.ColorBorder;
                Console.Write("X");
            }
                
        }
    }
}
