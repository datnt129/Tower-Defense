using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float speed;
    SpriteRenderer spriteRenderer;
    Rigidbody2D rb2D;
    Animator animator;
    float horizontal, vertical;
    float currentSpeed; 
    
    // Start is called before the first frame update
    void Start()
    {
        // Make the game run as fast as possible
        Application.targetFrameRate = -1;
        // Limit the framerate to 60
        Application.targetFrameRate = 60;

        spriteRenderer = GetComponent<SpriteRenderer>();
        rb2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            animator.SetTrigger("Attack");
        }

        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        
        if(!Mathf.Approximately(horizontal, 0.0f))
        {
            if(horizontal > 0)
            {
                spriteRenderer.flipX = false;
            } else if (horizontal < 0)
            {
                spriteRenderer.flipX = true;
            } 
        }
        
        currentSpeed = new Vector2(horizontal, vertical).magnitude;
        animator.SetFloat("Speed", currentSpeed );
    }
    
    void FixedUpdate()
    {
        Vector2 position = rb2D.position;
        position.x = position.x + speed * horizontal * Time.deltaTime;
        position.y = position.y + speed * vertical * Time.deltaTime;

        rb2D.MovePosition(position);
    }
}
