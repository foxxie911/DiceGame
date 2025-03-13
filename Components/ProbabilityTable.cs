using Spectre.Console;

namespace Foxxie911.DiceGame.Components
{
    public class ProbabilityTable()
    {
        public static void PrintProbabilityTable(List<Dice> dices)
        {
            var table = new Table();
            table.Border(TableBorder.Rounded);
            table.Centered();
            table.AddColumn(new TableColumn(new Markup("Probability")));
            foreach(Dice dice in dices){
                string diceFaces = dice.PrintFaces();
                table.AddColumn(new TableColumn(new Markup($"[bold green][{diceFaces}][/]")));
            }

            for (int i = 0; i < dices.Count; i++)
            {
                string[] rowString = new string[dices.Count + 1];
                rowString[0] = $"[bold green][{dices[i].PrintFaces()}][/]";
                for(int j = 1; j < rowString.Length; j++){
                    if(dices[i] == dices[j-1]){
                        rowString[j] = "x";
                        continue;
                    }
                    double probability = ProbabilityCalculator.CalculateProbability(dices[i], dices[j-1]);
                    rowString[j] = probability.ToString("0.00000");
                }
                table.AddRow(rowString);
            }
            AnsiConsole.Write(table);
        }
    }
}