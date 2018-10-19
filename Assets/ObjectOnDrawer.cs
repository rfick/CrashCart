using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectOnDrawer : MonoBehaviour {
    GameObject parentDrawer;
    //bool velocityGravity;
    Vector3 startPosition;
	// Use this for initialization
	void Start () {
        parentDrawer = transform.parent.gameObject;
        //velocityGravity = 0;
        startPosition = this.transform.localPosition;
	}
	
	// Update is called once per frame
	void Update () {
        BoxCollider boxColliderParent=parentDrawer.GetComponent<BoxCollider>();
        if(isObjectAboveCollider(this.gameObject,boxColliderParent))
        {
            this.GetComponent<Rigidbody>().useGravity = true;
            Debug.Log("hey gravity is being applied");
        }
        else { this.GetComponent<Rigidbody>().useGravity = false;
            startPosition = this.transform.localPosition;
        }
        
        if(this.transform.position.y<0)
        {
            this.transform.localPosition = startPosition;
        }

	}
    bool isObjectAboveCollider(GameObject go, BoxCollider boxCollider)
    {
        if(go.transform.position.y>boxCollider.transform.position.y)
        {
            if((go.transform.position.x>(boxCollider.transform.position.x-(boxCollider.size.x/2))&& go.transform.position.x > (boxCollider.transform.position.x + (boxCollider.size.x/2)))&& (go.transform.position.y > (boxCollider.transform.position.y - (boxCollider.size.y/2)) && go.transform.position.y > (boxCollider.transform.position.y + (boxCollider.size.y/2))))
            {
                return true;
            }
        }
        return false;
    }
    
   
}
