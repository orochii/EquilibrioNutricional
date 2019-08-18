using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingTexture : MonoBehaviour {
    [SerializeField] private Vector3 targetPosition;
    [SerializeField] private float duration;
    private float percent;

    void Update() {
        percent += Time.deltaTime / duration;
        if (percent > 1) percent %= 1.0f;
        transform.position = Vector3.Lerp(Vector3.zero, targetPosition, percent);
    }
}
