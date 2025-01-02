using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LocaleControlsetter : MonoBehaviour
{

    [Range(1,2)] public int localeNum;

    public GameObject localeCamera;
    public KeyCode leftInput;
    public KeyCode rightInput;
    public KeyCode upInput;
    public KeyCode downInput;
    public KeyCode place;

    public KeyCode rotateCameraLeft;
    public KeyCode rotateCameraRight;

    public KeyCode rotateBlockLeft;
    public KeyCode rotateBlockRight;
    public KeyCode rotateBlockForward;
    public KeyCode rotateBlockBack;

    public void Awake()
    {
        activatecontrols();
        getCameras();
    }

    #region Set Locale Controls
    public void activatecontrols()
    {
        
        if(localeNum == 1)
        {
            leftInput = KeyCode.A;
            rightInput = KeyCode.D;
            downInput = KeyCode.S;
            upInput = KeyCode.W;
            rotateCameraLeft = KeyCode.Q;
            rotateCameraRight = KeyCode.E;
            place = KeyCode.LeftShift;
            rotateBlockLeft = KeyCode.Alpha1;
            rotateBlockRight = KeyCode.Alpha2;
            rotateBlockForward = KeyCode.Alpha3;
            rotateBlockBack = KeyCode.Alpha4;
        } else if (localeNum == 2)
        {
            leftInput = KeyCode.J;
            rightInput = KeyCode.L;
            downInput = KeyCode.K;
            upInput = KeyCode.I;
            rotateCameraLeft = KeyCode.U;
            rotateCameraRight = KeyCode.O;
            place = KeyCode.RightShift;
            rotateBlockLeft = KeyCode.Alpha7;
            rotateBlockRight = KeyCode.Alpha8;
            rotateBlockForward = KeyCode.Alpha9;
            rotateBlockBack = KeyCode.Alpha0;
        }
        
    }
    #endregion

    #region Camera Assignment
    public void getCameras()
    {
        if(localeNum == 1)
        {
            localeCamera = GameObject.Find("Locale 1 Camera").gameObject;
        } else if (localeNum == 2)
        {
            localeCamera = GameObject.Find("Locale 2 Camera").gameObject;
        }
    }
    #endregion
}
