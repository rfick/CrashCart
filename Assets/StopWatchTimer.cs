using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class StopWatchTimer : MonoBehaviour {
    public bool restartTimer;
    public bool recordTime;
    public float timerValue;
    public List<float> timerStorage = new List<float>();
    public List<GameObject> gameObjectStorage = new List<GameObject>();
    public float finalTimer;
    public float objectStartTime;
    public bool activeSearch;
    public float giveUpTime = 15;
	// Use this for initialization
	void Start () {
        restartTimer = false;
        recordTime = false;
        activeSearch = true;
        timerValue = 0f;
        finalTimer = 0f;
	}
	
	// Update is called once per frame
	void Update () {
        if(recordTime && activeSearch)
        {
            timerValue = timerValue + Time.deltaTime;
            if ((timerValue - objectStartTime) > giveUpTime)
            {
                activeSearch = false;
                // Code to display answer
                Debug.Log("Timeout, displaying answer");
                GameObject answerObject = GameObject.Find("OVRPlayerController").GetComponent<AnuScript>().gameObjectToFind;
                GameObject activatedLight = answerObject.transform.Find("Point Light").gameObject;
                activatedLight.SetActive(true);

                GameObject drawer = answerObject.transform.parent.gameObject;
                Renderer rend = drawer.GetComponent<Renderer>();
                rend.sharedMaterial = (Material)Resources.Load("DrawerMaterialOutlined", typeof(Material));
            }
        }
        else
        {
            timerValue = 0f;
        }
        TextMesh t = GetComponent<TextMesh>();
        if(timerValue!=0f)
            t.text = timerValue.ToString("0.00")+"";


		
	}

    public void searchForObject()
    {
        objectStartTime = timerValue;
        activeSearch = true;
    }

    public void toggleTimerOnOff()
    {
        recordTime = !recordTime;
        
    }
    public void addTimer( GameObject gameObject)
    {
        timerStorage.Add(timerValue);
        finalTimer += timerValue;
        gameObjectStorage.Add(gameObject);
        timerValue = 0;
    }
    
}
