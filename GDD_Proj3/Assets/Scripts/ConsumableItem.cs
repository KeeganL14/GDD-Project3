using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemEffect
{
    restoreHealth, //0 add/subtract
    modifySpeed, //1 add/subtract
    modifyRangedCooldown, //2 add/subtract
    modifyDefense, //3 add/subtract
    ModifyDamage //4 multiplier
}
public class ConsumableItem : MonoBehaviour
{
    public ItemEffect itemType = 0;
    public float valueModifier; // the value to add/subtract/multiply/divide by
    public float effectTime; // how long the effect lasts
    public AudioSource itemPickupSound;

    private void Start()
    {
        
        if(itemPickupSound == null)
        {
            Debug.Log("The AudioSource is NULL!");
        }
    }

    private void Update()
    {        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player") // check if it picked up by the player
        {
            //play sound 
            itemPickupSound.Play();

            //Debug.Log("Item collided with an player");
            Destroy(gameObject, 0.05f);
        }
    }
}
