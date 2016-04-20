using UnityEngine;
using System.Collections;

public enum PLAYERSTATE
{
    IDLE = 0,
    ATTACK,
    DEFEND,
    DEAD
}


public class PlayerAnimationScript : MonoBehaviour
{

    PlayerMovement playerMovement;
    Animator anim;
    PlayerHealth playerHealth;

    Player2Health p2Health;
    float actionTimer = 0.0f;
    const float timeBetweenAction = 1.5f;

    public PLAYERSTATE p1State;

    void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
        playerHealth = GetComponent<PlayerHealth>();
        anim = GetComponent<Animator>();
        p2Health = GameObject.Find("Player02").GetComponent<Player2Health>();
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (playerHealth.IsPlayerAlive())
        {
            actionTimer += Time.deltaTime;
            anim.SetBool("Defend", false);

            if (Input.GetButton("X_button"))
            {
                Defend();

            }
            else {
                p1State = PLAYERSTATE.IDLE;
            }



            if (Input.GetButton("A_button") && (actionTimer >= timeBetweenAction))
            {
                CounterHit();
            }
            else if (Input.GetButton("B_button") && (actionTimer >= timeBetweenAction))
            {
                Slap();
            }
            else if (Input.GetButton("Y_button") && (actionTimer >= timeBetweenAction))
            {
                Slash();
            }



            if (Input.GetButton("Taunt") && (actionTimer >= timeBetweenAction))
            {
                Taunt();
                return;
            }

           
        }

    } // end Update()



    //////////////////////////////////////////////////////////////////////////////////////////
    void Defend()
    {
        p1State = PLAYERSTATE.DEFEND;
        actionTimer = 0.0f;
        anim.SetBool("Defend", true);
    }

    void Slap()
    {
        p1State = PLAYERSTATE.ATTACK;
        p2Health.isHit = true;
        actionTimer = 0.0f;
        anim.SetTrigger("Slap");
    }

    void CounterHit()
    {
        p1State = PLAYERSTATE.ATTACK;
        p2Health.isHit = true;
        actionTimer = 0.0f;
        anim.SetTrigger("CounterHit");
    }

    void Slash()
    {
        p1State = PLAYERSTATE.ATTACK;
        p2Health.isHit = true;
        actionTimer = 0.0f;
        anim.SetTrigger("Slash");
    }

    void Taunt()
    {
        actionTimer = 0.0f;
        anim.SetTrigger("Taunt");
    }
}
