using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    //VARIABLES
    Rigidbody rb;
    public float velocity = 10;
    public float jumpForce;
    public int maxJump = 2;
    public int countJump = 0;
    public bool isGrounded;
    public float airControl = 0.5f;

    float moveX, moveY;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {        
    }

    private void FixedUpdate()
    {
        Vector3 movimiento = new Vector3(moveX, 0, moveY);
        if (isGrounded)
            rb.AddForce(movimiento * velocity);
        else
            rb.AddForce(movimiento * velocity * airControl); //Reduce a un porcentaje del airControl cuando esta en el aire

        //Reduce el deslizamineto de la espera para que no sea tan rapido
        rb.linearVelocity = new Vector3(rb.linearVelocity.x * 0.98f, rb.linearVelocity.y, rb.linearVelocity.z * 0.98f);
    }

    void OnMove(InputValue moveValue)
    {
        Vector2 move = moveValue.Get<Vector2>();
        moveX = move.x;
        moveY = move.y;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Points"))
        {
            //other.gameObject.SetActive(false);
            Points points = other.gameObject.GetComponent<Points>();
            if (points != null) {
                points.GetPoints();
            }
        }
    }

    private void OnCollisionEnter(Collision collision) 
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            countJump = 0;
            isGrounded = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }

    void OnJump(InputValue jumpValue)
    {
        if (countJump < maxJump) { 
            rb.linearVelocity = new Vector3 (rb.linearVelocity.x, 0, rb.linearVelocity.z);
            rb.AddForce(Vector3.up * jumpForce, ForceMode.VelocityChange);
            countJump++;
        }
    }
}
