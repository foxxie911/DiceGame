using Spectre.Console;

namespace Foxxie911.DiceGame.Components
{
    public class DiceConfiguration
    {
        public static Dice[] Parse(string[] args)
        {
            // Error handling
            HandleDiceCountError(args);
            HandleFormatError(args);

            // Parsing from arguments to Dice objects
            Dice[] dices = new Dice[args.Length];
            for (int i = 0; i < args.Length; i++)
            {
                string[] faces = args[i].Split(",");
                int[] faceValues = new int[faces.Length];
                for (int j = 0; j < faces.Length; j++)
                {
                    faceValues[j] = int.Parse(faces[j]);
                }
                dices[i] = new Dice(faceValues);
            }
            return dices;
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
            foreach (string arg in args)
            {
                string[] facesAsStrings = arg.Split(",");
                for (int j = 0; j < facesAsStrings.Length; j++)
                {
                    if (!int.TryParse(facesAsStrings[j], out int face))
                    {
                        AnsiConsole.Markup("[bold red]Wrong format! You have to enter face values in comma separated numbers (e.g. 1,2,3,4,5,6)[/]\n");
                        Environment.Exit(0);
                    }
                }
                if (facesAsStrings.Length != 6)
                {
                    AnsiConsole.Markup("[bold red]You have to enter 6 face values for each dice. (e.g. 2,2,4,4,5,5)[/]\n");
                    Environment.Exit(0);
                }
            }
        }
    }
}