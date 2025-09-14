using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class logicWindow : MonoBehaviour
{
    [Header("Points")]
    [SerializeField] Transform point1;
    [SerializeField] Transform point2;

    [Header("Settings")]
    [SerializeField] LayerMask layerMask;
    [SerializeField] float moveSpeed = 2f;     // tốc độ dịch chuyển (units/second)
    [SerializeField] float checkRadius = 1f;   // bán kính kiểm tra player/obj
    [SerializeField] AudioSource audioSource;
    bool isMoving = false;
    Vector3 targetPos;

    void Start()
    {
        targetPos = transform.position;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L) && CheckPlayer())
        {
            audioSource.Play();
            ToggleTarget();
        }

        if (transform.position != targetPos)
        {
            isMoving = true;
            transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);

        
            if (Vector3.Distance(transform.position, targetPos) <= 0.001f)
            {
                transform.position = targetPos;
                isMoving = false;
            }
        }
    }

    bool CheckPlayer()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, checkRadius, layerMask);
        return colliders != null && colliders.Length > 0;
    }

    void ToggleTarget()
    {
        Vector3 p1 = (point1 != null) ? point1.position : transform.position;
        Vector3 p2 = (point2 != null) ? point2.position : transform.position;

       
        if (Vector3.Distance(transform.position, p1) < 0.01f)
        {
            targetPos = p2;
        }
        else if (Vector3.Distance(transform.position, p2) < 0.01f)
        {
            targetPos = p1;
        }
        else
        {
           
            float distToP1 = Vector3.Distance(transform.position, p1);
            float distToP2 = Vector3.Distance(transform.position, p2);
            targetPos = (distToP1 > distToP2) ? p1 : p2;
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, checkRadius);

        if (point1 != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(point1.position, 0.05f);
            Gizmos.DrawLine(transform.position, point1.position);
        }

        if (point2 != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(point2.position, 0.05f);
            Gizmos.DrawLine(transform.position, point2.position);
        }
    }
}
