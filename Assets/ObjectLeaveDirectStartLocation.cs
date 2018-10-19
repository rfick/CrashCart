using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectLeaveDirectStartLocation : MonoBehaviour {
    Vector3 startPosition;
    GameObject grabbedObjectLast ;
    public GameObject handOver;
    // Use this for initialization
    bool isGrabbed;
    bool canHandOverObject;
    Transform parentObject;
    Material materialOfObject;
    Color originalColor;
    public AudioClip audioQuestion;
    
	void Start () {
        startPosition = this.transform.localPosition;
        isGrabbed = false;
        grabbedObjectLast = this.gameObject;
        // parentObject = this.parentObject;
        parentObject = transform.parent;
        canHandOverObject = false;
        if (this.GetComponent<Renderer>() != null)
        {
            materialOfObject = this.GetComponent<Renderer>().material;
            originalColor = materialOfObject.color;
        }
        
       
        handOver = GameObject.Find("objectGrabberPerson");
       
    }
	
	// Update is called once per frame
	void Update () {
        
        if ((GameObject.Find("LeftHandAnchor").gameObject.GetComponent<OVRGrabber>().grabbedObject!=null && GameObject.Find("LeftHandAnchor").gameObject.GetComponent<OVRGrabber>().grabbedObject.name==this.gameObject.name)|| (GameObject.Find("RightHandAnchor").gameObject.GetComponent<OVRGrabber>().grabbedObject != null && GameObject.Find("RightHandAnchor").gameObject.GetComponent<OVRGrabber>().grabbedObject.name == this.gameObject.name))
        {
            // we store grabbedObjectLast as the object changes when we grab it so it is not the same object as the one that was grabbed.
            isGrabbed = true;
            //Debug.Log("is grabbed reached!!!");
            if((GameObject.Find("LeftHandAnchor").gameObject.GetComponent<OVRGrabber>().grabbedObject != null && GameObject.Find("LeftHandAnchor").gameObject.GetComponent<OVRGrabber>().grabbedObject.name == this.gameObject.name))
            {
                grabbedObjectLast = GameObject.Find("LeftHandAnchor").gameObject.GetComponent<OVRGrabber>().grabbedObject != null ? GameObject.Find("LeftHandAnchor").gameObject.GetComponent<OVRGrabber>().grabbedObject.gameObject : GameObject.Find("RightHandAnchor").gameObject.GetComponent<OVRGrabber>().grabbedObject.gameObject;
            }
            else
            {
                grabbedObjectLast = GameObject.Find("RightHandAnchor").gameObject.GetComponent<OVRGrabber>().grabbedObject != null ? GameObject.Find("RightHandAnchor").gameObject.GetComponent<OVRGrabber>().grabbedObject.gameObject : GameObject.Find("RightHandAnchor").gameObject.GetComponent<OVRGrabber>().grabbedObject.gameObject;
            }
            
            //startPosition = this.transform.localPosition;
            transform.parent=null;
            return;
        }
        else if(isGrabbed)// called when object is dropped
        {
            //Debug.Log("is grabbed true!!!");
            transform.parent = parentObject;
            if (!canHandOverObject)
                grabbedObjectLast.transform.localPosition = startPosition;
            else
            {
                // this.gameObject.SetActive(false);
                if (this.gameObject == GameObject.Find("OVRPlayerController").GetComponent<AnuScript>().gameObjectToFind)
                {
                    GameObject.Find("OVRPlayerController").GetComponent<AnuScript>().gameObjectToFind = null;//this triggers find the next object!
                  
                    GameObject.Find("Timer").GetComponent<StopWatchTimer>().addTimer(grabbedObjectLast);
                    grabbedObjectLast.SetActive(false);
                }
                else
                {
                    grabbedObjectLast.transform.localPosition = startPosition;
                }
                
            }
            //this.transform.localPosition = startPosition;
            
           isGrabbed = false;

        }
        else if(!isGrabbed)
        {
            //Debug.Log("is grabbed false!!!");
            // this.transform.localPosition = startPosition;
           
                startPosition = this.transform.localPosition;
              
        }
        
	}
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("is this even working?");
        if(collision.gameObject==handOver.gameObject)
        {
            canHandOverObject = true;
            if(this.GetComponent<Renderer>()!=null)
                materialOfObject.color = Color.gray;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject == handOver.gameObject)
        {
            canHandOverObject = false;
            if (this.GetComponent<Renderer>() != null)
                materialOfObject.color = originalColor;
        }
    }

}
