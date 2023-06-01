using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeaBattleBase.GamePlatforms.ConsolePlatform
{
    public class SetFlotillaConsole : ISetFlotilla
    {
        public List<Ship> SetShips()
        {
            Console.WriteLine("Set Flotilla");
            return new List<Ship>();
        }
    }
}
