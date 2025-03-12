using System;
using UnityEngine;

public class Enemys : MonoBehaviour
{
    //VARIABLES
    Vector3 firsPosition;
    GameObject player;
    
    public AudioClip enemySound;
    public ParticleSystem enemyParticles;
    public float velocityRotationSword = 250f;
    private bool movingToB = true;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        firsPosition = player.transform.position;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RotateObject();
    }

    void RotateObject() {
        switch (transform.gameObject.tag)
        {
            case "EnemySword":
                RotateSword();
                break;
            case "EnemyMinePosition":
                //EnemyMovement.Ene
                break;
            default:
                break;
        }
    }

    void RotateSword()
    {
        transform.Rotate(new Vector3(0, velocityRotationSword, 0) * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        ResetPosition();
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

        if (player != null) { 
            player.transform.position = firsPosition;
        }
    }
}
