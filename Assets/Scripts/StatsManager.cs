using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsManager : MonoBehaviour {
    private static StatsManager m_instance;
    public static StatsManager Instance { get { return m_instance; } }

    [Header("Elementos de juego")]
    [SerializeField] private StatBar hungerBar;
    [SerializeField] private StatBar fatBar;
    [SerializeField] private StatBar sugarBar;
    [SerializeField] private StatBar vitaminBar;
    [SerializeField] private GameOverMessage gameOverMessage;
    [SerializeField] private Animator characterAnimator;

    [Header("Rangos mínimos-máximos para estados")]
    [SerializeField] private Vector2 hungerMinMax;
    [SerializeField] private Vector2 fatMinMax;
    [SerializeField] private Vector2 sugarMinMax;
    [SerializeField] private Vector2 vitaminMinMax;

    [Header("Tazas de cambio de estados por segundo")]
    [SerializeField] private float decreaseRatePerSecond = 0.05f;
    [SerializeField] private float decreaseRateSpeedUp = 0.0001f;
    [SerializeField] private float decreaseRateMax = 0.04f;
    [SerializeField] private float debuffMultiplier = 2f;
    [SerializeField] private float obesityMultiplier = 0.5f;
    [SerializeField] private float obesityCooldown = 10f;

    [Header("Audio")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip[] clips;

    private float ratePerSecond;
    private float obesityTimer = 0;
    private bool dead;
    private string deathCause = "¿Hackeaste el juego mal?";
    
    private void Awake() {
        m_instance = this;
    }

    void Start() {
        // Set up starting speed
        ratePerSecond = decreaseRatePerSecond;
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

    private string[] foodNames = new string[7] { "Aguacate", "Chocolate", "Ensalada", "Laxante", "Naranja", "Pizza", "Queque" };
    private int[] foodStatistics = new int[7];
    private int lastFood = -1;

    public void CalcEffect(int foodType) {
        // Update your eaten foods :)
        lastFood = foodType;
        foodStatistics[foodType]++;
        // Do effects per food type.
        switch(foodType) {
            case 0: //Aguacate - 
                Debug.Log("AGUACATE");
                hungerBar.Value += 0.2f;
                fatBar.Value += 0.1f;
                vitaminBar.Value += 0.3f;
                break;
            case 1: //Choco - 
                Debug.Log("CHOCO");
                hungerBar.Value += 0.2f;
                fatBar.Value += 0.1f;
                sugarBar.Value += 0.3f;
                break;
            case 2: //Ensalada -
                Debug.Log("ENSALADA");
                hungerBar.Value += 0.2f;
                fatBar.Value -= 0.1f;
                sugarBar.Value -= 0.1f;
                break;
            case 3: //Laxante - 
                Debug.Log("LAXANTE");
                hungerBar.Value -= 0.3f;
                fatBar.Value -= 0.3f;
                sugarBar.Value -= 0.2f;
                vitaminBar.Value -= 0.2f;
                break;
            case 4: //Naranaja - 
                Debug.Log("NARANJA");
                hungerBar.Value += 0.1f;
                vitaminBar.Value += 0.15f;
                break;
            case 5: //Pizza - 
                Debug.Log("PIZZA");
                hungerBar.Value += 0.5f;
                fatBar.Value += 0.4f;
                break;
            case 6: //Queque - 
                Debug.Log("QUEQUE");
                hungerBar.Value += 0.4f;
                fatBar.Value += 0.2f;
                sugarBar.Value += 0.3f;
                break;
            default:
                // Do nothing
                break;
        }
    }

    void Update() {
        // Do nothing if you're already dead.
        if (dead) return;
        // Raise rate per second
        ratePerSecond += (Time.deltaTime * decreaseRateSpeedUp);
        ratePerSecond = Mathf.Clamp(ratePerSecond, 0, decreaseRateMax);
        // Initialize all rates
        float hungerRate = ratePerSecond;
        float fatRate = ratePerSecond;
        float sugarRate = ratePerSecond;
        float vitaminRate = ratePerSecond;
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
            ScoreCounter scoreCounter = GameObject.FindObjectOfType<ScoreCounter>();
            string favouriteFood = GetFavourite();
            string lastFoodName = GetLastFood();
            gameOverMessage.CallLose(deathCause, scoreCounter.Score, favouriteFood, lastFoodName); // Cambiar 0 a un score real
            ChangeHealth(0);
            PlayDeath();
        } else {
            // Update character visible status.
            if (HasObesity) ChangeHealth(3);
            else {
                float fH = Mathf.Abs(fatBar.Value - .5f);
                float sH = Mathf.Abs(sugarBar.Value - .5f);
                float vH = Mathf.Abs(vitaminBar.Value - .5f);
                float hH = (1 - hungerBar.Value) / 3f;
                //
                //float health = (fH + sH + vH) / 3;
                float health = Mathf.Max(fH, sH, vH, hH);
                if (health > 0.25f) ChangeHealth(1);
                else ChangeHealth(2);
            }
        }
    }

    private string GetLastFood() {
        if (lastFood < 0) return "N/A";
        return foodNames[lastFood];
    }
    private string GetFavourite() {
        int favouriteId = -1;
        for (int i = 0; i < foodStatistics.Length; i++) {
            if (favouriteId < 0) favouriteId = i;
            else {
                if (foodStatistics[i] > 0 && 
                    foodStatistics[i] > foodStatistics[favouriteId]) favouriteId = i;
            }
        }
        if (favouriteId < 0) return "N/A";
        return foodNames[favouriteId];
    }

    private void PlayDeath() {
        StatAudioControl[] allStatAudios = GameObject.FindObjectsOfType<StatAudioControl>();
        foreach (StatAudioControl sa in allStatAudios) sa.Stop();
        // Play death
        audioSource.PlayOneShot(clips[0]);
    }

    // 0:dead 1:sick 2:healthy 3:fat
    private void ChangeHealth(int v) {
        float realValue = v / 3f;
        characterAnimator.SetFloat("health", realValue);
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
