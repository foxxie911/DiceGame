namespace Foxxie911.DiceGame
{
    public class ProbabilityCalculator
    {
        public static double CalculateProbability(Dice chosenDice, Dice otherDice)
        {
            List<int> faces1 = [.. chosenDice.Faces];
            List<int> faces2 = [.. otherDice.Faces];

            return (double)faces1
            .SelectMany(f1 => faces2, (f1, f2) => f1 > f2 ? 1 : 0)
            .Sum() / (faces1.Count * faces2.Count);
        }
    }
}