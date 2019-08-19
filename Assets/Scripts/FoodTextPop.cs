using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FoodTextPop : MonoBehaviour {
    [SerializeField] private Text label;
    [SerializeField] private float upSpeed = 8;
    [SerializeField] private float leftAmpMultiplier = 5;
    [SerializeField] private float leftAmpSpeed = 5;
    [SerializeField] private float duration = 2f;
    float ampTimer;
    float life = 0;
    RectTransform rect;

    public void Set(string t) {
        label.text = t;
    }

    private void Start() {
        rect = (RectTransform)transform;
    }

    void Update() {
        ampTimer = (ampTimer + Time.deltaTime * leftAmpSpeed) % 360;
        float sine = Mathf.Sin(ampTimer * Mathf.Deg2Rad);
        Vector2 newPosition = rect.position;
        newPosition += new Vector2(sine * leftAmpMultiplier, upSpeed) * Time.deltaTime;
        rect.position = newPosition;
        life += Time.deltaTime;
        Color c = label.color;
        c.a = Mathf.InverseLerp(duration, duration*.75f, life);
        label.color = c;
        if (life > duration) Destroy(gameObject);
    }
}
