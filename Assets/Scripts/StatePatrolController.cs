using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;

public class StatePatrolController : MonoBehaviour
{
    [SerializeField]
    Transform[] points;

    [SerializeField]
    float speed;

    [SerializeField]
    bool isFacingRight;

    int _index;
    Transform _nextPoint;


    Transform _target;

    SpriteRenderer _renderer;

    void Start()
    {
        SetNextTarget(0);
    }
    void Update()
    {

        if (transform.position == _target.position)
        {
            _index++;

            if (_index >= points.Length)
            {
                _index = 0;
            }

            SetNextTarget(_index);
        }

        transform.position =
            Vector2.MoveTowards(transform.position, _target.position, speed * Time.deltaTime);
    }

    void SetNextTarget(int index)
    {
        _index = index;
        _target = points[_index];

        if (_target.position.x > transform.position.x && !isFacingRight)
        {
            isFacingRight = !isFacingRight;
            transform.Rotate(0.0F, 180.0F, 0, 0F);

        }
        else if (_target.position.x < transform.position.x && isFacingRight)
        {
            isFacingRight = !isFacingRight;
            transform.Rotate(0.0F, 180.0F, 0.0F);
        }
    }

}