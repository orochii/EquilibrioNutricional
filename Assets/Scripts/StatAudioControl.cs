using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatAudioControl : MonoBehaviour {
    public enum StatKind { FAT, SUGAR, VITAMIN }
    [SerializeField] private StatKind statKind;
    [SerializeField] private StatsManager stats;
    [SerializeField] private float distortionMultiplier = 1.2f;
    [SerializeField] private float distortionVolumeEffect = 0.5f;
    [SerializeField] private Vector2 lowPassMinMax = new Vector2Int(4200, 22000);
    private AudioSource audioSource;
    private AudioDistortionFilter distortionFilter;
    private AudioLowPassFilter lowPassFilter;
    private AudioEchoFilter echoFilter;

    void Start() {
        audioSource = GetComponent<AudioSource>();
        distortionFilter = GetComponent<AudioDistortionFilter>();
        lowPassFilter = GetComponent<AudioLowPassFilter>();
        echoFilter = GetComponent<AudioEchoFilter>();
    }

    void Update() {
        float val = GetVal();
        float distortion = Mathf.Abs(val - 0.5f) * 2;
        float volume = 1 - (distortion * distortionVolumeEffect);
        // Apply distortion
        distortionFilter.distortionLevel = distortion * distortionMultiplier;
        audioSource.volume = volume;
        // Update low pass filter
        lowPassFilter.cutoffFrequency = Mathf.Lerp(lowPassMinMax.y, lowPassMinMax.x, distortion);
        lowPassFilter.lowpassResonanceQ = Mathf.Lerp(1, 10, distortion);
        // Update echo filter
        echoFilter.delay = Mathf.Lerp(10, 100, distortion);
        echoFilter.wetMix = Mathf.Lerp(0, 1, distortion);
        echoFilter.dryMix = Mathf.Lerp(1, 0, distortion);
    }

    private float GetVal() {
        switch(statKind) {
            case StatKind.FAT:
                return stats.Fat;
            case StatKind.SUGAR:
                return stats.Sugar;
            case StatKind.VITAMIN:
                return stats.Vitamin;
            default:
                return 0;
        }
    }

    public void Stop() {
        audioSource.Stop();
    }
}
