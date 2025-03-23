using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    #region Variables
    private Vector3 _pointA;
    [SerializeField]
    public GameObject pointB;
    [SerializeField]
    public float speed = 2f;
    private bool _movingToB = true; 
    #endregion

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
        _pointA = transform.position;
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
            transform.position = Vector3.MoveTowards(transform.position, _pointA, speed * Time.deltaTime);
            if (transform.position == _pointA) _movingToB = true;
        }
    }
}
