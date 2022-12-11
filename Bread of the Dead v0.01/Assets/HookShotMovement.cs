using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookShotMovement : MonoBehaviour
{
    private Rigidbody body;
    private Rigidbody leftHand;
    private Rigidbody rightHand;
    private Rigidbody head;

    public GameObject GO_body;
    public GameObject GO_leftHand;
    public GameObject GO_rightHand;
    public GameObject GO_head;

    void Start(){
        body = GO_body.GetComponent<Rigidbody>();
        leftHand = GO_leftHand.GetComponent<Rigidbody>();
        rightHand = GO_rightHand.GetComponent<Rigidbody>();
        head = GO_head.GetComponent<Rigidbody>();
    }

    public void toggleGravity(bool value){
        body.useGravity = value;
        leftHand.useGravity = value;
        rightHand.useGravity = value;
        head.useGravity = value;
    }

    public void applyVelocity(Vector3 nVelocity){
        body.velocity = nVelocity;
        leftHand.velocity = nVelocity;
        rightHand.velocity = nVelocity;
        head.velocity = nVelocity;
    }

}
