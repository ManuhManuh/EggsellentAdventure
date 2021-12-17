using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{

    public void LoadChickenYard()
    {
        SceneManager.LoadScene("ChickenYard");
    }

    public void LoadInstructions()
    {
        SceneManager.LoadScene("Instructions");
    }

    public void LoadTitleScene()
    {
        SceneManager.LoadScene("TitleScene");
    }

    public void QuitLikeAChicken()
    {
        Application.Quit();
    }

}
