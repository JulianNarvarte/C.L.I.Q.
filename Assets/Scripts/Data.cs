using System.Collections.Generic;
using System.Diagnostics;

[System.Serializable]

public class Data
{
    public int IQ;
    
    public int[,] Scores = new int[27,3];

    //public string[] NombresLvl = new string[9];

    public Data(GameManager gamemanager)
    {
        IQ = gamemanager.IQ;
        for (int i = 0; i < Scores.GetLength(0); i++)
        {
            for (int c = 0; c < Scores.GetLength(1); c++)
            {
                Scores[i,c] = gamemanager.NivelesScore[i,c];
            }
        }
    }
}
