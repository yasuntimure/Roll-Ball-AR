﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    // Start is called before the first frame update
    public void PauseGame()
    {
        Time.timeScale = 0;
    }

    // Update is called once per frame
    public void ResumeGame()
    {
        Time.timeScale = 1;
    }
}
