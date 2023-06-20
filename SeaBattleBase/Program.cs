using SeaBattleBase.GamePlatforms;
using SeaBattleBase.GamePlatforms.ConsolePlatform;
using Terminal.Gui;

namespace SeaBattleBase
{
    public class Program
    {
        static void Main(string[] args)
        {
            //GamePlatform platformConsole = new GamePlatform(new GameViewConsole(),
            //                                                new SetFlotillaConsole(),
            //                                                new SetShotConsole());
            //Game game = new Game(platformConsole);

            //game.SetupPlayers();
            //game.SetupFlottilas();

            //Console.ReadKey();
            //game.View();

            //game.Process();
            GameApplicationConsole gameApplicationConsole = new GameApplicationConsole();
            gameApplicationConsole.Execute();
        }
            
    }
}