using SeaBattleBase.Players;
using SeaBattleBase.GamePlatforms;
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
        List<Player> players = new List<Player>();
        bool playerCurrent;
        GamePlatform platform = null!;

        public Game(GamePlatform platform)
        {
            this.platform = platform;
            HumanPlayer humanPlayer = new HumanPlayer(platform.Commander, platform.Shooter);
            //HumanPlayer humanPlayer = new HumanPlayer();
            //humanPlayer.Shooter = platform.Shooter;
            //humanPlayer.Commander = platform.Commander;

            players.Add(humanPlayer);
            players.Add(new ComputerPlayer());
        }

        public void SetupPlayers()
        {
            //foreach(Player player in players)
            platform.Viewer.Setup(players[0]);
            foreach (Player player in players)
                player.SetFlotiila();
        }
        public void View()
        {
            platform.Viewer.ViewGame(players);
        }

        public void Process()
        {
            HitType hit;

            while (true)
            {
                View();
                Point pointShot = players[playerCurrent.ToInt()].SetShot();
                hit = players[(!playerCurrent).ToInt()].GetShot(pointShot);

                // Destroy
                if (hit == HitType.Destroy)
                    if (players[(!playerCurrent).ToInt()].FlotillaSize == 0)
                        break;

                // Wound

                // Beside
                if (hit == HitType.Beside)
                    playerCurrent = !playerCurrent;
            }

        }

    }
}
