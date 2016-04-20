using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    public bool PAUSED = false;

    public int m_MatchTimeRemaining = 99;

    bool bIsMatchStarted = false;
    public bool bIsGameEnd = false;
    public bool bIsMatchPaused = false;

    GameObject m_PausePanel;

    int m_TimeToMatchStart = 3;

    float time = 0.0f;
    Text m_MatchLengthText;
    Text m_WinnerText;

    float m_ClearFightText = 0;

    Player2Health p2;
    PlayerHealth p1;

    void Awake() {
        Debug.Log("GAME MANAGER AWAKE");

        InitGame();
        Time.timeScale = 1;
    }


	void InitGame() {
        m_MatchLengthText = GameObject.Find("TimeRemaining").GetComponent<Text>();
        m_MatchLengthText.text = m_MatchTimeRemaining.ToString();
        m_WinnerText = GameObject.Find("WinnerText").GetComponent<Text>();
        m_PausePanel = GameObject.Find("Panel");
        m_PausePanel.SetActive(false);

        m_WinnerText.text = "Ready";
        InvokeRepeating("MatchStartCountdown", 1f, .5f);
    }

	// Use this for initialization
	void Start () {
        PAUSED = false;
        Time.timeScale = 1;
        p1 = GameObject.Find("Player01").GetComponent<PlayerHealth>();
        p2 = GameObject.Find("Player02").GetComponent<Player2Health>();

    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetButton("Pause"))
        {
            Debug.Log("Pause");
            PAUSED = !PAUSED;
        }

        if (PAUSED && m_PausePanel != null)
        {
            m_PausePanel.SetActive(true);
            Time.timeScale = 0;
        }
        else
        {
            if(m_PausePanel != null)
            {
                m_PausePanel.SetActive(false);
                Time.timeScale = 1;
            }

        }

        if (bIsMatchStarted)
        {
            if (m_ClearFightText >= .80f)
            {
                //m_WinnerText.text = "";
            }
            m_ClearFightText += Time.deltaTime;
        }
    }

    void MatchTimeCountdown()
    {
            if (!bIsGameEnd)
            {
                m_MatchTimeRemaining -= 1;
                m_MatchLengthText.text = m_MatchTimeRemaining.ToString();

                if (m_MatchTimeRemaining == 0)
                {
                    EndGame();
                }
            }
    }

    void MatchStartCountdown()
    {
        if (m_TimeToMatchStart != 0 && !bIsMatchStarted)
        {
            m_TimeToMatchStart -= 1;
        }
        else
        {
            Debug.Log("INVOKE COUNTDOWN START");
            bIsMatchStarted = true;
            CancelInvoke("MatchStartCountdown");
            InvokeRepeating("MatchTimeCountdown", 1f, 1f);
            m_WinnerText.text = "Fight";
            Debug.Log(bIsMatchStarted);

            StartCoroutine("Disappear");
        }
    }

    IEnumerator Disappear() {
        yield return new WaitForSeconds(2.0f);
        m_WinnerText.text = "";
    }


    //IDK if this works well, it feels like it does to me. 
    void EndGame()
    {
        Debug.Log("INVOKATION OF COUNTDOWN CANCELLED");
        m_WinnerText.text = "Time Out";
        CancelInvoke("MatchTimeCountdown");
        bIsGameEnd = true;
        if(p1.currenthealth > p2.currenthealth)
        {
            m_WinnerText.text = "PLAYER 1 WINS";
        }
        else if(p2.currenthealth > p1.currenthealth)
        {
            m_WinnerText.text = "PLAYER 2 WINS";
        }
        else
        {
            m_WinnerText.text = "TIE";
        }
        StartCoroutine("GameOver");
    }

    IEnumerator GameOver()
    {
        yield return new WaitForSeconds(3.0f);
        SceneManager.LoadScene("Title");
    }

    public void ReturnToMainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Title");
    }
}

