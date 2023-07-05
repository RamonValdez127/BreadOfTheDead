using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Movement : MonoBehaviour
{
    private State state;
    public GameObject mixer;
    public NavMeshAgent agent;
    Vector3 targetDestination;

    private enum State
    {
        standing,
        moving,
        attacking,
        rotating,
        rotating2,
        dead,
        waiting
    }
    public float health;

    int JhonnyPlat = 0;
    public int playerPlat;

    private float playerT = 0;

    public GameObject GameZone;
    public Vector3[] JhonnyPositions;

    Vector3 targetPos;
    Vector3 targetDirection;
    Quaternion oldRotation;
    Vector3 rotationStep;
    float targetRotation;
    int platChange;
    float rotationTime;
    public float  speed;

    public GameObject Enemy;
    private Animator anim;
    AnimatorStateInfo animStateInfo;
    bool animationFinished = true;
    int attacks = 0;

    private PlayerHealth tHealth;
    float NTime;

    float cTime = 0;
    bool isFinished = false;

    int currAttack = -1;

    bool fallAnimationFinished = true;
    


    // Start is called before the first frame update
    void Start()
    {
        anim =  Enemy.GetComponent<Animator>();
        state = State.waiting;
        targetDestination = agent.destination;
       
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(state);
        if ( GetComponent<PlayerHealth>().health == 0 && !isFinished){
            mixer.GetComponent<AudioList>().finDelJuego();
            isFinished = true;
            state = State.dead;
        }
            
        switch (state)
        {
            case State.waiting:
                breathe();
                break;
            case State.dead:
                die();
                break;
            case State.attacking:
                //Debug.Log("Atacamos");
                attack();
                break;
            case State.rotating:
                //Debug.Log("Rotamos");
                animationFinished = true;
                rotate();
                break;
            case State.rotating2:
                //Debug.Log("Rotamos2");
                rotate();
                break;
            case State.moving:
                //Debug.Log("Nos Movemos");
                moveToPlatform(JhonnyPlat);
                break;
            case State.standing:
                //Debug.Log("Entramos");
                playerPlat = GameZone.GetComponent<DetectPlayer>().activePlatform;
                if(playerPlat == JhonnyPlat){
                   state = State.attacking;
                }
                    
                else
                {
                    targetPos = JhonnyPositions[playerPlat];
                    targetDirection = (targetPos - transform.position).normalized;
                    platChange = JhonnyPlat - playerPlat;
                    if(platChange == 2 || platChange == -2)
                    {
                        targetRotation = 4f;
                        rotationStep = new Vector3(0f, -45f, 0f);
                    }
                    else if(platChange == 1 || platChange == -3)
                    {
                        targetRotation = 3f;
                        rotationStep = new Vector3(0f, -45f, 0f);
                    }
                    else
                    {
                        targetRotation = 3f;
                        rotationStep = new Vector3(0f, 45f, 0f);
                    }
                    state = State.rotating;
                    playerT = 0;


                }
                rotationTime = 0f;
                oldRotation = transform.rotation;
                break;
        }
    }

    void moveToPlatform(int newPlat)
    {   

        animStateInfo = anim.GetCurrentAnimatorStateInfo(0);
        NTime = animStateInfo.normalizedTime;
        if(NTime > 1.0f){
            animationFinished = true;
        }

        if(animationFinished){
            animationFinished = false;
            anim.SetTrigger("Move");
        }
        Debug.Log( Vector3.Distance(transform.position, targetPos) );

        if (Vector3.Distance(targetDestination, targetPos) > 1.0f)
        {
            targetDestination = targetPos;
            agent.destination = targetDestination;
        }

        

        else if (Vector3.Distance(transform.position, targetPos) <= 1.0f)
        {
            if(targetRotation == 4f)
            {
                state = State.standing;
                return;
            }
            rotationTime = 0f;
            oldRotation = transform.rotation;
            rotationStep *= -1f;
            targetRotation = 1f;
            state = State.rotating2;
            return;
        }
        //if(trans)
    }

    void rotate()
    {   
        animStateInfo = anim.GetCurrentAnimatorStateInfo(0);
        NTime = animStateInfo.normalizedTime;
        if(NTime > 1.0f){
            animationFinished = true;
        }

        if(animationFinished){
            animationFinished = false;
            anim.SetTrigger("Move");
        }

        rotationTime += Time.deltaTime;
        if(rotationTime >= targetRotation)
        {
            transform.rotation = oldRotation;
            transform.Rotate(rotationStep*rotationTime);
            if(state == State.rotating)
            {
                state = State.moving;
                JhonnyPlat = playerPlat;
            }
            else
                state = State.standing;
        }
        else
        {
            transform.Rotate(rotationStep*Time.deltaTime);
        }
    }

    void attack()
    {
        /*if(currAttack == -1){
            currAttack = Random.Range(1,4);
        }
        
        else if(currAttack == 1){
            if(animationFinished){
                animationFinished = false;
                cTime = 0;
                anim.SetTrigger("Attack1");
            }

            else{
                cTime += Time.deltaTime;
                if(cTime > 7.0f){
                    animationFinished = true;
                    attacks += 1;
                    Debug.Log(attacks);
                    currAttack = -1;
                    state = State.standing;
                }
            }
        }

        else if(currAttack == 2){
            if(animationFinished){
                animationFinished = false;
                cTime = 0;
                anim.SetTrigger("Attack2");
            }

            else{
                cTime += Time.deltaTime;
                if(cTime > 6.0f){
                    animationFinished = true;
                    attacks += 1;
                    Debug.Log(attacks);
                    currAttack = -1;
                    state = State.standing;
                }
            }
        }

        else if(currAttack == 3){
            if(animationFinished){
                animationFinished = false;
                cTime = 0;
                currAttack = -1;
                anim.SetTrigger("Attack3");
            }

            else{
                cTime += Time.deltaTime;
                if(cTime > 6.0f){
                    animationFinished = true;
                    attacks += 1;
                    Debug.Log(attacks);
                    state = State.standing;
                }
            }
        }*/

        string attackLabel = "Attack";
       
        attackLabel += Random.Range(1, 4);
        Debug.Log(attackLabel);
        anim.SetTrigger(attackLabel);
        state = State.standing;
    }

    void breathe()
    {
        if(animationFinished){
                animationFinished = false;
                cTime = 0;
                anim.SetTrigger("Start");
            }

            else{
                cTime += Time.deltaTime;
                if(cTime > 8.0f){
                    animationFinished = true;
                    state = State.standing;
                }
            }
    }

    void die()
    {   
        if(animationFinished){
                animationFinished = false;
                cTime = 0;
                currAttack = -1;
                anim.SetTrigger("Die");
            }

            else{
                
                
            }
        
    }
}
