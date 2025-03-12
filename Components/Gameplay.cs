// TODO Implement the whome gameplay logic.

using System.Security.Cryptography;
using Spectre.Console;

namespace Foxxie911.DiceGame.Components
{
    public class Gameplay(List<Dice> dices)
    {
        bool botFirst;
        public void StartGame(string[] args)
        {
            Dice botDice = new Dice([]);
            Dice userDice = new Dice([]);
            AnsiConsole.Markup("[bold green]Welcome to the Dice Game![/]\n");
            AnsiConsole.Markup("Let's determine who will make the first move.\n");
            var firstMove = FairMoveGenerator.FirstMoveSelector();
            AnsiConsole.Markup("I have selected a random value in the range [bold]0-1[/].\n");
            AnsiConsole.Markup($"[bold green]HMAC:[/] [bold]{firstMove["HMAC"]}[/]\n");
            AnsiConsole.Markup("Try to guess my selection.\n");
            AnsiConsole.Markup("0 - 0\n1 - 1\nX - Exit\n? - Help\n");
            AnsiConsole.Markup("Your selection: ");
            var userInput = Console.ReadLine();
            Console.WriteLine();
            AnsiConsole.Markup($"[bold green]My selection:[/] {firstMove["Message"]}\n");
            AnsiConsole.Markup($"[bold green]Key:[/] {firstMove["Key"]}\n");
            GetFirstThrower(firstMove["Message"], userInput);
            if (botFirst)
            {
                botDice = GetBotDice(dices);
                AnsiConsole.Markup("I will choose the dice first.\n");
                AnsiConsole.Markup($"I have choosen [bold green]{botDice.PrintFaces}[/] dice.\n");
                userDice = GetUserDice(dices);
                AnsiConsole.Markup($"You have selected [bold green]{userDice.PrintFaces}[/] dice.\n");
            }
            if (!botFirst)
            {
                AnsiConsole.Markup("You will make the first move.\n");
                userDice = GetUserDice(dices);
                AnsiConsole.Markup($"You have selected [bold green]{userDice.PrintFaces}[/] dice.\n");
                botDice = GetBotDice(dices);
                AnsiConsole.Markup($"I have chosen [bold green]{botDice.PrintFaces}[/] dice.\n");
            }

            AnsiConsole.Markup("It's my time to throw the dice.\n");
            var botThrowInput = FairMoveGenerator.ThrowInputSelector();
            AnsiConsole.Markup("I have selected a random value in the range [bold]0-5[/].\n");
            AnsiConsole.Markup($"[bold green]HMAC:[/] [bold]{botThrowInput["HMAC"]}[/]\n");

            AnsiConsole.Markup("[bold green]Your options:[/]\n");
            AnsiConsole.Markup("0 - 0\n1 - 1\n2 - 2\n3 - 3\n4 - 4\n5 - 5\nX - Exit\n? - Help\n");
            AnsiConsole.Markup("Your selection: ");
            userInput = Console.ReadLine();
            int userThrowInput = int.Parse(userInput);

            AnsiConsole.Markup($"I have selected {botThrowInput["Message"]}\n");
            AnsiConsole.Markup($"[bold green]Key:[/] {botThrowInput["Key"]}\n");
            int botThrowIndex = int.Parse(botThrowInput["Message"]) + userThrowInput % 6;
            var botThrowValue = botDice.Faces.ElementAt(botThrowIndex);
            AnsiConsole.Markup("My throw value is [bold green]{botThrowValue}[/]\n");

            AnsiConsole.Markup("Now your turn to throw the dice.\n");

            botThrowInput = FairMoveGenerator.ThrowInputSelector();
            AnsiConsole.Markup("I have selected a random value in the range [bold]0-5[/].\n");
            AnsiConsole.Markup($"[bold green]HMAC:[/] [bold]{botThrowInput["HMAC"]}[/]\n");

            AnsiConsole.Markup("[bold green]Your options:[/]\n");
            AnsiConsole.Markup("0 - 0\n1 - 1\n2 - 2\n3 - 3\n4 - 4\n5 - 5\nX - Exit\n? - Help\n");
            AnsiConsole.Markup("Your selection: ");
            userInput = Console.ReadLine();
            userThrowInput = int.Parse(userInput);

            AnsiConsole.Markup($"I have selected {botThrowInput["Message"]}\n");
            AnsiConsole.Markup($"[bold green]Key:[/] {botThrowInput["Key"]}\n");
            int userThrowIndex = int.Parse(botThrowInput["Message"]) + userThrowInput % 6;
            var userThrowValue = userDice.Faces.ElementAt(userThrowIndex);
            AnsiConsole.Markup("Your throw value is [bold green]{userThrowValue}[/]\n");

            if (userThrowValue > botThrowValue) AnsiConsole.Markup($"You have won the game! {userThrowValue} > {botThrowValue}\n");
            if (userThrowValue < botThrowValue) AnsiConsole.Markup($"I have won the game! {userThrowValue} < {botThrowValue}\n");
            if (userThrowValue == botThrowValue) AnsiConsole.Markup($"It's a draw! {userThrowValue} == {botThrowValue}\n");
            // TODO Implement user throw input function.


        }

        private bool GetFirstThrower(string botSelection, string userSelection)
        {
            int botSelectionInt = int.Parse(botSelection);
            int userSelectionInt = int.Parse(userSelection);
            if (botSelectionInt != userSelectionInt) return botFirst = true;
            return botFirst = false;
        }

        private static Dice GetBotDice(List<Dice> dices)
        {
            var random = RandomNumberGenerator.GetInt32(dices.Count);
            Dice botDice = dices.ElementAt(random);
            dices.RemoveAt(random);
            return botDice;
        }

        private static Dice GetUserDice(List<Dice> dices)
        {
            AnsiConsole.Markup("Choose your dice:\n");
            foreach (var dice in dices)
            {
                AnsiConsole.Markup($"{dices.IndexOf(dice)} - {dice.PrintFaces}\n");
            }
            AnsiConsole.Markup("X- Exit\n? - Help\n");
            var userInput = Console.ReadLine();
            if (userInput == "X") Environment.Exit(0);
            // if(userInput == "?") Help(); TODO implement Help method
            var userDice = dices.ElementAt(int.Parse(userInput));
            dices.RemoveAt(int.Parse(userInput));
            return userDice;
        }
    }
}