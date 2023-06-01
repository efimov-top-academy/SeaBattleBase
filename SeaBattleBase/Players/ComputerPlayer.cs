using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SeaBattleBase;

namespace SeaBattleBase.Players
{
    public class ComputerPlayer : Player
    {
        Random random = new Random();
        public ComputerPlayer()
        {
        }

        public override void SetFlotiila()
        {
            Flotilla.Add(new Ship(2, 6, 4, Direction.Horizontal));
            Flotilla.Add(new Ship(0, 4, 3, Direction.Vertical));
            Flotilla.Add(new Ship(4, 5, 3, Direction.Horizontal));
            Flotilla.Add(new Ship(1, 2, 2, Direction.Vertical));
            Flotilla.Add(new Ship(4, 2, 2, Direction.Vertical));
            Flotilla.Add(new Ship(9, 3, 2, Direction.Horizontal));
            Flotilla.Add(new Ship(0, 9, 1, Direction.Horizontal));
            Flotilla.Add(new Ship(5, 0, 1, Direction.Horizontal));
            Flotilla.Add(new Ship(7, 3, 1, Direction.Horizontal));
            Flotilla.Add(new Ship(6, 6, 1, Direction.Horizontal));

            Field.SetFlotilla(Flotilla);
        }

        public override Point SetShot()
        {
            return new Point(random.Next(0, 9), random.Next(0, 9));
        }
    }
}
