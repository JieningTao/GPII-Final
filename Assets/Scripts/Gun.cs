using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField]
    protected float TBS = 0.1f;

    [SerializeField]
    protected Transform BulletSpawn;

    [SerializeField]
    protected GameObject Bullet;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual void Shoot(bool a)
    {
        if (a)
        {
            GameObject NewLaser = Instantiate(Bullet, BulletSpawn.position, BulletSpawn.rotation);
        }
        
    }

    

}
