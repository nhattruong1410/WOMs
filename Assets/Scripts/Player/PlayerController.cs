using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;

    public LayerMask IOLayer;
    public LayerMask grassLayer;
    
    private bool isMoving;
    private Rigidbody2D rb;
    private Vector3 input = new Vector3(0,0,1);
    private Vector3 vec3Zero = new Vector3(0,0,1);

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        Move();
    }

    
    void Move()
    {
        if (!isMoving)
        {
            input.x = Input.GetAxis("Horizontal");
            input.y = Input.GetAxis("Vertical");

            if (input != vec3Zero)
            {
                var targetPos = transform.position;
                transform.position += input * (Time.deltaTime * moveSpeed);

                // if (IsWalkable(targetPos))
                // {
                //     StartCoroutine(Move(targetPos));
                // }
            }
        }

        CheckForEncouter();
    }

    private bool IsWalkable(Vector3 targerPos)
    {
        if (Physics2D.OverlapCircle(targerPos, 0.2f, IOLayer)!= null)
        {
            return false;
        }
        return true;
    }

    private void CheckForEncouter()
    {
        if(Physics2D.OverlapCircle(transform.position,0.2f, grassLayer))
        {
            if (Random.Range(1, 101) <= 10)
            {
                Debug.Log("Encouter a monsters");
            }
        }
    }
}

