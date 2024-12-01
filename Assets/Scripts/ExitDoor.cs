using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitDoor : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Save this level as completed
            int currentLevel = SceneManager.GetActiveScene().buildIndex;
            PlayerPrefs.SetInt("Level_" + currentLevel + "_Completed", 1);
            PlayerPrefs.Save();

            // Load the level select scene
            SceneManager.LoadScene(1); 
        }
    }
}
