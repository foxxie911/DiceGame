using System.Security.Cryptography;
using Foxxie911.DiceGame.Components;
using Spectre.Console;

class Program
{
    static void Main(string[] args)
    {
        var validity = new ValidityService(123);
        AnsiConsole.Markup($"[bold green]Message:[/] {validity.Message}\n");
        AnsiConsole.Markup($"[bold green]Key:[/] {validity.Key}\n");
        AnsiConsole.Markup($"[bold green]HMAC:[/] {validity.HMAC}\n");
    }
}