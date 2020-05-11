using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunComponents : MonoBehaviour
{
    public interface IGunComponent
    {
        void Shoot(bool key);
    }

    public class GunComponent : IGunComponent 
    {
        [SerializeField]
        public GameObject Bullet;
        [SerializeField]
        public Transform BulletSpawn;

        public virtual void Shoot(bool key)
        {

        }

    }

    public class Pistol:GunComponent
    {
        public override void Shoot(bool key)
        {
            if (key)
            {
                GameObject NewLaser = Instantiate(Bullet, BulletSpawn.position, BulletSpawn.rotation);
            }
        }
    }


}
