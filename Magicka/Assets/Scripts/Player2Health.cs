using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Player2Health : MonoBehaviour
{

    public int currenthealth;
    int maxHealth = 100;
    Animator anim;
    PlayerTwoMovement playerMovement;
    PlayerTwoAnimationScript playerAnim;
    Slider m_PlayerTwoHealthSlider;
    Text m_WinnerText;

    PlayerAnimationScript p1;
    PlayerTwoAnimationScript p2;

    public bool isHit = false;

    // Use this for initialization
    void Start()
    {
        currenthealth = maxHealth;
        anim = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerTwoMovement>();
        playerAnim = GetComponent<PlayerTwoAnimationScript>();
        m_PlayerTwoHealthSlider = GameObject.Find("P2_Slider").GetComponent<Slider>();
        m_PlayerTwoHealthSlider.value = maxHealth;
        m_WinnerText = GameObject.Find("WinnerText").GetComponent<Text>();

        p1 = GameObject.Find("Player01").GetComponent<PlayerAnimationScript>();
        p2 = GetComponent<PlayerTwoAnimationScript>();
    }

    // Update is called once per frame
    void Update() { }

    public void Heal(int _amount)
    {
        currenthealth += _amount;
        m_PlayerTwoHealthSlider.value = currenthealth;
        if (currenthealth > maxHealth)
        {
            currenthealth = maxHealth;
        }
    }

    public void TakeDamage(int _amount)
    {
        if (isHit && IsPlayerAlive())
        {
            currenthealth -= _amount;
            m_PlayerTwoHealthSlider.value = currenthealth;
            Debug.Log("Player2: " + currenthealth);
            if (!IsPlayerAlive())
            {
                Death();
            }
            isHit = false;
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
        Debug.Log("DEAD!");
        anim.SetTrigger("Death");
        playerMovement.enabled = false;
        playerAnim.enabled = false;
        m_WinnerText.text = "P1 Winner";
        StartCoroutine("GameOver");
    }

    IEnumerator GameOver()
    {
        yield return new WaitForSeconds(3.0f);
        Application.LoadLevel("Title");
    }


    void OnTriggerStay(Collider c)
    {
        if (c.gameObject.tag == "Player" && c.gameObject.name == "Player01")
        {
        Debug.Log ("Collision on Player 2");

            if (p1.p1State == PLAYERSTATE.ATTACK
                &&
            p2.p2State != PLAYERSTATE.DEFEND)
            {
                TakeDamage(10);
            }

        }
    }
}
