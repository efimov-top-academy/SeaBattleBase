using SeaBattleBase.Players;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeaBattleBase
{
    public static class Extensions
    {
        public static int ToInt(this bool value)
        {
            return value ? 1 : 0;
        }
    }
    public class Game
    {
        public List<Player> Players { set; get; } = new List<Player>();
        public bool PlayerCurrent { set; get; }

        public Game()
        {
            Players.Add(new Player());
            Players.Add(new Player());
        }

        public bool Step(Point pointShot)
        {
            HitType hit;

            //Point pointShot = Players[PlayerCurrent.ToInt()].SetShot();
            hit = Players[(!PlayerCurrent).ToInt()].GetShot(pointShot);

            // Destroy
            if (hit == HitType.Destroy)
                if (Players[(!PlayerCurrent).ToInt()].FlotillaSize == 0)
                    return false;

            // Wound

            // Beside
            if (hit == HitType.Beside)
                PlayerCurrent = !PlayerCurrent;

            return true;

        }

    }
}
