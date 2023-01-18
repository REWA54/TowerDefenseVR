using TMPro;
using UnityEngine;

public class SaveSystem : MonoBehaviour
{
    public TMP_Text highscoreUI;
    public int highScore;
    public void SaveIfNeeded(int score)
    {
        if (score > highScore)
        {
            PlayerPrefs.SetInt("HighScore", score);
        }
        UpdateUI();
    }
    private void Start()
    {
        highScore = PlayerPrefs.GetInt("HighScore", 0);
        UpdateUI();
    }
    void UpdateUI()
    {
        highscoreUI.text = highScore.ToString();
    }
}
