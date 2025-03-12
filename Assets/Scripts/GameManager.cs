using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //VARIABLES
    int score = 0;
    public int totalPoints = 10;
    public TMP_Text scoreText;
    public static GameManager instance;
    public GameObject panelWin;

    private void Awake()
    {
        score = 0;
        Time.timeScale = 1;

        if (instance == null)
        {
            instance = this;
            //DontDestroyOnLoad(gameObject);
        }
        else { 
            Destroy(gameObject);
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        UpdateScoreText();
        Application.targetFrameRate = 60;
        QualitySettings.vSyncCount = 1;
    }

    // Update is called once per frame
    void Update()
    {
        WinLevel();
    }

    public void AddPoint(int point)
    {
        score += point;
        UpdateScoreText();
    }

    void UpdateScoreText()
    {
        if (scoreText != null) { 
            scoreText.text = score.ToString() + "/" + totalPoints.ToString();
        }
    }

    void WinLevel()
    {
        if(score == totalPoints)
        {
            Cursor.lockState = CursorLockMode.Confined; // Desbloquea nuevamente el cursor del mouse despues del playermovement
            panelWin.SetActive(true);
            Time.timeScale = 0f;
        }
    }


}
