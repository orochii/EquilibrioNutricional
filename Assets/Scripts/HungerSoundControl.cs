using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HungerSoundControl : MonoBehaviour {
    [SerializeField] private StatsManager stats;

    private AudioSource source;

    void Start() {
        source = GetComponent<AudioSource>();
    }

    void Update() {
        source.volume = (1 - stats.Hunger);
    }
}
