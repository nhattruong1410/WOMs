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
    private Vector2 input;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (!isMoving)
        {
            input.x = Input.GetAxis("Horizontal");
            input.y = Input.GetAxis("Vertical");

            if (input.x != 0 ) input.y = 0;

            if (input != Vector2.zero)
            {
                var targetPos = transform.position;
                targetPos.x += input.x;
                targetPos.y += input.y;

                if (IsWalkable(targetPos))
                {
                    StartCoroutine(Move(targetPos));
                }
            }
        }
    }

    IEnumerator Move(Vector3 targetPos)
    {
        isMoving = true;
        while ((targetPos - transform.position).sqrMagnitude > moveSpeed)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);
            yield return null;
        }
        transform.position = targetPos;

        isMoving = false;

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
        if(Physics2D.OverlapCircle(transform.position,0.2f, grassLayer)!= null)
        {
            if (Random.Range(1, 101) <= 10)
            {
                Debug.Log("Encouter a monsters");
            }
        }
    }
}

