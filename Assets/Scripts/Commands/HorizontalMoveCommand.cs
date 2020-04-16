using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalMoveCommand : Command
{
    private float Direction;

    public HorizontalMoveCommand(IPlayer Player, float _direction) : base(Player)
    {
        Direction = _direction;
    }

    public override void Execute()
    {

        Player.rigidbody.AddForce(Vector2.right * Direction*Player.Speed);
        Vector2 ClampedVelocity = Player.rigidbody.velocity;
        ClampedVelocity.x = Mathf.Clamp(Player.rigidbody.velocity.x, -Player.MaxSpeed, Player.MaxSpeed);
        Player.rigidbody.velocity = ClampedVelocity;

    }
}
