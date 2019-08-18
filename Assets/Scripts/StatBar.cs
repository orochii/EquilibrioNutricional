using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatBar : MonoBehaviour {
    public enum BarDirection { HORIZONTAL, VERTICAL }

    [SerializeField] private Image bar;
    [SerializeField] private BarDirection barDirection;
    private Vector2 startingSize;
    private Vector2 startingRect;
    [Range(0,1)] [SerializeField] private float m_value = .5f;
    [SerializeField] private bool useFill = false;
    
    public float Value {
        get {
            return m_value;
        }
        set {
            m_value = Mathf.Clamp01(value);
            RefreshSize();
        }
    }

    private void RefreshSize() {
        // Using fill is easy, and kewt
        if (useFill) {
            bar.fillAmount = m_value;
            return;
        }
        Vector2 newSize = startingRect * m_value;
        if (barDirection == BarDirection.HORIZONTAL) {
            bar.rectTransform.sizeDelta = new Vector2(newSize.x, startingRect.y);
        } else {
            bar.rectTransform.sizeDelta = new Vector2(startingRect.x, newSize.y);
        }
    }

    void Awake() {
        float rx = bar.rectTransform.rect.width;
        float ry = bar.rectTransform.rect.height;
        startingSize = new Vector2(bar.rectTransform.sizeDelta.x, bar.rectTransform.sizeDelta.y);
        startingRect = new Vector2(rx, ry);
        RefreshSize();
    }

    private void FixedUpdate() {
        RefreshSize();
    }
}
