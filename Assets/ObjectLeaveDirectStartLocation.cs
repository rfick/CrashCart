using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectLeaveDirectStartLocation : MonoBehaviour {
    Vector3 startPosition;
    Quaternion startRotation;
    Vector3 startScale;
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
        startRotation = this.transform.localRotation;
        startScale = this.transform.localScale;
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
        double eps = 0.000001;
        if ((GameObject.Find("LeftHandAnchor").gameObject.GetComponent<OVRGrabber>().grabbedObject!=null && Vector3.Distance(GameObject.Find("LeftHandAnchor").gameObject.GetComponent<OVRGrabber>().grabbedObject.transform.position, this.gameObject.transform.position) < eps || (GameObject.Find("RightHandAnchor").gameObject.GetComponent<OVRGrabber>().grabbedObject != null && Vector3.Distance(GameObject.Find("RightHandAnchor").gameObject.GetComponent<OVRGrabber>().grabbedObject.transform.position, this.gameObject.transform.position) < eps)))
        {
            // we store grabbedObjectLast as the object changes when we grab it so it is not the same object as the one that was grabbed.
            isGrabbed = true;
            //Debug.Log("is grabbed reached!!!");
            if((GameObject.Find("LeftHandAnchor").gameObject.GetComponent<OVRGrabber>().grabbedObject != null && Vector3.Distance(GameObject.Find("LeftHandAnchor").gameObject.GetComponent<OVRGrabber>().grabbedObject.transform.position, this.gameObject.transform.position) < eps))
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
            {
                grabbedObjectLast.transform.localPosition = startPosition;
                grabbedObjectLast.transform.localRotation = startRotation;
                grabbedObjectLast.transform.localScale = startScale;
            }
            else
            {
                if (string.Equals(this.gameObject.name,GameObject.Find("OVRPlayerController").GetComponent<AnuScript>().gameObjectToFind.name))
                {
                    GameObject.Find("OVRPlayerController").GetComponent<AnuScript>().gameObjectToFind = null;//this triggers find the next object!

                    GameObject.Find("Timer").GetComponent<StopWatchTimer>().addTimer(grabbedObjectLast);
                    grabbedObjectLast.SetActive(false);
                }
                else
                {
                    grabbedObjectLast.transform.localPosition = startPosition;
                    grabbedObjectLast.transform.localRotation = startRotation;
                    grabbedObjectLast.transform.localScale = startScale;
                }

            }
            //this.transform.localPosition = startPosition;
            
           isGrabbed = false;

        }
        else if(!isGrabbed)
        {
            //Debug.Log("is grabbed false!!!");
            // this.transform.localPosition = startPosition;
           
                //startPosition = this.transform.localPosition;
              
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
