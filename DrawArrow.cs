using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawArrow : MonoBehaviour {

    public static DrawArrow instance;
    public bool inQuiver = false;

    void Awake() {
        if (instance == null)
        {
            instance = this;
        }
    }

    void OnDestroy() {
        if (instance == this)
        {
            instance = null;
        }
    }

    private void OnTriggerEnter(Collider col) {
        if (col.gameObject.name == "Quiver")
        {
            inQuiver = true;
        }
    }

    private void OnTriggerExit(Collider col) {
        if (col.gameObject.name == "Quiver")
        {
            inQuiver = false;
        }
    }
}
