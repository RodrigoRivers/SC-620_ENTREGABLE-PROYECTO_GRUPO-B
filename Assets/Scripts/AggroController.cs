using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AggroController : MonoBehaviour
{
    [SerializeField]
    Transform target;

    [SerializeField]
    float speed;

    [SerializeField]
    float awakeDistance;

    [SerializeField]
    float stopDistance;

    [SerializeField]
    bool isFacingRight;

    float _distance;
    bool _isChasing;

    Vector2 _position;

    void Start()
    {

        _position = transform.position;
    }

    void Update()
    {
        _distance =
            Vector2.Distance(transform.position, target.position);

        if (_distance <= awakeDistance && !_isChasing)
        {
            _isChasing = true;
        }
        else if (_distance >= stopDistance && _isChasing)
        {
            _isChasing = false;
        }

        Vector2 lookAt = Vector2.zero;

        if (_isChasing)
        {
            Vector2 position = new Vector2(target.position.x, transform.position.y);

            transform.position =
                Vector2.MoveTowards(transform.position, position, speed * Time.deltaTime);
            lookAt = position;
        }

        else if ((Vector2)transform.position != _position)
        {
            transform.position =
                Vector2.MoveTowards(transform.position, _position, speed * Time.deltaTime);
            lookAt = _position;
        }

        if (lookAt.x > transform.position.x)
        {
            if (!isFacingRight)
            {
                isFacingRight = !isFacingRight;
                transform.Rotate(0.0F, 180.0F, 0.0F);
            }
        }

        else
        {
            if (isFacingRight)
            {
                isFacingRight = !isFacingRight;
                transform.Rotate(0.0F, 180.0F, 0.0F);
            }
        }
    }

}