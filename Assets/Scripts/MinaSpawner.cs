using UnityEngine;

public class MinaSpawner : MonoBehaviour
{
    public GameObject minaPrefab;
    public float tiempoEntreSpawns = 5f;
    public float fuerzaHorizontal = 5f;
    public float fuerzaVertical = 10f;
    public Transform[] puntosDeSpawn; // puntos en las 4 paredes
    public Transform jugador; // ¡el jugador en movimiento!

    private void Start()
    {
        InvokeRepeating(nameof(SpawnMina), 2f, tiempoEntreSpawns);
    }

    void SpawnMina()
    {
        // Elegimos un punto aleatorio en los bordes
        Transform puntoSpawn = puntosDeSpawn[Random.Range(0, puntosDeSpawn.Length)];

        GameObject mina = Instantiate(minaPrefab, puntoSpawn.position, Quaternion.identity);
        Rigidbody rb = mina.GetComponent<Rigidbody>();

        if (rb != null && jugador != null)
        {
            // Direccionamos hacia la posición actual del jugador
            Vector3 direccion = (jugador.position - puntoSpawn.position).normalized;

            // Fuerza con efecto de parábola (horizontal + impulso hacia arriba)
            Vector3 fuerza = new Vector3(direccion.x * fuerzaHorizontal, fuerzaVertical, direccion.z * fuerzaHorizontal);
            rb.AddForce(fuerza, ForceMode.VelocityChange);
        }
    }
}
