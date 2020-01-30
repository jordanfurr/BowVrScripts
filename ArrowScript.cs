
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;


public class ArrowScript : MonoBehaviour {
    
    public SteamVR_ActionSet m_ActionSet;
    public SteamVR_Action_Boolean m_BooleanAction;
    Rigidbody rBody;

    private bool readyToNock;
    private bool isAttached = false;
    private bool released = false;

    private float timePassed;

    private void Awake() {
        m_BooleanAction = SteamVR_Actions._default.GrabPinch;
        readyToNock = false;

        rBody = GetComponent<Rigidbody>();
    }

    private void Start() {
        m_ActionSet.Activate(SteamVR_Input_Sources.Any, 0, true);
        timePassed = 0f;
    }

    private void OnTriggerEnter(Collider col) {
        if (col.gameObject.name == "Golden Bow")
        {
            readyToNock = true;
        }
    }

    private void OnTriggerExit(Collider col) {
        
        if (col.gameObject.name == "Golden Bow")
        {
            readyToNock = false;
        }
    }

    void Update() {

        if (!isAttached && m_BooleanAction.GetState(SteamVR_Input_Sources.RightHand))
        {
            if (readyToNock) {
                ArrowManager.instance.AttachArrowToBow();
                ArrowManager.instance.bow.GetComponent<AudioSource>().Play();
                isAttached = true;

            }
        }else if (isAttached &&! m_BooleanAction.GetState(SteamVR_Input_Sources.RightHand))
        {
            isAttached = false;
            released = true;
            ArrowManager.instance.ReleaseArrow();
            GetComponent<AudioSource>().Play();
            
        }
        
        if (released)
        {
            transform.forward = Vector3.SlerpUnclamped(transform.forward, rBody.velocity.normalized, Time.deltaTime * 4f);
            timePassed += Time.deltaTime;

            if (timePassed >= 7f)
            {
                Destroy(gameObject);
            }
        }
    }
}

