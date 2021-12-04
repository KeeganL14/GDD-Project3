using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject projectilePrefab;

    public float chaseSpeed;
    public float followDistance;
    public Transform targetPoint;
    public float damage = 15.0f;
    public float health = 25.0f;
    public float defense = 5.0f;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //Do the more simple calculation first
        //If the distance between the target and enemy is less than follow distance,
        //check if there's something in the way, and if not, lerp towards the target
        if (Vector2.Distance(transform.position, targetPoint.position) < followDistance)
        {
            //find the vector between the target and the enemy
            Vector3 targetDirection = targetPoint.position - transform.position;

            RaycastHit2D hitObj = Physics2D.Raycast(transform.position, targetDirection);

            if (hitObj.collider != null)
            {
                //do nothing
            }
            if (hitObj.collider == null || hitObj.collider.gameObject.tag == "Player")
            {
                // follow the player
                MoveAtConstantSpeed(targetDirection, chaseSpeed);
            }
        }

        if (health <= 0.0f)
        {
            Destroy(gameObject);
        }
    }

    void MoveAtConstantSpeed(Vector3 direction, float speed)
    {
        transform.position += (direction.normalized * speed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerProjectile>() != null || collision.gameObject.tag == "PlayerProjectile") //check if it is an enemy
        {
            TakeDamage(20.0f);
            //Debug.Log("Enemy collided with an player projectile!");
        }
    }

    void TakeDamage(float damage)
    {
        damage -= (damage * (defense / 100));
        health -= damage;
    }

    public float GetDamage()
    {
        return damage;
    }

    void LerpToPos(Transform a, Transform b, float speed)
    {
        transform.position = Vector2.Lerp(a.position, b.position, speed);
    }

}
