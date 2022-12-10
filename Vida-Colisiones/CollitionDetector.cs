using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollitionDetector : MonoBehaviour
{
    public PlayerHealth Player;
    public GameObject Atole;

    private void OnTriggerEnter(Collider other){
        if(other.tag == "Enemy"){
            Player.TakeDamage(10);
        }
        if(other.tag == "Atole"){
            Player.RestoreHealth(10);
            Atole.SetActive (false);
        }
    }
}
