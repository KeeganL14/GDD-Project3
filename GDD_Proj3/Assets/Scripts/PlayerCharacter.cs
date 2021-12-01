using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : MonoBehaviour
{
    public GameObject projectilePrefab;
    public GameObject managerObject;

    GameManager gameManager;
    
    float x = 0;
    float y = 0;
    float speed;
    float health;
    float maxHealth = 50.0f;
    float rangedCooldownTimer;
    float rangedCooldown = 0.25f;
    float basePlayerSpeed = 45.0f;
    float baseRangedCooldownSpeed = 45.0f;

    float[] itemEffectTimers;

    uint currentLevel;

    // Start is called before the first frame update
    void Start()
    {
        itemEffectTimers = new float[3]; //index of the array corresponds to the int value of the enum
                                         // EX: index 2 corresponds to the timer for the modifyRangedCooldown item effect
        health = maxHealth;
        rangedCooldownTimer = rangedCooldown;
        gameManager = managerObject.GetComponent<GameManager>();
    }

    void Update()
    {
        GameObject bullet;

        if (health < 0.0f)
        {
            gameManager.isPlaying = false;
            currentLevel = 1;
            Start(); //reset values
            return;
        }
        else //if (gameManager.isPlaying == true)
        {
            #region Movement
            if (!Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.RightArrow) &&
                !Input.GetKey(KeyCode.UpArrow) && !Input.GetKey(KeyCode.DownArrow))
            {
                x = Input.GetAxisRaw("Horizontal");
                y = Input.GetAxisRaw("Vertical");
            }
            //normalize the direction so the player doesn't move faster in the diagonal
            Vector2 direction = new Vector2(x, y).normalized;
            GetComponent<Rigidbody2D>().velocity = direction * speed;
            #endregion

            #region Ranged Combat
            if (rangedCooldownTimer > 0)
            {
                rangedCooldownTimer -= Time.deltaTime;
            }
            else if (rangedCooldownTimer < 0)
            {
                rangedCooldownTimer = 0;
            }
            else if (rangedCooldownTimer == 0)
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
                //TODO: Implement
            }
            #endregion

            #region Item Effects
            for (int i = 1; i < itemEffectTimers.Length; i++)
            {
                if (itemEffectTimers[i] > 0.0f)
                {
                    itemEffectTimers[i] -= Time.deltaTime;
                }
                else // the effect has worn off, reset values to base
                {
                    switch (i)
                    {
                        case 1: //modifySpeed
                            speed = basePlayerSpeed;
                            break;
                        case 2: //modifyRangedCooldown
                            rangedCooldown = baseRangedCooldownSpeed;
                            break;
                    }
                }
            }
            #endregion
        }
    }

    private void HandleItems(ConsumableItem item)
    {
        switch (item.itemType)
        {
            case ItemEffect.restoreHealth:
                health += item.valueModifier;
                if (health > maxHealth)
                {
                    health = maxHealth;
                }
                break;

            case ItemEffect.modifySpeed:
                speed += item.valueModifier;
                itemEffectTimers[1] = item.effectTime;
                break;

            case ItemEffect.modifyRangedCooldown:
                rangedCooldown += item.valueModifier;
                itemEffectTimers[2] = item.effectTime;
                break;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<ConsumableItem>() != null) //check if it is an item
        {
            HandleItems(collision.gameObject.GetComponent<ConsumableItem>());
        }
        else if (collision.gameObject.GetComponent<Enemy>() != null) //check if it is an enemy
        {

        }
        return;
    }
}
