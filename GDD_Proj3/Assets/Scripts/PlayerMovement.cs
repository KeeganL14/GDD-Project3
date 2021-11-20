using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    float x = 0;
    float y = 0;
    public float speed;

    void Start()
    {

    }

    void Update()
    {
        if (!Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.RightArrow) &&
    !Input.GetKey(KeyCode.UpArrow) && !Input.GetKey(KeyCode.DownArrow))
        {
            x = Input.GetAxisRaw("Horizontal");
            y = Input.GetAxisRaw("Vertical");
        }

        //normalize the direction so the player doesn't move faster in the diagonal
        Vector2 direction = new Vector2(x, y).normalized;

        GetComponent<Rigidbody2D>().velocity = direction * speed;
    }
}
