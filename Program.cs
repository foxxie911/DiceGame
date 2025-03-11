using System.Security.Cryptography;
using Spectre.Console;

class Program
{
    static void Main(string[] args)
    {
        if (args.Length < 3) {
            AnsiConsole.Markup($"[bold red]You have to pass three or more dices. You passed {args.Length}.[/]\n");
            Environment.Exit(0);
        }
        int argLength = args[0].Length;
        foreach(var arg in args){
            string[] facesAsStr = arg.Split(",");
            foreach(var faceAsStr in facesAsStr){
                if(!int.TryParse(faceAsStr, out int ignore)){
                    AnsiConsole.Markup("[bold red]Wrong format! You have to enter face values in comma separated numbers (e.g. 1,2,3,4,5)[/]\n");
                    Environment.Exit(0);
                }
            }
            if(arg.Length != argLength){ 
                AnsiConsole.Markup("[bold red]Each dice must have same amount of faces. (e.g. 1,2,3 10,15,20 24,25,26)[/]\n");
                Environment.Exit(0);
            }
            
        }
    }
}