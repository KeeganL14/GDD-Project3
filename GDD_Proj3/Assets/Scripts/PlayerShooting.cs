using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{

    public GameObject projectilePrefab;
    public float timeBetweenShots;
    private float timer;

    // Start is called before the first frame update
    void Start()
    {
        timer = timeBetweenShots;
    }

    // Update is called once per frame
    void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }

        GameObject temp;

        if (timer == 0)
        {
            //shoot bullets in different directions
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                temp = Instantiate(projectilePrefab);
                temp.transform.position = transform.position;
                temp.GetComponent<Projectile>().direction = new Vector2(-1, 0);
                timer = timeBetweenShots;
            }

            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                temp = Instantiate(projectilePrefab);
                temp.transform.position = transform.position;
                temp.GetComponent<Projectile>().direction = new Vector2(1, 0);
                timer = timeBetweenShots;
            }

            else if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                temp = Instantiate(projectilePrefab);
                temp.transform.position = transform.position;
                temp.GetComponent<Projectile>().direction = new Vector2(0, 1);
                timer = timeBetweenShots;
            }

            else if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                temp = Instantiate(projectilePrefab);
                temp.transform.position = transform.position;
                temp.GetComponent<Projectile>().direction = new Vector2(0, -1);
                timer = timeBetweenShots;
            }
        }

        if(timer < 0)
        {
            timer = 0;
        }    

    }
}
