using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Transform debugHitPointTransform;

    public Transform playerCam;

    [Header("Movement")]
    public float moveSpeed;

    public float groundDrag;

    public float jumpForce;
    public float jumpCooldown;
    public float airMultiplier;
    bool readyToJump = true;

    [Header("Keybinds")]
    public KeyCode jumpKey = KeyCode.Space;

    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask whatIsGround;
    bool grounded;

    public Transform orientation;

    float horizontalInput;
    float verticalInput;

    Vector3 moveDirection;

    Rigidbody rb;
    
    private State state;

    private Vector3 hookshotPosition;

    public float maxAirSpeed;

    public float hookshotSpeedMultiplier;

    private enum State
    {
        Normal,
        HookshotFlying
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        state = State.Normal;
    }

    // Update is called once per frame
    void Update()
    {
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatIsGround);
        switch(state)
        {
            default:
                break;
            case State.Normal:
                MyInput();
                SpeedControl();
                HandleHookshotStart();

                if(grounded)
                    rb.drag = groundDrag;
                else
                    rb.drag = 0;
                break;
            case State.HookshotFlying:
                HandleHookshotMovement();
                SpeedControl();
                break;
        }
    }

    void FixedUpdate()
    {
        MovePlayer();
    }

    private void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        if(Input.GetKey(jumpKey) && readyToJump && grounded)
        {
            readyToJump = false;

            Jump();

            Invoke(nameof(ResetJump), jumpCooldown);
        }
    }

    private void MovePlayer()
    {
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        if(grounded)
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);

        else
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force);
    }

    private void SpeedControl()
    {
        float currSpeed;
        if(grounded) 
            currSpeed = moveSpeed;
        else 
            currSpeed = maxAirSpeed;
        Vector3 flatVel = new Vector3(rb.velocity.x, rb.velocity.y, rb.velocity.z);

        if(flatVel.magnitude > currSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * currSpeed;
            rb.velocity = new Vector3(limitedVel.x, limitedVel.y, limitedVel.z);
        }
    }

    private void Jump()
    {
        //rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }
    private void ResetJump()
    {
        readyToJump = true;
    }

    private void HandleHookshotStart()
    {
        if(TestInputDownHookshot())
        {
            if(Physics.Raycast(playerCam.position, playerCam.forward, out RaycastHit raycastHit))
            {
                rb.useGravity = false;
                rb.velocity = new Vector3(0f, 0f, 0f);
                debugHitPointTransform.position = raycastHit.point;
                state = State.HookshotFlying;
                hookshotPosition = raycastHit.point;
            }
        }
    }

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
        if(Input.GetKey(jumpKey))
        {
            Jump();
            state = State.Normal;
            rb.useGravity = true;
        }
    }

    private bool TestInputDownHookshot()
    {
        return Input.GetKeyDown(KeyCode.E);
    }
}
