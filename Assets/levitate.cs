using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class levitate : MonoBehaviour {
    public float magnitude;
    public float speed;
    float angle;
    public float currentY;
	// Use this for initialization
	void Start () {
        angle = 0f;
        currentY=transform.position.y;
	}
	
	// Update is called once per frame
	void Update () {
        angle = angle + speed*Time.deltaTime;
        if (angle > 180)
            angle = 0f;
        transform.position = new Vector3(transform.position.x,currentY+ Mathf.Sin(angle)*magnitude, transform.position.z);

	}
}
