using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public float chaseSpeed;
    public float followDistance;
    public Transform targetPoint;

    protected float health = 50.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //do the more simple calculation first; If the distance between the target and enemy is less than follow distance,
        //check if theres something in the way, and if not, lerp towards the target
        if(Vector2.Distance(transform.position, targetPoint.position) < followDistance)
        {

            //find the vector between the target and the enemy
            Vector3 targetDirection = targetPoint.position - transform.position;

            RaycastHit2D hitObj = Physics2D.Raycast(transform.position, targetDirection);

            if (hitObj.collider != null)
            {
                //do nothing
            }
            if(hitObj.collider == null || hitObj.collider.gameObject.tag == "Projectile")
            {
                MoveAtConstantSpeed(targetDirection, chaseSpeed);
            }
        }
    }

    void MoveAtConstantSpeed(Vector3 direction, float speed)
    {
        transform.position += (direction.normalized * speed * Time.deltaTime);
    }

    void LerpToPos(Transform a, Transform b, float speed)
    {
        transform.position = Vector2.Lerp(a.position, b.position, speed);
    }

}
