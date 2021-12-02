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
    float defense;
    float rangedCooldown;
    float rangedCooldownTimer;

    float maxHealth = 50.0f;
    float basePlayerSpeed = 45.0f;
    float baseRangedCooldownSpeed = 0.35f;

    float[] itemEffectTimers;

    uint currentLevel;

    // Start is called before the first frame update
    void Start()
    {
        itemEffectTimers = new float[3]; //index of the array corresponds to the int value of the enum
                                         // EX: index 2 corresponds to the timer for the modifyRangedCooldown item effect
        defense = 5.0f;
        health = maxHealth;
        rangedCooldown = baseRangedCooldownSpeed;
        rangedCooldownTimer = rangedCooldown;
        speed = basePlayerSpeed;
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

            if (rangedCooldownTimer == 0)
            {
                float offset = 0.1f;
                //shoot bullets in different directions
                if (Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    bullet = Instantiate(projectilePrefab);
                    bullet.transform.position = transform.position + new Vector3(-offset, 0, 0);
                    bullet.GetComponent<Projectile>().direction = new Vector2(-1, 0);
                    rangedCooldownTimer = rangedCooldown;
                }
                else if (Input.GetKeyDown(KeyCode.RightArrow))
                {
                    bullet = Instantiate(projectilePrefab);
                    bullet.transform.position = transform.position + new Vector3(offset, 0, 0);
                    bullet.GetComponent<Projectile>().direction = new Vector2(1, 0);
                    rangedCooldownTimer = rangedCooldown;
                }
                else if (Input.GetKeyDown(KeyCode.UpArrow))
                {
                    bullet = Instantiate(projectilePrefab);
                    bullet.transform.position = transform.position + new Vector3(0, offset, 0);
                    bullet.GetComponent<Projectile>().direction = new Vector2(0, 1);
                    rangedCooldownTimer = rangedCooldown;
                }
                else if (Input.GetKeyDown(KeyCode.DownArrow))
                {
                    bullet = Instantiate(projectilePrefab);
                    bullet.transform.position = transform.position + new Vector3(0, -offset, 0);
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
                if (rangedCooldown < 0)
                {
                    rangedCooldown = 0;
                }
                itemEffectTimers[2] = item.effectTime;
                break;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<ConsumableItem>() != null || collision.gameObject.tag == "Item") //check if it is an item
        {
            HandleItems(collision.gameObject.GetComponent<ConsumableItem>());
            Debug.Log("Player collided with an item");
        }
        if (collision.gameObject.GetComponent<Enemy>() != null || collision.gameObject.tag == "Enemy") //check if it is an enemy
        {
            TakeDamage(20.0f);
            Debug.Log("Player collided with an enemy");
        }
    }

    void TakeDamage(float damage)
    {
        damage -= (damage * (defense / 100));
        health -= damage;
        Debug.Log(health);
    }


    private void OnGUI()
    {
        // for debug purposes!!!
        GUI.color = Color.white;
        GUI.skin.box.fontSize = 18;

        GUI.Box(new Rect(0, 10, 100, 30), "Health: " + health);
        GUI.Box(new Rect(0, 40, 100, 30), "Speed: " + speed);
        GUI.Box(new Rect(0, 70, 225, 30), "Ranged Cooldown: " + rangedCooldown);


        if (itemEffectTimers[1] > 0.0f)
        {
            int minutes = Mathf.FloorToInt((itemEffectTimers[1] % 3600) / 60);
            int seconds = Mathf.FloorToInt(itemEffectTimers[1] % 60);
            string time = string.Format("{0:00}:{1:00}:{2:00}", minutes, seconds);
            GUI.Box(new Rect(100, 10, 250, 30), "Speed effect: " + time);
            minutes = Mathf.FloorToInt((itemEffectTimers[2] % 3600) / 60);
            seconds = Mathf.FloorToInt(itemEffectTimers[2] % 60);
            time = string.Format("{0:00}:{1:00}:{2:00}", minutes, seconds);
            GUI.Box(new Rect(100, 40, 350, 30), "Ranged cooldown effect: " + time);

        }

        GUI.skin.box.wordWrap = true;
    }
}
