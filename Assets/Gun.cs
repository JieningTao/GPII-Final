using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{

    //this class will attach to the player using decorater pattern, with different weapons triggered by the same fire command.

    [SerializeField]
    Transform BulletSpawn;

    [SerializeField]
    GameObject Bullet;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        GameObject NewLaser = Instantiate(Bullet, BulletSpawn.position, BulletSpawn.rotation);
    }

}
