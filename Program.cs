using Foxxie911.DiceGame;

class Program
{
    static void Main(string[] args)
    {
        var game = new Gameplay(args);
        game.StartGame();
    }
}