using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MountainMenu : MonoBehaviour
{
    
    [SerializeField]
    private float effectSpeed;

    private float length;

    private float startPos;

    private void Start()
    {
        // Save the initial position on x
        startPos = transform.position.x;

        // Save the length using the sprite renderer boundary on x
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    private void Update()
    {
        // Move the background to the left by the speed
        transform.Translate(Vector3.left * effectSpeed * Time.deltaTime);

        // If my position is less than the initial position minus lenght -> move background to them right
        if (transform.position.x < startPos - length)
        {
            transform.position = Vector3.right * startPos * length;
        }
    }
}
