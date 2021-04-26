using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeLevel : MonoBehaviour
{
    public void NextLevelHandler (){
        Scene scene = SceneManager.GetActiveScene();
<<<<<<< HEAD
        Debug.Log(scene.buildIndex);
        SceneManager.LoadScene(1);
=======
        SceneManager.LoadScene(scene.buildIndex + 1);

>>>>>>> 5daafb59a9d7a5cc695d8eb4ebff9e8c1014cdd3
    }
}
