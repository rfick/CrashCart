using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialScript : MonoBehaviour
{
    public GameObject display;
    public bool showJoystick, move1, move2, move3;
    GameObject Player;
    Vector3 startPosition;
    GameObject redArrowSpace, yellowArrowSpace, blueArrowSpace, redArrow, yellowArrow, blueArrow;
    Vector3 redPos, yellowPos, bluePos;
    public GameObject collideObject;
    BoxCollider startPoint;
    public float measureTime;
    bool objectGrabbed;
    public bool objectPassed;
    public bool endTutorial;
    bool gameStart;
    bool outOfTutorial;

    public AudioSource audioSource;
    public AudioClip ac1;
    public AudioClip ac2;
    public AudioClip ac3;
    public AudioClip ac4;
    public AudioClip ac5;
    public AudioClip ac6;
    public AudioClip ac7;
    public AudioClip ac8;
    public AudioClip ac9;
    bool playedSoundPassThis;
    bool playSoundStart;
    bool pressMiddleKey;

    // Use this for initialization
    void Start()
    {
        display.GetComponent<TextMesh>().text = "";
        showJoystick = true;
        move1 = move2 = move3 = false;
        Player = GameObject.Find("OVRPlayerController");
        Player.GetComponent<ButtonPressedAnu>().allGrayButton();
        startPosition = transform.position;

        redArrowSpace = GameObject.Find("RedArrowSpace").gameObject;
        redPos = redArrowSpace.transform.position;
        redArrowSpace.transform.position = new Vector3(0, 0, 0);

        blueArrowSpace = GameObject.Find("BlueArrowSpace").gameObject;
        bluePos = blueArrowSpace.transform.position;
        blueArrowSpace.transform.position = new Vector3(0, 0, 0);

        //yellowArrowSpace = GameObject.Find("YellowArrowSpace").gameObject;
        //yellowPos = yellowArrowSpace.transform.position;
        //yellowArrowSpace.transform.position = new Vector3(0, 0, 0);

        redArrow = GameObject.Find("RedArrow");
        blueArrow = GameObject.Find("BlueArrow");
        // yellowArrow = GameObject.Find("YellowArrow");
        GameObject.Find("heart").transform.localScale = new Vector3(0, 0, 0);
        startPoint = GameObject.Find("Cart_Plane").GetComponent<BoxCollider>();
        objectGrabbed = false;
        //endTutorial = false;
        objectPassed = false;
        gameStart = false;
        outOfTutorial = false;
        playedSoundPassThis = true;
        playSoundStart = true;
        pressMiddleKey = true;
        measureTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (!endTutorial)
        {
            if (showJoystick)
            {
                
               // Debug.Log("show joystick true" + Vector3.Distance(transform.position, startPosition));
               
                if (Time.time - measureTime > 3f && playSoundStart)
                {
                    display.GetComponent<TextMesh>().text = "Use the left hand\njoystick to move";
                    playSoundTutorial1();
                    playSoundStart = false;
                    Debug.Log("called playsoundstart is true");
                }
                Player.GetComponent<ButtonPressedAnu>().controlController(true);
                if (Vector3.Distance(transform.position, startPosition) < 3.5f)
                {
                    Debug.Log("thumbs still shwowing?");
                    Player.GetComponent<ButtonPressedAnu>().pressButton(OVRInput.Button.PrimaryThumbstickUp);
                }
                else
                {
                    showJoystick = false;
                    Player.GetComponent<ButtonPressedAnu>().controlController(false);
                    display.GetComponent<TextMesh>().text = "Move to the\n Orange Arrow Space";
                    playSoundTutorial2();
                    redArrowSpace.transform.position = redPos;
                    GameObject.Find("RedArrow").GetComponent<levitate>().currentY = -1f;
                    startPoint = GameObject.Find("RedArrowSpace").GetComponent<BoxCollider>();
                }
            }
            else if (!move1)
            {
                if (checkCollision(redArrowSpace))
                {
                    redArrowSpace.SetActive(false);
                    display.GetComponent<TextMesh>().text = "Move to the\n Yellow Arrow Space";
                    playSoundTutorial3();
                    blueArrowSpace.transform.position = bluePos;
                    GameObject.Find("BlueArrow").GetComponent<levitate>().currentY = -1f;
                    startPoint = GameObject.Find("BlueArrowSpace").GetComponent<BoxCollider>();
                    move1 = true;
                }
                // display.GetComponent<TextMesh>().text = "Press the left hand\njoystick to move";
            }
            else if (!move2)
            {
                if (checkCollision(blueArrowSpace))
                {
                    measureTime = Time.time;
                    blueArrowSpace.SetActive(false);
                    display.GetComponent<TextMesh>().text = "Great job!! Now lets\n learn to grab! ";
                    playSoundTutorial4();
                    move2 = true;

                }
            }
            else if (!objectGrabbed)
            {
                if (Time.time - measureTime > 5)
                {
                    display.GetComponent<TextMesh>().text = "Press the key at middle\nfinger to grab the heart";
                    if (pressMiddleKey)
                    {
                        playSoundTutorial5();
                        pressMiddleKey = false;
                    }
                    GameObject.Find("heart").transform.localScale = new Vector3(0.05f, 0.05f, 0.05f);

                }
                if ((GameObject.Find("LeftHandAnchor").gameObject.GetComponent<OVRGrabber>().grabbedObject != null && GameObject.Find("LeftHandAnchor").gameObject.GetComponent<OVRGrabber>().grabbedObject.name == "heart") || (GameObject.Find("RightHandAnchor").gameObject.GetComponent<OVRGrabber>().grabbedObject != null && GameObject.Find("RightHandAnchor").gameObject.GetComponent<OVRGrabber>().grabbedObject.name == "heart"))
                {
                    objectGrabbed = true;
                    measureTime = Time.time;
                    display.GetComponent<TextMesh>().text = "Awesome!!";
                    playSoundTutorial6();
                    measureTime = Time.time;

                }
                /*if (objectGrabbed)
                {
                    if (Time.time - measureTime > 10f)
                    {
                        if (GameObject.Find("heart") != null)
                            GameObject.Find("heart").transform.localScale = new Vector3(0, 0, 0);
                        display.GetComponent<TextMesh>().text = "move to the green area\nto begin";
                        GameObject.Find("OculusTouchR").transform.localScale = new Vector3(0, 0, 0);
                        GameObject.Find("OculusTouchL").transform.localScale = new Vector3(0, 0, 0);
                        GameObject playerController = GameObject.Find("OVRPlayerController").gameObject;
                        playerController.transform.position = new Vector3(-5, playerController.transform.position.y, -4.81f);
                        endTutorial = true;

                    }
                }*/
            }
            else if (!objectPassed)
            {
                if (Time.time - measureTime > 2)
                {
                    if (playedSoundPassThis)
                    {
                        display.GetComponent<TextMesh>().text = "Now pass this heart\n to the person.";
                        playSoundTutorial7();
                        playedSoundPassThis = false;
                    }
                       
                    measureTime = Time.time;

                }
            }
            else if (!gameStart)
            {
                display.GetComponent<TextMesh>().text = "Great now you are ready\n Starting game..";
                playSoundTutorial8();
                if (GameObject.Find("heart") != null)
                    GameObject.Find("heart").transform.localScale = new Vector3(0, 0, 0);
                if (Time.time - measureTime > 2f)
                {
                    gameStart = true;
                }
            }
            else
            {
                if (Time.time - measureTime > 5f)
                {
                    if (GameObject.Find("heart") != null)
                        GameObject.Find("heart").transform.localScale = new Vector3(0, 0, 0);
                    display.GetComponent<TextMesh>().text = "move to the green area\nto begin";
                    playSoundTutorial9();
                    GameObject.Find("OculusTouchR").transform.localScale = new Vector3(0, 0, 0);
                    GameObject.Find("OculusTouchL").transform.localScale = new Vector3(0, 0, 0);
                    GameObject playerController = GameObject.Find("OVRPlayerController").gameObject;
                    playerController.transform.position = new Vector3(-5, playerController.transform.position.y, -4.81f);
                    endTutorial = true;

                }
            }
        }
        else
        {
            if (!outOfTutorial)
            {
                if (GameObject.Find("heart") != null)
                    GameObject.Find("heart").transform.localScale = new Vector3(0, 0, 0);
                //display.GetComponent<TextMesh>().text = "Get ready!";
                //playSoundTutorial9();
                //GameObject.Find("OculusTouchR").transform.localScale = new Vector3(0, 0, 0);
                //GameObject.Find("OculusTouchL").transform.localScale = new Vector3(0, 0, 0);
                GameObject playerController = GameObject.Find("OVRPlayerController").gameObject;
                playerController.transform.position = new Vector3(-4.296f, playerController.transform.position.y, 2.721f);
                // playerController.transform.rotation.SetEulerAngles(0, -285, 0);
                Vector3 eulerAngle = playerController.transform.eulerAngles;
                eulerAngle.y = -285;
                eulerAngle.x = 0;
                eulerAngle.z = 0;
                playerController.transform.eulerAngles = eulerAngle;
                endTutorial = true;
                outOfTutorial = true;
            }


        }
    }
    void EndTutorial()
    {

    }
    bool checkCollision(GameObject colorSpace)
    {
        //Debug.Log(collideObject.transform.position);
        if ((startPoint.size.x / 2 + startPoint.transform.position.x) > collideObject.transform.position.x && (startPoint.transform.position.x - startPoint.size.x / 2) < collideObject.transform.position.x && (startPoint.size.z / 2 + startPoint.transform.position.z) > collideObject.transform.position.z && (startPoint.transform.position.z - startPoint.size.z / 2) < collideObject.transform.position.z)
        {

            Debug.Log("Questions start!!!!!!!!!!!!!!!!!!");
            //GameObject cartPlane = GameObject.Find("Cart_Plane");
            //GameObject.Find("Timer").GetComponent<StopWatchTimer>().toggleTimerOnOff();
            return true;

        }
        return false;
    }



    // audio for tutorial

    void playSoundTutorial1()
    {
        audioSource.clip = ac1;
       // Debug.Log("playing");
        audioSource.Play();
    }

    void playSoundTutorial2()
    {
        audioSource.clip = ac2;
        //Debug.Log("playing");
        audioSource.Play();
    }
    void playSoundTutorial3()
    {
        audioSource.clip = ac3;
       // Debug.Log("playing");
        audioSource.Play();
    }
    void playSoundTutorial4()
    {
        audioSource.clip = ac4;
        //Debug.Log("playing");
        audioSource.Play();
    }
    void playSoundTutorial5()
    {
        audioSource.clip = ac5;
        Debug.Log("play audio source 5???");
        audioSource.Play();
    }
    void playSoundTutorial6()
    {
        audioSource.clip = ac6;
        //Debug.Log("playing");
        audioSource.Play();
    }

    void playSoundTutorial7()
    {
        audioSource.clip = ac7;
        //Debug.Log("playing");
        audioSource.Play();
    }
    void playSoundTutorial8()
    {
        audioSource.clip = ac8;
      //  Debug.Log("playing");
        audioSource.Play();
    }

    void playSoundTutorial9()
    {
        audioSource.clip = ac9;
       // Debug.Log("playing");
        audioSource.Play();
    }
}
