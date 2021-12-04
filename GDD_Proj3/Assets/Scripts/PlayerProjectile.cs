using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectile : MonoBehaviour
{
    public Vector2 direction;
    public float speed = 7.5f;
    public float projectileLifetime = 1.25f;
    public float damage;

    public ParticleSystem explosion;
    AudioSource projectileSound;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, projectileLifetime);

        //instantiate the particle system at the bullet's location before its destroyed

        // Play shoot sound
        projectileSound = GetComponent<AudioSource>();
        if (projectileSound == null)
        {
            Debug.Log("The AudioSource is NULL!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<Rigidbody2D>().velocity = direction * speed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //if a bullet collides with something (not the player), break it
        if (collision.gameObject.tag != "Player" && collision.gameObject.tag != "Item")
        {
            //instantiate the particle system at the bullet's location before its destroyed
            ParticleSystem temp = Instantiate(explosion);
            temp.transform.position = gameObject.transform.position;
            explosion.Play();
            Destroy(gameObject, 0.05f);
        }
    }

    public float GetDamage()
    {
        return damage;
    }
}