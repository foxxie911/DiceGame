namespace Foxxie911.DiceGame
{
    public class Dice(int[] faces)
    {
        int[] faces = faces;

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

        public string PrintFaces()
        {
            string facesString = "[";
            for(int i = 0; i < faces.Length; i++){
                facesString += faces[i] ;
                if(i < faces.Length-1){
                    facesString += ", ";
                }
            }
            facesString += "]";
            return facesString;
        }

    }
}