using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelEnd : MonoBehaviour
{
    // Start is called before the first frame update
    //Scene levelSelectScene = SceneManager.GetSceneByName("LevelSelection");
    //int levelSelectScene1 = SceneManager.

    public void NextLevel()
    {
        int currLevel = 3;
        if (currLevel < 11)
        {
            SceneManager.LoadScene(++currLevel);
        } else
        {
            SceneManager.LoadScene(1);
            currLevel = 3;
        }
        
    }

    public void SelectLevel()
    {
        SceneManager.LoadScene(1);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("TitleScene");
    }
}

