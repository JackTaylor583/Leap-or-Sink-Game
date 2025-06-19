using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HighScoreinMainMenu : MonoBehaviour
{
    public TextMeshProUGUI highscoreText;

    void Start() // Finds high score and displays it in the main menu
    {
        int savedHighScore = PlayerPrefs.GetInt("HighScore", 0);
        highscoreText.text = savedHighScore.ToString();
    }
}
