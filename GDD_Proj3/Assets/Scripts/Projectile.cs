using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    public Vector2 direction;
    public float speed;
    public float projectileLifetime;
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
        //instantiate the particle system at the bullet's location before its destryoed
        ParticleSystem temp = Instantiate(explosion);
        temp.transform.position = gameObject.transform.position;
        explosion.Play();

        //if a bullet collides with something, break it
        Destroy(gameObject, 0.05f);
    }
}
