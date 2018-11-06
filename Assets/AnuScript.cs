using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnuScript : MonoBehaviour {
    float staticY;
    int counter = 1000;
    public bool startQuestionnaireSession;
    BoxCollider startPoint;
    public int numberOfQuestion;
    // Use this for initialization
    public List<GameObject> itemsInCart=new List<GameObject>();
    public List<GameObject> drawers=new List<GameObject>();
    public GameObject gameObjectToFind;
    public GameObject collideObject;
    public string name;
    public bool countdown;
    public List<string> playerRecords;
    bool savePlayerPref;
    public AudioSource audioSource;
    float totalTime;
	void Start () {
        savePlayerPref = true;
        countdown = false;
        totalTime = 0f;
       foreach(GameObject drawer in drawers )// find the objects inside the drawers!
        {
            //Transform[] allChildren = drawer.GetComponentsInChildren<Transform>();// the objects are children of the drawer
            List<Transform> allChildren = new List<Transform>();
            foreach (Transform trans in drawer.transform)
            {
                allChildren.Add(trans);
            }
            Transform[] allChildrenArray = allChildren.ToArray();
            foreach (Transform child in allChildrenArray)
            {
                if (child.name.IndexOf('#') < 0 && child.gameObject.activeSelf)
                {
                    itemsInCart.Add(child.gameObject);
                }
               
            }

        }
       foreach(GameObject drawer in drawers)// removing the drawers from the objects list!
        {
           
            if(itemsInCart.Contains(drawer))
            {
                itemsInCart.Remove(drawer);
            }
            
        }
        startQuestionnaireSession = false;
        startPoint = GameObject.Find("Cart_Plane").GetComponent<BoxCollider>();// that green arrow from where game starts!!
        gameObjectToFind = null;// the questionned object
        staticY = transform.position.y;
        if (name == null)// for storing the name of the person
        {
            name = "Default";
        }
        string savedList = PlayerPrefs.GetString("SavedList");// place where data is stored regarding name and objects picked up and time taken
        string[] savedListArray = savedList.Split(' ');
        //PlayerPrefs.DeleteAll();
        for(int i=0;i<savedListArray.Length;i++)
        {
            playerRecords.Add(savedListArray[i]+" and values are "+PlayerPrefs.GetString(savedListArray[i]));
        }
        
    }
	
	// Update is called once per frame
	void Update () {
        if(!startQuestionnaireSession && !countdown)// did the person reach the green arrow?
        {
            checkStartQuestionnaire();
        }
        else if(!countdown)
        {
           // Debug.Log("1");
            if(gameObjectToFind==null)
            {
                Random random = new Random();
               // Debug.Log("2");
                int itemsRemainingCount=itemsInCart.Count;
                if(itemsRemainingCount>0&& numberOfQuestion > 0)
                {
                    //Debug.Log("3");
                    numberOfQuestion--;
                    gameObjectToFind = itemsInCart[Random.Range(0,itemsRemainingCount)];
                    itemsInCart.Remove(gameObjectToFind);
                    GameObject questionText = GameObject.Find("QuestionText");
                    TextMesh questionTextMesh = questionText.GetComponent<TextMesh>();
                    questionTextMesh.text = "Q"+(10-numberOfQuestion)+". Pass me \n" + gameObjectToFind.name;
                    
                    if (gameObjectToFind.GetComponent<ObjectLeaveDirectStartLocation>().audioQuestion != null)
                    {
                        Debug.Log("found audio question");
                    }
                    audioSource.clip = gameObjectToFind.GetComponent<ObjectLeaveDirectStartLocation>().audioQuestion;
                    audioSource.Play();
                    GameObject.Find("Timer").GetComponent<StopWatchTimer>().searchForObject();


                }
                else
                {
                   // Debug.Log("4");
                    GameObject questionText = GameObject.Find("QuestionText");
                    TextMesh questionTextMesh = questionText.GetComponent<TextMesh>();
                    questionTextMesh.text = "Congratulations";
                    GameObject.Find("Timer").GetComponent<StopWatchTimer>().recordTime = false;
                    GameObject.Find("Timer").GetComponent<TextMesh>().text = "Total Time \n"+GameObject.Find("Timer").GetComponent<StopWatchTimer>().getFinalTimer();
                   // GameObject.Find("Timer").GetComponent<StopWatchTimer>().timerValue = 0f;
                    if (savePlayerPref)
                    {
                        onSave();
                        savePlayerPref = false;
                    }
                }
            }
        }
       
		if(counter>0)
        {
            counter--;
            if(counter==0)
                staticY = transform.position.y;
            return;
        }
        else
        {
            if(Mathf.Abs(staticY-transform.position.y)>0.1f)
            {
                Debug.Log("you have changed ur height!!!!");
                transform.position= transform.position + this.transform.forward * -1f * Time.deltaTime;
            }
        }
       


	}
    void onSave()
    {

        Debug.Log("reached onSave1");
        string savedList = "";
        if(PlayerPrefs.GetString("SavedList")==null)
        {
            PlayerPrefs.SetString("SavedList", "");
        }
        else
        {
            savedList = PlayerPrefs.GetString("SavedList");
        }
        Debug.Log("reached onSave2");
        int counterName = 0;
        while(PlayerPrefs.HasKey(name+counterName))
        {
            counterName++;
        }
        Debug.Log("reached onSave3");
        string valueToStore = "";
        List<float> timerStorage = GameObject.Find("Timer").GetComponent<StopWatchTimer>().timerStorage;
        List<GameObject> goStorage = GameObject.Find("Timer").GetComponent<StopWatchTimer>().gameObjectStorage;
        Debug.Log("reached onSave3.1 " + timerStorage.Count);
        for (int i= 0; i<timerStorage.Count; i++)
        {
            Debug.Log(i);
            valueToStore = valueToStore + "#";
            valueToStore = timerStorage[i] + " " + goStorage[i].name;
        }
        Debug.Log("reached onSave4");
        PlayerPrefs.SetString(name + counterName, valueToStore);
        Debug.Log("reached onSave5");
        PlayerPrefs.SetString("SavedList", savedList + " " + (name + counterName));
        Debug.Log("reached onSave6");
        Debug.Log(PlayerPrefs.GetString("SavedList")+"!!!!!!!!!!!!!!!!! is the savedList");
        
    }
    void checkStartQuestionnaire()// check for collision between player and green arrow region
    {
        //Debug.Log(collideObject.transform.position);
        if((startPoint.size.x/2+startPoint.transform.position.x)> collideObject.transform.position.x&& (startPoint.transform.position.x-startPoint.size.x / 2 ) < collideObject.transform.position.x&& (startPoint.size.z / 2 + startPoint.transform.position.z) > collideObject.transform.position.z && (startPoint.transform.position.z-startPoint.size.z / 2 ) < collideObject.transform.position.z)
        {
            //startQuestionnaireSession = true;
            Debug.Log("Questions start!!!!!!!!!!!!!!!!!!");
            GameObject cartPlane = GameObject.Find("Cart_Plane");
            GameObject.Find("Timer").GetComponent<StopWatchTimer>().toggleTimerOnOff();
            GameObject.Find("Timer").GetComponent<StopWatchTimer>().startCountdown();
            countdown = true;
            if (cartPlane!=null)
            {
                cartPlane.SetActive(false);
               
            }
            
        }
    }
}
