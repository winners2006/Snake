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

    bool isMoved = false;
    Coroutine movingCorountine;

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
            MoveHorizontel(-lineChangeSpeed);
        }
        if (Input.GetKeyDown(KeyCode.D) && pointFinish < lineOffset)
        {
            MoveHorizontel(lineChangeSpeed);
        }
        
    }

    void MoveHorizontel(float speed)
    {
        pointStart = pointFinish;
        pointFinish += Mathf.Sign(speed) * lineOffset;

        if (isMoved ) { StopCoroutine(movingCorountine); isMoved = false; }
        movingCorountine = StartCoroutine(MoveCoroutine(speed));

        targetVelosity = new Vector3(-lineChangeSpeed, 0, 0);
    }

    IEnumerator MoveCoroutine(float vectorX)
    {
        isMoved = true;
        while (Mathf.Abs(pointStart - transform.position.x) < lineOffset)
        {
            yield return new WaitForFixedUpdate();

            snake.velocity = new Vector3(vectorX, snake.velocity.y, 0);
            float x = Mathf.Clamp(transform.position.x, Mathf.Min(pointStart, pointFinish), Mathf.Max(pointStart, pointFinish));
            transform.position = new Vector3(x, transform.position.y, transform.position.z);
        }
        snake.velocity = Vector3.zero;
        transform.position = new Vector3(pointFinish, transform.position.y, transform.position.z);
        isMoved = false;
    }
}
