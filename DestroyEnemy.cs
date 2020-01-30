using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class DestroyEnemy : MonoBehaviour {

    public float dildoSpeed;
    public GameObject dildoModel;
    private GameObject flyingDildo;
    private GameObject arrow;
    private Transform enemyTransform;
    //private bool[] spotsTakenCopy;
    private Transform[] spawnsCopy;
    
    private void OnTriggerEnter(Collider enemy) {
        if (enemy.name != "Golden Bow") {
            if (enemy.gameObject.tag == "Enemy") {

                ArrowManager.instance.IncScore();
                //get enemyTransform
                enemyTransform = enemy.transform;
                Destroy(enemy.gameObject);
                //instantiate missle at same position as enemyTransform
                flyingDildo = Instantiate(dildoModel);
                flyingDildo.transform.position = enemyTransform.position;
                Rigidbody rBody = flyingDildo.GetComponent<Rigidbody>();

                flyingDildo.transform.LookAt(ArrowManager.instance.headTransform.position);
                rBody.velocity = dildoSpeed * flyingDildo.transform.forward;
                flyingDildo.transform.Rotate(90f, 0f, 0f);

                spawnsCopy = SpawnEnemies.instance.spawns;
                
                for (int i=0; i<19; i++)
                {
                    if (enemyTransform.position == spawnsCopy[i].position)
                    {
                        SpawnEnemies.instance.spotsTaken[i] = false;
                        SpawnEnemies.instance.enemyCount -= 1;
                        break;
                    }
                }
            }
            else if (enemy.name == "StartCube")
            {
                SpawnEnemies.instance.gameStarted = true;
                enemy.gameObject.GetComponent<MeshRenderer>().enabled = false;
                enemy.gameObject.GetComponent<BoxCollider>().enabled = false;
                GameObject.FindGameObjectWithTag("StartText").GetComponent<MeshRenderer>().enabled = false;
                GameObject.FindGameObjectWithTag("ScoreText").GetComponent<MeshRenderer>().enabled = false;
            }
            else if (enemy.name != "Head" && enemy.name != "Quiver")
            {
                arrow = transform.parent.gameObject;
                Rigidbody rBodyArrow = arrow.GetComponent<Rigidbody>();
                rBodyArrow.freezeRotation = true;
                rBodyArrow.useGravity = false;
                rBodyArrow.velocity = new Vector3(0f, 0f, 0f);
                GetComponent<AudioSource>().Play();
            }
        }
    }
}
