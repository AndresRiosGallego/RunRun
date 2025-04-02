using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    #region Variables
    
    int _score = 0;
    [SerializeField]
    public int totalTiempo = 3;
    [SerializeField]
    public int totalPoints = 10;
    [SerializeField]
    public TMP_Text scoreText;
    [SerializeField]
    public static GameManager instance;
    [SerializeField]
    public GameObject panelWin;
    [SerializeField]
    public GameObject panelPause;

    float _tiempoRestante = 0f; //minutos en segundos
    [SerializeField]
    public int totalVidas = 3;
    int _vidas = 1;
    [SerializeField]
    public TMP_Text tiempoTexto; //UI del tiempo
    [SerializeField]
    public TMP_Text vidasTexto;  //UI de las vidas
    [SerializeField]
    public GameObject panelGameOver; //Panel de Game Over

    private bool _juegoTerminado = false;

    Vector3 _firsPosition;
    GameObject _player;
    [SerializeField]
    public AudioClip gameOverSound;
    [SerializeField]
    public ParticleSystem gameOverParticles;

    #endregion

    private void Awake()
    {
        _score = 0;
        Time.timeScale = 1;

        if (instance == null)
        {
            instance = this;
            //DontDestroyOnLoad(gameObject);
        }
        else { 
            Destroy(gameObject);
        }

        _player = GameObject.FindGameObjectWithTag("Player");
        if(_player != null)
            _firsPosition = _player.transform.position;

        _tiempoRestante = (float)totalTiempo * 60;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        UpdateScoreText();
        ActualizarUI();
        panelGameOver.SetActive(false); // Asegurar que el panel esté oculto al inicio
        Application.targetFrameRate = 60;
        QualitySettings.vSyncCount = 1;

        //Cursor.visible = true; // Hace visible el cursor
        //Cursor.lockState = CursorLockMode.None; // Desbloquea el cursor
    }


    // Update is called once per frame
    void Update()
    {
        if (_juegoTerminado) return; // Si el juego terminó, no hacer nada
        ControlKeyBoard();
        ControlarTiempo();
        ActualizarUI();
        WinLevel();
    }

    private void ControlKeyBoard()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) 
        {
            if (panelPause.activeSelf)
            {
                Cursor.visible = false; // Hace visible el cursor
                Cursor.lockState = CursorLockMode.Locked; // Desbloquea el cursor

                Time.timeScale = 1f;
                panelPause.SetActive(false);
            }
            else
            {
                Cursor.visible = true; // Hace visible el cursor
                Cursor.lockState = CursorLockMode.None; // Desbloquea el cursor

                Time.timeScale = 0f;
                panelPause.SetActive(true);
            }
        }
    }

    private void ActualizarUI()
    {
        tiempoTexto.text = Mathf.CeilToInt(_tiempoRestante) + "s";
        vidasTexto.text = _vidas + "/" + totalVidas;
    }
    private void ControlarTiempo()
    {
        _tiempoRestante -= Time.deltaTime;
        if (_tiempoRestante <= 0)
        {
            _tiempoRestante = 0;
            PerderVida();
        }
    }

    private void PerderVida()
    {
        _vidas++;

        if (_vidas > totalVidas)
        {
            ResetPosition();
            UpdateScoreText();
            GameOver();
        }
        else
        {
            ReiniciarTiempo();
            UpdateScoreText();
            ResetPosition();
        }
        ReactivePoints();
    }

    private void ReactivePoints()
    {
        foreach (var item in GameObject.FindGameObjectsWithTag("Points"))
        {
            //item.SetActive(true);
            item.GetComponent<MeshRenderer>().enabled = true;
            item.GetComponent<Collider>().enabled = true;
        }
    }

    private void ReiniciarTiempo()
    {
        _tiempoRestante = (float)totalTiempo * 60; //Reinicia el tiempo 
        _score = 0;
    }

    private void GameOver()
    {
        Time.timeScale = 0f; //Una pausa para no dejar jugar mas
        _juegoTerminado = true;
        panelGameOver.SetActive(true); // Muestra el panel de Game Over
    }

    public void AddPoint(int point)
    {
        _score += point;
        UpdateScoreText();
    }

    void UpdateScoreText()
    {
        if (scoreText != null) { 
            scoreText.text = _score.ToString() + "/" + totalPoints.ToString();
        }
    }

    void WinLevel()
    {
        if(_score == totalPoints)
        {
            Cursor.lockState = CursorLockMode.Confined; // Desbloquea nuevamente el cursor del mouse despues del playermovement
            panelWin.SetActive(true);
            Time.timeScale = 0f;
        }
    }

    private void ResetPosition()
    {
        //Instanciar Sonido
        if (gameOverSound != null)
        {
            GameObject audioObject = new GameObject("PointEffectSound");
            AudioSource audioSource = audioObject.AddComponent<AudioSource>();
            audioSource.clip = gameOverSound;
            audioSource.Play();
            Destroy(audioObject, gameOverSound.length);
        }

        //Instanciar Particulas
        if (gameOverParticles != null)
        {
            ParticleSystem particleSystem = Instantiate(gameOverParticles, transform.position, Quaternion.identity);
            particleSystem.Play();
            Destroy(particleSystem.gameObject, particleSystem.main.duration);
        }

        if (_player != null)
        {
            _player.transform.position = _firsPosition;
        }
    }
}
