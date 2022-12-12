using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrinkingAtole : MonoBehaviour
{
    public GameObject gameZone;
    public PlayerHealth Healing;
    void OnTriggerEnter(Collider other) {
        //Debug.Log("Entro algo");
        if (other.tag == "Atole")
        {
            Healing.RestoreHealth(50);
            other.gameObject.SetActive(false);
            gameZone.GetComponent<DetectPlayer>().toggleWait();
        }
    }
}
