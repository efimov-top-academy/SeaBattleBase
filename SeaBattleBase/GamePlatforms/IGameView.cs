using SeaBattleBase.Players;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace SeaBattleBase.GamePlatforms
{
    public interface IGameView
    {
        void Setup(Player playerHuman);
	    void ViewGame(List<Player> players);
    }
}
