using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitDoor : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(!collision.CompareTag("Player"))
        {
            return;
        }
        SceneManager.LoadScene(3);//Index for Level End scene
    }
}
