using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

public class ScoreCounter : MonoBehaviour {
    [SerializeField] Text scoreMarker;
    private StatsManager stats;
    private float secondsElapsed;
    private int score;
    public int Score { get { return score; } }

    void Start() {
        stats = GameObject.FindObjectOfType<StatsManager>();
    }

    void Update() {
        if (!stats.Dead) {
            secondsElapsed += Time.deltaTime;
            if (secondsElapsed > 1) {
                // Discount from seconds elapsed the integer amount of seconds.
                int wholeSeconds = (int)secondsElapsed;
                secondsElapsed -= wholeSeconds;
                // Now add the whole seconds to the score counter.
                score += wholeSeconds;
                // Update the score marker.
                scoreMarker.text = score.ToString("000 000 000");
            }
        }
    }
}