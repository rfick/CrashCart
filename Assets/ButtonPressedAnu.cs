using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonPressedAnu : MonoBehaviour {
   // String[] ControllerButtonList = { "mesh_button_b", "mesh_button_a", "mesh_stickR", "mesh_trigger_fingerR", "mesh_trigger_palmR", "mesh_button_oR", "mesh_touchR",
    //"mesh_button_x", "mesh_button_y", "mesh_stickL", "mesh_trigger_fingerL", "mesh_trigger_palmL", "mesh_button_oL", "mesh_touchL",};
    public GameObject[] GOControllerButton;
    Vector3 lstickRotation;
    Vector3 rstickRotation;
    public bool controllingController=false;
	// Use this for initialization
	void Start () {
        lstickRotation = GameObject.Find("mesh_stickL").transform.localEulerAngles;
        rstickRotation = GameObject.Find("mesh_stickR").transform.localEulerAngles;
	}
	public void controlController(bool control)
    {
        controllingController = control;
        allGrayButton();
    }
	// Update is called once per frame
	void Update () {
        if (!controllingController)
        {
            allGrayButton();
            foreach (int value in Enum.GetValues(typeof(OVRInput.Button)))
            {
                Console.WriteLine(((OVRInput.Button)value).ToString());
                if (OVRInput.Get((OVRInput.Button)value))
                {
                   // Debug.Log((OVRInput.Button)value);

                    switch ((OVRInput.Button)value)
                    {

                        case OVRInput.Button.One:
                            GameObject mbx = GameObject.Find("mesh_button_a");
                            mbx.GetComponent<Renderer>().material.color = Color.green;
                            break;
                        case OVRInput.Button.Two:
                            mbx = GameObject.Find("mesh_button_b");
                            mbx.GetComponent<Renderer>().material.color = Color.green;
                            break;
                        case OVRInput.Button.PrimaryIndexTrigger:
                            mbx = GameObject.Find("mesh_trigger_fingerL");
                            mbx.GetComponent<Renderer>().material.color = Color.green;
                            break;
                        case OVRInput.Button.PrimaryHandTrigger:
                            mbx = GameObject.Find("mesh_trigger_palmL");
                            mbx.GetComponent<Renderer>().material.color = Color.green;
                            break;
                        case OVRInput.Button.Start:
                            mbx = GameObject.Find("mesh_button_oL");
                            mbx.GetComponent<Renderer>().material.color = Color.green;
                            break;
                        case OVRInput.Button.PrimaryThumbstick:
                            mbx = GameObject.Find("mesh_stickL");
                            mbx.GetComponent<Renderer>().material.color = Color.green;
                            break;
                        case OVRInput.Button.PrimaryThumbstickDown:
                            mbx = GameObject.Find("mesh_stickL");
                            mbx.GetComponent<Renderer>().material.color = Color.green;
                            mbx.transform.eulerAngles = new Vector3(140, mbx.transform.rotation.y, mbx.transform.rotation.z);// 270-  required 40 
                            break;
                        case OVRInput.Button.PrimaryThumbstickUp:
                            mbx = GameObject.Find("mesh_stickL");
                            mbx.GetComponent<Renderer>().material.color = Color.green;
                            mbx.transform.eulerAngles = new Vector3(220, mbx.transform.rotation.y, mbx.transform.rotation.z);// 270+  required 40 
                            break;
                        case OVRInput.Button.PrimaryThumbstickLeft:
                            mbx = GameObject.Find("mesh_stickL");
                            mbx.GetComponent<Renderer>().material.color = Color.green;
                            mbx.transform.localEulerAngles = new Vector3(mbx.transform.rotation.x, 40, mbx.transform.rotation.z);
                            break;
                        case OVRInput.Button.PrimaryThumbstickRight:
                            mbx = GameObject.Find("mesh_stickL");
                            mbx.GetComponent<Renderer>().material.color = Color.green;
                            mbx.transform.localEulerAngles = new Vector3(mbx.transform.rotation.x, -40, mbx.transform.rotation.z);
                            break;

                        /////////////////////////////// Right controller
                        case OVRInput.Button.Three:
                            mbx = GameObject.Find("mesh_button_x");
                            mbx.GetComponent<Renderer>().material.color = Color.green;
                            break;
                        case OVRInput.Button.Four:
                            mbx = GameObject.Find("mesh_button_y");
                            mbx.GetComponent<Renderer>().material.color = Color.green;
                            break;
                        case OVRInput.Button.SecondaryIndexTrigger:
                            mbx = GameObject.Find("mesh_trigger_fingerR");
                            mbx.GetComponent<Renderer>().material.color = Color.green;
                            break;
                        case OVRInput.Button.SecondaryHandTrigger:
                            mbx = GameObject.Find("mesh_trigger_palmR");
                            mbx.GetComponent<Renderer>().material.color = Color.green;
                            break;
                        case OVRInput.Button.Back:
                            mbx = GameObject.Find("mesh_button_oR");
                            mbx.GetComponent<Renderer>().material.color = Color.green;
                            break;
                        case OVRInput.Button.SecondaryThumbstick:
                            mbx = GameObject.Find("mesh_stickR");
                            mbx.GetComponent<Renderer>().material.color = Color.green;
                            break;
                        case OVRInput.Button.SecondaryThumbstickDown:
                            mbx = GameObject.Find("mesh_stickR");
                            mbx.GetComponent<Renderer>().material.color = Color.green;
                            mbx.transform.eulerAngles = new Vector3(140, mbx.transform.rotation.y, mbx.transform.rotation.z);// 270-  required 40 
                            break;
                        case OVRInput.Button.SecondaryThumbstickUp:
                            mbx = GameObject.Find("mesh_stickR");
                            mbx.GetComponent<Renderer>().material.color = Color.green;
                            mbx.transform.eulerAngles = new Vector3(220, mbx.transform.rotation.y, mbx.transform.rotation.z);// 270+  required 40 
                            break;
                        case OVRInput.Button.SecondaryThumbstickLeft:
                            mbx = GameObject.Find("mesh_stickR");
                            mbx.GetComponent<Renderer>().material.color = Color.green;
                            mbx.transform.localEulerAngles = new Vector3(mbx.transform.rotation.x, 40, mbx.transform.rotation.z);
                            break;
                        case OVRInput.Button.SecondaryThumbstickRight:
                            mbx = GameObject.Find("mesh_stickR");
                            mbx.GetComponent<Renderer>().material.color = Color.green;
                            mbx.transform.localEulerAngles = new Vector3(mbx.transform.rotation.x, -40, mbx.transform.rotation.z);
                            break;





                    }
                }
            }
        }
    }
   public void allGrayButton()
    {
        foreach(GameObject go in GOControllerButton)
        {
            if (go.name != "mesh_touchL" && go.name != "mesh_touchR")
            {
                go.GetComponent<Renderer>().material.color = Color.gray;
            }
        }
        GameObject.Find("mesh_stickL").transform.localEulerAngles = lstickRotation;
        GameObject.Find("mesh_stickR").transform.localEulerAngles = rstickRotation;
    }
    public void pressButton(OVRInput.Button button)
    {
        Debug.Log(button+" press button");
        switch (button)
        {

            case OVRInput.Button.One:
                GameObject mbx = GameObject.Find("mesh_button_a");
                mbx.GetComponent<Renderer>().material.color = Color.red;
                break;
            case OVRInput.Button.Two:
                mbx = GameObject.Find("mesh_button_b");
                mbx.GetComponent<Renderer>().material.color = Color.red;
                break;
            case OVRInput.Button.PrimaryIndexTrigger:
                mbx = GameObject.Find("mesh_trigger_fingerL");
                mbx.GetComponent<Renderer>().material.color = Color.red;
                break;
            case OVRInput.Button.PrimaryHandTrigger:
                mbx = GameObject.Find("mesh_trigger_palmL");
                mbx.GetComponent<Renderer>().material.color = Color.red;
                break;
            case OVRInput.Button.Start:
                mbx = GameObject.Find("mesh_button_oL");
                mbx.GetComponent<Renderer>().material.color = Color.red;
                break;
            case OVRInput.Button.PrimaryThumbstick:
                mbx = GameObject.Find("mesh_stickL");
                mbx.GetComponent<Renderer>().material.color = Color.red;
                break;
            case OVRInput.Button.PrimaryThumbstickDown:
                mbx = GameObject.Find("mesh_stickL");
                mbx.GetComponent<Renderer>().material.color = Color.red;
                mbx.transform.eulerAngles = new Vector3(140, mbx.transform.rotation.y, mbx.transform.rotation.z);// 270-  required 40 
                break;
            case OVRInput.Button.PrimaryThumbstickUp:
                mbx = GameObject.Find("mesh_stickL");
                mbx.GetComponent<Renderer>().material.color = Color.red;
                mbx.transform.eulerAngles = new Vector3(220, mbx.transform.rotation.y, mbx.transform.rotation.z);// 270+  required 40 
                break;
            case OVRInput.Button.PrimaryThumbstickLeft:
                mbx = GameObject.Find("mesh_stickL");
                mbx.GetComponent<Renderer>().material.color = Color.red;
                mbx.transform.localEulerAngles = new Vector3(mbx.transform.rotation.x, 40, mbx.transform.rotation.z);
                break;
            case OVRInput.Button.PrimaryThumbstickRight:
                mbx = GameObject.Find("mesh_stickL");
                mbx.GetComponent<Renderer>().material.color = Color.red;
                mbx.transform.localEulerAngles = new Vector3(mbx.transform.rotation.x, -40, mbx.transform.rotation.z);
                break;

            /////////////////////////////// Right controller
            case OVRInput.Button.Three:
                mbx = GameObject.Find("mesh_button_x");
                mbx.GetComponent<Renderer>().material.color = Color.red;
                break;
            case OVRInput.Button.Four:
                mbx = GameObject.Find("mesh_button_y");
                mbx.GetComponent<Renderer>().material.color = Color.red;
                break;
            case OVRInput.Button.SecondaryIndexTrigger:
                mbx = GameObject.Find("mesh_trigger_fingerR");
                mbx.GetComponent<Renderer>().material.color = Color.red;
                break;
            case OVRInput.Button.SecondaryHandTrigger:
                mbx = GameObject.Find("mesh_trigger_palmR");
                mbx.GetComponent<Renderer>().material.color = Color.red;
                break;
            case OVRInput.Button.Back:
                mbx = GameObject.Find("mesh_button_oR");
                mbx.GetComponent<Renderer>().material.color = Color.red;
                break;
            case OVRInput.Button.SecondaryThumbstick:
                mbx = GameObject.Find("mesh_stickR");
                mbx.GetComponent<Renderer>().material.color = Color.red;
                break;
            case OVRInput.Button.SecondaryThumbstickDown:
                mbx = GameObject.Find("mesh_stickR");
                mbx.GetComponent<Renderer>().material.color = Color.red;
                mbx.transform.eulerAngles = new Vector3(140, mbx.transform.rotation.y, mbx.transform.rotation.z);// 270-  required 40 
                break;
            case OVRInput.Button.SecondaryThumbstickUp:
                mbx = GameObject.Find("mesh_stickR");
                mbx.GetComponent<Renderer>().material.color = Color.red;
                mbx.transform.eulerAngles = new Vector3(220, mbx.transform.rotation.y, mbx.transform.rotation.z);// 270+  required 40 
                break;
            case OVRInput.Button.SecondaryThumbstickLeft:
                mbx = GameObject.Find("mesh_stickR");
                mbx.GetComponent<Renderer>().material.color = Color.red;
                mbx.transform.localEulerAngles = new Vector3(mbx.transform.rotation.x, 40, mbx.transform.rotation.z);
                break;
            case OVRInput.Button.SecondaryThumbstickRight:
                mbx = GameObject.Find("mesh_stickR");
                mbx.GetComponent<Renderer>().material.color = Color.red;
                mbx.transform.localEulerAngles = new Vector3(mbx.transform.rotation.x, -40, mbx.transform.rotation.z);
                break;





        }
    }
}
