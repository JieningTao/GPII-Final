using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CommandProcessor))]
[RequireComponent(typeof(Rigidbody2D))]


public class Player : Damageable, IPlayer
{
    [SerializeField]
    private float Speed = 16;
    [SerializeField]
    private float MaxSpeed = 8;
    [SerializeField]
    private float JumpForce =5;
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

    
    private Collider2D[] GroundHitResults = new Collider2D[16];
    private Collider2D[] LeftWallHitResults = new Collider2D[16];
    private Collider2D[] RightWallHitResults = new Collider2D[16];

    private InputHandler MyInput;
    private CommandProcessor MyCP;
    private Rigidbody2D rigidbody;


    public bool CurrentPlaythrough;
    private int ExtraJumps = 0;
    bool facingRight;

    Rigidbody2D IPlayer.rigidbody { get => rigidbody; }
    float IPlayer.Speed { get { return Speed; }}
    float IPlayer.MaxSpeed { get { return MaxSpeed; } }
    float IPlayer.Jumpforce { get { return JumpForce; } }

    private void Awake()
    {
        facingRight = true;
        if (GetComponent<InputHandler>() != false)
        {
            MyInput = GetComponent<InputHandler>();
            CurrentPlaythrough = true;
        }
        else
            CurrentPlaythrough = false;
        rigidbody = GetComponent<Rigidbody2D>();
        MyCP = GetComponent<CommandProcessor>();
    }

    private void FixedUpdate()
    {
        if (CurrentPlaythrough)
        {
            //player controlled
            List<Command> CommandsThisUpdate = new List<Command>();
            float Direction = MyInput.ReadMovement();
            if (Direction != 0)
            {
                var MoveCommand = new HorizontalMoveCommand(this, Direction);
                CommandsThisUpdate.Add(MoveCommand);
            }
            if (MyInput.ReadJump())
            {
                if (OnGround())
                {
                    var JumpCommand = new JumpCommand(this, Vector2.up);
                    CommandsThisUpdate.Add(JumpCommand);

                }
                else if (!OnGround() && TouchingWall() == 'N' && ExtraJumps > 0)
                {
                    ExtraJumps--;
                    var JumpCommand = new JumpCommand(this, Vector2.up);
                    CommandsThisUpdate.Add(JumpCommand);
                }
                else if (TouchingWall() != 'N' && !OnGround())
                {
                    if (TouchingWall() == 'L')
                    {
                        var JumpCommand = new JumpCommand(this, (Vector2.up + Vector2.right).normalized);
                        CommandsThisUpdate.Add(JumpCommand);
                    }
                    else
                    {
                        var JumpCommand = new JumpCommand(this, (Vector2.up + Vector2.left).normalized);
                        CommandsThisUpdate.Add(JumpCommand);
                    }
                }
            }

            MyCP.ManualExecuteCommand(CommandsThisUpdate);
        }
        else
        {

        }
    }


    private char TouchingWall()
    {
        if (FrontWallDetectTrigger.OverlapCollider(WallContactFilter, LeftWallHitResults) > 0)
        {
            if (!facingRight)
                return 'L';
            else
                return 'R';
        }
        if (BackWallDetectTrigger.OverlapCollider(WallContactFilter, RightWallHitResults) > 0)
        {
            if (!facingRight)
                return 'R';
            else
                return 'L';
        }
        return 'N';
    }

    private bool OnGround()
    {
        bool a = GroundDetectTrigger.OverlapCollider(GroundContactFilter, GroundHitResults) > 0;
        if (a)
            ExtraJumps = MaxExtraJumps;
        return a;
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    public override void Hit()
    {
        base.Hit();
    }
}
