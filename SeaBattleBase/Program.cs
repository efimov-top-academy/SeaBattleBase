using SeaBattleBase.GamePlatforms;
using SeaBattleBase.GamePlatforms.ConsolePlatform;

namespace SeaBattleBase
{
    public class Program
    {
        static void Main(string[] args)
        {
            GamePlatform platformConsole = new GamePlatform(new GameViewConsole(),
                                                            new SetFlotillaConsole(),
                                                            new SetShotConsole());
            Game game = new Game(platformConsole);

            game.SetupPlayers();
            game.View();

            //game.Process();
        }
    }
}