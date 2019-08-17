using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsManager : MonoBehaviour {
    [Header("Elementos de juego")]
    [SerializeField] private StatBar hungerBar;
    [SerializeField] private StatBar fatBar;
    [SerializeField] private StatBar sugarBar;
    [SerializeField] private StatBar vitaminBar;
    [SerializeField] private GameOverMessage gameOverMessage;

    [Header("Rangos mínimos-máximos para estados")]
    [SerializeField] private Vector2 hungerMinMax;
    [SerializeField] private Vector2 fatMinMax;
    [SerializeField] private Vector2 sugarMinMax;
    [SerializeField] private Vector2 vitaminMinMax;

    [Header("Tazas de cambio de estados por segundo")]
    [SerializeField] private float decreaseRatePerSecond = 0.05f;
    [SerializeField] private float debuffMultiplier = 2f;
    [SerializeField] private float obesityMultiplier = 0.5f;
    [SerializeField] private float obesityCooldown = 10f;

    private float obesityTimer = 0;
    private bool dead;
    private string deathCause = "¿Hackeaste el juego mal?";

    void Start() {
        // Randomize starting stats.
        float perc = UnityEngine.Random.Range(0.25f, 0.75f);
        hungerBar.Value = Mathf.Lerp(hungerMinMax.x, hungerMinMax.y, perc);
        perc = UnityEngine.Random.Range(0.25f, 0.75f);
        fatBar.Value = Mathf.Lerp(fatMinMax.x, fatMinMax.y, perc);
        perc = UnityEngine.Random.Range(0.25f, 0.75f);
        sugarBar.Value = Mathf.Lerp(sugarMinMax.x, sugarMinMax.y, perc);
        perc = UnityEngine.Random.Range(0.25f, 0.75f);
        vitaminBar.Value = Mathf.Lerp(vitaminMinMax.x, vitaminMinMax.y, perc);
    }

    void Update() {
        // Do nothing if you're already dead.
        if (dead) return;
        // Initialize all rates
        float hungerRate = decreaseRatePerSecond;
        float fatRate = decreaseRatePerSecond;
        float sugarRate = decreaseRatePerSecond;
        float vitaminRate = decreaseRatePerSecond;
        /*
         * Calculate hunger rate and obesity effects.
         */
        if (!HasObesity) {
            if (hungerBar.Value > hungerMinMax.y) obesityTimer = Time.time + obesityCooldown;
        } else {
            hungerRate *= obesityMultiplier;
            fatRate *= obesityMultiplier;
            sugarRate *= obesityMultiplier;
            vitaminRate *= obesityMultiplier;
        }
        // Efecto de estados en otros estados
        if (sugarBar.Value < sugarMinMax.x) hungerRate *= debuffMultiplier; // Si el azucar está baja
        if (vitaminBar.Value > vitaminMinMax.y) hungerRate *= debuffMultiplier; // Si las vitaminas están altas
        if (fatBar.Value < fatMinMax.x) vitaminRate *= debuffMultiplier; // Si la grasa está baja
        // Submit all changes
        hungerBar.Value -= hungerRate * Time.deltaTime;
        fatBar.Value -= fatRate * Time.deltaTime;
        sugarBar.Value -= sugarRate * Time.deltaTime;
        vitaminBar.Value -= vitaminRate * Time.deltaTime;
        // Check if you're... uh... sleeping with the fish -_- zZZ
        // Muerte por inanición
        if (hungerBar.Value <= hungerMinMax.x) {
            dead = true;
            deathCause = "Inanición";
        }
        // Muerte por paro cardíaco
        if (fatBar.Value > fatMinMax.y) {
            dead = true;
            deathCause = "Paro cardíaco";
        }
        // Muerte por coma diabético
        if (sugarBar.Value > sugarMinMax.y) {
            dead = true;
            deathCause = "Coma diabético";
        }
        // Muerte por escorbuto
        if (vitaminBar.Value <= vitaminMinMax.x) {
            dead = true;
            deathCause = "Escorbuto";
        }
        // Do something if dead
        if (dead) {
            Debug.Log("Causa: " + deathCause);
            gameOverMessage.CallLose(deathCause, 123456789); // Cambiar 0 a un score real
        }
    }

    // Accessors to values/states
    public bool HasObesity { get { return obesityTimer > Time.time; } }
    public float Hunger {
        get {
            return hungerBar.Value;
        }
        set {
            hungerBar.Value = value;
        }
    }
    public float Fat {
        get {
            return fatBar.Value;
        }
        set {
            fatBar.Value = value;
        }
    }
    public float Sugar {
        get {
            return sugarBar.Value;
        }
        set {
            sugarBar.Value = value;
        }
    }
    public float Vitamin {
        get {
            return vitaminBar.Value;
        }
        set {
            vitaminBar.Value = value;
        }
    }
    public bool Dead { get { return dead; } }
}
