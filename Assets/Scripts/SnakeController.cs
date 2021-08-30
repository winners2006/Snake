using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeController : MonoBehaviour
{
    Vector3 startGamePosition;
    Quaternion StartGameRotation;

    Vector3 targetVelosity;

    public float lineOffset = 2.5f;
    public float lineChangeSpeed = 15;
    public float pointStart;
    public float pointFinish;

    Rigidbody snake;

    void Start()
    {
        snake = GetComponent<Rigidbody>();
        startGamePosition = transform.position;
        StartGameRotation = transform.rotation;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.A) && pointFinish > -lineOffset)
        {
            pointStart = pointFinish;
            pointFinish -= lineOffset;
            targetVelosity = new Vector3(-lineChangeSpeed, 0, 0);

        }
        if (Input.GetKeyDown(KeyCode.D) && pointFinish < lineOffset)
        {
            pointStart = pointFinish;
            pointFinish += lineOffset;
            targetVelosity = new Vector3(lineChangeSpeed, 0, 0);
        }
        float x = Mathf.Clamp(transform.position.x, Mathf.Min(pointStart, pointFinish), Mathf.Max(pointStart, pointFinish));
        transform.position = new Vector3(x, transform.position.y, transform.position.z);
    }

    private void FixedUpdate()
    {
        snake.velocity = targetVelosity;
        if((transform.position.x > pointFinish && targetVelosity.x >0) ||
            (transform.position.x < pointFinish && targetVelosity.x < 0))
        {
            targetVelosity = Vector3.zero;
            snake.velocity = targetVelosity;
            snake.position = new Vector3(pointFinish, snake.position.y, snake.position.z);
        }
    }

    void MoveHorizontel(float speed)
    {

    }
}
