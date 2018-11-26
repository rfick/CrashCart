using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class StopWatchTimer : MonoBehaviour {
    public bool restartTimer;
    public bool recordTime;
    public float timerValue;
    public List<float> timerStorage = new List<float>();
    public List<GameObject> gameObjectStorage = new List<GameObject>();
    private float finalTimer;
    public float objectStartTime;
    public bool activeSearch;
    public bool countdown;
    public float giveUpTime = 15;
    private GameObject drawer;
    // Use this for initialization
    void Start () {
        restartTimer = false;
        recordTime = false;
        activeSearch = true;
        countdown = false;
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
                GameObject activatedLight = answerObject.transform.Find("Spot Light").gameObject;
                activatedLight.SetActive(true);

                drawer = answerObject.transform.parent.gameObject;
                drawer.GetComponent<ChangeMaterial>().onTimerEnd();
            }
            TextMesh t = GetComponent<TextMesh>();
            if (timerValue != 0f)
                t.text = timerValue.ToString("0.00") + "";
        }
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

    public string getFinalTimer()
    {
        return finalTimer.ToString();
    }

    public void startCountdown()
    {
        countdown = true;
        timerValue = 5; //5 second countdown
    }

    public void endCountdown()
    {
        countdown = false;
        timerValue = 0f;
    }

    public void addTimer( GameObject gameObject)
    {
        timerStorage.Add(timerValue);
        finalTimer += timerValue;
        gameObjectStorage.Add(gameObject);
        timerValue = 0;
        if (drawer)
        {
            drawer.GetComponent<ChangeMaterial>().onTimerReset();
        }
    }
    
}
