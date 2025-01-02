using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BlockLogic : MonoBehaviour
{
    public string newTag = "PlayerGround";
    public bool isColliding = false;

    private void OnCollisionEnter(Collision collision)
    {
        transform.tag = newTag;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Trigger Collider") {
            isColliding = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        isColliding = false;
    }
}
