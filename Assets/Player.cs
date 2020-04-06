using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    [SerializeField]
    private float Speed;


    private float HorizontalInput;
    private Rigidbody2D MyRB;
    private Vector2 GoingDirection;


    // Start is called before the first frame update
    void Start()
    {
        MyRB = GetComponent<Rigidbody2D>();
        GoingDirection = Vector2.zero;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        HandleMovement();   
    }


    private void Move()
    {
        //MyRB.AddForce(GoingDirection*speed);
    }

    private void InputHandler()
    {
        GoingDirection = Vector2.zero;
        if (Input.GetKey(KeyCode.D))
        {
            
        }
    }

    private void HandleMovement()
    {

        GoingDirection = new Vector2(Input.GetAxisRaw("Horizontal"), 0).normalized;
        this.GetComponent<Rigidbody>().MovePosition(this.GetComponent<Rigidbody>().position + transform.TransformDirection(GoingDirection) * Speed * Time.deltaTime);

    }
}
