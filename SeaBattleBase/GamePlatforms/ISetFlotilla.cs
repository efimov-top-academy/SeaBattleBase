using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace SeaBattleBase.GamePlatforms
{
    public interface ISetFlotilla
    {
        List<Ship> SetShips(string name);
    }
}
