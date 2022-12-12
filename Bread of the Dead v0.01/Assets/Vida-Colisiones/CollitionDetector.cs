using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class CollitionDetector : MonoBehaviour
{
    public PlayerHealth damageTaker;
    public string damageDealer;

    private void OnTriggerEnter(Collider other){
        if(damageDealer == "Johnny" && other.tag == "Body"){
            damageTaker.TakeDamage(10);
            if(damageTaker.health == 0)
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

            
        }

        if(damageDealer == "Player" && other.tag == "Weapon"){
            damageTaker.TakeDamage(5);
            
        }
    }
}
