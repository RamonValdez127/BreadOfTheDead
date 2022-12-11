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

    public void toggleGravity(){
        body.useGravity = !body.useGravity;
        leftHand.useGravity = !leftHand.useGravity;
        rightHand.useGravity = !rightHand.useGravity;
        head.useGravity = !head.useGravity;
    }

    public void applyVelocity(Vector3 nVelocity){
        body.velocity = nVelocity;
        leftHand.velocity = nVelocity;
        rightHand.velocity = nVelocity;
        head.velocity = nVelocity;
    }

}
