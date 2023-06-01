using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeaBattleBase.GamePlatforms
{
    public class GamePlatform
    {
        public IGameView Viewer { get; } = null!;
        public ISetShot Shooter { get; } = null!;
        public ISetFlotilla Commander { get; } = null!;
        public GamePlatform(IGameView viewer, ISetFlotilla commander, ISetShot shooter)
        {
            Viewer = viewer;
            Shooter = shooter;
            Commander = commander;
        }
    }
}
