using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class ZombieController : MonoBehaviour
{
    // member
    Animator zombieAnim;
    NavMeshAgent agent;
    GameObject target;

    // inspector
    public float walkSpeed;
    public float runSpeed;
    public int zombieAT;

   public enum STATE
    {
        IDLE,
        CHASE,//(RUN)
        WANDER, //(WALK)
        ATTACK,
        DEAD
    };
    STATE state = STATE.IDLE;


    private void Start()
    {
        zombieAnim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        target = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        switch (state)
        {
            case STATE.IDLE:
                TurnOffTrigger();
               if (CanSeePlayer())
                {
                    state = STATE.CHASE;                    
                }
                else if(Random.Range(0, 5000) < 10)                
                {
                    state = STATE.WANDER;
                }

                break;
            case STATE.WANDER:
                if (!agent.hasPath)
                {
                    float x = transform.position.x + Random.Range(-5, 5);
                    float z = transform.position.z + Random.Range(-5, 5);

                    Vector3 newPos = new Vector3(x, transform.position.y, z);
                    agent.SetDestination(newPos);
                    agent.stoppingDistance = 0;

                    TurnOffTrigger();
                    agent.speed = walkSpeed;
                    zombieAnim.SetBool("Walk", true);
                }
                    if (Random.Range(0, 5000) < 5)
                    {
                        state = STATE.IDLE;
                        agent.ResetPath();
                    }
                    if (CanSeePlayer())
                {
                    state = STATE.CHASE;                  
                }

                break;
            case STATE.ATTACK:
                if (GameState.gameOver)
                {
                    TurnOffTrigger();
                    agent.ResetPath();
                    state = STATE.WANDER;

                    return;
                }
                TurnOffTrigger();
                zombieAnim.SetBool("Attack", true);

                transform.LookAt(new Vector3(target.transform.position.x, transform.position.y, target.transform.position.z));

                if (DistanceToPlayer() > agent.stoppingDistance + 2)
                {
                    state = STATE.CHASE;                  
                }
                break;

            case STATE.CHASE:
                if (GameState.gameOver)
                {
                    TurnOffTrigger();
                    agent.ResetPath();
                    state = STATE.WANDER;
                    return;
                }
                

                agent.SetDestination(target.transform.position);

                agent.stoppingDistance = 3;

                TurnOffTrigger();
                agent.speed = runSpeed;
                zombieAnim.SetBool("Run", true);

                if (agent.remainingDistance <= agent.stoppingDistance)
                {
                    state = STATE.ATTACK;
                }

                if (LostPlayer())
                {
                    agent.ResetPath();
                    state = STATE.WANDER;                    
                }
                break;
            case STATE.DEAD:
                Destroy(agent);
                zombieAnim.SetBool("Dead", true);  break;           
        }

        
    }

    float DistanceToPlayer()
    {
       if (GameState.gameOver)
        {
           return Mathf.Infinity;
        }
       return Vector3.Distance(target.transform.position, gameObject.transform.position);
    }

    bool CanSeePlayer()
    {
        if (DistanceToPlayer() < 10)
        {
            return true;
        }
        return false;
    }

    bool LostPlayer()
    {
        if (DistanceToPlayer() > 15)
        {
            return true;
        }
        return false ;
    }

    public void TurnOffTrigger()
    {
        zombieAnim.SetBool("Wander",false);
        zombieAnim.SetBool("Dead", false);
        zombieAnim.SetBool("Attack", false);
        zombieAnim.SetBool("Run", false);

    }

    public void DamagePlayer()
    {
        if(target != null)
        target.GetComponent<AcquireChanController>().TakeHit(zombieAT);
    }

    public void Attack()
    {
        if (DistanceToPlayer() <= agent.stoppingDistance)
        {
            TurnOffTrigger();
            zombieAnim.SetBool("Attack", true);
        }
        transform.LookAt(new Vector3(target.transform.position.x, transform.position.y, target.transform.position.z));

    }
    public void Run()
    {

    }

    public void Walk()
    {

    }

    public void Idle()
    {

    }
}
