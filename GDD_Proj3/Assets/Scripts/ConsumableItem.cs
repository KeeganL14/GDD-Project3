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
        if (collision.gameObject.GetComponent<PlayerCharacter>() != null || collision.gameObject.tag == "Player") // check if it is an item
        {
            //Debug.Log("Item collided with an player");
            itemPickupSound.Play();
            Destroy(gameObject, 0.10f);
        }
    }
}
