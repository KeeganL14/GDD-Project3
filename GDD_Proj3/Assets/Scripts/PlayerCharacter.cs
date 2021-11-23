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
            gameManager.isPlaying = false;
            currentLevel = 1;
            Start(); //reset values
            return;
        }

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
                        characterMovement.speed = basePlayerSpeed;
                        break;
                    case 2: //modifyRangedCooldown
                        characterCombat.rangedCooldown = baseRangedCooldownSpeed;
                        break;
                }
            }
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

        }
        return;
    }
}
