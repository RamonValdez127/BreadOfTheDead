using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimateEnemy : MonoBehaviour
{

    public GameObject Enemy;
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim =  Enemy.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetTrigger("Move");
    }
}
