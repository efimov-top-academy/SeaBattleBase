using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeaBattleBase.GamePlatforms.ConsolePlatform
{
    public class SetShotConsole : ISetShot
    {
        public Point SetShot()
        {
            Console.WriteLine("Set Shot");
            return new Point();
        }
    }
}
