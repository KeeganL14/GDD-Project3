using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
<<<<<<< Updated upstream
    public float chaseSpeed;
    public float followDistance;
    public Transform targetPoint;

    protected float health = 25.0f;
=======
    public GameObject projectilePrefab;
    public GameObject healthItemPrefab;
    public GameObject speedItemPrefab;
    public GameObject cooldownItemPrefab;

    public AudioSource fireball;

    public bool canShoot = false;

    public float shootCooldown = 0.15f;
    public float chaseSpeed = 5.0f;
    public float followDistance = 6.0f;
    public float damage = 10.0f;
    public float health = 30.0f;
    public float defense = 5.0f;
    public float itemDropRate = 5.0f; // 5%

    public Transform targetPoint;

    private float despawnTime;
    private float timer;
>>>>>>> Stashed changes

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
<<<<<<< Updated upstream
        //Do the more simple calculation first
        //If the distance between the target and enemy is less than follow distance,
        //check if there's something in the way, and if not, lerp towards the target
        if (Vector2.Distance(transform.position, targetPoint.position) < followDistance)
        {
            //find the vector between the target and the enemy
            Vector3 targetDirection = targetPoint.position - transform.position;

            RaycastHit2D hitObj = Physics2D.Raycast(transform.position, targetDirection);

            if (hitObj.collider != null)
=======
        timer -= Time.deltaTime;

        if (health <= 0)
        {
            /* if (Random.Range(0, 100) < itemDropRate)
>>>>>>> Stashed changes
            {
                //do nothing
            }
<<<<<<< Updated upstream
            if (hitObj.collider == null || hitObj.collider.gameObject.tag == "Projectile")
=======
            */
            gameObject.SetActive(false);
        }
        else
        {
            //Do the more simple calculation first
            //If the distance between the target and enemy is less than follow distance,
            //check if there's something in the way, and if not, lerp towards the target
            if (Vector2.Distance(transform.position, targetPoint.position) < followDistance)
>>>>>>> Stashed changes
            {
                MoveAtConstantSpeed(targetDirection, chaseSpeed);
            }
        }
    }

    void MoveAtConstantSpeed(Vector3 direction, float speed)
    {
<<<<<<< Updated upstream
        transform.position += (direction.normalized * speed * Time.deltaTime);
=======
        GetComponent<Rigidbody2D>().MovePosition(transform.position + direction.normalized * speed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerProjectile>() != null || collision.gameObject.tag == "PlayerProjectile")
        {
            TakeDamage(collision.gameObject.GetComponent<PlayerProjectile>().GetDamage());
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
>>>>>>> Stashed changes
    }

    void LerpToPos(Transform a, Transform b, float speed)
    {
        transform.position = Vector2.Lerp(a.position, b.position, speed);
    }

<<<<<<< Updated upstream
=======
    private void ShootBullet(Vector2 direction)
    {
        direction = direction.normalized;
        Vector3 offset = new Vector3(direction.x, direction.y, 0) / 10f;
        GameObject bullet;
        bullet = Instantiate(projectilePrefab);
        bullet.transform.position = transform.position + offset;
        bullet.GetComponent<EnemyProjectile>().direction = direction;
    }
>>>>>>> Stashed changes
}
