using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    private Rigidbody2D rb;
    private Vector3 velocity;
    private float horizontalInput;
    [SerializeField]
    private float runSpeed = 10f;

    [SerializeField]
    public LayerMask groundLayer;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
        horizontalInput = 1;
        //transform.forward = new Vector2(horizontalInput, 0);
        rb.velocity = new Vector2(horizontalInput * runSpeed, rb.velocity.y);
        
    }
}
