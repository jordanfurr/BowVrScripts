using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class ArrowManager : MonoBehaviour {

    public static ArrowManager instance;

    private int playerHP;

    private GameObject currentArrow;
    public GameObject bow;
    public GameObject arrowModel;
    public GameObject bowAttachPoint;
    public GameObject stringStartPoint;
    public GameObject stringAttachPoint;
    public GameObject rightHandModel;
    public SteamVR_Behaviour_Pose rightController;
    public SteamVR_Behaviour_Pose leftController;

    public GameObject headset;
    public Transform headTransform;
    private AudioSource audio;

    private bool isAttached = false;
    public float speed;

    public int score;

    // Awake is called before Start
    void Awake() {
        if (instance == null)
        {
            instance = this;
        }
        audio = GetComponent<AudioSource>();
        rightHandModel.SetActive(true);
        score = 0;
        playerHP = 3;
    }

    void OnDestroy() {
        if (instance == this)
        {
            instance = null;
        }
    }

    public void DecPlayerHP() {
        playerHP -= 1;
        audio.Play();
    }

    private void PullString() {

        float distance = 11f * (stringStartPoint.transform.position - rightController.transform.position).magnitude;
        if (distance > 5.4f)
        {
            distance = 5.4f;
        }
        stringAttachPoint.transform.localPosition = stringStartPoint.transform.localPosition + new Vector3(distance, 0f, 0f);
    }

    public void ReleaseArrow() {
        float distance = 11f * (stringStartPoint.transform.position - rightController.transform.position).magnitude;
        if (distance > 5.4f)
        {
            distance = 5.4f;
        }
        if (currentArrow != null)
        {
            Rigidbody rBody = currentArrow.GetComponent<Rigidbody>();
            currentArrow.transform.parent = null;
            rBody.velocity = speed * distance * currentArrow.transform.forward;
            rBody.useGravity = true;

            isAttached = false;
            currentArrow = null;
            rightHandModel.SetActive(true);

            stringAttachPoint.transform.position = stringStartPoint.transform.position;
        }
    }
	
	// Update is called once per frame
	void Update () {
        AttachArrowToHand();
        if (isAttached)
        {
            PullString();
            bow.transform.rotation = Quaternion.LookRotation(leftController.transform.position - rightController.transform.position, -leftController.transform.forward);
        }

        headTransform = headset.transform;

        if (playerHP <= 0)
        {
            playerHP = 3;
            ResetGame();
        }
	}

    private void ResetGame() {
        int scoreCopy = score;
        score = 0;
        SpawnEnemies.instance.gameStarted = false;

        SpawnEnemies.instance.DespawnAllEnemies();

        GameObject startCube = GameObject.FindGameObjectWithTag("StartCube");
        startCube.gameObject.GetComponent<MeshRenderer>().enabled = true;
        startCube.gameObject.GetComponent<BoxCollider>().enabled = true;

        GameObject.FindGameObjectWithTag("StartText").GetComponent<MeshRenderer>().enabled = true;
        GameObject scoreText = GameObject.FindGameObjectWithTag("ScoreText");
        scoreText.GetComponent<TextMesh>().text = "Previous score: " + scoreCopy;
        scoreText.GetComponent<MeshRenderer>().enabled = true;

    }

    private void AttachArrowToHand() {
        if (currentArrow == null) {
            if (DrawArrow.instance.inQuiver) {
                currentArrow = Instantiate(arrowModel);
                currentArrow.transform.parent = rightController.transform;
                currentArrow.transform.localPosition = new Vector3(0f, -0.01f, 0.26f);
                currentArrow.transform.localRotation = Quaternion.identity;
                rightHandModel.SetActive(false);
            }
        }
    }

    public void AttachArrowToBow() {
        currentArrow.transform.parent = stringAttachPoint.transform;
        currentArrow.transform.localPosition = bowAttachPoint.transform.localPosition;
        currentArrow.transform.rotation = bowAttachPoint.transform.rotation;

        isAttached = true;
    }

    public void IncScore() {
        score += 1;
    }
}
