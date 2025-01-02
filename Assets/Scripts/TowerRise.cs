using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerRise : MonoBehaviour
{
    //Variables
    public GameObject focalPoint;
    [Range (0,1)] public float speed;
    public int posAdjust;
    public bool isRising = false;

    private void OnTriggerStay(Collider collision)
    {
        if (collision.gameObject.CompareTag("PlayerGround"))
        {
            isRising = true;
            focalPoint.transform.position = Vector3.Lerp(focalPoint.transform.position, new Vector3(focalPoint.transform.position.x, focalPoint.transform.position.y + posAdjust, focalPoint.transform.position.z), speed);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        isRising = false;
    }
}
