using System.Text;

namespace Foxxie911.DiceGame.Components
{
    public class Dice(int[] faces)
    {
        private readonly int[] faces = faces;

        public int GetFaceValue(int faceIndex)
        {
            return faces[faceIndex];
        }

        public int[] Faces
        {
            get
            {
                return faces;
            }
        }

        public string PrintFaces(int[] faces)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append('[');
            for (int i = 0; i < faces.Length; i++)
            {
                sb.Append(faces[i]);
                if (i < faces.Length - 1)
                {
                    sb.Append(", ");
                }
            }
            sb.Append("]");
            return sb.ToString();
        }

    }
}