using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScript : MonoBehaviour
{
    public void RestartHandler(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void ExitHandler(){
        SceneManager.LoadScene(0);

    }
}
