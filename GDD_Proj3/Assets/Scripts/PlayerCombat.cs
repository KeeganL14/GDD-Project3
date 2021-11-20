using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public GameObject projectilePrefab;
    public float rangedCooldown = 0.25f;
    float rangedCooldownTimer;

    void Start()
    {
        rangedCooldownTimer = rangedCooldown;
    }

    void Update()
    {
        #region Ranged Combat
        GameObject bullet;
        if (rangedCooldownTimer > 0)
        {
            rangedCooldownTimer -= Time.deltaTime;
        }
        else if (rangedCooldownTimer < 0)
        {
            rangedCooldownTimer = 0;
        }
        else //if (timer == 0)
        {
            //shoot bullets in different directions
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                bullet = Instantiate(projectilePrefab);
                bullet.transform.position = transform.position;
                bullet.GetComponent<Projectile>().direction = new Vector2(-1, 0);
                rangedCooldownTimer = rangedCooldown;
            }

            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                bullet = Instantiate(projectilePrefab);
                bullet.transform.position = transform.position;
                bullet.GetComponent<Projectile>().direction = new Vector2(1, 0);
                rangedCooldownTimer = rangedCooldown;
            }

            else if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                bullet = Instantiate(projectilePrefab);
                bullet.transform.position = transform.position;
                bullet.GetComponent<Projectile>().direction = new Vector2(0, 1);
                rangedCooldownTimer = rangedCooldown;
            }

            else if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                bullet = Instantiate(projectilePrefab);
                bullet.transform.position = transform.position;
                bullet.GetComponent<Projectile>().direction = new Vector2(0, -1);
                rangedCooldownTimer = rangedCooldown;
            }
        }
        #endregion

        #region Melee Combat
        if (Input.GetKeyDown(KeyCode.Space))
        {

        }
        #endregion

    }
}
