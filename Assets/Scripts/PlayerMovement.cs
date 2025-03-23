using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerMovement : MonoBehaviour
{
    #region Variables
    float _moveX, _moveY;
    [SerializeField]
    public float speed = 5f;
    [SerializeField]
    public float jumpForce = 7f;
    [SerializeField]
    public float sensitivity = 2f;
    //public float airControl = 0.5f;
    private Rigidbody _rb;
    [SerializeField]
    public Transform cameraTransform;
    private float _rotationY = 0f;
    private Vector3 _moveDirection;
    [SerializeField]
    public Animator animator; 
    #endregion

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        //cameraTransform = Camera.main.transform;
        //Cursor.lockState = CursorLockMode.Locked; // Oculta y bloquea el cursor al centro
    }

    void Update()
    {
        RotatePlayer();

        if (Input.GetKeyDown(KeyCode.Space)) // Saltar
        {
            Jump();
        }

        animator.SetFloat("OnSpeed", Mathf.Abs(_moveDirection.x) + Mathf.Abs(_moveDirection.z));
        animator.SetBool("Grounded", IsGrounded());
    }

    void FixedUpdate()
    {
        MovePlayer();
    }

    void MovePlayer()
    {
        float horizontal = Input.GetAxis("Horizontal"); // A y D
        float vertical = Input.GetAxis("Vertical"); // W y S

        // Tomar la direcci�n de la c�mara para mover al jugador
        Vector3 forward = cameraTransform.forward;
        Vector3 right = cameraTransform.right;
        forward.y = 0; // Mantener el movimiento en plano
        right.y = 0;

        _moveDirection = (forward * vertical + right * horizontal).normalized;

        // Aplicar movimiento con Rigidbody
        _rb.MovePosition(_rb.position + _moveDirection * speed * Time.fixedDeltaTime);
    }

    //void OnMove(InputValue moveValue)
    //{
    //    Vector2 move = moveValue.Get<Vector2>();
    //    moveX = move.x;
    //    moveY = move.y;
    //}

    //void MovePlayer()
    //{
    //    Vector3 direction = cameraTransform.forward * moveX + cameraTransform.right * moveY;
    //    direction.y = 0f; // Evita que el personaje se incline

    //    if (IsGrounded())
    //        rb.AddForce(direction * speed);
    //    else
    //        rb.AddForce(direction * speed * airControl); //Reduce a un porcentaje del airControl cuando esta en el aire

    //    //Reduce el deslizamineto de la espera para que no sea tan rapido
    //    rb.linearVelocity = new Vector3(direction.x, rb.linearVelocity.y, direction.z);

    //    //rb.linearVelocity = new Vector3(rb.linearVelocity.x * 0.98f, rb.linearVelocity.y, rb.linearVelocity.z * 0.98f);
    //}

    void RotatePlayer()
    {
        float mouseX = Input.GetAxis("Mouse X") * sensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity;

        // Rotacion del personaje en el eje Y (horizontal)
        transform.Rotate(Vector3.up * mouseX);

        // Rotacion vertical de la camara
        _rotationY -= mouseY;
        _rotationY = Mathf.Clamp(_rotationY, -90f, 90f); // Limita la rotacion vertical
        cameraTransform.localRotation = Quaternion.Euler(_rotationY, 0, 0);
    }

    void Jump()
    {
        if (IsGrounded())
        {
            _rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    bool IsGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, 1.1f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Points"))
        {
            //other.gameObject.SetActive(false);
            Points points = other.gameObject.GetComponent<Points>();
            if (points != null)
            {
                points.GetPoints();
            }
        }
    }
}
