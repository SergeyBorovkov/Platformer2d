using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private Enemy _enemy;
    [SerializeField] private float _speed;
    [SerializeField] private List<EnemyTravelPoint> _points;

    private int _currentPoint;

    private void Update()
    {
        Transform target = _points[_currentPoint].transform;

        transform.position = Vector3.MoveTowards(transform.position, target.position, _speed * Time.deltaTime);

        if (transform.position == target.position)
        {
            _currentPoint++;
            
            if (_currentPoint >= _points.Count)            
                _currentPoint = 0;            
        }
    }
}
