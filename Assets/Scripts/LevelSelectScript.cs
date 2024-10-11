using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelectScript : MonoBehaviour
{
    public void MainMenuButton()
    {
        SceneManager.LoadScene(1);

    }
    public void playLevelOne()
    {
        SceneManager.LoadScene(0);

    }
    public void playLevelTwo()
    {
        SceneManager.LoadScene("scene2");

    }

}
