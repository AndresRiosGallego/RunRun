using UnityEditor;
using UnityEngine;

public class Points : MonoBehaviour
{

    #region Variables
    [SerializeField]
    public AudioClip pointSound;
    [SerializeField]
    public ParticleSystem pointParticles; 
    #endregion


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
        transform.Rotate(new Vector3(0, 20, 0) * Time.deltaTime);
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
        
        //Destroy(gameObject);
        //gameObject.SetActive(false);

        gameObject.GetComponent<MeshRenderer>().enabled = false;
        gameObject.GetComponent<Collider>().enabled = false;
    }
}
