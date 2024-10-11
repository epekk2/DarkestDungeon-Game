using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ColorTile : MonoBehaviour
{
    // Start is called before the first frame update
    SpriteRenderer sprite;
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    /*void Update()
    {
        
    }*/
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player"))
        {
            return;
        }
        //SceneManager.LoadScene(3);//Index for Level End scene
        //SceneManager.LoadScene("Level End");
        sprite.color = new Color(1, 1, 1, 1);
    }
}
