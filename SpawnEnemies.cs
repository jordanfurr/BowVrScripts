using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemies : MonoBehaviour {

    public Transform[] spawns;
    public bool[] spotsTaken;
    public GameObject enemyPrefab;
    private GameObject newEnemy;
    private int randInt;
    private float timePassed;
    private float timeNeededToSpawn;
    public int crazyEasy, superEasy, kindaEasy, easy, lightNormal, normal, hard, kindaHard, veryHard, superHard, insane;
    public int enemyCount;
    private int scoreCopy;
    public bool gameStarted;
    public int numberOfSpawns;

    public static SpawnEnemies instance;

    void Awake() {
        if (instance == null)
        {
            instance = this;
        }
        gameStarted = false;
    }

    void OnDestroy() {
        if (instance == this)
        {
            instance = null;
        }
    }

    // Use this for initialization
    void Start () {
        spotsTaken = new bool[numberOfSpawns];
        for (int i=0; i< numberOfSpawns; i++)
        {
            spotsTaken[i] = false;
        }
        spawns = GetComponentsInChildren<Transform>();
        timeNeededToSpawn = 4f;
        enemyCount = 0;
    }
	
	// Update is called once per frame
	void Update () {
        scoreCopy = ArrowManager.instance.score;
        if (gameStarted)
        {
            timePassed += Time.deltaTime;
            SpawnEnemy();
        }
        else
        {
            timePassed = 0f;
        }
	}

    public void DespawnAllEnemies() {
        for (int i = 0; i < numberOfSpawns; i++)
        {
            spotsTaken[i] = false;
        }
        GameObject[] allEnemies;
        allEnemies = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject livingEnemy in allEnemies)
        {
            Destroy(livingEnemy);
        }
    }

    void ActuallySpawnEnemy() {
        if (enemyCount < 9)
        {
            timePassed = 0f;
            randInt = Random.Range(0, 18);
            while (spotsTaken[randInt])
            {
                randInt = Random.Range(0, 18);
            }

            newEnemy = Instantiate(enemyPrefab);
            newEnemy.transform.position = spawns[randInt].position;

            spotsTaken[randInt] = true;
            enemyCount += 1;
        }
        /*
        Debug.Log("########### NEW ###########");
        foreach (bool spot in spotsTaken)
        {
            Debug.Log(spot);
        }
        */
    }

    void SpawnEnemy() {
        if (scoreCopy < crazyEasy)
        {
            timeNeededToSpawn = 3.7f;
        }
        else if (scoreCopy < superEasy)
        {
            timeNeededToSpawn = 3.4f;
        }
        else if (scoreCopy < kindaEasy)
        {
            timeNeededToSpawn = 3.1f;
        }
        else if (scoreCopy < easy)
        {
            timeNeededToSpawn = 2.8f;
        }
        else if (scoreCopy < lightNormal)
        {
            timeNeededToSpawn = 2.5f;
        }
        else if (scoreCopy < normal)
        {
            timeNeededToSpawn = 2.2f;
        }
        else if (scoreCopy < hard)
        {
            timeNeededToSpawn = 1.9f;
        }
        else if (scoreCopy < kindaHard)
        {
            timeNeededToSpawn = 1.6f;
        }
        else if (scoreCopy < veryHard)
        {
            timeNeededToSpawn = 1.3f;
        }
        else if (scoreCopy < superHard)
        {
            timeNeededToSpawn = 1f;
        }
        else if (scoreCopy < insane)
        {
            timeNeededToSpawn = 0.7f;
        }
        else
        {
            timeNeededToSpawn = 0.5f;
        }

        if (timePassed > timeNeededToSpawn)
        {
            ActuallySpawnEnemy();
        }

    }
}
