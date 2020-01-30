using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRotate : MonoBehaviour {

    public float dildoSpeed;
    public float rotateSpeed;
    public float increaseFactor;

    public GameObject dildoModel;
    private GameObject flyingDildo;

    private Transform[] spawnsCopy;
    private Transform tempTransform;

	// Update is called once per frame
	void Update () {
        transform.Rotate(Vector3.up * (rotateSpeed * Time.deltaTime));
        rotateSpeed *= 1 + increaseFactor * Time.deltaTime;

        if (rotateSpeed > 2000)
        {
            //destroy it, shoot at player, spawn new enemy
            tempTransform = transform;
            GetComponent<BoxCollider>().isTrigger = true;
            //instantiate missle at same position as enemyTransform
            flyingDildo = Instantiate(dildoModel);
            flyingDildo.transform.position = tempTransform.position;
            Rigidbody rBody = flyingDildo.GetComponent<Rigidbody>();

            flyingDildo.transform.LookAt(ArrowManager.instance.headTransform.position);
            rBody.velocity = dildoSpeed * flyingDildo.transform.forward;
            flyingDildo.transform.Rotate(90f, 0f, 0f);

            //spotsTakenCopy = SpawnEnemies.instance.spotsTaken;
            spawnsCopy = SpawnEnemies.instance.spawns;

            for (int i = 0; i < 19; i++)
            {
                if (tempTransform.position == spawnsCopy[i].position)
                {
                    SpawnEnemies.instance.spotsTaken[i] = false;
                    SpawnEnemies.instance.enemyCount -= 1;
                    break;
                }
            }
            Destroy(gameObject);

        }
    }
}
