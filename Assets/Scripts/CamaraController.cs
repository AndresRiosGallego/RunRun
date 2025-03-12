using UnityEngine;

public class CamaraController : MonoBehaviour
{
    public Transform player; // Referencia al personaje
    public float sensitivity = 2f; // Sensibilidad del mouse
    public float rotationLimit = 80f; // L�mite vertical de la c�mara

    private float rotationX = 0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; // Bloquear el cursor en el centro de la pantalla
    }

    void Update()
    {
        // Obtener el movimiento del mouse
        float mouseX = Input.GetAxis("Mouse X") * sensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity;

        // Rotar horizontalmente el jugador
        player.Rotate(Vector3.up * mouseX);

        // Rotar verticalmente la c�mara
        rotationX -= mouseY;
        rotationX = Mathf.Clamp(rotationX, -rotationLimit, rotationLimit); // Limitar la rotaci�n vertical
        transform.localRotation = Quaternion.Euler(rotationX, 0f, 0f);
    }
}
