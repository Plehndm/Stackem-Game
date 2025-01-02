using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TowerFall : MonoBehaviour
{
    //Variables
    public LocaleControlsetter LocaleControlsetter;
    public GameObject focalPoint;
    private TowerRise maxHeight;
    [Range(0, 1)] public float speed;
    public int posAdjust;
    private bool isFalling = false;

    private void Start()
    {
        maxHeight = GameObject.Find("Height Maximum " + LocaleControlsetter.localeNum.ToString()).GetComponent<TowerRise>();
    }

    private void Update()
    {
        if (isFalling && !maxHeight.isRising)
        {
            StartCoroutine(CameraFalling());
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.CompareTag("PlayerGround"))
        {
            isFalling = true;
        }
    }

    private void OnTriggerStay(Collider collision)
    {
        if (collision.gameObject.CompareTag("PlayerGround"))
        {
            isFalling = false;
        }
    }

    IEnumerator CameraFalling()
    {
        focalPoint.transform.position = Vector3.Lerp(focalPoint.transform.position, new Vector3(focalPoint.transform.position.x, focalPoint.transform.position.y - posAdjust, focalPoint.transform.position.z), speed);
        yield return null;
    }
}
