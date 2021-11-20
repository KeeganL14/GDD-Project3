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
}
