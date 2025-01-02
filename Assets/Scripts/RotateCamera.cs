using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.Windows;

public class RotateCamera : MonoBehaviour
{
    // Variables
    public float rotationSpeed;
    public LocaleControlsetter LocaleControlsetter;

    public GameObject anchor_1;
    public GameObject anchor_2;
    public GameObject anchor_3;
    public GameObject anchor_4;
    public Camera localeCam;

    private int currAnchor;

    private void Start()
    {
        LocaleControlsetter.activatecontrols();
        localeCam.transform.position = anchor_1.transform.position;
        localeCam.transform.rotation = anchor_1.transform.rotation;

        currAnchor = 1;
    }

    void Update()
    {
        if (UnityEngine.Input.GetKeyDown(LocaleControlsetter.rotateCameraLeft))
        {

            if(currAnchor == 1)
            {
                localeCam.transform.position = anchor_4.transform.position;
                localeCam.transform.rotation = anchor_4.transform.rotation;
                currAnchor = 4;
            } else if (currAnchor == 2)
            {
                localeCam.transform.position = anchor_1.transform.position;
                localeCam.transform.rotation = anchor_1.transform.rotation;
                currAnchor = 1;
            } else if(currAnchor == 3)
            {
                localeCam.transform.position = anchor_2.transform.position;
                localeCam.transform.rotation = anchor_2.transform.rotation;
                currAnchor = 2;
            } else if(currAnchor == 4)
            {
                localeCam.transform.position = anchor_3.transform.position;
                localeCam.transform.rotation = anchor_3.transform.rotation;
                currAnchor = 3;
            }

        } else if(UnityEngine.Input.GetKeyDown(LocaleControlsetter.rotateCameraRight))
        {

            if (currAnchor == 3)
            {
                localeCam.transform.position = anchor_4.transform.position;
                localeCam.transform.rotation = anchor_4.transform.rotation;
                currAnchor = 4;
            }
            else if (currAnchor == 4)
            {
                localeCam.transform.position = anchor_1.transform.position;
                localeCam.transform.rotation = anchor_1.transform.rotation;
                currAnchor = 1;
            }
            else if (currAnchor == 1)
            {
                localeCam.transform.position = anchor_2.transform.position;
                localeCam.transform.rotation = anchor_2.transform.rotation;
                currAnchor = 2;
            }
            else if (currAnchor == 2)
            {
                localeCam.transform.position = anchor_3.transform.position;
                localeCam.transform.rotation = anchor_3.transform.rotation;
                currAnchor = 3;
            }

        }
    }
}
