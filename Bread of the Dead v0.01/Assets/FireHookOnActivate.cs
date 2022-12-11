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

    public float hookshotSpeed = 0.1f;

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
            movement.GetComponent<HookShotMovement>().applyVelocity(hookshotDirection * hookshotSpeed);
        }

        Debug.Log("Ground is: ");
        Debug.Log(movement.GetComponent<ContinuousMovementPhysics>().CheckIfGrounded());
        Debug.Log("Hooked is: ");
        Debug.Log(hooked);
    
    }


    private void HandleHookshotStart(ActivateEventArgs arg)
    {   
        Debug.Log("Trigger press");
        if(!hooked && Physics.Raycast(transform.position, transform.forward, out RaycastHit raycastHit))
        {   
            
            movement.GetComponent<HookShotMovement>().toggleGravity(false);
            debugHitPointTransform.position = raycastHit.point;
            hookshotDirection = (raycastHit.point - transform.position).normalized;
            hooked = true;
            movement.GetComponent<HookShotMovement>().applyVelocity(hookshotDirection * hookshotSpeed);
        }
    }

    private void HandleHookshotEnd(DeactivateEventArgs arg)
    {
        Debug.Log("Trigger left");
        movement.GetComponent<HookShotMovement>().toggleGravity(true);
        hooked = false;
    }

    private void HandleHookshotDrop(SelectExitEventArgs arg)
    {
        if(hooked){
            movement.GetComponent<HookShotMovement>().toggleGravity(true);
            hooked = false;
        }
        
    }

    

}
