using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpCommand : Command
{
    private Vector2 Direction;

    public JumpCommand(IPlayer Player, Vector2 _direction) : base(Player)
    {
        Direction = _direction;
    }

    public override void Execute()
    {

        Player.rigidbody.AddForce( Direction * Player.Jumpforce,ForceMode2D.Impulse);
        
    }

}
