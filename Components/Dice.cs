namespace Foxxie911.DiceGame
{
    public class Dice(int[] faces)
    {
        readonly int[] faces = faces;

        public int[] Faces
        {
            get
            {
                return faces;
            }
        }

        public string PrintFaces()
        {
            return $"[{string.Join(", ", faces)}]";
        }

    }
}