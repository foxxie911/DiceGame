namespace Foxxie911.DiceGame
{
    public class ProbabilityCalculator
    {
        public static double CalculateProbability(Dice chosenDice, Dice ohterDice)
        {
            int[] faces1 = chosenDice.Faces;
            int[] faces2 = ohterDice.Faces;
            var totalCombinations = faces1.Length * faces2.Length;
            var favorableCombinations = 0;
            for (int i = 0; i < faces1.Length; i++)
            {
                for (int j = 0; j < faces2.Length; j++)
                {
                    if (faces1[i] > faces2[j])
                    {
                        favorableCombinations++;
                    }
                }
            }
            return (double)favorableCombinations / totalCombinations;
        }
    }
}