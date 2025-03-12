using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private Vector3 pointA;
    public GameObject pointB;
    public float speed = 2f;

    private bool movingToB = true;

    private void Awake()
    {
        pointA = transform.position;
    }

    private void Update()
    {
        EnemyMoveSpotASpotB();
    }

    public void EnemyMoveSpotASpotB()
    {
        if (movingToB)
        {
            transform.position = Vector3.MoveTowards(transform.position, pointB.transform.position, speed * Time.deltaTime);
            if (transform.position == pointB.transform.position) movingToB = false;
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, pointA, speed * Time.deltaTime);
            if (transform.position == pointA) movingToB = true;
        }
    }
}
