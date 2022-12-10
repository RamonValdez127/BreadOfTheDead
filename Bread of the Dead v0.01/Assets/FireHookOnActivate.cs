using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem;



public class FireHookOnActivate : MonoBehaviour
{   
    /*public GameObject bullet;
    public Transform spawnPoint;
    public float fireSpeed = 20;
    // Start is called before the first frame update

    public GameObject movement;
*/
    private bool hooked = false;

    public GameObject movement;

    public Transform debugHitPointTransform;

    Vector3 hookshotDirection;

    public InputActionProperty leftActivate;

    float hookshotSpeed = 0.1f;
    float auxHookshotSpeed = 0.1f;
    float deceleration = 1f;

    void Start()
    {
        XRGrabInteractable grabbable = GetComponent<XRGrabInteractable>();
        grabbable.activated.AddListener(HandleHookshotStart);
        grabbable.deactivated.AddListener(HandleHookshotEnd);
        grabbable.selectExited.AddListener(HandleHookshotDrop);
    }

    // Update is called once per frame
    void Update()
    {
    
        if (hooked){
            movement.GetComponent<ActionBasedContinuousMoveProvider>().callMoveRig(hookshotDirection * hookshotSpeed);
        }
        else if(auxHookshotSpeed > 0.0001f){
            movement.GetComponent<ActionBasedContinuousMoveProvider>().callMoveRig(hookshotDirection * auxHookshotSpeed);
            deceleration *= .9f;
        }
    }


    private void HandleHookshotStart(ActivateEventArgs arg)
    {
        if(!hooked && Physics.Raycast(transform.position, transform.forward, out RaycastHit raycastHit))
        {   
            
            movement.GetComponent<ActionBasedContinuousMoveProvider>().useGravity = false;
            debugHitPointTransform.position = raycastHit.point;
            hookshotDirection = (raycastHit.point - transform.position).normalized;
            movement.GetComponent<ActionBasedContinuousMoveProvider>().useGravity = false;
            hooked = true;
            auxHookshotSpeed = hookshotSpeed;
        }
    }

    private void HandleHookshotEnd(DeactivateEventArgs arg)
    {
        movement.GetComponent<ActionBasedContinuousMoveProvider>().useGravity = true;
        hooked = false;
    }

    private void HandleHookshotDrop(SelectExitEventArgs arg)
    {
        movement.GetComponent<ActionBasedContinuousMoveProvider>().useGravity = true;
        hooked = false;
    }

    

}
