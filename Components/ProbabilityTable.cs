using Spectre.Console;

namespace Foxxie911.DiceGame
{
    public class ProbabilityTable()
    {
        public static void PrintProbabilityTable(List<Dice> dices)
        {
            var table = new Table();
            table.Border(TableBorder.Rounded);
            table.Centered();
            table.AddColumn(new TableColumn(new Markup("Probability")));

            dices.ForEach(dice =>
            {
                table.AddColumn(new TableColumn(new Markup($"[bold green][{dice.PrintFaces()}][/]")));
            });

            dices.ForEach(dice =>
            {
                string[] rowString =
                [
                    $"[bold green][{dice.PrintFaces()}][/]",
                    .. dices
                    .Select(d => d == dice ? "x" : ProbabilityCalculator.CalculateProbability(dice, d)
                    .ToString("0.00000"))
,
                ];

                table.AddRow(rowString);
            });

            AnsiConsole.Write(table);
        }
    }
}