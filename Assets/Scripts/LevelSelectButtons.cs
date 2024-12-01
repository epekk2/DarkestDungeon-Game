using UnityEngine;
using UnityEngine.UI;
using static System.Net.Mime.MediaTypeNames;

public class LevelSelectButtons : MonoBehaviour
{
    public Button[] levelButtons;  // Assign all 9 buttons in the inspector
    public Color completedLevelColor = Color.green;

    void Start()
    {
        UpdateButtonColors();
    }

    void UpdateButtonColors()
    {
        for (int i = 0; i < levelButtons.Length; i++)
        {
            // The actual scene index for each level
            int levelSceneIndex = i + 3;  // Assuming levels start at index 3

            // Check if this level was completed
            if (PlayerPrefs.GetInt("Level_" + levelSceneIndex + "_Completed", 0) == 1)
            {
                // Change the button color
                UnityEngine.UI.Image buttonImage = levelButtons[i].GetComponent<UnityEngine.UI.Image>();
                if (buttonImage != null)
                {
                    buttonImage.color = completedLevelColor;
                }
            }
        }
    }
}