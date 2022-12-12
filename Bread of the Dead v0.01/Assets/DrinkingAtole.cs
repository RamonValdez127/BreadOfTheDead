using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrinkingAtole : MonoBehaviour
{
    void OnTriggerEnter(Collider other) {
        //Debug.Log("Entro algo");
        if (other.tag == "Atole")
        {
            Destroy(other.gameObject);
        }
    }
}
