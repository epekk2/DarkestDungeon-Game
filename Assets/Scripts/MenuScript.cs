using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuScript : MonoBehaviour
{
    // Start is called before the first frame update
    public levelManage gm1;
    void Start()
    {
        //GameManager.Awake();
        //GameManager.instance.Awake();

    }

    //// Update is called once per frame
    //void Update()
    //{

    //}

    public void PlayGame()
    {
        SceneManager.LoadScene(1);

    }

    public void playLevelOne()
    {
        SceneManager.LoadScene(2);
    }

    public void playLevelTwo()
    {
        if (levelManage.currentLevel >= 2)
        {
            SceneManager.LoadScene(3);
        }
    }
    public void playLevelThree()
    {
        if (levelManage.currentLevel >= 3)
        {
            SceneManager.LoadScene(6);
        }

    }

    public void QuitGame()
    {
        Application.Quit();
    }




}
