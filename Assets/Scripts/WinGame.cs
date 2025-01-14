using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinGame : MonoBehaviour
{
    public bool playerWon = false;

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "PlayerGround") {
            playerWon = true;
        }
    }
}
