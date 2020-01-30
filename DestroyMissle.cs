using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyMissle : MonoBehaviour {

    private void OnCollisionEnter(Collision other) {
        if (other.transform.tag == "Player")
        {
            ArrowManager.instance.DecPlayerHP();
        }

        Destroy(gameObject);
    }
}
