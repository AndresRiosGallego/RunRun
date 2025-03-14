using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private Vector3 pointA;
    public GameObject pointB;
    public float speed = 2f;

    private bool _movingToB = true;

    public bool MovingToB { 
        get 
        {
            return _movingToB; 
        }
        private set 
        {
            _movingToB = value; 
        } 
    }

    private void Awake()
    {
        pointA = transform.position;
    }

    public void EnemyMoveSpotASpotB()
    {
        if (_movingToB)
        {
            transform.position = Vector3.MoveTowards(transform.position, pointB.transform.position, speed * Time.deltaTime);
            if (transform.position == pointB.transform.position) _movingToB = false;
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, pointA, speed * Time.deltaTime);
            if (transform.position == pointA) _movingToB = true;
        }
    }
}
