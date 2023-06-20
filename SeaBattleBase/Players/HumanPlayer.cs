using SeaBattleBase.GamePlatforms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeaBattleBase.Players
{
    /*
    public class HumanPlayer : Player
    {
        public ISetFlotilla Commander { get; set; } = null!;
        public ISetShot Shooter { get; set; } = null!;
        public HumanPlayer(string name) : base(name)
        {
        }
        public HumanPlayer(ISetFlotilla commander, ISetShot shooter)
        {
            Commander = commander;
            Shooter = shooter;
        }

        public override void SetFlotiila()
        {
            this.Flotilla = Commander.SetShips(this.Name);
            Field.SetFlotilla(this.Flotilla);
        }

        public override Point SetShot()
        {
            return Shooter.SetShot();
        }
    }
    */
}
