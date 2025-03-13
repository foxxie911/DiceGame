using Foxxie911.DiceGame.Components;

class Program
{
    static void Main(string[] args)
    {
        var game = new Gameplay(args);
        game.StartGame();
    }
}