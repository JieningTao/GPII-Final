using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootCommand : Command
{
    bool shoot;
    public ShootCommand(IPlayer Player,bool _shoot) : base(Player)
    {
        shoot = _shoot;
    }

    public override void Execute()
    {

        Player.Shoot(shoot);

    }
}
