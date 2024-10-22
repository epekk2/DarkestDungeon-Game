using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class levelManage : MonoBehaviour
{
    public static levelManage instance;
    public static int currentLevel;
    public static Dictionary<int, int> buildIndexToLevel = new Dictionary<int, int>();

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            currentLevel = 1;
            //DontDestroyOnLoad(gameObject);
            // Adding key-value pairs to the dictionary
            //key:buildIndex, value: level number
            //add all levels
            
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        buildIndexToLevel.Add(2, 1);
        buildIndexToLevel.Add(3, 2);
        buildIndexToLevel.Add(6, 3);
    }

    public static int updateLevel(int buildSceneIndex)
    {
        currentLevel = levelManage.buildIndexToLevel[buildSceneIndex] + 1;
        return 0;
    }



}
