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
    #endregion

    private void Awake()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _firsPosition = _player.transform.position;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
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
            if(other.gameObject.CompareTag("Player"))
                ResetPosition();
        }
    }

    private void ResetPosition()
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

        if (_player != null) { 
            _player.transform.position = _firsPosition;
        }
    }
}
