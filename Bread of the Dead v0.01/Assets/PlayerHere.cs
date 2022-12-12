using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHere : MonoBehaviour
{
    public int number;

    void OnTriggerEnter(Collider other) {
        if (other.tag == "Body")
        {
            transform.parent.GetComponent<DetectPlayer>().activePlatform = number; 
        }
    }

}
