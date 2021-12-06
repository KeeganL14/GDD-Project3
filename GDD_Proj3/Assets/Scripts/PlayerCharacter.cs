using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : MonoBehaviour
{
    public GameObject projectilePrefab;
    public GameObject managerObject;
    GameManager gameManager;

    public Vector3 startPosition;

    float x = 0;
    float y = 0;
    float speed;
    float health;
    float defense;
    float damage;
    float rangedCooldown;
    float rangedCooldownTimer;

    float maxHealth = 100.0f;
    float baseDamage = 20.0f;
    float baseDefense = 30.0f;
    float basePlayerSpeed = 70.0f;
    float baseRangedCooldownSpeed = 0.45f;

    float[] itemEffectTimers;

    uint currentLevel;

    public AudioSource shootSound;

    // Start is called before the first frame update
    void Start()
    {
        itemEffectTimers = new float[5]; //index of the array corresponds to the int value of the enum
                                         // EX: index 2 corresponds to the timer for the modifyRangedCooldown item effect

        health = maxHealth;
        damage = baseDamage;
        defense = baseDefense;
        speed = basePlayerSpeed;
        rangedCooldown = baseRangedCooldownSpeed;
        rangedCooldownTimer = rangedCooldown;

        gameManager = managerObject.GetComponent<GameManager>();
        GetComponent<Transform>().position = startPosition;
    }

    void Update()
    {
        if (health < 0.0f)
        {
            //set the animator to show the death animation
            //this doesnt work because the character game object is being disabled somewhere in the code
            //GetComponent<Animator>().SetBool("isDead", true);

            gameManager.ActivateGameOverMenu();
        }
        else if (gameManager.isPlaying == true)
        {
            #region Movement
            if (!Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.RightArrow) &&
                !Input.GetKey(KeyCode.UpArrow) && !Input.GetKey(KeyCode.DownArrow))
            {
                x = Input.GetAxisRaw("Horizontal");
                y = Input.GetAxisRaw("Vertical");
            }

            if (Mathf.Abs(x) + Mathf.Abs(y) > 0.005f)
            {
                //set the animator to show the walking animation
                GetComponent<Animator>().SetBool("isMoving", true);
            }
            else
            {
                //set the animator to show the idle animation
                GetComponent<Animator>().SetBool("isMoving", false);
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
                //shoot bullets in different directions
                if (Input.GetKey(KeyCode.LeftArrow))
                {
                    ShootBullet(new Vector2(-1, 0));
                }
                else if (Input.GetKey(KeyCode.RightArrow))
                {
                    ShootBullet(new Vector2(1, 0));
                }
                else if (Input.GetKey(KeyCode.UpArrow))
                {
                    ShootBullet(new Vector2(0, 1));
                }
                else if (Input.GetKey(KeyCode.DownArrow))
                {
                    ShootBullet(new Vector2(0, -1));
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
                        case 3: //modifyDefense
                            defense = baseDefense;
                            break;
                        case 4: //modifyDamage
                            damage = baseDamage;
                            break;
                    }
                }
            }
            #endregion
        }
    }

    private void ShootBullet(Vector2 direction)
    {
        Vector3 offset = new Vector3(direction.x, direction.y, 0) / 10f;
        GameObject bullet;
        bullet = Instantiate(projectilePrefab);
        bullet.transform.position = transform.position + offset;
        bullet.GetComponent<PlayerProjectile>().direction = direction;
        bullet.GetComponent<PlayerProjectile>().damage = this.damage;
        rangedCooldownTimer = rangedCooldown;

        //play audio
        shootSound.Play();
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
                if (speed < 1)
                {
                    speed = 1;
                }
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

            case ItemEffect.modifyDefense:
                defense += item.valueModifier;
                if (defense < 0)
                {
                    defense = 0;
                }
                itemEffectTimers[3] = item.effectTime;
                break;

            case ItemEffect.ModifyDamage:
                damage *= item.valueModifier;
                itemEffectTimers[4] = item.effectTime;
                break;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<ConsumableItem>() != null || collision.gameObject.tag == "Item") //check if it is an item
        {
            HandleItems(collision.gameObject.GetComponent<ConsumableItem>());
            //Debug.Log("Player collided with an item");
        }
        if (collision.gameObject.GetComponent<Enemy>() != null || collision.gameObject.tag == "Enemy") //check if it is an enemy
        {
            TakeDamage(collision.gameObject.GetComponent<Enemy>().GetDamage());
            // Debug.Log("Player collided with an enemy");
        }
        if (collision.gameObject.GetComponent<EnemyProjectile>() != null || collision.gameObject.tag == "EnemyProjectile") //check if it is an enemy projectile
        {
            TakeDamage(collision.gameObject.GetComponent<EnemyProjectile>().GetDamage());
            //Debug.Log("Player collided with an enemy projectile");
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

    private void OnGUI()
    {
        // !!!for debug purposes!!!
        GUI.color = Color.white;
        GUI.skin.box.fontSize = 25;

        GUI.Box(new Rect(0, 0, 300, 50), "Health: " + health);
        GUI.Box(new Rect(0, 50, 300, 50), "Speed: " + speed);
        GUI.Box(new Rect(0, 100, 300, 50), "Shoot Speed: " + rangedCooldown);

        GUI.skin.box.wordWrap = true;
    }
}
