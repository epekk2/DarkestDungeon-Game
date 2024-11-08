using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    // Start is called before the first frame update
    //void Start()
    //{
        
    //}

    //// Update is called once per frame
    //void Update()
    //{
        
    //}

    public void PlayGame()
    {
        SceneManager.LoadScene(1);

    }

    public void goMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void playLevelOne()
    {
        SceneManager.LoadScene(3);
    }

    public void playLevelTwo()
    {
        SceneManager.LoadScene(4);
    }

    public void playLevelThree()
    {
        SceneManager.LoadScene(5);
    }

    public void playLevelFour()
    {
        SceneManager.LoadScene(6);
    }

    public void playLevelFive()
    {
        SceneManager.LoadScene(7);
    }

    public void playLevelSix()
    {
        SceneManager.LoadScene(8);
    }

    public void playLevelSeven()
    {
        SceneManager.LoadScene(9);
    }

    public void playLevelEight()
    {
        SceneManager.LoadScene(10);
    }

    public void playLevelNine()
    {
        SceneManager.LoadScene(11);
    }
    public void playLevelTen()
    {
        SceneManager.LoadScene(12);
    }

    public void QuitGame()
    {
        Application.Quit();
    }




}
