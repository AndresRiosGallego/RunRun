using UnityEngine;

public class Points : MonoBehaviour
{
    //VARIABLES
    public AudioClip pointSound;
    public ParticleSystem pointParticles;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RotatePoint();
    }

    void RotatePoint()
    {
        transform.Rotate(new Vector3(15, 45, 30) * Time.deltaTime);
    }

    public void GetPoints()
    {
        //Instanciar Sonido
        if (pointSound != null) {
            GameObject audioObject = new GameObject("PointEffectSound");
            AudioSource audioSource = audioObject.AddComponent<AudioSource>();
            audioSource.clip = pointSound;
            audioSource.Play();
            Destroy(audioObject, pointSound.length);
        }

        //Instanciar Particulas
        if (pointParticles != null) { 
            ParticleSystem particleSystem = Instantiate(pointParticles, transform.position, Quaternion.identity);
            particleSystem.Play();
            Destroy(particleSystem.gameObject, particleSystem.main.duration);
        }

        GameManager.instance.AddPoint(1);
        Destroy(gameObject);
    }
}
