using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : MonoBehaviour
{
    public GameObject managerObject;
    GameManager gameManager;
    PlayerMovement characterMovement;
    PlayerCombat characterCombat;

    public float basePlayerSpeed = 45.0f;
    public float baseRangedCooldownSpeed = 45.0f;
    public float maxHealth = 50.0f;
    public float health;
    uint currentLevel;

    float[] itemEffectTimers;

    // Start is called before the first frame update
    void Start()
    {
        itemEffectTimers = new float[3]; //index of the array corresponds to the int value of the enum
                                         // EX: index 2 corresponds to the timer for the modifyRangedCooldown item effect
        health = maxHealth;

        gameManager = managerObject.GetComponent<GameManager>();

        characterCombat = GetComponent<PlayerCombat>();
        characterCombat.rangedCooldown = baseRangedCooldownSpeed;

        characterMovement = GetComponent<PlayerMovement>();
        characterMovement.speed = basePlayerSpeed;
    }

    void Update()
    {
        if (health < 0.0f)
        {
<<<<<<< Updated upstream
            gameManager.isPlaying = false;
            currentLevel = 1;
            Start(); //reset values
            return;
        }

        for (int i = 1; i < itemEffectTimers.Length; i++)
        {
            if (itemEffectTimers[i] > 0.0f)
=======
            animationHandler.SetBool("isDead", true);
            gameManager.isPlaying = false;
            gameManager.ActvateGameOverMenu();
        }
        else if (gameManager.isPlaying == true)
        {
            //#region Movement
            if (!Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.RightArrow) &&
                !Input.GetKey(KeyCode.UpArrow) && !Input.GetKey(KeyCode.DownArrow))
            {
                x = Input.GetAxisRaw("Horizontal");
                y = Input.GetAxisRaw("Vertical");
            }

            if (Mathf.Abs(x) + Mathf.Abs(y) > 0.005f)
            {   //set the animator to show the walking animation
                animationHandler.SetBool("isMoving", true);
            }
            else
            {   //set the animator to show the idle animation
                animationHandler.SetBool("isMoving", false);
            }

            //flip the player based on x input
            if (x < 0)
            {
                gameObject.transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
            }
            else if (x > 0)
            {
                gameObject.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
            }

            //normalize the direction so the player doesn't move faster in the diagonal
            Vector2 direction = new Vector2(x, y).normalized;
            GetComponent<Rigidbody2D>().velocity = direction * speed;
            //#endregion

            //#region Ranged Combat
            if (rangedCooldownTimer > 0)
            {
                rangedCooldownTimer -= Time.deltaTime;
            }
            else if (rangedCooldownTimer < 0)
            {
                rangedCooldownTimer = 0;
            }

            if (rangedCooldownTimer == 0)
>>>>>>> Stashed changes
            {
                itemEffectTimers[i] -= Time.deltaTime;
            }
<<<<<<< Updated upstream
            else // the effect has worn off, reset values to base
=======
            //#endregion

            //#region Item Effects
            for (int i = 1; i < itemEffectTimers.Length; i++)
>>>>>>> Stashed changes
            {
                switch (i)
                {
                    case 1: //modifySpeed
                        characterMovement.speed = basePlayerSpeed;
                        break;
                    case 2: //modifyRangedCooldown
                        characterCombat.rangedCooldown = baseRangedCooldownSpeed;
                        break;
                }
            }
<<<<<<< Updated upstream
=======
            //#endregion
>>>>>>> Stashed changes
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
                characterMovement.speed += item.valueModifier;
                itemEffectTimers[1] = item.effectTime;
                break;

            case ItemEffect.modifyRangedCooldown:
                characterCombat.rangedCooldown += item.valueModifier;
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
<<<<<<< Updated upstream

        }
        return;
=======
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

<<<<<<< HEAD
        
        damageSound.Play();

=======
        // Needs to be fixed in unity
        //damageSound.Play();
>>>>>>> f77ae2c7b90dffb3b205faeb53cb896c27e1bed0
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
>>>>>>> Stashed changes
    }
}
