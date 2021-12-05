using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Vector2 direction;
    public float speed = 7.5f;
    public float projectileLifetime = 1.25f;
    public ParticleSystem explosion;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, projectileLifetime);

        //instantiate the particle system at the bullet's location before its destroyed
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<Rigidbody2D>().velocity = direction * speed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //if a bullet collides with something (not the player or a consumable), break it
        if (collision.gameObject.tag != "Item" && collision.gameObject.tag != "Enemy")
        {
            //instantiate the particle system at the bullet's location before its destroyed
            ParticleSystem temp = Instantiate(explosion);
            temp.transform.position = gameObject.transform.position;
            explosion.Play();
            Destroy(gameObject, 0.05f);
        }
    }
}
