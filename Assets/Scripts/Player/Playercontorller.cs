using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playercontorller : MonoBehaviour
{
    private Rigidbody2D rb;
    public float speed; //
    public float jumpForce; //

    // Start is called before the first frame update
    //start
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    //update
    void Update()
    {
        
    }

    private void FixedUpdate()  //
    {
        Movement();
    }


    void Movement()
    {
        //float horizontalInput = Input.GetAxis("Horizontal"); //
        float horizontalInput = Input.GetAxisRaw("Horizontal"); //
        rb.velocity = new Vector2(horizontalInput * speed, rb.velocity.y);

        //
        if (horizontalInput != 0) {
            transform.localScale = new Vector3(horizontalInput, 1, 1);
        }
    }
}
