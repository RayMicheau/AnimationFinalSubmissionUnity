using UnityEngine;
using System.Collections;

public class PlayerTwoAnimationScript : MonoBehaviour
{

    PlayerTwoMovement playerTwoMovement;
    Animator anim;
    Player2Health player2Health;
    float actionTimer = 0.0f;
    const float timeBetweenAction = 1.5f;

    public PLAYERSTATE p2State;
    PlayerHealth p1Health;

    void Awake()
    {
        playerTwoMovement = GetComponent<PlayerTwoMovement>();
        player2Health = GetComponent<Player2Health>();
        anim = GetComponent<Animator>();
        p1Health = GameObject.Find("Player01").GetComponent<PlayerHealth>();
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (player2Health.IsPlayerAlive())
        {
            actionTimer += Time.deltaTime;
            anim.SetBool("Defend", false);

            Defend();

            if (Input.GetButton("A_button2") && (actionTimer >= timeBetweenAction))
            {
                CounterHit();
                return;
            }
            else if (Input.GetButton("B_button2") && (actionTimer >= timeBetweenAction))
            {
                Slap();
                return;
            }
            if (Input.GetButton("Taunt2") && (actionTimer >= timeBetweenAction))
            {
                Taunt();
                return;
            }

           
        }

    } // end Update()



    //////////////////////////////////////////////////////////////////////////////////////////
    void Defend()
    {
        if (Input.GetButton("X_button2"))
        {
            p2State = PLAYERSTATE.DEFEND;
            actionTimer = 0.0f;
            anim.SetBool("Defend", true);
        }
        else {
            p2State = PLAYERSTATE.IDLE;
        }
    }

    void Slap()
    {
        p2State = PLAYERSTATE.ATTACK;
        p1Health.isHit = true;
        actionTimer = 0.0f;
        anim.SetTrigger("CounterHit");
    }

    void CounterHit()
    {
        p2State = PLAYERSTATE.ATTACK;
        p1Health.isHit = true;
        actionTimer = 0.0f;
        anim.SetTrigger("Slap");
    }

    void Taunt()
    {
        actionTimer = 0.0f;
        anim.SetTrigger("Taunt");
    }

}
