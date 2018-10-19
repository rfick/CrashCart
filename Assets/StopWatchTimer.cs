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
	// Use this for initialization
	void Start () {
        restartTimer = false;
        recordTime = false;
        timerValue = 0f;
        finalTimer = 0f;
	}
	
	// Update is called once per frame
	void Update () {
        if(recordTime)
        {
            timerValue = timerValue + Time.deltaTime;
        }
        else
        {
            timerValue = 0f;
        }
        TextMesh t = GetComponent<TextMesh>();
        if(timerValue!=0f)
            t.text = timerValue.ToString("0.00")+"";


		
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
