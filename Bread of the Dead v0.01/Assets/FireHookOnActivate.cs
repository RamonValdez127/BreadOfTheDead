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
    private bool throwingHook = false;

    public AudioClip clip;
    private AudioSource source;
    public GameObject movement;
    public Transform ShotExitPosition;

    public Transform debugHitPointTransform;

    Vector3 hookshotDirection;
    Vector3 hookshotPosition;

    public float hookshotSpeed = 0.1f;

    public Transform mixerBladeTransform;
    public GameObject fakeBlade;
    public float hookshotThrowSpeed;
    float hookshotSize;

    void Start()
    {
        mixerBladeTransform.gameObject.SetActive(false);
        XRGrabInteractable grabbable = GetComponent<XRGrabInteractable>();
        grabbable.activated.AddListener(HandleHookshotStart);
        grabbable.deactivated.AddListener(HandleHookshotEnd);
        grabbable.selectExited.AddListener(HandleHookshotDrop);
        source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
    
        if (throwingHook)
        {
            HandleHookshotThrow();
        }
        if (hooked){
            mixerBladeTransform.position = hookshotPosition;
            movement.GetComponent<HookShotMovement>().applyVelocity(hookshotDirection * hookshotSpeed);
        }

        /*Debug.Log("Ground is: ");
        Debug.Log(movement.GetComponent<ContinuousMovementPhysics>().CheckIfGrounded());
        Debug.Log("Hooked is: ");
        Debug.Log(hooked);*/
    
    }


    private void HandleHookshotStart(ActivateEventArgs arg)
    {   
        //Debug.Log("Trigger press");
        if(!throwingHook && !hooked && Physics.Raycast(ShotExitPosition.position, ShotExitPosition.transform.forward, out RaycastHit raycastHit))
        {   
            fakeBlade.SetActive(false);
            mixerBladeTransform.gameObject.SetActive(true);
            mixerBladeTransform.position = ShotExitPosition.position;
            mixerBladeTransform.rotation = ShotExitPosition.rotation;
            debugHitPointTransform.position = raycastHit.point;
            hookshotPosition = raycastHit.point;
            //hookshotDirection = (raycastHit.point - ShotExitPosition.transform.position).normalized;
            throwingHook = true;
            hookshotSize = 0f;
        }

        else{
            source.PlayOneShot(clip);
        }
    }

    private void HandleHookshotThrow()
    {
        hookshotDirection = (hookshotPosition - ShotExitPosition.position).normalized;
        hookshotSize += hookshotThrowSpeed * Time.deltaTime;
        mixerBladeTransform.position += (hookshotThrowSpeed * Time.deltaTime)*hookshotDirection;//new Vector3(mixerBladeTransform.localPosition.x, mixerBladeTransform.localPosition.y, hookshotSize*25/2);

        if(hookshotSize >= Vector3.Distance(ShotExitPosition.position, hookshotPosition))
        {
            hooked = true;
            throwingHook = false;
            movement.GetComponent<HookShotMovement>().toggleGravity(false);
            movement.GetComponent<HookShotMovement>().applyVelocity(hookshotDirection * hookshotSpeed);
        }
    }

    private void HandleHookshotEnd(DeactivateEventArgs arg)
    {
        //Debug.Log("Trigger left");
        movement.GetComponent<HookShotMovement>().toggleGravity(true);
        hooked = false;
        mixerBladeTransform.position = ShotExitPosition.transform.position;
        fakeBlade.SetActive(true);
        mixerBladeTransform.gameObject.SetActive(false);
    }

    private void HandleHookshotDrop(SelectExitEventArgs arg)
    {
        if(hooked){
            movement.GetComponent<HookShotMovement>().toggleGravity(true);
            hooked = false;
        }
        mixerBladeTransform.position = ShotExitPosition.transform.position;
        fakeBlade.SetActive(true);
        mixerBladeTransform.gameObject.SetActive(false);
    }

    

}
