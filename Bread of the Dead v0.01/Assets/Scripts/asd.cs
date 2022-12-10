using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Hook : MonoBehaviour
{   
    public GameObject bullet;
    public Transform spawnPoint;
    public float fireSpeed = 20;
    // Start is called before the first frame update

    public Transform debugHitPointTransform;

    void Start()
    {
        XRGrabInteractable grabbable = GetComponent<XRGrabInteractable>();
        grabbable.activated.AddListener(HandleHookshotStart);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FireBullet(ActivateEventArgs arg){
        GameObject spawnedBullet = Instantiate(bullet);
        spawnedBullet.transform.position = spawnPoint.position;
        spawnedBullet.GetComponent<Rigidbody>().velocity = spawnPoint.forward * fireSpeed;
        Destroy(spawnedBullet, 5);
    }

    private void HandleHookshotStart(ActivateEventArgs arg)
    {
        if(Physics.Raycast(transform.position, transform.forward, out RaycastHit raycastHit))
        {
            //rb.useGravity = false; // quitar gravedad?
            //rb.velocity = new Vector3(0f, 0f, 0f);
            debugHitPointTransform.position = raycastHit.point;
            //state = State.HookshotFlying;
            //hookshotPosition = raycastHit.point;
        }
    }
/*
    private void HandleHookshotMovement()
    {
         Vector3 hookshotDirection = (hookshotPosition - transform.position).normalized;
        
        //float hookshotSpeed = Vector3.Distance(transform.position, hookshotPosition);
        
        rb.velocity = hookshotDirection * hookshotSpeedMultiplier;
        float reachedDestinationDistance = 2f;
        if(Vector3.Distance(transform.position, hookshotPosition) < reachedDestinationDistance)
        {
            state = State.Normal;
            rb.useGravity = true;
        }

        if(TestInputDownHookshot())
        {
            state = State.Normal;
            rb.useGravity = true;
        }
    }*/
}
