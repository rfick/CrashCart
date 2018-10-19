using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialObjectPass : MonoBehaviour {
    Vector3 startPosition;
    GameObject grabbedObjectLast;
    public GameObject handOver;
    // Use this for initialization
    bool isGrabbed;
    bool canHandOverObject;
    Transform parentObject;
    Material materialOfObject;
    Color originalColor;
    GameObject toPass;
    void Start()
    {
        
        isGrabbed = false;
        grabbedObjectLast = this.gameObject;
        // parentObject = this.parentObject;
        parentObject = transform.parent;
        canHandOverObject = false;
        materialOfObject = this.GetComponent<Renderer>().material;
        originalColor = materialOfObject.color;
        handOver = GameObject.Find("objectGrabberPerson1");
        toPass = GameObject.Find("OVRPlayerController");

    }

    // Update is called once per frame
    void Update()
    {

        if ((GameObject.Find("LeftHandAnchor").gameObject.GetComponent<OVRGrabber>().grabbedObject != null && GameObject.Find("LeftHandAnchor").gameObject.GetComponent<OVRGrabber>().grabbedObject.name == this.gameObject.name) || (GameObject.Find("RightHandAnchor").gameObject.GetComponent<OVRGrabber>().grabbedObject != null && GameObject.Find("RightHandAnchor").gameObject.GetComponent<OVRGrabber>().grabbedObject.name == this.gameObject.name))
        {
            // we store grabbedObjectLast as the object changes when we grab it so it is not the same object as the one that was grabbed.
            isGrabbed = true;
            //Debug.Log("is grabbed reached!!!");
           
            
            return;
        }
        else if (isGrabbed)// called when object is dropped
        {
            //Debug.Log("is grabbed true!!!");

            if (canHandOverObject)
            {
                //game start!!!
                toPass.GetComponent<TutorialScript>().objectPassed = true;
                toPass.GetComponent<TutorialScript>().measureTime = Time.time;
            }
           
            //this.transform.localPosition = startPosition;

            isGrabbed = false;

        }
       

    }
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("is this even working?");
        if (collision.gameObject == handOver.gameObject)
        {
            canHandOverObject = true;
            materialOfObject.color = Color.gray;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject == handOver.gameObject)
        {
            canHandOverObject = false;
            materialOfObject.color = originalColor;
        }
    }
}
