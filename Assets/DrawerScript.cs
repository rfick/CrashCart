using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawerScript : MonoBehaviour {
    Vector3 startPosition;
    Quaternion startRotation;
    Vector3 drawerLength;
    public List<GameObject> drawerObjects;
	// Use this for initialization
	void Start () {
        startRotation = transform.localRotation;
        startPosition = transform.localPosition;
        drawerLength = GetComponent<BoxCollider>().size;
        //Debug.Log("Limit is " + startPosition.x + " is the min and max is   " + (startPosition.x + drawerLength.x)+ "    "+drawerLength+" is the collider size the object size is "+GetComponent<Renderer>().bounds);
	}
	
	// Update is called once per frame
	void Update () {
        transform.localRotation = startRotation;
        float _localX = transform.localPosition.x;
        transform.localPosition = new Vector3(Mathf.Clamp(_localX,startPosition.x,startPosition.x+drawerLength.x), startPosition.y, startPosition.z);
        GameObject player = GameObject.Find("OVRPlayerController");
       
//Debug.Log(transform.localPosition+"  "+drawerLength.x);

    }
}
