using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : Gun
{
    [SerializeField]
    private string WhiteListTag;

    public override void Shoot(bool a)
    {


    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.isTrigger)
        {
            if (!collision.CompareTag(WhiteListTag))
                collision.GetComponent<Damageable>().Hit();
        }
    }

}
