using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuActions : MonoBehaviour
{
    public void LoadByIndex(int sceneIndex) {
        SceneManager.LoadScene(sceneIndex);
    }
    
    public void ExitGame() {
        // TODO: Show a popup dialog
        // Convert this to a static method
        Application.Quit();
    }
}
