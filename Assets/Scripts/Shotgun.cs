using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : Gun
{ 
    [SerializeField]
    private int PelletCount;

    public override void Shoot(bool a)
    {
        for (int i = 0; i < PelletCount; i++)
        {
            GameObject NewLaser = Instantiate(Bullet, BulletSpawn.position, BulletSpawn.rotation);
            NewLaser.transform.Rotate(new Vector3(0, 0, Random.Range(0f, 10f)));
        }

    }

}
