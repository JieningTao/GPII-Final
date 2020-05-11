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
    [SerializeField]
    public Collider2D BodyCollider;

    [SerializeField]
    private GameObject DyingBurst;
    
    private Collider2D[] GroundHitResults = new Collider2D[16];
    private Collider2D[] LeftWallHitResults = new Collider2D[16];
    private Collider2D[] RightWallHitResults = new Collider2D[16];

    private InputHandler MyInput;
    private CommandProcessor MyCP;
    private Rigidbody2D rigidbody;
    


    public bool CurrentPlaythrough;
    public PlayerManager Manager;
    private int ExtraJumps = 0;
    bool facingRight;
    private bool AttackingPreviousUpdate;
    [SerializeField]
    private Gun MyGun;

    Rigidbody2D IPlayer.rigidbody { get => rigidbody; }
    float IPlayer.Speed { get { return Speed; }}
    float IPlayer.MaxSpeed { get { return MaxSpeed; } }
    float IPlayer.Jumpforce { get { return JumpForce; } }

    private void Awake()
    {
        AttackingPreviousUpdate = false;
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
            if (MyInput.ReadAttack() != AttackingPreviousUpdate)
            {
                var ShootCommand = new ShootCommand(this, MyInput.ReadAttack());
                CommandsThisUpdate.Add(ShootCommand);
            }

            AttackingPreviousUpdate = MyInput.ReadAttack();
            MyCP.ManualExecuteCommand(CommandsThisUpdate);
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
        Debug.Log("Player Shot");
        if (CurrentPlaythrough)
        {
            Manager.Respawn();
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
            Physics2D.IgnoreCollision(BodyCollider,collision.collider);
        
    }

    public virtual void Shoot(bool key)
    {
        MyGun.Shoot(key);
    }

    public void EquipGun(Gun Mygun)
    {
        MyGun = Mygun;
    }

    public void ReInitialize()
    {
        MyCP.Commands.Clear();
        Destroy(MyGun.gameObject);
        rigidbody.velocity = new Vector3(0,0,0);
    }

    public void Explode()
    {
        GameObject Burst = Instantiate(DyingBurst,transform.position,transform.rotation,null);
    }
}
