using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerMoveScript : MonoBehaviour
{
#region Plugin Variables

    [SerializeField]
    [Tooltip("The speed that players start to move")]
    private float AccelrationForce =5;
    [SerializeField]
    private float JumpForce = 15;
    [SerializeField]
    private float WallJumpForce = 15;
    [SerializeField]
    private float MaxSpeed = 20;
    [SerializeField]
    [Tooltip("Amount of in-air jumps avaliable to player")]
    private int MaxExtraJumps = 1;
    [SerializeField]
    private ContactFilter2D GroundContactFilter;
    [SerializeField]
    private Collider2D GroundDetectTrigger;
    [SerializeField]
    private ContactFilter2D WallContactFilter;
    [SerializeField]
    private Collider2D FrontWallDetectTrigger;
    [SerializeField]
    private Collider2D BackWallDetectTrigger;
    [SerializeField]
    private Collider2D playergroundcollider;
    [SerializeField]
    [Tooltip("How much the player slows down in the air")]
    private float airspeedreduction;
    [SerializeField]
    private ParticleSystem wallsmoke;
    #endregion

    
    private Rigidbody2D Thisrigidbody;
    private int Score;
    private bool IsDead;
    [SerializeField]
    private float HorizontalInput;
    private int ExtraJumps;
    private Collider2D[] GroundHitResults = new Collider2D[16];
    private Collider2D[] LeftWallHitResults = new Collider2D[16];
    private Collider2D[] RightWallHitResults = new Collider2D[16];

    bool facingRight = true;

    // Use this for initialization
    void Start ()
    {
        Thisrigidbody = GetComponent<Rigidbody2D>();
        Score = 0;
    }
	
    void Update ()
    {
        if (!IsDead)
        {
            RefillJumps();
            HandleHorizontalInput();
            HandleJumpInput();
        }
    }
    
    private void FixedUpdate()
    {
        if (!IsDead)
        {
            HorizontalMovement();
        }
        else if(Input.GetButtonDown("Activate"))
        {

        }
    }

    private bool OnGround()
    {
       return GroundDetectTrigger.OverlapCollider(GroundContactFilter, GroundHitResults)>0;
    }
    
    private char TouchingWall()
    {
        if (FrontWallDetectTrigger.OverlapCollider(WallContactFilter, LeftWallHitResults) > 0 )
        {
            if(!facingRight)
            return 'L';
            else
            return 'R';
        }
        if (BackWallDetectTrigger.OverlapCollider(WallContactFilter, RightWallHitResults) > 0 )
        {
            if (!facingRight)
                return 'R';
            else
                return 'L';
        }
        return 'N';
    }

    private void RefillJumps()
    {
        if (OnGround())
        {
            ExtraJumps = MaxExtraJumps;
        }
        if (TouchingWall()!='N')
        {
            ExtraJumps = MaxExtraJumps;
        }
    }

    private void HandleHorizontalInput()
    {
            HorizontalInput = Input.GetAxisRaw("Horizontal");
    }

 

    private void HandleJumpInput()
    {
        if (Input.GetButtonDown("Jump") && OnGround())
        {
            Thisrigidbody.AddForce(Vector2.up * JumpForce, ForceMode2D.Impulse);

        }
        else if (Input.GetButtonDown("Jump") && !OnGround() && TouchingWall()=='N' && ExtraJumps > 0)
        {
            ExtraJumps--;
            Thisrigidbody.AddForce(Vector2.up * JumpForce, ForceMode2D.Impulse);
        }
        else if (Input.GetButtonDown("Jump") && TouchingWall() != 'N' && !OnGround())
        {
            if (TouchingWall() == 'L')
                Thisrigidbody.AddForce(Vector2.up * JumpForce + Vector2.right * WallJumpForce, ForceMode2D.Impulse);
            else
                Thisrigidbody.AddForce(Vector2.up * JumpForce + Vector2.left * WallJumpForce, ForceMode2D.Impulse);
        }
    }

    private void HorizontalMovement()
    {
        float accelerationToUse = OnGround() ? AccelrationForce : AccelrationForce * airspeedreduction;
        Thisrigidbody.AddForce(Vector2.right * HorizontalInput * accelerationToUse);
        Vector2 ClampedVelocity = Thisrigidbody.velocity;
        ClampedVelocity.x = Mathf.Clamp(Thisrigidbody.velocity.x, -MaxSpeed, MaxSpeed);
        Thisrigidbody.velocity = ClampedVelocity;
    }


    public void Killed()
    {
        IsDead = true;

        Thisrigidbody.constraints = RigidbodyConstraints2D.None;

    }
    
    void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
