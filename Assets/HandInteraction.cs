using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class HandInteraction : MonoBehaviour
{
    public GameObject objectGrabbed = null;
    public GameObject hand;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // check if the trigger is pressed
        if (SteamVR_Input.GetStateDown("GrabPinch", SteamVR_Input_Sources.Any))
        {
            objectGrabbed.transform.parent = hand.transform;
            Rigidbody rb = objectGrabbed.GetComponent<Rigidbody>();
            rb.isKinematic = true;
        }

        if (SteamVR_Input.GetStateUp("GrabPinch", SteamVR_Input_Sources.Any))
        {
            objectGrabbed.transform.parent = null;
            Rigidbody rb = objectGrabbed.GetComponent<Rigidbody>();
            rb.isKinematic = false;
            

            Hand h = GetComponent<Hand>();
            Rigidbody toThrow = objectGrabbed.GetComponent<Rigidbody>();
            toThrow.velocity = h.trackedObject.GetVelocity();
            Debug.Log(toThrow.velocity.ToString());
            toThrow.angularVelocity = h.trackedObject.GetAngularVelocity();
            Debug.Log(toThrow.angularVelocity.ToString());
            objectGrabbed = null;
        }
    }
    void OnTriggerEnter(Collider other) { objectGrabbed = other.gameObject; }
}
