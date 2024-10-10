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
        int nextScene = SceneManager.GetActiveScene().buildIndex + 1;
        if (SceneManager.GetSceneByBuildIndex(nextScene).IsValid())
        {
            SceneManager.LoadScene(nextScene);
        } else
        {
            SceneManager.LoadScene("LevelSelection");
        }


    }
    public void SelectLevel()
    {
        SceneManager.LoadScene("LevelSelection");
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("TitleScene");
    }
}

