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
        scoreText.text = finalScore.ToString("000 000 000");
        gameObject.SetActive(true);
    }

    public void OnHomeClick() {
        SceneManager.LoadScene(0);
    }

    public void OnRestartClick() {
        SceneManager.LoadScene(1);
    }
}
