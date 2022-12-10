using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetObject : MonoBehaviour
{   
    // Start is called before the first frame update
   void OnTriggerEnter(Collider other) {
        other.gameObject.transform.position = new Vector3(0.9024243f, 0.7833f, 2.7605f);
    }
}
