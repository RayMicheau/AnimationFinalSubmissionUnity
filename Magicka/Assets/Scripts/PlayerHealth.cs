using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{

    public int currenthealth;
    int maxHealth = 100;
    Animator anim;
    PlayerMovement playerMovement;
    PlayerAnimationScript playerAnim;
    Slider m_PlayerHealthSlider;
    Text m_WinnerText;

    PlayerAnimationScript p1;
    PlayerTwoAnimationScript p2;

    public bool isHit = false;

    // Use this for initialization
    void Start()
    {
        currenthealth = maxHealth;
        anim = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
        playerAnim = GetComponent<PlayerAnimationScript>();
        m_PlayerHealthSlider = GameObject.Find("P1_Slider").GetComponent<Slider>();
        m_WinnerText = GameObject.Find("WinnerText").GetComponent<Text>();
        m_PlayerHealthSlider.value = currenthealth;


        p1 = GetComponent<PlayerAnimationScript>();
        p2 = GameObject.Find("Player02").GetComponent<PlayerTwoAnimationScript>();
    }

    // Update is called once per frame
    void Update() { }

    public void TakeDamage(int _amount)
    {
        if (isHit && IsPlayerAlive())
        {
            currenthealth -= _amount;
            m_PlayerHealthSlider.value = currenthealth;
            Debug.Log(currenthealth);
            if (!IsPlayerAlive())
            {
                Death();
            }
            isHit = false;
        }
    }

    public void Heal(int _amount)
    {
        currenthealth += _amount;
        m_PlayerHealthSlider.value = currenthealth;
        if (currenthealth > maxHealth)
        {
            currenthealth = maxHealth;
        }
    }

    public bool IsPlayerAlive()
    {
        if (currenthealth <= 0)
        {
            return false;
        }
        else {
            return true;
        }
    }

    void Death()
    {
        anim.SetTrigger("Death");
        playerMovement.enabled = false;
        playerAnim.enabled = false;
        m_WinnerText.text = "P2 Winner";
        StartCoroutine("GameOver");
    }

    IEnumerator GameOver()
    {
        yield return new WaitForSeconds(3.0f);
        Application.Quit();
    }


    void OnTriggerStay(Collider c)
    {
        //Debug.Log ("Collision on Player 1");
        if (c.gameObject.tag == "Player" && c.gameObject.name == "Player02")
        {

            if (p2.p2State == PLAYERSTATE.ATTACK
                &&
            p1.p1State != PLAYERSTATE.DEFEND)
            {
                TakeDamage(10);
            }

        }
    }
} // end PlayerHealth class