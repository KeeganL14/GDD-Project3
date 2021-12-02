using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemEffect
{
    restoreHealth, //0
    modifySpeed, //1
    modifyRangedCooldown //2
}
public class ConsumableItem : MonoBehaviour
{
    public ItemEffect itemType = 0;
    public float valueModifier; // the value to add/subtract/multiply/divide by
    public float effectTime; // how long the effect lasts

    
    private void Start()
    {        
    }
    private void Update()
    {        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerCharacter>() != null || collision.gameObject.tag == "Player") //check if it is an item
        {
            Debug.Log("Item collided with an player");
            Destroy(gameObject, 0.05f);
        }
    }
}
