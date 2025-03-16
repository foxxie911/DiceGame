using System.Security.Cryptography;
using Spectre.Console;

namespace Foxxie911.DiceGame
{
    public class Gameplay(string[] args)
    {
        List<Dice> dices = [new Dice([])];
        List<Dice> allDices = [new Dice([])];
        bool botFirst;
        Dice botDice = new Dice([]);
        Dice userDice = new Dice([]);

        public void StartGame()
        {
            dices = DiceConfiguration.Parse(args).ToList<Dice>();
            allDices = dices.ToList();
            
            AnsiConsole.Markup("[bold green]Welcome to the Dice Game![/]\n");
            
            GetFirstThrower();
            if (botFirst) BotFirstDiceSelection();
            if (!botFirst) UserFistDiceSelection();

            Console.WriteLine("It's my time to throw the dice");
            int botThrowValue = GetThrowValue(botDice);
            AnsiConsole.Markup($"My throw value is [bold green]{botThrowValue}[/]\n");

            Console.WriteLine("It's your turn to throw the dice");
            int userThrowValue = GetThrowValue(userDice);
            AnsiConsole.Markup($"Your throw value is [bold green]{userThrowValue}[/]\n");

            PrintResult(botThrowValue, userThrowValue);

        }

        private int GetUserInput()
        {
            Console.Write("Your selection: ");
            var userInput = Console.ReadLine();
             
            if (userInput == "?")
            {
                ProbabilityTable.PrintProbabilityTable(allDices);
                Console.Write("Your selection: ");
                userInput = Console.ReadLine();
            }
            if (userInput == "X") Environment.Exit(0);
            return int.Parse(userInput);
        }

        private bool GetFirstThrower()
        {
            AnsiConsole.Markup("Let's determine who will make the first move.\n");
            
            var botSelection = FairMoveGenerator.FirstMoveSelector();
            AnsiConsole.Markup("I have selected a random value in the range [bold]0-1[/].\n");
            AnsiConsole.Markup($"[bold green]HMAC:[/] [bold]{botSelection["HMAC"]}[/]\n");
            AnsiConsole.Markup("Try to guess my selection.\n");
            AnsiConsole.Markup("0 - 0\n1 - 1\nX - Exit\n? - Help\n");

            int userSelection = GetUserInput();
            AnsiConsole.Markup($"[bold green]My selection:[/] {botSelection["Message"]}\n");
            AnsiConsole.Markup($"[bold green]Key:[/] [bold]{botSelection["Key"]}[/]\n");

            if (int.Parse(botSelection["Message"]) != userSelection) return botFirst = true;
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
            Dice dice = dices.ElementAt(random);
            dices.RemoveAt(random);
            return dice;
        }

        private Dice GetUserDice()
        {
            AnsiConsole.Markup("Choose your dice:\n");

            dices.ForEach(dice => Console.WriteLine($"{dices.IndexOf(dice)} - {dice.PrintFaces()}"));
            AnsiConsole.Markup("X- Exit\n? - Help\n");
            
            Console.Write("Your selection: ");
            int userSelection = GetUserInput();
            Dice dice = dices.ElementAt(userSelection);
            dices.RemoveAt(userSelection);
            
            return dice;
        }

        private void PrintUserOption(int faceCount)
        {
            AnsiConsole.Markup("[bold green]Your options:[/]\n");
            for (int i = 0; i < faceCount; i++) Console.WriteLine($"{i} - {i}");
            AnsiConsole.Markup("X - Exit\n? - Help\n");
        }

        private int GetThrowValue(Dice dice)
        {
            var botThrowInput = FairMoveGenerator.ThrowInputSelector(dice.Faces.Length);
            AnsiConsole.Markup("I have selected a random value in the range [bold]0-5[/].\n");
            AnsiConsole.Markup($"[bold green]HMAC:[/] [bold]{botThrowInput["HMAC"]}[/]\n");

            PrintUserOption(userDice.Faces.Length);
            int userThrowInput = GetUserInput();

            AnsiConsole.Markup($"I have selected {botThrowInput["Message"]}\n");
            AnsiConsole.Markup($"[bold green]Key:[/] [bold]{botThrowInput["Key"]}[/]\n");
            
            int botThrowIndex = (int.Parse(botThrowInput["Message"]) + userThrowInput) % 6;
            return dice.Faces.ElementAt(botThrowIndex);
        }

        public static void PrintResult(int botThrowValue, int userThrowValue)
        {
            if (botThrowValue > userThrowValue) AnsiConsole.Markup("[bold red]You lose![/]\n");
            if (botThrowValue < userThrowValue) AnsiConsole.Markup("[bold green]You won![/]\n");
            if (botThrowValue == userThrowValue) AnsiConsole.Markup("[bold blue]It's a draw.[/]\n");
        }
    }
}