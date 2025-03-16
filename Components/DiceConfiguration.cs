using Spectre.Console;

namespace Foxxie911.DiceGame
{
    public class DiceConfiguration
    {
        public static Dice[] Parse(string[] args)
        {
            HandleDiceCountError(args);
            HandleFormatError(args);
            return [.. args.Select(arg => new Dice([.. arg.Split(",").Select(int.Parse)]))];

        }

        private static void HandleDiceCountError(string[] args)
        {
            if (args.Length < 3)
            {
                AnsiConsole.Markup($"[bold red]You have to pass three or more dices. You passed {args.Length}.[/]\n");
                Environment.Exit(0);
            }
        }

        private static void HandleFormatError(string[] args)
        {
            var LEGAL_FACE_COUNT = 6;
            if(args.Any(arg => arg.Split(",").Length != LEGAL_FACE_COUNT ||
            !arg.Split(",").All(f => int.TryParse(f, out int _))))
            {
                    AnsiConsole.Markup($"[bold red]You have to enter {LEGAL_FACE_COUNT} face values for each dice. (e.g. 2,2,4,4,5,5)[/]\n");
                    Environment.Exit(0);
            }
        }
    }
}