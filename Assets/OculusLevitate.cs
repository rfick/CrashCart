using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OculusLevitate : MonoBehaviour {
    Quaternion initialRotOffset;

	// Use this for initialization
	void Start () {
        initialRotOffset = this.transform.rotation;
	}
	
	// Update is called once per frame
	void Update () {

        GameObject hand = transform.parent.gameObject;
        Vector3 handPosition = hand.transform.position;
        Quaternion handRotation = hand.transform.rotation * initialRotOffset;
        //handRotation.x = (-1) * handRotation.x;

        this.transform.position = handPosition;
        this.transform.rotation = handRotation.normalized;
    }
}
