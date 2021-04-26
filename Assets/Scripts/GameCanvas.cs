﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCanvas : MonoBehaviour
{
    [SerializeField] private GameObject pauseCanvas;

    public void PauseHandler(){
        Time.timeScale = 0;
        pauseCanvas.SetActive(true);
        
    }
}
