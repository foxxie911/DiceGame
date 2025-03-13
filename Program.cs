using System.Security.Cryptography;
using Foxxie911.DiceGame.Components;
using Spectre.Console;

class Program
{
    static void Main(string[] args)
    {
        var game = new Gameplay(args);
        game.StartGame();
    }
}