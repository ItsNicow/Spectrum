using System;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    Vector3 endPosition;
    float speed;

    Action landAction;

    bool initialized;

    void Update()
    {
        if (initialized)
        {
            float step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, endPosition, step);

            if (transform.position.x == endPosition.x && transform.position.y == endPosition.y)
            {
                Land();
            }
        }
    }

    public void Init(Vector3 startPosition, Vector3 endPosition, float speed, Action landAction)
    {
        this.endPosition = endPosition;
        this.speed = speed;
        this.landAction = landAction;

        transform.position = startPosition;
        transform.up = transform.position - endPosition;

        initialized = true;
    }

    void Land()
    {
        landAction.Invoke();
        Destroy(gameObject);
    }
}
