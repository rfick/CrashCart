using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OculusLevitate : MonoBehaviour {
    float y;
	// Use this for initialization
	void Start () {
        y = transform.localPosition.y;
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = new Vector3(transform.position.x, y, transform.position.z);
	}
}
