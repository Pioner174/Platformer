using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Finish : MonoBehaviour
{   
    [SerializeField] private GameObject LevelComnpleteCanvas;
    [SerializeField] private GameObject MessageUI;
    private bool _isActivated = false;

    public void Activate(){
        _isActivated = true;
        MessageUI.SetActive(false);
    }
    public void FinishLevel()
    {
        if (_isActivated){
            gameObject.SetActive(false);
            LevelComnpleteCanvas.SetActive(true);
        }
        else{
            MessageUI.SetActive(true);
        }

        
    }
}
