using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Settings : MonoBehaviour
{
    // Start is called before the first frame update
    private static int difficulty = 1;
    

    public  void SetDifficulty(int diff)
    {
        difficulty = diff;
    }
    public static int GetDifficulty()
    {
        return difficulty;
    }
}
