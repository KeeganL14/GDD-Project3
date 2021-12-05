﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject projectilePrefab;
    public GameObject healthItemPrefab;
    public GameObject speedItemPrefab;
    public GameObject cooldownItemPrefab;

    public bool canShoot;
    public float shotTimer = 0.5f;
    public float chaseSpeed = 1.0f;
    public float followDistance = 6.0f;
    public Transform targetPoint;
    public float damage = 15.0f;
    public float health = 25.0f;
    public float defense = 5.0f;
    private float despawnTime;

    public float itemDropRate = 15.0f; // 15%

    private float timer;

    // Start is called before the first frame update
    void Start()
    {
        despawnTime = 0.05f;
        timer = shotTimer;
    }

    // Update is called once per frame
    void Update()
    {

        timer -= Time.deltaTime;

        //Do the more simple calculation first
        //If the distance between the target and enemy is less than follow distance,
        //check if there's something in the way, and if not, lerp towards the target
        if (Vector2.Distance(transform.position, targetPoint.position) < followDistance)
        {
            //find the vector between the target and the enemy
            Vector3 targetDirection = targetPoint.position - transform.position;

            RaycastHit2D hitObj = Physics2D.Raycast(transform.position, targetDirection);

            Debug.Log(hitObj.collider.gameObject.tag);

            if (hitObj.collider == null
                                || hitObj.collider.gameObject.tag == "Player"
                                || hitObj.collider.gameObject.tag == "Item"
                                || hitObj.collider.gameObject.tag == "Projectile"
                                || hitObj.collider.gameObject.tag == "Enemy")
            {
                // follow the player
                MoveAtConstantSpeed(targetDirection, chaseSpeed);
            }
        }

        if(canShoot && timer <= 0)
        {
            timer = shotTimer;
            ShootBullet(targetPoint.position - transform.position);
        }

    }

    void MoveAtConstantSpeed(Vector3 direction, float speed)
    {
        GetComponent<Rigidbody2D>().MovePosition(transform.position + direction.normalized * speed * Time.deltaTime);
        Debug.Log("Moving");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerProjectile>() != null || collision.gameObject.tag == "PlayerProjectile")
        {
            TakeDamage(collision.gameObject.GetComponent<PlayerProjectile>().GetDamage());

            //check if the enemy is dead
            if (health <= 0)
            {
                if (Random.Range(0, 100) < itemDropRate)
                {
                    GameObject item;
                    switch (Random.Range(1, 4))
                    {
                        case 1:
                            item = Instantiate(healthItemPrefab);
                            item.transform.position = this.transform.position;
                            break;
                        case 2:
                            item = Instantiate(speedItemPrefab);
                            item.transform.position = this.transform.position;
                            break;
                        case 3:
                            item = Instantiate(cooldownItemPrefab);
                            item.transform.position = this.transform.position;
                            break;

                    }                    
                }

                DestroySelf(despawnTime);

                //Debug.Log("Enemy collided with an player projectile!");
            }
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

    public void DestroySelf(float timeUnitlDespawn)
    {
        Destroy(gameObject, timeUnitlDespawn);
    }

    void LerpToPos(Transform a, Transform b, float speed)
    {
        transform.position = Vector2.Lerp(a.position, b.position, speed);
    }

    private void ShootBullet(Vector2 direction)
    {
        direction = direction.normalized;
        Vector3 offset = new Vector3(direction.x, direction.y, 0) / 10f;
        GameObject bullet;
        bullet = Instantiate(projectilePrefab);
        bullet.transform.position = transform.position + offset;
        bullet.GetComponent<EnemyProjectile>().direction = direction;

    }

}
