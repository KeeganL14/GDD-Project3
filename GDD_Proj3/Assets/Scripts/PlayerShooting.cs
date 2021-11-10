using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{

    public GameObject projectilePrefab;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        GameObject temp;

        //shoot bullets in different directions
        if(Input.GetKeyDown(KeyCode.LeftArrow))
        {
            temp = Instantiate(projectilePrefab);
            temp.transform.position = transform.position;
            temp.GetComponent<Projectile>().direction = new Vector2(-1, 0);
        }

        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            temp = Instantiate(projectilePrefab);
            temp.transform.position = transform.position;
            temp.GetComponent<Projectile>().direction = new Vector2(1, 0);
        }

        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            temp = Instantiate(projectilePrefab);
            temp.transform.position = transform.position;
            temp.GetComponent<Projectile>().direction = new Vector2(0, 1);
        }

        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            temp = Instantiate(projectilePrefab);
            temp.transform.position = transform.position;
            temp.GetComponent<Projectile>().direction = new Vector2(0, -1);
        }

    }
}
