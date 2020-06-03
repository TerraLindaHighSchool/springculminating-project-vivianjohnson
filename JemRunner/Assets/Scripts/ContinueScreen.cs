using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ContinueScreen : MonoBehaviour
{
    public Button continueToLevelOneButton;
    public Button goBackToMainMenuButton; 

    public void GoToLevelOne()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene("Start"); 
    }
}
