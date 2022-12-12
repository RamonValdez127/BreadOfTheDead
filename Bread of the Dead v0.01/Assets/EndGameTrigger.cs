using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGameTrigger : MonoBehaviour
{
     private void OnTriggerEnter(Collider other){
        if(other.tag == "Body"){
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
