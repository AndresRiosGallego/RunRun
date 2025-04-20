using System;
using UnityEngine;

public class Enemys : MonoBehaviour
{
    #region Variables
    Vector3 _firsPosition;
    GameObject _player;
    [SerializeField]
    public AudioClip enemySound;
    [SerializeField]
    public ParticleSystem enemyParticles;
    [SerializeField]
    public float velocityRotationSword = 250f;
    [SerializeField]
    public float velocityRotationLogRolling = 250f;
    [SerializeField]
    public float velMoveLogRolling = 2f;
    [SerializeField]
    public float velocityTransactionLogRolling = 2f;
    [SerializeField]
    public float velBalanceHammer = 2f;
    [SerializeField]
    public float maxAngleBalanceHammer = 20f;    
    private int _directionLogRolling = 1;
    [SerializeField]
    public float velocityRotationBalde = 250f;
    [SerializeField]
    public float impulsoSpringY = 5f; // Controla la fuerza del salto del player
    
    [SerializeField]
    public float escalaMaxima = 1f;
    [SerializeField]
    public float velocidadEscala = 5f;

    private Vector3 _escalaInicial;
    private Vector3 _escalaEstirada;

    #endregion

    private void Awake()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _firsPosition = _player.transform.position;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _escalaInicial = transform.localScale;
        _escalaEstirada = new Vector3(_escalaInicial.x, escalaMaxima, _escalaInicial.z);
    }

    // Update is called once per frame
    void Update()
    {
        RotateAndMoveObject();
    }

    void RotateAndMoveObject() {
        switch (transform.gameObject.tag)
        {
            case "EnemySword":
                RotateSword();
                break;
            case "EnemyMinePosition":
                EnemyMovePositionAB();                
                break;
            case "EnemyHammer":
                EnemyBalanceHammer();
                break;
            case "EnemyWallBadMove":
                EnemyMovePositionAB();                
                break;
            case "EnemyLogRolling":
                EnemyLogRolling();
                break;
            case "EnemyBlade":
                EnemyBlade();
                break;
            case "EnemyMineBigMove":
                EnemyMineBigMove();
                break;            
            default:
                break;
        }
    }


    private void EnemyMineBigMove()
    {
        EnemyMovePositionAB();
    }

    private void EnemyBlade()
    {
        EnemyMovePositionAB();
        transform.Rotate(new Vector3(velocityRotationBalde, 0, 0) * Time.deltaTime);
    }

    private void EnemyLogRolling()
    {
        EnemyMovePositionAB();
        transform.Rotate(new Vector3(0, 0, velocityRotationLogRolling * _directionLogRolling) * Time.deltaTime);
    }

    private void EnemyBalanceHammer()
    {
        float angle = maxAngleBalanceHammer * Mathf.Sin(Time.time * velBalanceHammer); // Oscila entre -20 y 20
        transform.localRotation = Quaternion.Euler(0, 0, angle);
    }

    private void EnemyMovePositionAB()
    {
        EnemyMovement enemyMovement = transform.gameObject.GetComponent<EnemyMovement>();
        enemyMovement.EnemyMoveSpotASpotB();
        if (enemyMovement.MovingToB)
            _directionLogRolling = 1;
        else
            _directionLogRolling = -1;

    }

    void RotateSword()
    {
        transform.Rotate(new Vector3(0, velocityRotationSword, 0) * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other != null) {
            if (other.gameObject.CompareTag("Player"))
            {
                if (this.gameObject.CompareTag("EnemySpring"))
                    EnemySpring(other);
                else
                    ResetPosition();
            }
        }
    }

    private void EnemySpring(Collider other)
    {
        ActiavacionEfectosEnemy();
        // Impulsar jugador
        Rigidbody rb = other.GetComponent<Rigidbody>();
        if (rb != null)
        {
            // Estirar resorte
            StopAllCoroutines();
            StartCoroutine(EstirarResorte());

            rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z); // resetear Y
            rb.AddForce(Vector3.up * impulsoSpringY, ForceMode.VelocityChange); // aplicar impulso
            
            //transform.localScale = new Vector3(transform.localScale.x, 1, transform.localScale.z);
            //transform.localScale = new Vector3(transform.localScale.x, 0, transform.localScale.z);
        }
    }    

    System.Collections.IEnumerator EstirarResorte()
    {
        float t = 0;
        while (t < 1)
        {
            t += Time.deltaTime * velocidadEscala;
            transform.localScale = Vector3.Lerp(_escalaInicial, _escalaEstirada, t);
            yield return null;
        }

        // Volver a escala original
        t = 0;
        while (t < 1)
        {
            t += Time.deltaTime * velocidadEscala;
            transform.localScale = Vector3.Lerp(_escalaEstirada, _escalaInicial, t);
            yield return null;
        }
    }

    private void ResetPosition()
    {
        ActiavacionEfectosEnemy();
        if (_player != null) { 
            _player.transform.position = new Vector3(0f, 2.74f, -4.33f);
            //_player.transform.position = _firsPosition;
        }
    }

    private void ActiavacionEfectosEnemy()
    {
        //Instanciar Sonido
        if (enemySound != null)
        {
            GameObject audioObject = new GameObject("PointEffectSound");
            AudioSource audioSource = audioObject.AddComponent<AudioSource>();
            audioSource.clip = enemySound;
            audioSource.Play();
            Destroy(audioObject, enemySound.length);
        }

        //Instanciar Particulas
        if (enemyParticles != null)
        {
            ParticleSystem particleSystem = Instantiate(enemyParticles, transform.position, Quaternion.identity);
            particleSystem.Play();
            Destroy(particleSystem.gameObject, particleSystem.main.duration);
        }

    }
}
