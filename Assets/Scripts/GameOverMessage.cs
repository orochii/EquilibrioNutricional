using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Globalization;

public class GameOverMessage : MonoBehaviour {
    [SerializeField] private Text deathCauseText;
    [SerializeField] private Text scoreText;

    private int finalScore;
    

    public void CallLose(string reason, int score = 0) {
        deathCauseText.text = reason;
        finalScore = score;
        NumberFormatInfo nfi = (NumberFormatInfo)CultureInfo.InvariantCulture.NumberFormat.Clone();
        nfi.NumberGroupSeparator = " ";
        scoreText.text = finalScore.ToString("n", nfi);
        gameObject.SetActive(true);
    }

    public void OnRestartClick() {
        SceneManager.LoadScene(0);
    }
}
