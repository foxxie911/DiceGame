using System.Security.Cryptography;
using Spectre.Console;

namespace Foxxie911.DiceGame
{
    public class Gameplay(string[] args)
    {
        List<Dice> dices = [new Dice([])];
        bool botFirst;
        Dice botDice = new Dice([]);
        Dice userDice = new Dice([]);

        public void StartGame()
        {
            dices = DiceConfiguration.Parse(args).ToList<Dice>();
            AnsiConsole.Markup("[bold green]Welcome to the Dice Game![/]\n");
            GetFirstThrower();
            if (botFirst)
            {
                BotFirstDiceSelection();
            }
            if (!botFirst)
            {
                UserFistDiceSelection();
            }

            int botThrowValue = GetBotThrowValue();
            AnsiConsole.Markup($"My throw value is [bold green]{botThrowValue}[/]\n");

            int userThrowValue = GetUserThrowValue();
            AnsiConsole.Markup($"Your throw value is [bold green]{userThrowValue}[/]\n");

            PrintResult(botThrowValue, userThrowValue);

        }

        private bool GetFirstThrower()
        {
            AnsiConsole.Markup("Let's determine who will make the first move.\n");
            var botInput = FairMoveGenerator.FirstMoveSelector();
            AnsiConsole.Markup("I have selected a random value in the range [bold]0-1[/].\n");
            AnsiConsole.Markup($"[bold green]HMAC:[/] [bold]{botInput["HMAC"]}[/]\n");
            AnsiConsole.Markup("Try to guess my selection.\n");
            AnsiConsole.Markup("0 - 0\n1 - 1\nX - Exit\n? - Help\n");
            AnsiConsole.Markup("Your selection: ");
            var userInput = Console.ReadLine();
            if (userInput == "?")
            {
                ProbabilityTable.PrintProbabilityTable(dices);
                Console.Write("Your selection: ");
                userInput = Console.ReadLine();
            }
            if (userInput == "X") Environment.Exit(0);
            AnsiConsole.Markup($"[bold green]My selection:[/] {botInput["Message"]}\n");
            AnsiConsole.Markup($"[bold green]Key:[/] {botInput["Key"]}\n");
            int botSelectionInt = int.Parse(botInput["Message"]);
#pragma warning disable CS8604 // Possible null reference argument.
            int userSelectionInt = int.Parse(userInput);
#pragma warning restore CS8604 // Possible null reference argument.
            if (botSelectionInt != userSelectionInt) return botFirst = true;
            return botFirst = false;
        }

        private void BotFirstDiceSelection()
        {
            botDice = GetBotDice();
            AnsiConsole.Markup($"I will choose the dice first.\n");
            AnsiConsole.Markup($"I have choosen [bold green][{botDice.PrintFaces()}][/] dice.\n");
            userDice = GetUserDice();
            AnsiConsole.Markup($"You have selected [bold green][{userDice.PrintFaces()}][/] dice.\n");
        }

        private void UserFistDiceSelection()
        {
            AnsiConsole.Markup("You will make the first move.\n");
            userDice = GetUserDice();
            AnsiConsole.Markup($"You have selected [bold green][{userDice.PrintFaces()}][/] dice.\n");
            botDice = GetBotDice();
            AnsiConsole.Markup($"I have chosen [bold green][{botDice.PrintFaces()}][/] dice.\n");
        }

        private Dice GetBotDice()
        {
            var random = RandomNumberGenerator.GetInt32(dices.Count);
            Dice botDice = dices.ElementAt(random);
            dices.RemoveAt(random);
            return botDice;
        }

        private Dice GetUserDice()
        {
            AnsiConsole.Markup("Choose your dice:\n");
            foreach (Dice dice in dices)
            {
                Console.WriteLine($"{dices.IndexOf(dice)} - {dice.PrintFaces()}");
            }
            AnsiConsole.Markup("X- Exit\n? - Help\n");
            Console.Write("Your selection: ");
            var userInput = Console.ReadLine();
            if (userInput == "?")
            {
                ProbabilityTable.PrintProbabilityTable(dices);
                Console.Write("Your selection: ");
                userInput = Console.ReadLine();
            }
            if (userInput == "X") Environment.Exit(0);
#pragma warning disable CS8604 // Possible null reference argument.
            var userDice = dices.ElementAt(int.Parse(userInput));
#pragma warning restore CS8604 // Possible null reference argument.
            dices.RemoveAt(int.Parse(userInput));
            return userDice;
        }

        private int GetBotThrowValue()
        {
            AnsiConsole.Markup("It's my time to throw the dice.\n");
            var botThrowInput = FairMoveGenerator.ThrowInputSelector();
            AnsiConsole.Markup("I have selected a random value in the range [bold]0-5[/].\n");
            AnsiConsole.Markup($"[bold green]HMAC:[/] [bold]{botThrowInput["HMAC"]}[/]\n");

            AnsiConsole.Markup("[bold green]Your options:[/]\n");
            AnsiConsole.Markup("0 - 0\n1 - 1\n2 - 2\n3 - 3\n4 - 4\n5 - 5\nX - Exit\n? - Help\n");
            AnsiConsole.Markup("Your selection: ");
            var userInput = Console.ReadLine();
            if (userInput == "?")
            {
                ProbabilityTable.PrintProbabilityTable(dices);
                Console.Write("Your selection: ");
                userInput = Console.ReadLine();
            }
            if (userInput == "X") Environment.Exit(0);
#pragma warning disable CS8604 // Possible null reference argument.
            int userThrowInput = int.Parse(userInput);
#pragma warning restore CS8604 // Possible null reference argument.

            AnsiConsole.Markup($"I have selected {botThrowInput["Message"]}\n");
            AnsiConsole.Markup($"[bold green]Key:[/] {botThrowInput["Key"]}\n");
            int botThrowIndex = (int.Parse(botThrowInput["Message"]) + userThrowInput) % 6;
            return botDice.Faces.ElementAt(botThrowIndex);
        }

        private int GetUserThrowValue()
        {
            AnsiConsole.Markup("It's your turn to throw the dice.\n");

            var botThrowInput = FairMoveGenerator.ThrowInputSelector();
            AnsiConsole.Markup("I have selected a random value in the range [bold]0-5[/].\n");
            AnsiConsole.Markup($"[bold green]HMAC:[/] [bold]{botThrowInput["HMAC"]}[/]\n");

            AnsiConsole.Markup("[bold green]Your options:[/]\n");
            AnsiConsole.Markup("0 - 0\n1 - 1\n2 - 2\n3 - 3\n4 - 4\n5 - 5\nX - Exit\n? - Help\n");
            AnsiConsole.Markup("Your selection: ");
            var userInput = Console.ReadLine();
            if (userInput == "?")
            {
                ProbabilityTable.PrintProbabilityTable(dices);
                Console.Write("Your selection: ");
                userInput = Console.ReadLine();
            }
            if (userInput == "X") Environment.Exit(0);
#pragma warning disable CS8604 // Possible null reference argument.
            int userThrowInput = int.Parse(userInput);
#pragma warning restore CS8604 // Possible null reference argument.

            AnsiConsole.Markup($"I have selected {botThrowInput["Message"]}\n");
            AnsiConsole.Markup($"[bold green]Key:[/] {botThrowInput["Key"]}\n");
            int userThrowIndex = (int.Parse(botThrowInput["Message"]) + userThrowInput) % 6;
            return userDice.Faces.ElementAt(userThrowIndex);
        }

        public static void PrintResult(int botThrowValue, int userThrowValue)
        {
            if (botThrowValue > userThrowValue) AnsiConsole.Markup("[bold red]You lose![/]\n");
            if (botThrowValue < userThrowValue) AnsiConsole.Markup("[bold green]You won![/]\n");
            if (botThrowValue == userThrowValue) AnsiConsole.Markup("[bold blue]It's a draw.[/]\n");
        }
    }
}