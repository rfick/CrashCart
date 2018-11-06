using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeMaterial : MonoBehaviour {
    public Material[] material;
    Renderer rend;


	// Use this for initialization
	void Start () {
        rend = GetComponent<Renderer>();
        rend.sharedMaterial = material[0];
	}
	
    public void onTimerEnd()
    {
        Debug.Log("Changing material");
        rend.sharedMaterial = material[1];
    }

    public void onTimerReset()
    {
        Debug.Log("Resetting material");
        rend.sharedMaterial = material[0];
    }
}
