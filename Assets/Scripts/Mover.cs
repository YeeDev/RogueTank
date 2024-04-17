using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yee.Math;

public class Mover : MonoBehaviour
{
    [Range(1, 10)] [SerializeField] float speed;
    [Range(1, 10)] [SerializeField] float rotationSpeed;

    public IEnumerator Move(Vector3 addedPoint)
    {
        Vector3 newPosition = transform.position + addedPoint;
        Vector3 direction = newPosition - transform.position;

        Quaternion lookAt = Quaternion.LookRotation(direction, transform.up);

        float angle = MathY.CalculateFlatAngle(transform.forward, direction);
        if (angle > 1)
        {
            float time = 0;
            float timeToAdd = rotationSpeed * Time.fixedDeltaTime;
            while (time < 1f)
            {
                time += angle > 91 ? timeToAdd * 0.5f : timeToAdd;
                transform.rotation = Quaternion.Lerp(transform.rotation, lookAt, time);
                yield return new WaitForFixedUpdate();
            }
            transform.rotation = lookAt;
        }

        while (Vector3.SqrMagnitude(transform.position - newPosition) > 0.01f)
        {
            transform.Translate(direction * speed * Time.fixedDeltaTime, Space.World);
            yield return new WaitForFixedUpdate();
        }

        transform.position = newPosition;
    }
}
