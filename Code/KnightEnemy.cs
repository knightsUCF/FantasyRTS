using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityStandardAssets.Characters.ThirdPerson;






public class KnightEnemy : MonoBehaviour
{


    // S E T T I N G S ////////////////////////////////////////////////////////////


    public Vector3 destination = Vector3.zero;


    // R E F E R E N C E S ////////////////////////////////////////////////////////

    public Camera cam;
    public NavMeshAgent agent;
    public ThirdPersonCharacter character;
    Animator animator;



    // G L O B A L S //////////////////////////////////////////////////////////////


    public enum State
    {
        Idle,
        Walk,
        Attack
    }

    public State state;


    bool attack = false;

    public bool engagingTarget = false;




    // M E T H O D S //////////////////////////////////////////////////////////////


    void Load()
    {
        animator = GetComponent<Animator>();
        agent.updateRotation = false; // we only want the rotation updated by the character
        cam = Camera.main;
        state = State.Idle;
    }



    public void MoveToDestination()
    {
        state = State.Walk;
        agent.isStopped = false;
        agent.SetDestination(destination); // move agent

    }



    void Locomotion()
    {
        if (state != State.Attack) // || state != State.Idle) // second clause to prevent from walking in place after destroying enemy, the other problem was delaying the routine since they happen at the same time and we were not registering the new idle state, while the collider was still there
        {
            if (agent.remainingDistance > agent.stoppingDistance)
            {
                state = State.Walk;
                character.Move(agent.desiredVelocity, false, false); // crouch - false, jump - false
            }

            else
            {
                state = State.Idle;
                character.Move(Vector3.zero, false, false); // stop moving
            }
        }
    }

        


    void Animate()
    {
        switch (state)
        {
            case State.Idle:
                animator.SetInteger("condition", 0);
                break;

            case State.Walk:
                animator.SetInteger("condition", 1);
                break;

            case State.Attack:
                animator.SetInteger("condition", 2);
                break;
        }
    }



    void FaceEnemy()
    {

    }



    void EngageTarget()
    {
        engagingTarget = true;
        character.Move(Vector3.zero, false, false); // stop moving
        agent.velocity = Vector3.zero;
        agent.isStopped = true;
        state = State.Attack;
    }



    void DisengageTarget()
    {
        attack = false;
    }


    // brute force safety net 

    void SetStateToIdleBasedOnMovementSpeed()
    {
        if (agent.velocity == Vector3.zero)
        {
            state = State.Idle;
        }
    }



    void OnTriggerEnter(Collider c)
    {
        if (c.tag == "Player")
        {
            EngageTarget();
            Debug.Log("Detected player");
        }
    }


    // on lose target stop attack and walk or go to default animation movement state (moving for calvary), adjust per script
    // or perhaps when we click somewhere away from the enemy we should go back to the animation state, or make the collider boundary narrow enough where we automatically start walking
    // for some reason we don't have a collider on the player, perhaps we can put a navmesh obstacle on the enemy so we don't overlap the collider by too wide of a margin


    void OnTriggerExit(Collider c)
    {
        if (c.tag == "Enemy")
        {
            DisengageTarget();
        }
    }


    public void EnemyDestroyed()
    {
        // TODO: do some sort of thing where we can detect how many enemies are in the area, so stop pursuing the destroyed enemy with the attack animation, and move on to the next one
        // this will also be set by the unit acquire settings, so aggressive type they will keep requiring, defensive stance, they will just hold their position, until the player points them at a new enemy, and also an inbetween state which will acquire enemies with a smaller radius than the agressive stance
        Invoke("SetStateToIdleAfterAttackDone", 1);
    }


    void SetStateToIdleAfterAttackDone()
    {
        // if (state != State.Attack) state = State.Idle;
        state = State.Idle;

    }

    public void SetDestination(Vector3 d)
    {
        destination = d;
    }


    void RunRoutines()
    {
        if (!engagingTarget) Locomotion();
        if (!engagingTarget) MoveToDestination();
        Animate();
    }


    
    
    // M A I N ///////////////////////////////////////////////////////////////////////////////



    void Start()
    {
        state = State.Attack;

        Load();
    }


    void Update()
    {
        RunRoutines();
    }

}







/*

    https://answers.unity.com/questions/1296581/ai-stop-and-attack.html


    void SpeedUpSlowlyStopQuickly()
    {
        // https://answers.unity.com/questions/236828/how-can-i-stop-navmesh-agent-sliding.html
        if (agent.hasPath) agent.acceleration = (agent.remainingDistance < closeEnoughMeters) ? deceleration : acceleration;
    }


void Move()
{
    if (agent.remainingDistance > agent.stoppingDistance)
    {
        animator.SetInteger("condition", 1);
        character.Move(agent.desiredVelocity, false, false); // crouch - false, jump - false
    }

    else
    {
        animator.SetInteger("condition", 0);
        character.Move(Vector3.zero, false, false); // stop moving
    }
}


// S E T T I N G S 


public float speed = 2;
    float rotationSpeed = 80;
    float gravity = 8;
    float rotation = 0;
    Vector3 moveDirection = Vector3.zero;

  

    // R E F E R E N C E S

    CharacterController characterController;
    Animator animator;

    

    // M E T H O D S

    // Move

    void Move()
    {
        // only move when the player is grounded

        if (characterController.isGrounded)
        {
            
            // check for player input, and move on W (if not attacking)

            if (Input.GetKey(KeyCode.W))
            {
                
                // if attacking don't move
                
                if (animator.GetBool("attack") == true)
                {
                    return;
                }

                // move if not attacking
                
                else if (animator.GetBool("attack") == false)
                {
                    animator.SetBool("run", true);
                    animator.SetInteger("condition", 1);
                    moveDirection = new Vector3(0, 0, 1);
                    moveDirection *= speed;
                    moveDirection = transform.TransformDirection(moveDirection); // we move in the direction of the rotation
                }
            }

            // stop moving on let go

            if (Input.GetKeyUp(KeyCode.W))
            {
                animator.SetBool("run", false);
                animator.SetInteger("condition", 0);
                moveDirection = Vector3.zero;
            }
        }

        // rotation

        rotation += Input.GetAxis("Horizontal") * rotationSpeed * Time.deltaTime;
        transform.eulerAngles = new Vector3(0, rotation, 0);

        // apply movement

        moveDirection.y -= gravity * Time.deltaTime;
        characterController.Move(moveDirection * Time.deltaTime);
    }


    


    void GetInput()
    {
        // we don't want to attack if the controller is not grounded

        if(characterController.isGrounded)
        {
            if(Input.GetMouseButtonDown(0))
            {
                // so that we can attack while walking

                if(animator.GetBool("run") == true)
                {
                    animator.SetBool("run", false);
                    animator.SetInteger("condition", 0);
                }

                // if we are not running

                if (animator.GetBool("run") == false) Attack();

            }
        }
    }


    void Attack()
    {
        StartCoroutine("AttackRoutine");
    }


    IEnumerator AttackRoutine()
    {
        animator.SetBool("attack", true);
        animator.SetInteger("condition", 2); // play attack animation
        yield return new WaitForSeconds(1);
        animator.SetInteger("condition", 0);
        animator.SetBool("attack", false);
    }


    // M A I N 
    
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }


    void Update()
    {
        Move();
        GetInput();
    }


    */



