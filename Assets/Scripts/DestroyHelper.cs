using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyHelper : MonoBehaviour {
    public void CallDestroySelf() {
        Destroy(gameObject);
    }
}
